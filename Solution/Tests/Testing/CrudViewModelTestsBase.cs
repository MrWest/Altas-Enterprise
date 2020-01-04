using CompanyName.Atlas.Contracts.Domain.Common;
using Moq;
using Moq.Protected;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Services;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    /// <summary>
    /// Base class of test classes testing crud view model derivers.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the crud view model class to test.</typeparam>
    /// <typeparam name="TItem">The type of the item in the presenters managed in the tested crud view model class.</typeparam>
    /// <typeparam name="TPresenter">The type of the presenters managed in the tested crud view model class.</typeparam>
    /// <typeparam name="TServices">The type of the item manager application services used by the tested crud view model class.</typeparam>
    public abstract class CrudViewModelTestsBase<TViewModel, TItem, TPresenter, TServices> : BindableTestsBase<TViewModel, TItem>
        where TItem : class, IEntity
        where TPresenter : class, IPresenter<TItem>
        where TServices : class, IItemManagerApplicationServices<TItem>
        where TViewModel : CrudViewModelBase<TItem, TPresenter, TServices>
    {
        /// <summary>
        /// Initializes the test scenario.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            CreateApplicationServicesMock();
            CreateApplicationServices();

            ServiceLocatorMock.Setup(x => x.GetInstance<TServices>()).Returns(ApplicationServices);
            ServiceLocatorMock.Setup(x => x.GetInstance<IStatusBarServices>()).Returns(StatusBarServices);
        }


        /// <summary>
        /// Gets the mock of the Application Services instance used by the tested crud view model.
        /// </summary>
        public Mock<TServices> ApplicationServicesMock { get; protected set; }

        /// <summary>
        /// Gets the mocked instance of the Application Services instance used by the tested crud view model.
        /// </summary>
        public TServices ApplicationServices { get; protected set; }


        /// <summary>
        /// Constructs and assigns to the Application Services mock property the mock of the application services used by the tested crud view model.
        /// </summary>
        protected virtual void CreateApplicationServicesMock()
        {
            ApplicationServicesMock = new Mock<TServices>();
        }

        /// <summary>
        /// Constructs and assigns to the Application Services property the application services mocked instance used by the tested crud view model.
        /// </summary>
        protected virtual void CreateApplicationServices()
        {
            ApplicationServices = ApplicationServicesMock.Object;
        }

        /// <summary>
        /// Setups the CreatePresenterFor method to return the correct presenter.
        /// </summary>
        /// <param name="item">The item to create a presenter for.</param>
        /// <param name="presenter">The presenter to return for <paramref name="item"/>.</param>
        protected void SetupCreatePresenterFor(TItem item, TPresenter presenter)
        {
            TestMock.Protected().Setup<TPresenter>("CreatePresenterFor", item).Returns(presenter);
        }

        /// <summary>
        /// Gets the presenter for the given item.
        /// </summary>
        /// <param name="item">The item to get the presenter for.</param>
        /// <returns>An presenter of type <typeparamref name="TPresenter"/> for <paramref name="item"/>.</returns>
        protected TPresenter GetPresenterFor(TItem item)
        {
            return (TPresenter)TestObjectInternals.Invoke("CreatePresenterFor", item);
        }

        /// <summary>
        /// Sets up the tested view model to return the proper presenter.
        /// </summary>
        protected virtual void SetupPresenter()
        {
            TestMock.Protected().Setup<TPresenter>("CreatePresenterFor", ItExpr.IsAny<TItem>()).Returns<TItem>(item =>
            {
                var mock = new Mock<TPresenter>();
                mock.SetupProperty(x => x.Object);
                mock.Object.Object = item;

                return mock.Object;
            });
        }
    }
}
