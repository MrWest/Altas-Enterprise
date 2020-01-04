using System;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Presentation.ViewModels
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InvestmentElementViewModelTests :
        CrudViewModelTestsBase<InvestmentElementViewModelTests.InvestmentElementViewModelStub, IInvestmentElement, IInvestmentElementPresenter, IItemManagerApplicationServices<IInvestmentElement>>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IInvestmentElementPresenter>()).Returns(() =>
            {
                var mock = new Mock<IInvestmentElementPresenter>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            ServiceLocatorMock.Setup(x => x.GetInstance<IBudgetPresenter>()).Returns(() =>
            {
                var mock = new Mock<IBudgetPresenter>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentComponentPresenter>()).Returns(() =>
            {
                var mock = new Mock<IEquipmentComponentPresenter>();
                mock.SetupAllProperties();

                return mock.Object;
            });
            ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionComponentPresenter>()).Returns(() =>
            {
                var mock = new Mock<IConstructionComponentPresenter>();
                mock.SetupAllProperties();

                return mock.Object;
            });
            ServiceLocatorMock.Setup(x => x.GetInstance<IOtherExpensesComponentPresenter>()).Returns(() =>
            {
                var mock = new Mock<IOtherExpensesComponentPresenter>();
                mock.SetupAllProperties();

                return mock.Object;
            });
            ServiceLocatorMock.Setup(x => x.GetInstance<IWorkCapitalComponentPresenter>()).Returns(() =>
            {
                var mock = new Mock<IWorkCapitalComponentPresenter>();
                mock.SetupAllProperties();

                return mock.Object;
            });
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreatePresenterFor_GivenNullInvestmentElement_Throws()
        {
            TestObjectInternals.Invoke("CreatePresenterFor", (IInvestmentElement)null);
        }

        [TestMethod]
        public void CreatePresenterFor_GivenInvestmentElement_ReturnsCorrectInstanceOfInvestmentElementPresenterWithProperInitialization()
        {
            // Arrange
            var equipmentComponent = new Mock<IEquipmentComponent>();
            var constructionComponent = new Mock<IConstructionComponent>();
            var otherExpensesComponent = new Mock<IOtherExpensesComponent>();
            var workCapitalComponent = new Mock<IWorkCapitalComponent>();
            var budget = new Mock<IBudget>();
            budget.SetupGet(x => x.EquipmentComponent).Returns(equipmentComponent.Object);
            budget.SetupGet(x => x.ConstructionComponent).Returns(constructionComponent.Object);
            budget.SetupGet(x => x.OtherExpensesComponent).Returns(otherExpensesComponent.Object);
            budget.SetupGet(x => x.WorkCapitalComponent).Returns(workCapitalComponent.Object);
            var investmentElement = new Mock<IInvestmentElement>();
            investmentElement.SetupGet(x => x.Budget).Returns(budget.Object);

            // Act
            var investmentElementPresenter = (IInvestmentElementPresenter)TestObjectInternals.Invoke("CreatePresenterFor", investmentElement.Object);

            // Assert
            Assert.IsNotNull(investmentElementPresenter.Budget);

            IBudgetPresenter budgetPresenter = investmentElementPresenter.Budget;
            Assert.AreSame(investmentElementPresenter, budgetPresenter.InvestmentElement);
            Assert.IsNotNull(budgetPresenter.EquipmentComponent);
            Assert.IsNotNull(budgetPresenter.ConstructionComponent);
            Assert.IsNotNull(budgetPresenter.OtherExpensesComponent);
            Assert.IsNotNull(budgetPresenter.WorkCapitalComponent);

            IEquipmentComponentPresenter equipmentComponentPresenter = budgetPresenter.EquipmentComponent;
            Mock<IEquipmentComponentPresenter> equipmentComponentPresenterMock = Mock.Get(equipmentComponentPresenter);
            equipmentComponentPresenterMock.VerifySet(x => x.Object = equipmentComponent.Object);
            equipmentComponentPresenterMock.VerifySet(x => x.Budget = budgetPresenter);

            IConstructionComponentPresenter constructionComponentPresenter = budgetPresenter.ConstructionComponent;
            Mock<IConstructionComponentPresenter> constructionComponentPresenterMock = Mock.Get(constructionComponentPresenter);
            constructionComponentPresenterMock.VerifySet(x => x.Object = constructionComponent.Object);
            constructionComponentPresenterMock.VerifySet(x => x.Budget = budgetPresenter);

            IOtherExpensesComponentPresenter otherExpensesComponentPresenter = budgetPresenter.OtherExpensesComponent;
            Mock<IOtherExpensesComponentPresenter> otherExpensesComponentPresenterMock = Mock.Get(otherExpensesComponentPresenter);
            otherExpensesComponentPresenterMock.VerifySet(x => x.Object = otherExpensesComponent.Object);
            otherExpensesComponentPresenterMock.VerifySet(x => x.Budget = budgetPresenter);

            IWorkCapitalComponentPresenter workCapitalComponentPresenter = budgetPresenter.WorkCapitalComponent;
            Mock<IWorkCapitalComponentPresenter> workCapitalComponentPresenterMock = Mock.Get(workCapitalComponentPresenter);
            workCapitalComponentPresenterMock.VerifySet(x => x.Object = workCapitalComponent.Object);
            workCapitalComponentPresenterMock.VerifySet(x => x.Budget = budgetPresenter);
        }


        [TestMethod]
        public void OnAddedItem_InitializesTheComponents()
        {
            // Arrange
            var equipment = Mock.Of<IEquipmentComponent>();
            var construction = Mock.Of<IConstructionComponent>();
            var otherExpenses = Mock.Of<IOtherExpensesComponent>();
            var workCapital = Mock.Of<IWorkCapitalComponent>();

            var equipmentPlannedResources = new Mock<IEquipmentPlannedResourceViewModel>();
            equipmentPlannedResources.SetupProperty(x => x.Component);
            var equipmentExecutedResources = new Mock<IEquipmentExecutedResourceViewModel>();
            equipmentExecutedResources.SetupProperty(x => x.Component);
            var equipmentPlannedActivities = new Mock<IEquipmentPlannedActivityViewModel>();
            equipmentPlannedActivities.SetupProperty(x => x.Component);
            var equipmentExecutedActivities = new Mock<IEquipmentExecutedActivityViewModel>();
            equipmentExecutedActivities.SetupProperty(x => x.Component);

            var constructionPlannedResources = new Mock<IConstructionPlannedResourceViewModel>();
            constructionPlannedResources.SetupProperty(x => x.Component);
            var constructionExecutedResources = new Mock<IConstructionExecutedResourceViewModel>();
            constructionExecutedResources.SetupProperty(x => x.Component);
            var constructionPlannedActivities = new Mock<IConstructionPlannedActivityViewModel>();
            constructionPlannedActivities.SetupProperty(x => x.Component);
            var constructionExecutedActivities = new Mock<IConstructionExecutedActivityViewModel>();
            constructionExecutedActivities.SetupProperty(x => x.Component);

            var otherExpensesPlannedResources = new Mock<IOtherExpensesPlannedResourceViewModel>();
            otherExpensesPlannedResources.SetupProperty(x => x.Component);
            var otherExpensesExecutedResources = new Mock<IOtherExpensesExecutedResourceViewModel>();
            otherExpensesExecutedResources.SetupProperty(x => x.Component);
            var otherExpensesPlannedActivities = new Mock<IOtherExpensesPlannedActivityViewModel>();
            otherExpensesPlannedActivities.SetupProperty(x => x.Component);
            var otherExpensesExecutedActivities = new Mock<IOtherExpensesExecutedActivityViewModel>();
            otherExpensesExecutedActivities.SetupProperty(x => x.Component);

            var workCapitalPlannedResources = new Mock<IWorkCapitalPlannedResourceViewModel>();
            workCapitalPlannedResources.SetupProperty(x => x.Component);
            var workCapitalExecutedResources = new Mock<IWorkCapitalExecutedResourceViewModel>();
            workCapitalExecutedResources.SetupProperty(x => x.Component);
            var workCapitalPlannedActivities = new Mock<IWorkCapitalPlannedActivityViewModel>();
            workCapitalPlannedActivities.SetupProperty(x => x.Component);
            var workCapitalExecutedActivities = new Mock<IWorkCapitalExecutedActivityViewModel>();
            workCapitalExecutedActivities.SetupProperty(x => x.Component);

            var equipmentPresenter = Mock.Of<IEquipmentComponentPresenter>(x =>
                x.PlannedResources == equipmentPlannedResources.Object &&
                x.ExecutedResources == equipmentExecutedResources.Object &&
                x.PlannedActivities == equipmentPlannedActivities.Object &&
                x.ExecutedActivities == equipmentExecutedActivities.Object);
            var constructionPresenter = Mock.Of<IConstructionComponentPresenter>(x =>
                x.PlannedResources == constructionPlannedResources.Object &&
                x.ExecutedResources == constructionExecutedResources.Object &&
                x.PlannedActivities == constructionPlannedActivities.Object &&
                x.ExecutedActivities == constructionExecutedActivities.Object);
            var otherExpensesPresenter = Mock.Of<IOtherExpensesComponentPresenter>(x =>
                x.PlannedResources == otherExpensesPlannedResources.Object &&
                x.ExecutedResources == otherExpensesExecutedResources.Object &&
                x.PlannedActivities == otherExpensesPlannedActivities.Object &&
                x.ExecutedActivities == otherExpensesExecutedActivities.Object);
            var workCapitalPresenter = Mock.Of<IWorkCapitalComponentPresenter>(x =>
                x.PlannedResources == workCapitalPlannedResources.Object &&
                x.ExecutedResources == workCapitalExecutedResources.Object &&
                x.PlannedActivities == workCapitalPlannedActivities.Object &&
                x.ExecutedActivities == workCapitalExecutedActivities.Object);

            var investmentElement = new Mock<IInvestmentElementPresenter>();
            investmentElement.Setup(x => x.Object.Budget.EquipmentComponent).Returns(equipment);
            investmentElement.Setup(x => x.Budget.EquipmentComponent).Returns(equipmentPresenter);
            investmentElement.Setup(x => x.Object.Budget.ConstructionComponent).Returns(construction);
            investmentElement.Setup(x => x.Budget.ConstructionComponent).Returns(constructionPresenter);
            investmentElement.Setup(x => x.Object.Budget.OtherExpensesComponent).Returns(otherExpenses);
            investmentElement.Setup(x => x.Budget.OtherExpensesComponent).Returns(otherExpensesPresenter);
            investmentElement.Setup(x => x.Object.Budget.WorkCapitalComponent).Returns(workCapital);
            investmentElement.Setup(x => x.Budget.WorkCapitalComponent).Returns(workCapitalPresenter);
            

            // Act
            TestObjectInternals.Invoke("OnAddedItem", TestObject, new ItemEventArgs<IInvestmentElementPresenter>(investmentElement.Object));

            // Assert
            equipmentPlannedResources.VerifySet(x => x.Component = equipment);
            equipmentPlannedActivities.VerifySet(x => x.Component = equipment);
            equipmentExecutedResources.VerifySet(x => x.Component = equipment);
            equipmentExecutedActivities.VerifySet(x => x.Component = equipment);

            constructionPlannedResources.VerifySet(x => x.Component = construction);
            constructionPlannedActivities.VerifySet(x => x.Component = construction);
            constructionExecutedResources.VerifySet(x => x.Component = construction);
            constructionExecutedActivities.VerifySet(x => x.Component = construction);

            otherExpensesPlannedResources.VerifySet(x => x.Component = otherExpenses);
            otherExpensesPlannedActivities.VerifySet(x => x.Component = otherExpenses);
            otherExpensesExecutedResources.VerifySet(x => x.Component = otherExpenses);
            otherExpensesExecutedActivities.VerifySet(x => x.Component = otherExpenses);

            workCapitalPlannedResources.VerifySet(x => x.Component = workCapital);
            workCapitalPlannedActivities.VerifySet(x => x.Component = workCapital);
            workCapitalExecutedResources.VerifySet(x => x.Component = workCapital);
            workCapitalExecutedActivities.VerifySet(x => x.Component = workCapital);
        }


        public class InvestmentElementViewModelStub :
            InvestmentElementViewModelBase<IInvestmentElement, IInvestmentElementPresenter, IItemManagerApplicationServices<IInvestmentElement>>
        {
        }
    }
}
