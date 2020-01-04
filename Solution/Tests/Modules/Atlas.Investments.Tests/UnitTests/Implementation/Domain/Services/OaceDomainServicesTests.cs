using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Services
{
    [TestClass, ExcludeFromCodeCoverage]
    public class OaceDomainServicesTests : DomainServicesTestsBase<IOace, OaceDomainServices>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IOace>()).Returns(() =>
            {
                var mock = new Mock<IOace>();
                mock.SetupAllProperties();

                return mock.Object;
            });
        }


        [TestMethod]
        public void Create_CreatesNewInstanceOfOACE()
        {
            // Act
            IOace oace = TestObject.Create();

            // Assert
            Assert.IsNotNull(oace);
        }

        [TestMethod]
        public void Create_CreatesOACEWithCorrectName()
        {
            // Act
            IOace oace = TestObject.Create();

            // Assert
            Assert.AreEqual(Resources.NewOace_Name, oace.Name);
        }

        [TestMethod]
        public void Create_CreatesOACEWithCorrectDescription()
        {
            // Act
            IOace oace = TestObject.Create();

            // Assert
            Assert.AreEqual(Resources.NewOace_Description, oace.Description);
        }
    }
}