using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Services
{
    [TestClass, ExcludeFromCodeCoverage]
    public class OsdeDomainServicesTests : DomainServicesTestsBase<IOsde, OsdeDomainServices>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IOsde>()).Returns(() =>
            {
                var mock = new Mock<IOsde>();
                mock.SetupAllProperties();

                return mock.Object;
            });
        }


        [TestMethod]
        public void Create_CreatesNewInstanceOfOSDE()
        {
            // Act
            IOsde oace = TestObject.Create();

            // Assert
            Assert.IsNotNull(oace);
        }

        [TestMethod]
        public void Create_CreatesOSDEWithCorrectName()
        {
            // Act
            IOsde oace = TestObject.Create();

            // Assert
            Assert.AreEqual(Resources.NewOsde_Name, oace.Name);
        }

        [TestMethod]
        public void Create_CreatesOSDEWithCorrectDescription()
        {
            // Act
            IOsde oace = TestObject.Create();

            // Assert
            Assert.AreEqual(Resources.NewOsde_Description, oace.Description);
        }
    }
}