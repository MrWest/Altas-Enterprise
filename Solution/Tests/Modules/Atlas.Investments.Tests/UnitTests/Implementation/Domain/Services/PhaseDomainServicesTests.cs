using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Services
{
    [TestClass, ExcludeFromCodeCoverage]
    public class PhaseDomainServicesTests : DomainServicesTestsBase<IPhase, PhaseDomainServices>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IPhase>()).Returns(() =>
            {
                var mock = new Mock<IPhase>();
                mock.SetupAllProperties();

                return mock.Object;
            });
        }


        [TestMethod]
        public void Create_CreatesNewInstanceOfPhase()
        {
            // Act
            IPhase oace = TestObject.Create();

            // Assert
            Assert.IsNotNull(oace);
        }

        [TestMethod]
        public void Create_CreatesPhaseWithCorrectName()
        {
            // Act
            IPhase oace = TestObject.Create();

            // Assert
            Assert.AreEqual(Resources.NewPhase_Name, oace.Name);
        }

        [TestMethod]
        public void Create_CreatesPhaseWithCorrectDescription()
        {
            // Act
            IPhase oace = TestObject.Create();

            // Assert
            Assert.AreEqual(Resources.NewPhase_Description, oace.Description);
        }
    }
}