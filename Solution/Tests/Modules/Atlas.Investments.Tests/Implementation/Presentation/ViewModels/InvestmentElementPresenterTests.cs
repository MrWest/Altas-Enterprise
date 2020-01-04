using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
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
    //public class InvestmentElementPresenterTests : PresenterTestsBase<IInvestmentElement, IInvestmentElementPresenter, IInvestmentElementManagerApplicationServices>
    //{
    //    [TestInitialize]
    //    public override void Initialize()
    //    {
    //        base.Initialize();

    //        ApplicationServicesMock.SetupProperty(x => x.Parent);

    //        ServiceLocatorMock.Setup(x => x.GetInstance<IInvestmentElementViewModel>()).Returns(() =>
    //        {
    //            var mock = new Mock<IInvestmentElementViewModel>();
    //            mock.SetupAllProperties();
    //            return mock.Object;
    //        });

    //        ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentPlannedResourceViewModel>()).Returns(() =>
    //        {
    //            var mock = new Mock<IEquipmentPlannedResourceViewModel>();
    //            mock.SetupAllProperties();

    //            return mock.Object;
    //        });
    //        ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentExecutedResourceViewModel>()).Returns(() =>
    //        {
    //            var mock = new Mock<IEquipmentExecutedResourceViewModel>();
    //            mock.SetupAllProperties();

    //            return mock.Object;
    //        });
    //        ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentPlannedActivityViewModel>()).Returns(() =>
    //        {
    //            var mock = new Mock<IEquipmentPlannedActivityViewModel>();
    //            mock.SetupAllProperties();

    //            return mock.Object;
    //        });
    //        ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentExecutedActivityViewModel>()).Returns(() =>
    //        {
    //            var mock = new Mock<IEquipmentExecutedActivityViewModel>();
    //            mock.SetupAllProperties();

    //            return mock.Object;
    //        });

    //        ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionPlannedResourceViewModel>()).Returns(() =>
    //        {
    //            var mock = new Mock<IConstructionPlannedResourceViewModel>();
    //            mock.SetupAllProperties();

    //            return mock.Object;
    //        });
    //        ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionPlannedActivityViewModel>()).Returns(() =>
    //        {
    //            var mock = new Mock<IConstructionPlannedActivityViewModel>();
    //            mock.SetupAllProperties();

    //            return mock.Object;
    //        });
    //        ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionExecutedResourceViewModel>()).Returns(() =>
    //        {
    //            var mock = new Mock<IConstructionExecutedResourceViewModel>();
    //            mock.SetupAllProperties();

    //            return mock.Object;
    //        });
    //        ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionExecutedActivityViewModel>()).Returns(() =>
    //        {
    //            var mock = new Mock<IConstructionExecutedActivityViewModel>();
    //            mock.SetupAllProperties();

    //            return mock.Object;
    //        });

    //        ServiceLocatorMock.Setup(x => x.GetInstance<IOtherExpensesPlannedResourceViewModel>()).Returns(() =>
    //        {
    //            var mock = new Mock<IOtherExpensesPlannedResourceViewModel>();
    //            mock.SetupAllProperties();

    //            return mock.Object;
    //        });

    //        ServiceLocatorMock.Setup(x => x.GetInstance<IWorkCapitalPlannedResourceViewModel>()).Returns(() =>
    //        {
    //            var mock = new Mock<IWorkCapitalPlannedResourceViewModel>();
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


    //    [TestMethod, ExpectedException(typeof(InvalidOperationException))]
    //    public void Budget_IfReadBeforeIsSet_Throws()
    //    {
    //        Console.WriteLine(TestObject.Budget);
    //    }

    //    [TestMethod, ExpectedException(typeof(ArgumentNullException))]
    //    public void Budget_GivenNullBudget_Throws()
    //    {
    //        TestObject.Budget = null;
    //    }

    //    [TestMethod]
    //    public void Budget_GivenBudget_GetGivenBudget()
    //    {
    //        // Arrange
    //        var budget = Mock.Of<IBudgetPresenter>();

    //        // Act
    //        TestObject.Budget = budget;

    //        // Assert
    //        Assert.AreSame(budget, TestObject.Budget);
    //    }


    //    [TestMethod]
    //    public void Elements_ReturnsLoadedViewModel()
    //    {
    //        // Act
    //        IInvestmentElementViewModel viewModel = TestObject.Elements;

    //        // Assert
    //        Mock<IInvestmentElementViewModel> viewModelMock = Mock.Get(viewModel);
    //        viewModelMock.Verify(x => x.Load());
    //    }

    //    [TestMethod]
    //    public void Elements_IfParent_ReturnsViewModelWithParentEqualToThePresenterOne()
    //    {
    //        // Act
    //        IInvestmentElementViewModel viewModel = TestObject.Elements;

    //        // Assert
    //        Assert.AreSame(TestObject, viewModel.Parent);
    //    }


    //    [TestMethod]
    //    public void IsExpanded_GivenSameValue_DoesNotNotifyPropertyChanges()
    //    {
    //        TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.IsExpanded, () => true);
    //    }

    //    [TestMethod]
    //    public void IsExpanded_GivenDifferentValue_NotifiesPropertyChanges()
    //    {
    //        TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.IsExpanded, () => true, () => false);
    //    }

    //    [TestMethod]
    //    public void IsExpanded_GivenDifferentValue_GetsSetValue()
    //    {
    //        TestGetsSetValue(x => x.IsExpanded, () => true, false);
    //    }


    //    [TestMethod]
    //    public void Depth_IfTreeRoot_Returns0()
    //    {
    //        // Act
    //        int depth = TestObject.Depth;

    //        // Assert
    //        Assert.AreEqual(0, depth);
    //    }

    //    [TestMethod]
    //    public void Depth_IfInLevel1_Returns1()
    //    {
    //        // Arrange
    //        TestObject.Parent = Mock.Of<IInvestmentElementPresenter>();

    //        // Act
    //        int depth = TestObject.Depth;

    //        // Assert
    //        Assert.AreEqual(1, depth);
    //    }

    //    [TestMethod]
    //    public void Depth_IfInLevel4_Returns4()
    //    {
    //        // Arrange
    //        TestObject.Parent = Mock.Of<IInvestmentElementPresenter>(x =>
    //            x.Depth == 3 && x.Parent == Mock.Of<IInvestmentElementPresenter>(y =>
    //                y.Depth == 2 && y.Parent == Mock.Of<IInvestmentElementPresenter>(z =>
    //                    z.Depth == 1 && z.Parent == Mock.Of<IInvestmentElementPresenter>(zz => zz.Depth == 0))));

    //        // Act
    //        int depth = TestObject.Depth;

    //        // Assert
    //        Assert.AreEqual(4, depth);
    //    }


    //    [TestMethod]
    //    public void ShortName_IfNameIsShorterThanLimit_ReturnsName()
    //    {
    //        // Arrange
    //        TestObject.Name = "123456";

    //        // Act
    //        string shortName = TestObject.ShortName;

    //        // Assert
    //        Assert.AreEqual(TestObject.Name, shortName);
    //    }

    //    [TestMethod]
    //    public void ShortName_IfNameIsOnLimit_ReturnsShortenedName()
    //    {
    //        // Arrange
    //        TestObject.Name = "1234567";

    //        // Act
    //        string shortName = TestObject.ShortName;

    //        // Assert
    //        Assert.AreEqual(TestObject.Name.Substring(0, 6) + "...", shortName);
    //    }

    //    [TestMethod]
    //    public void ShortName_IfNameIsBeyondLimit_ReturnsShortenedName()
    //    {
    //        // Arrange
    //        TestObject.Name = "123456745896";

    //        // Act
    //        string shortName = TestObject.ShortName;

    //        // Assert
    //        Assert.AreEqual(TestObject.Name.Substring(0, 6) + "...", shortName);
    //    }


    //    [TestMethod]
    //    public void Name_IsITreeNodeInterfaced_ReturnsOwnName()
    //    {
    //        // Arrange
    //        TestObject.Name = "A";

    //        // Act
    //        var treeElement = TestObject as ITreeNode;
    //        string name = treeElement.Name;

    //        // Assert
    //        Assert.AreEqual("A", name);
    //    }


    //    [TestMethod]
    //    public void Parent_IsITreeNodeInterfaced_ReturnsOwnParent()
    //    {
    //        // Arrange
    //        var expectedParent = Mock.Of<IInvestmentElementPresenter>();
    //        TestObject.Parent = expectedParent;

    //        // Act
    //        var treeElement = TestObject as ITreeNode;
    //        ITreeNode actualParent = treeElement.Parent;

    //        // Assert
    //        Assert.AreEqual(expectedParent, actualParent);
    //    }


    //    [TestMethod]
    //    public void Children_IsITreeNodeInterfaced_ReturnsOwnElements()
    //    {
    //        // Act
    //        var treeElement = TestObject as ITreeNode;
    //        IEnumerable<ITreeNode> actualChildren = treeElement.Children;

    //        // Assert
    //        Assert.AreEqual(TestObject.Elements.Items, actualChildren);
    //    }
    //}
}
