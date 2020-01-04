using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Services
{
    [TestClass, ExcludeFromCodeCoverage]
    public class WageScaleDomainServicesTests : DomainServicesTestsBase<IWageScale, WageScaleDomainServices>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IWageScale>()).Returns(() =>
            {
                var mock = new Mock<IWageScale>();
                mock.SetupAllProperties();

                return mock.Object;
            });
        }


        [TestMethod]
        public void Create_CreatesNewInstanceOfWageScale()
        {
            // Act
            IWageScale oace = TestObject.Create();

            // Assert
            Assert.IsNotNull(oace);
        }

        [TestMethod]
        public void Create_CreatesWageScaleWithCorrectName()
        {
            // Act
            IWageScale oace = TestObject.Create();

            // Assert
            Assert.AreEqual(Resources.NewWageScale_Name, oace.Name);
        }
    }
}