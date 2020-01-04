using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Properties;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Domain.Services
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InvestmentDomainServicesTests : DomainServicesTestsBase<IInvestment, IInvestmentDomainServices>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IBudget>()).Returns(() =>
            {
                var mock = new Mock<IBudget>();
                mock.SetupAllProperties();

                return mock.Object;
            });
            ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentComponent>()).Returns(() =>
            {
                var mock = new Mock<IEquipmentComponent>();
                mock.SetupAllProperties();

                return mock.Object;
            });
            ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionComponent>()).Returns(() =>
            {
                var mock = new Mock<IConstructionComponent>();
                mock.SetupAllProperties();

                return mock.Object;
            });
            ServiceLocatorMock.Setup(x => x.GetInstance<IOtherExpensesComponent>()).Returns(() =>
            {
                var mock = new Mock<IOtherExpensesComponent>();
                mock.SetupAllProperties();

                return mock.Object;
            });
            ServiceLocatorMock.Setup(x => x.GetInstance<IWorkCapitalComponent>()).Returns(() =>
            {
                var mock = new Mock<IWorkCapitalComponent>();
                mock.SetupAllProperties();

                return mock.Object;
            });
        }


        [TestMethod]
        public void Create_CreatesANewInvestmentElementWithCorrectName()
        {
            // Act
            IInvestmentElement newElement = TestObject.Create();

            // Assert
            Assert.AreEqual(Resources.NewInvestmentElementName, newElement.Name);
        }

        [TestMethod]
        public void Create_CreatesANewInvestmentElementWithCorrectDescription()
        {
            // Act
            IInvestmentElement newElement = TestObject.Create();

            // Assert
            Assert.AreEqual(Resources.NewInvestmentElementDescription, newElement.Description);
        }

        [TestMethod]
        public void Create_CreatesANewInvestmentElementWithCorrectBudgetAndItsComponents()
        {
            // Act
            IInvestmentElement element = TestObject.Create();

            // Assert
            Assert.IsNotNull(element.Budget);

            IBudget budget = element.Budget;
            Assert.IsNotNull(budget.EquipmentComponent);
            Assert.IsNotNull(budget.ConstructionComponent);
            Assert.IsNotNull(budget.OtherExpensesComponent);
            Assert.IsNotNull(budget.WorkCapitalComponent);

            Assert.IsNotNull(element.Budget.InvestmentElement);
            Assert.IsNotNull(element.Budget.EquipmentComponent.Budget);
            Assert.IsNotNull(element.Budget.ConstructionComponent.Budget);
            Assert.IsNotNull(element.Budget.OtherExpensesComponent.Budget);
            Assert.IsNotNull(element.Budget.WorkCapitalComponent.Budget);
        }
    }
}
