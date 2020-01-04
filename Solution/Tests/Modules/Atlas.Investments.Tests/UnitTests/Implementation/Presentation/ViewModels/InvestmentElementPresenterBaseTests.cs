using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class InvestmentElementPresenterBaseTests :
        PresenterTestsBase<IInvestmentElement, InvestmentElementPresenterBaseTests.InvestmentElementPresenterStub, IItemManagerApplicationServices<IInvestmentElement>>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ServiceLocatorMock.Setup(x => x.GetInstance<IInvestmentComponentViewModel>()).Returns(() =>
            {
                var mock = new Mock<IInvestmentComponentViewModel>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentPlannedResourceViewModel>()).Returns(() =>
            {
                var mock = new Mock<IEquipmentPlannedResourceViewModel>();
                mock.SetupAllProperties();

                return mock.Object;
            });
            ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentExecutedResourceViewModel>()).Returns(() =>
            {
                var mock = new Mock<IEquipmentExecutedResourceViewModel>();
                mock.SetupAllProperties();

                return mock.Object;
            });
            ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentPlannedActivityViewModel>()).Returns(() =>
            {
                var mock = new Mock<IEquipmentPlannedActivityViewModel>();
                mock.SetupAllProperties();

                return mock.Object;
            });
            ServiceLocatorMock.Setup(x => x.GetInstance<IEquipmentExecutedActivityViewModel>()).Returns(() =>
            {
                var mock = new Mock<IEquipmentExecutedActivityViewModel>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionPlannedResourceViewModel>()).Returns(() =>
            {
                var mock = new Mock<IConstructionPlannedResourceViewModel>();
                mock.SetupAllProperties();

                return mock.Object;
            });
            ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionPlannedActivityViewModel>()).Returns(() =>
            {
                var mock = new Mock<IConstructionPlannedActivityViewModel>();
                mock.SetupAllProperties();

                return mock.Object;
            });
            ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionExecutedResourceViewModel>()).Returns(() =>
            {
                var mock = new Mock<IConstructionExecutedResourceViewModel>();
                mock.SetupAllProperties();

                return mock.Object;
            });
            ServiceLocatorMock.Setup(x => x.GetInstance<IConstructionExecutedActivityViewModel>()).Returns(() =>
            {
                var mock = new Mock<IConstructionExecutedActivityViewModel>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            ServiceLocatorMock.Setup(x => x.GetInstance<IOtherExpensesPlannedResourceViewModel>()).Returns(() =>
            {
                var mock = new Mock<IOtherExpensesPlannedResourceViewModel>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            ServiceLocatorMock.Setup(x => x.GetInstance<IWorkCapitalPlannedResourceViewModel>()).Returns(() =>
            {
                var mock = new Mock<IWorkCapitalPlannedResourceViewModel>();
                mock.SetupAllProperties();

                return mock.Object;
            });
        }


        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Budget_IfReadBeforeIsSet_Throws()
        {
            Console.WriteLine(TestObject.Budget);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Budget_GivenNullBudget_Throws()
        {
            TestObject.Budget = null;
        }

        [TestMethod]
        public void Budget_GivenBudget_SetsGivenBudget()
        {
            // Arrange
            var budget = Mock.Of<IBudgetPresenter>();

            // Act
            TestObject.Budget = budget;

            // Assert
            Assert.AreSame(budget, TestObject.Budget);
        }


        [TestMethod]
        public void IsExpanded_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.IsExpanded, () => true);
        }

        [TestMethod]
        public void IsExpanded_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.IsExpanded, () => true, () => false);
        }

        [TestMethod]
        public void IsExpanded_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.IsExpanded, () => true, false);
        }


        [TestMethod]
        public void ShortName_IfNameIsShorterThanLimit_ReturnsName()
        {
            // Arrange
            TestObject.Name = "123456";

            // Act
            string shortName = TestObject.ShortName;

            // Assert
            Assert.AreEqual(TestObject.Name, shortName);
        }

        [TestMethod]
        public void ShortName_IfNameIsOnLimit_ReturnsShortenedName()
        {
            // Arrange
            TestObject.Name = "1234567";

            // Act
            string shortName = TestObject.ShortName;

            // Assert
            Assert.AreEqual(TestObject.Name.Substring(0, 6) + "...", shortName);
        }

        [TestMethod]
        public void ShortName_IfNameIsBeyondLimit_ReturnsShortenedName()
        {
            // Arrange
            TestObject.Name = "123456745896";

            // Act
            string shortName = TestObject.ShortName;

            // Assert
            Assert.AreEqual(TestObject.Name.Substring(0, 6) + "...", shortName);
        }


        [TestMethod]
        public void Name_GivenName_NotifiesShortNameHasChanged()
        {
            // Act
            TestObject.Name = "A";

            // Assert
            CollectionAssert.Contains(ChangeTracker.ChangedProperties, ExtractPropertyName(x => x.ShortName));
        }

        [TestMethod]
        public void Name_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.Name, () => "A");
        }

        [TestMethod]
        public void Name_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.Name, () => "A", () => "B");
        }

        [TestMethod]
        public void Name_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.Name, () => "A");
        }


        [TestMethod]
        public void Code_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.Code, () => "A");
        }

        [TestMethod]
        public void Code_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.Code, () => "A", () => "B");
        }

        [TestMethod]
        public void Code_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.Code, () => "A");
        }


        [TestMethod]
        public void Location_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.Location, () => "A");
        }

        [TestMethod]
        public void Location_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.Location, () => "A", () => "B");
        }

        [TestMethod]
        public void Location_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.Location, () => "A");
        }


        [TestMethod]
        public void Constructor_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.Constructor, () => "A");
        }

        [TestMethod]
        public void Constructor_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.Constructor, () => "A", () => "B");
        }

        [TestMethod]
        public void Constructor_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.Constructor, () => "A");
        }


        [TestMethod]
        public void Objective_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.Objective, () => "A");
        }

        [TestMethod]
        public void Objective_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.Objective, () => "A", () => "B");
        }

        [TestMethod]
        public void Objective_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.Objective, () => "A");
        }


        [TestMethod]
        public void Scope_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.Scope, () => "A");
        }

        [TestMethod]
        public void Scope_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.Scope, () => "A", () => "B");
        }

        [TestMethod]
        public void Scope_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.Scope, () => "A");
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Value_GivenNullValue_Throws()
        {
            TestObject.Value = null;
        }

        [TestMethod]
        public void Value_GivenInvestmentElement_SetsIt()
        {
            // Arrange
            var investmentElement = Mock.Of<IInvestmentElement>();

            // Act
            TestObject.Value = investmentElement;

            // Assert
            Assert.AreSame(investmentElement, TestObject.Value);
        }

        [TestMethod]
        public void Value_GivenInvestmentElement_TheValueActuallyGoesToTheObjectProperty()
        {
            // Arrange
            var investmentElement = Mock.Of<IInvestmentElement>();

            // Act
            TestObject.Value = investmentElement;

            // Assert
            Assert.AreSame(investmentElement, TestObject.Object);
        }


        [TestMethod]
        public void Object_GivenInvestmentElement_SetsIt()
        {
            // Arrange
            IPresenter<IInvestmentElement> presenter = TestObject;
            var investmentElement = new Mock<IInvestmentElement>();

            // Act
            presenter.Object = investmentElement.Object;

            // Assert
            Assert.AreSame(TestObject.Object, presenter.Object);
            Assert.AreSame(investmentElement.Object, presenter.Object);
        }


        [TestMethod]
        public void Elements_ReturnsCorrectInstanceOfViewModel()
        {
            TestReturnsCorrectInnerCrudViewModelInstance(x => x.Elements);
        }

        [TestMethod]
        public void Elements_SetsCorrectlyTheParentPropertyValue()
        {
            TestReturnedCrudViewModelInstanceGetsTheParentReferenceInitialized(x => x.Elements, x => x.InvestmentElement);
        }

        [TestMethod]
        public void Elements_ReturnsLoadedInstanceOfViewModel()
        {
            TestReturnedCrudViewModelInstanceGetsLoaded(x => x.Elements);
        }


        [TestMethod]
        public void Children_ReturnsChildrenTreeNodes()
        {
            // Arrange
            var investmentComponentViewModel = TestObject.Elements;

            // Act
            IEnumerable<ITreeNode> actualNodes = TestObject.Children;

            // Assert
            Assert.AreSame(investmentComponentViewModel, actualNodes);
        }


        public class InvestmentElementPresenterStub : InvestmentElementPresenterBase<IInvestmentElement, IItemManagerApplicationServices<IInvestmentElement>>
        {
            public InvestmentElementPresenterStub(IInvestmentElement investmentElement)
                : base(investmentElement)
            {
            }

            public override int Depth
            {
                get { return 0; }
            }

            public override ITreeNode Parent { get; set; }
        }
    }
}
