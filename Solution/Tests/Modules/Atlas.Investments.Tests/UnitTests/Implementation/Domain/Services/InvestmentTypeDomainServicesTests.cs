using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Services
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InvestmentTypeDomainServicesTests : DomainServicesTestsBase<IInvestmentType, InvestmentTypeDomainServices>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IInvestmentType>()).Returns(() =>
            {
                var mock = new Mock<IInvestmentType>();
                mock.SetupAllProperties();

                return mock.Object;
            });
        }


        [TestMethod]
        public void Create_CreatesNewInstanceOfInvestmentType()
        {
            // Act
            IInvestmentType investmentType = TestObject.Create();

            // Assert
            Assert.IsNotNull(investmentType);
        }

        [TestMethod]
        public void Create_CreatesInvestmentTypeWithCorrectName()
        {
            // Act
            IInvestmentType investmentType = TestObject.Create();

            // Assert
            Assert.AreEqual(Resources.NewInvestmentType_Name, investmentType.Name);
        }

        [TestMethod]
        public void Create_CreatesInvestmentTypeWithCorrectDescription()
        {
            // Act
            IInvestmentType investmentType = TestObject.Create();

            // Assert
            Assert.AreEqual(Resources.NewInvestmentType_Description, investmentType.Description);
        }
    }
}