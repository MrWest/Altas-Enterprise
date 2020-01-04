using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
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

namespace CompanyName.Atlas.Investments.Tests.Implementation.Presentation.ViewModels
{
    //[TestClass, ExcludeFromCodeCoverage]
    //public class InvestmentElementViewModelTests :
    //    CrudViewModelTestsBase<InvestmentElementViewModel, IInvestmentElement, IInvestmentElementPresenter, IInvestmentElementManagerApplicationServices>
    //{
    //    [TestInitialize]
    //    public override void Initialize()
    //    {
    //        base.Initialize();

    //        ApplicationServicesMock.SetupProperty(x => x.Parent);
    //        ServiceLocatorMock.Setup(x => x.GetInstance<IInvestmentElementPresenter>()).Returns(() =>
    //        {
    //            var mock = new Mock<IInvestmentElementPresenter>();
    //            mock.SetupAllProperties();

    //            return mock.Object;
    //        });

    //        ServiceLocatorMock.Setup(x => x.GetInstance<IBudgetPresenter>()).Returns(() =>
    //        {
    //            var mock = new Mock<IBudgetPresenter>();
    //            mock.SetupAllProperties();

    //            return mock.Object;
    //        });

    //        ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentComponentPresenter>()).Returns(() =>
    //        {
    //            var mock = new Mock<IEquipmentComponentPresenter>();
    //            mock.SetupAllProperties();

    //            return mock.Object;
    //        });
    //        ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionComponentPresenter>()).Returns(() =>
    //        {
    //            var mock = new Mock<IConstructionComponentPresenter>();
    //            mock.SetupAllProperties();

    //            return mock.Object;
    //        });
    //        ServiceLocatorMock.Setup(x => x.GetInstance<IOtherExpensesComponentPresenter>()).Returns(() =>
    //        {
    //            var mock = new Mock<IOtherExpensesComponentPresenter>();
    //            mock.SetupAllProperties();

    //            return mock.Object;
    //        });
    //        ServiceLocatorMock.Setup(x => x.GetInstance<IWorkCapitalComponentPresenter>()).Returns(() =>
    //        {
    //            var mock = new Mock<IWorkCapitalComponentPresenter>();
    //            mock.SetupAllProperties();

    //            return mock.Object;
    //        });
    //    }


    //    [TestMethod]
    //    public void CreateServices_IfParentIsNull_CreatesServiceWithParentNull()
    //    {
    //        // Act
    //        var services = (IInvestmentElementManagerApplicationServices)TestObjectInternals.Invoke("CreateServices");

    //        // Assert
    //        Assert.IsNull(services.Parent);
    //    }

    //    [TestMethod]
    //    public void CreateServices_IfParentIsNotNull_CreatesServiceWithParentCorrectlySet()
    //    {
    //        // Arrange
    //        var parent = Mock.Of<IInvestmentElement>();
    //        var parentPresenter = Mock.Of<IInvestmentElementPresenter>(x => x.Object == parent);
    //        TestObject.Parent = parentPresenter;

    //        // Act
    //        var services = (IInvestmentElementManagerApplicationServices)TestObjectInternals.Invoke("CreateServices");

    //        // Assert
    //        Assert.AreSame(parent, services.Parent);
    //    }


    //    [TestMethod]
    //    public void CreatePresenterFor_IfParentIsNull_CreatesAPresenterWithNullParent()
    //    {
    //        // Act
    //        var presenter = (IInvestmentElementPresenter)TestObjectInternals.Invoke("CreatePresenterFor", CreateInvestmentElement().Object);

    //        // Assert
    //        Assert.IsNull(presenter.Parent);
    //    }

    //    [TestMethod]
    //    public void CreatePresenterFor_IfParentIsNotNull_CreatesAPresenterWithTheParentCorrectlyInitialized()
    //    {
    //        // Arrange
    //        var parent = Mock.Of<IInvestmentElement>();
    //        var parentPresenter = Mock.Of<IInvestmentElementPresenter>(x => x.Object == parent);

    //        TestObject.Parent = parentPresenter;

    //        // Act
    //        var presenter = (IInvestmentElementPresenter)TestObjectInternals.Invoke("CreatePresenterFor", CreateInvestmentElement().Object);

    //        // Assert
    //        Assert.AreSame(parentPresenter, presenter.Parent);
    //    }

    //    [TestMethod]
    //    public void CreatePresenterFor_ReturnsPresenterWithAllItsDataSet()
    //    {
    //        // Act
    //        IInvestmentElement investmentElement = CreateInvestmentElement().Object;
    //        IBudget budget = investmentElement.Budget;
    //        IEquipmentComponent equipment = budget.EquipmentComponent;
    //        IConstructionComponent construction = budget.ConstructionComponent;
    //        IOtherExpensesComponent otherExpenses = budget.OtherExpensesComponent;
    //        IWorkCapitalComponent workCapital = budget.WorkCapitalComponent;

    //        var presenter = (IInvestmentElementPresenter)TestObjectInternals.Invoke("CreatePresenterFor", investmentElement);

    //        // Assert
    //        Assert.IsNotNull(presenter.Budget);

    //        IBudgetPresenter budgetPresenter = presenter.Budget;

    //        Assert.AreSame(budget, budgetPresenter.Object);
    //        Assert.IsNotNull(budgetPresenter.EquipmentComponent);
    //        Assert.IsNotNull(budgetPresenter.ConstructionComponent);
    //        Assert.IsNotNull(budgetPresenter.OtherExpensesComponent);
    //        Assert.IsNotNull(budgetPresenter.WorkCapitalComponent);

    //        IEquipmentComponentPresenter equipmentPresenter = budgetPresenter.EquipmentComponent;
    //        Assert.AreSame(equipment, equipmentPresenter.Object);

    //        IConstructionComponentPresenter constructionPresenter = budgetPresenter.ConstructionComponent;
    //        Assert.AreSame(construction, constructionPresenter.Object);

    //        IOtherExpensesComponentPresenter otherExpensesPresenter = budgetPresenter.OtherExpensesComponent;
    //        Assert.AreSame(otherExpenses, otherExpensesPresenter.Object);

    //        IWorkCapitalComponentPresenter workCapitalPresenter = budgetPresenter.WorkCapitalComponent;
    //        Assert.AreSame(workCapital, workCapitalPresenter.Object);
    //    }


    //    [TestMethod]
    //    public void OnAddedItem_InitializesTheComponents()
    //    {
    //        // Arrange
    //        var equipment = Mock.Of<IEquipmentComponent>();
    //        var construction = Mock.Of<IConstructionComponent>();
    //        var otherExpenses = Mock.Of<IOtherExpensesComponent>();
    //        var workCapital = Mock.Of<IWorkCapitalComponent>();

    //        var equipmentPlannedResources = new Mock<IEquipmentPlannedResourceViewModel>();
    //        equipmentPlannedResources.SetupProperty(x => x.Component);
    //        var equipmentExecutedResources = new Mock<IEquipmentExecutedResourceViewModel>();
    //        equipmentExecutedResources.SetupProperty(x => x.Component);
    //        var equipmentPlannedActivities = new Mock<IEquipmentPlannedActivityViewModel>();
    //        equipmentPlannedActivities.SetupProperty(x => x.Component);
    //        var equipmentExecutedActivities = new Mock<IEquipmentExecutedActivityViewModel>();
    //        equipmentExecutedActivities.SetupProperty(x => x.Component);

    //        var constructionPlannedResources = new Mock<IConstructionPlannedResourceViewModel>();
    //        constructionPlannedResources.SetupProperty(x => x.Component);
    //        var constructionExecutedResources = new Mock<IConstructionExecutedResourceViewModel>();
    //        constructionExecutedResources.SetupProperty(x => x.Component);
    //        var constructionPlannedActivities = new Mock<IConstructionPlannedActivityViewModel>();
    //        constructionPlannedActivities.SetupProperty(x => x.Component);
    //        var constructionExecutedActivities = new Mock<IConstructionExecutedActivityViewModel>();
    //        constructionExecutedActivities.SetupProperty(x => x.Component);

    //        var otherExpensesPlannedResources = new Mock<IOtherExpensesPlannedResourceViewModel>();
    //        otherExpensesPlannedResources.SetupProperty(x => x.Component);
    //        var otherExpensesExecutedResources = new Mock<IOtherExpensesExecutedResourceViewModel>();
    //        otherExpensesExecutedResources.SetupProperty(x => x.Component);
    //        var otherExpensesPlannedActivities = new Mock<IOtherExpensesPlannedActivityViewModel>();
    //        otherExpensesPlannedActivities.SetupProperty(x => x.Component);
    //        var otherExpensesExecutedActivities = new Mock<IOtherExpensesExecutedActivityViewModel>();
    //        otherExpensesExecutedActivities.SetupProperty(x => x.Component);

    //        var workCapitalPlannedResources = new Mock<IWorkCapitalPlannedResourceViewModel>();
    //        workCapitalPlannedResources.SetupProperty(x => x.Component);
    //        var workCapitalExecutedResources = new Mock<IWorkCapitalExecutedResourceViewModel>();
    //        workCapitalExecutedResources.SetupProperty(x => x.Component);
    //        var workCapitalPlannedActivities = new Mock<IWorkCapitalPlannedActivityViewModel>();
    //        workCapitalPlannedActivities.SetupProperty(x => x.Component);
    //        var workCapitalExecutedActivities = new Mock<IWorkCapitalExecutedActivityViewModel>();
    //        workCapitalExecutedActivities.SetupProperty(x => x.Component);

    //        var equipmentPresenter = Mock.Of<IEquipmentComponentPresenter>(x =>
    //            x.PlannedResources == equipmentPlannedResources.Object &&
    //            x.ExecutedResources == equipmentExecutedResources.Object &&
    //            x.PlannedActivities == equipmentPlannedActivities.Object &&
    //            x.ExecutedActivities == equipmentExecutedActivities.Object);
    //        var constructionPresenter = Mock.Of<IConstructionComponentPresenter>(x =>
    //            x.PlannedResources == constructionPlannedResources.Object &&
    //            x.ExecutedResources == constructionExecutedResources.Object &&
    //            x.PlannedActivities == constructionPlannedActivities.Object &&
    //            x.ExecutedActivities == constructionExecutedActivities.Object);
    //        var otherExpensesPresenter = Mock.Of<IOtherExpensesComponentPresenter>(x =>
    //            x.PlannedResources == otherExpensesPlannedResources.Object &&
    //            x.ExecutedResources == otherExpensesExecutedResources.Object &&
    //            x.PlannedActivities == otherExpensesPlannedActivities.Object &&
    //            x.ExecutedActivities == otherExpensesExecutedActivities.Object);
    //        var workCapitalPresenter = Mock.Of<IWorkCapitalComponentPresenter>(x =>
    //            x.PlannedResources == workCapitalPlannedResources.Object &&
    //            x.ExecutedResources == workCapitalExecutedResources.Object &&
    //            x.PlannedActivities == workCapitalPlannedActivities.Object &&
    //            x.ExecutedActivities == workCapitalExecutedActivities.Object);

    //        var investmentElement = new Mock<IInvestmentElementPresenter>();
    //        investmentElement.Setup(x => x.Object.Budget.EquipmentComponent).Returns(equipment);
    //        investmentElement.Setup(x => x.Budget.EquipmentComponent).Returns(equipmentPresenter);
    //        investmentElement.Setup(x => x.Object.Budget.ConstructionComponent).Returns(construction);
    //        investmentElement.Setup(x => x.Budget.ConstructionComponent).Returns(constructionPresenter);
    //        investmentElement.Setup(x => x.Object.Budget.OtherExpensesComponent).Returns(otherExpenses);
    //        investmentElement.Setup(x => x.Budget.OtherExpensesComponent).Returns(otherExpensesPresenter);
    //        investmentElement.Setup(x => x.Object.Budget.WorkCapitalComponent).Returns(workCapital);
    //        investmentElement.Setup(x => x.Budget.WorkCapitalComponent).Returns(workCapitalPresenter);
            

    //        // Act
    //        TestObjectInternals.Invoke("OnAddedItem", TestObject, new ItemEventArgs<IInvestmentElementPresenter>(investmentElement.Object));

    //        // Assert
    //        equipmentPlannedResources.VerifySet(x => x.Component = equipment);
    //        equipmentPlannedActivities.VerifySet(x => x.Component = equipment);
    //        equipmentExecutedResources.VerifySet(x => x.Component = equipment);
    //        equipmentExecutedActivities.VerifySet(x => x.Component = equipment);

    //        constructionPlannedResources.VerifySet(x => x.Component = construction);
    //        constructionPlannedActivities.VerifySet(x => x.Component = construction);
    //        constructionExecutedResources.VerifySet(x => x.Component = construction);
    //        constructionExecutedActivities.VerifySet(x => x.Component = construction);

    //        otherExpensesPlannedResources.VerifySet(x => x.Component = otherExpenses);
    //        otherExpensesPlannedActivities.VerifySet(x => x.Component = otherExpenses);
    //        otherExpensesExecutedResources.VerifySet(x => x.Component = otherExpenses);
    //        otherExpensesExecutedActivities.VerifySet(x => x.Component = otherExpenses);

    //        workCapitalPlannedResources.VerifySet(x => x.Component = workCapital);
    //        workCapitalPlannedActivities.VerifySet(x => x.Component = workCapital);
    //        workCapitalExecutedResources.VerifySet(x => x.Component = workCapital);
    //        workCapitalExecutedActivities.VerifySet(x => x.Component = workCapital);
    //    }


    //    private static Mock<IInvestmentElement> CreateInvestmentElement()
    //    {
    //        var workCapital = Mock.Of<IWorkCapitalComponent>();
    //        var otherExpenses = Mock.Of<IOtherExpensesComponent>();
    //        var construction = Mock.Of<IConstructionComponent>();
    //        var equipment = Mock.Of<IEquipmentComponent>();
            
    //        var budget = Mock.Of<IBudget>(x =>
    //            x.EquipmentComponent == equipment &&
    //            x.ConstructionComponent == construction &&
    //            x.OtherExpensesComponent == otherExpenses &&
    //            x.WorkCapitalComponent == workCapital);

    //        var investmentElement = new Mock<IInvestmentElement>();
    //        investmentElement.SetupAllProperties();
    //        investmentElement.Object.Budget = budget;

    //        return investmentElement;
    //    }
    //}
}
