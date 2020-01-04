using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using CompanyName.Atlas.Contracts.Domain.Common;
using Moq;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    /// <summary>
    /// Base class of the test classes testing presenter view models.
    /// </summary>
    /// <typeparam name="TItem">The type of the item wrapped by the presenter.</typeparam>
    /// <typeparam name="TPresenter">The type of the presenter to test.</typeparam>
    /// <typeparam name="TServices">
    /// The type of the application services used to carry on with the saving of the changes made to the testing presenter view model.
    /// </typeparam>
    public abstract class PresenterTestsBase<TItem, TPresenter, TServices> : BindableTestsBase<TPresenter, TPresenter>
        where TPresenter : class, INotifyPropertyChanged, IInteractionRequester
        where TItem : class, IEntity
        where TServices : class, IItemManagerApplicationServices<TItem>
    {
        /// <summary>
        /// Initializes the test scenario.
        /// </summary>
        public override void Initialize()
        {
            CreateEntityMock();
            CreateEntity();

            base.Initialize();

            CreateApplicationServicesMock();
            CreateApplicationServices();
            ServiceLocatorMock.Setup(x => x.GetInstance<TServices>()).Returns(ApplicationServices);
            ApplicationServicesMock.Setup(x => x.CanUpdate(It.IsAny<TItem>())).Returns(true);
        }


        /// <summary>
        /// Gets the mock of the entity wrapped in the tested presenter view model.
        /// </summary>
        public Mock<TItem> EntityMock { get; private set; }

        /// <summary>
        /// Gets the mocked instance of the entity wrapped in the tested presenter view model.
        /// </summary>
        public TItem Entity { get; private set; }

        /// <summary>
        /// Gets the mock of the application services used by the testing presenter view model.
        /// </summary>
        public Mock<TServices> ApplicationServicesMock { get; private set; }

        /// <summary>
        /// Gets the mocked instance of the application services used by the testing presenter view model.
        /// </summary>
        public TServices ApplicationServices { get; private set; }


        /// <summary>
        /// Constructs and assigns to the EntityMock property a mock of the item wrapped by the tested presenter view model.
        /// </summary>
        protected virtual void CreateEntityMock()
        {
            EntityMock = new Mock<TItem>();
            EntityMock.SetupAllProperties();
        }

        /// <summary>
        /// Constructs and assigns to the Entity property a mocked entity instance wrapped by the tested presenter view model.
        /// </summary>
        protected virtual void CreateEntity()
        {
            Entity = EntityMock.Object;
        }

        /// <summary>
        /// Constructs and assigns to the ApplicationServicesMock property a mock of the item wrapped by the tested presenter view model.
        /// </summary>
        protected virtual void CreateApplicationServicesMock()
        {
            ApplicationServicesMock = new Mock<TServices>();
        }

        /// <summary>
        /// Constructs and assigns to the ApplicationServices property a mocked application services instance wrapped by the tested presenter view model.
        /// </summary>
        protected virtual void CreateApplicationServices()
        {
            ApplicationServices = ApplicationServicesMock.Object;
        }

        /// <summary>
        /// Constructs an instance of the presenter view model mock and assigns it to the TestMock property.
        /// </summary>
        protected override void CreateMock()
        {
            TestMock = new Mock<TPresenter>(Entity) { CallBase = true };
        }

        /// <summary>
        /// Tests that the bindable when given a new value to a property it sets that value and the property return such value when requested.
        /// And optionally also check that the entity gets the value set as well.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property's value.</typeparam>
        /// <param name="propertyExpression">The expression defining the property to test.</param>
        /// <param name="getNewValue">A function to retrieve the value to set to the property.</param>
        /// <param name="isEntityProperty">
        /// True if the property also exists in the entity and the property being tested in a wrapper of it; false if the property is just a presenter view model's property for its own funtioning totally unrelated from the entity.
        /// </param>
        public void TestGetsSetValue<TProperty>(Expression<Func<TPresenter, TProperty>> propertyExpression, Func<TProperty> getNewValue, bool isEntityProperty = true)
        {
            // Arrange
            TProperty propertyValue = getNewValue();

            // Act
            string propertyName = ExtractPropertyName<TProperty>(propertyExpression);
            SetProperty(TestObject, propertyName, propertyValue);

            // Assert
            var value = GetProperty<TProperty>(TestObject, propertyName);
            Assert.AreEqual(propertyValue, value);

            if (isEntityProperty)
                Assert.AreEqual(propertyValue, GetEntityProperty<TProperty>(propertyName));
        }

        public virtual void TestReturnsCorrectInnerCrudViewModelInstance<TViewModel>(Expression<Func<TPresenter, TViewModel>> propertyExpression)
            where TViewModel : class, ICrudViewModel
        {
            // Arrange
            var newViewModel = Mock.Of<TViewModel>();
            ServiceLocatorMock.Setup(x => x.GetInstance<TViewModel>()).Returns(newViewModel);

            // Act
            string propertyName = ExtractPropertyName<TViewModel>(propertyExpression);
            var actualViewModel = GetProperty<TViewModel>(TestObject, propertyName);

            // Assert
            Assert.AreSame(newViewModel, actualViewModel);
        }

        public virtual void TestReturnedCrudViewModelInstanceGetsLoaded<TViewModel>(Expression<Func<TPresenter, TViewModel>> propertyExpression)
            where TViewModel : class, ICrudViewModel
        {
            // Arrange
            var newViewModel = Mock.Of<TViewModel>();
            Mock<TViewModel> newViewModelMock = Mock.Get(newViewModel);
            ServiceLocatorMock.Setup(x => x.GetInstance<TViewModel>()).Returns(newViewModel);

            // Act
            string propertyName = ExtractPropertyName<TViewModel>(propertyExpression);
            GetProperty<TViewModel>(TestObject, propertyName);

            // Assert
            newViewModelMock.Verify(x => x.Load());
        }

        public virtual void TestReturnedCrudViewModelInstanceGetsTheParentReferenceInitialized<TViewModel, TReference>(Expression<Func<TPresenter, TViewModel>> propertyExpression, Expression<Func<TViewModel, TReference>> referencePropertyExpression)
            where TViewModel : class, ICrudViewModel
        {
            // Arrange
            var newViewModel = Mock.Of<TViewModel>();
            ServiceLocatorMock.Setup(x => x.GetInstance<TViewModel>()).Returns(newViewModel);

            // Act
            string propertyName = ExtractPropertyName<TViewModel>(propertyExpression);
            string referencePropertyName = ExtractPropertyName(referencePropertyExpression);
            var actualViewModel = GetProperty<TViewModel>(TestObject, propertyName);
            var referenceObject = GetProperty<TViewModel, TReference>(actualViewModel, referencePropertyName);

            // Assert
            Assert.AreSame(TestObject, referenceObject);
        }

        protected virtual TProperty GetEntityProperty<TProperty>(string propertyName)
        {
            PropertyInfo propertyInfo = GetEntityPropertyInfo(propertyName);
            return (TProperty)propertyInfo.GetValue(Entity);
        }

        private PropertyInfo GetEntityPropertyInfo(string propertyName)
        {
            Type type = Entity.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            return propertyInfo;
        }
    }
}
