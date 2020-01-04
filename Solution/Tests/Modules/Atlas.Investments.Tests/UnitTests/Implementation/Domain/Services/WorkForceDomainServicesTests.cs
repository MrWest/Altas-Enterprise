using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Services
{
    [TestClass, ExcludeFromCodeCoverage]
    public class WorkForceDomainServicesTests : DomainServicesTestsBase<IWorkForce, WorkForceDomainServices>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IWorkForce>()).Returns(() =>
            {
                var mock = new Mock<IWorkForce>();
                mock.SetupAllProperties();

                return mock.Object;
            });
        }


        [TestMethod]
        public void Create_CreatesNewInstanceOfWorkForce()
        {
            // Act
            IWorkForce oace = TestObject.Create();

            // Assert
            Assert.IsNotNull(oace);
        }

        [TestMethod]
        public void Create_CreatesWorkForceWithCorrectName()
        {
            // Act
            IWorkForce oace = TestObject.Create();

            // Assert
            Assert.AreEqual(Resources.NewWorkForce_Name, oace.Name);
        }
    }
}