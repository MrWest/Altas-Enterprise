using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using Moq;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    /// <summary>
    /// Base class of the tests for the domain services classes.
    /// </summary>
    /// <typeparam name="T">The type of the entities the domain services deals with.</typeparam>
    /// <typeparam name="TDomainServices">The type of the actual domain services class to test.</typeparam>
    public abstract class DomainServicesTestsBase<T, TDomainServices> :
        ValidatedTestsBase<TDomainServices, T>
        where T : class, IEntity
        where TDomainServices : class, IDomainServices<T>
    {
        /// <summary>
        /// Initializes the scenario of the current test class.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<T>()).Returns(() =>
            {
                var mock = new Mock<T>();
                mock.SetupAllProperties();

                return mock.Object;
            });
        }
    }
}
