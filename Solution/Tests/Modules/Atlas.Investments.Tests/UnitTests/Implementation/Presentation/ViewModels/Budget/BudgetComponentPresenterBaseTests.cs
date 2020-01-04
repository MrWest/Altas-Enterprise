using System;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Presentation.ViewModels.Budget
{
    [TestClass, ExcludeFromCodeCoverage]
    public class BudgetComponentPresenterBaseTests : PresenterTestsBase<IBudgetComponent, BudgetComponentPresenterBaseTests.BudgetComponentPresenterStub, IItemManagerApplicationServices<IBudgetComponent>>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Budget_IfReadBeforeSet_Throws()
        {
            Console.WriteLine(TestObject.Budget);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Budget_GivenNullBudget_Throws()
        {
            TestObject.Budget = null;
        }

        [TestMethod]
        public void Budget_GivenBudget_GetsSuchsBudget()
        {
            // Arrange
            var budget = Mock.Of<IBudgetPresenter>();

            // Act
            TestObject.Budget = budget;

            // Assert
            Assert.AreSame(budget, TestObject.Budget);
        }


        [TestMethod]
        public void PlannedResources_ReturnsCorrectInstanceOfDesiredCrudViewModel()
        {
            TestReturnsCorrectInnerCrudViewModelInstance(x => x.PlannedResources);
        }

        [TestMethod]
        public void PlannedResources_ReturnsLoadedInstanceOfDesiredCrudViewModel()
        {
            TestReturnedCrudViewModelInstanceGetsLoaded(x => x.PlannedResources);
        }

        [TestMethod]
        public void PlannedResources_ReturnsInstanceOfDesiredCrudViewModelWithComponentReferenceInitialized()
        {
            // Arrange
            ServiceLocatorMock.Setup(x => x.GetInstance<IPlannedResourceViewModel<IBudgetComponent, IPlannedResourcePresenter<IBudgetComponent>>>()).Returns(() =>
            {
                var mock = new Mock<IPlannedResourceViewModel<IBudgetComponent, IPlannedResourcePresenter<IBudgetComponent>>>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            // Act
            var actualComponent = TestObject.PlannedResources.Component;

            // Assert
            Assert.AreSame(TestObject.Object, actualComponent);
        }


        [TestMethod]
        public void ExecutedResources_ReturnsCorrectInstanceOfDesiredCrudViewModel()
        {
            TestReturnsCorrectInnerCrudViewModelInstance(x => x.ExecutedResources);
        }

        [TestMethod]
        public void ExecutedResources_ReturnsLoadedInstanceOfDesiredCrudViewModel()
        {
            // Arrange
            ServiceLocatorMock.Setup(x => x.GetInstance<IExecutedResourceViewModel<IBudgetComponent, IExecutedResourcePresenter<IBudgetComponent>>>()).Returns(() =>
            {
                var mock = new Mock<IExecutedResourceViewModel<IBudgetComponent, IExecutedResourcePresenter<IBudgetComponent>>>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            TestReturnedCrudViewModelInstanceGetsLoaded(x => x.ExecutedResources);
        }

        [TestMethod]
        public void ExecutedResources_ReturnsInstanceOfDesiredCrudViewModelWithComponentReferenceInitialized()
        {
            // Arrange
            ServiceLocatorMock.Setup(x => x.GetInstance<IExecutedResourceViewModel<IBudgetComponent, IExecutedResourcePresenter<IBudgetComponent>>>()).Returns(() =>
            {
                var mock = new Mock<IExecutedResourceViewModel<IBudgetComponent, IExecutedResourcePresenter<IBudgetComponent>>>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            // Act
            var actualComponent = TestObject.ExecutedResources.Component;

            // Assert
            Assert.AreSame(TestObject.Object, actualComponent);
        }


        [TestMethod]
        public void PlannedActivities_ReturnsCorrectInstanceOfDesiredCrudViewModel()
        {
            TestReturnsCorrectInnerCrudViewModelInstance(x => x.PlannedActivities);
        }

        [TestMethod]
        public void PlannedActivities_ReturnsLoadedInstanceOfDesiredCrudViewModel()
        {
            // Arrange
            ServiceLocatorMock.Setup(x => x.GetInstance<IPlannedActivityViewModel<IBudgetComponent, IPlannedActivityPresenter<IBudgetComponent>>>()).Returns(() =>
            {
                var mock = new Mock<IPlannedActivityViewModel<IBudgetComponent, IPlannedActivityPresenter<IBudgetComponent>>>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            TestReturnedCrudViewModelInstanceGetsLoaded(x => x.PlannedActivities);
        }

        [TestMethod]
        public void PlannedActivities_ReturnsInstanceOfDesiredCrudViewModelWithComponentReferenceInitialized()
        {
            // Arrange
            ServiceLocatorMock.Setup(x => x.GetInstance<IPlannedActivityViewModel<IBudgetComponent, IPlannedActivityPresenter<IBudgetComponent>>>()).Returns(() =>
            {
                var mock = new Mock<IPlannedActivityViewModel<IBudgetComponent, IPlannedActivityPresenter<IBudgetComponent>>>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            // Act
            var actualComponent = TestObject.PlannedActivities.Component;

            // Assert
            Assert.AreSame(TestObject.Object, actualComponent);
        }


        [TestMethod]
        public void ExecutedActivities_ReturnsCorrectInstanceOfDesiredCrudViewModel()
        {
            TestReturnsCorrectInnerCrudViewModelInstance(x => x.ExecutedActivities);
        }

        [TestMethod]
        public void ExecutedActivities_ReturnsLoadedInstanceOfDesiredCrudViewModel()
        {
            TestReturnedCrudViewModelInstanceGetsLoaded(x => x.ExecutedActivities);
        }

        [TestMethod]
        public void ExecutedActivities_ReturnsInstanceOfDesiredCrudViewModelWithComponentReferenceInitialized()
        {
            // Arrange
            ServiceLocatorMock.Setup(x => x.GetInstance<IExecutedActivityViewModel<IBudgetComponent, IExecutedActivityPresenter<IBudgetComponent>>>()).Returns(() =>
            {
                var mock = new Mock<IExecutedActivityViewModel<IBudgetComponent, IExecutedActivityPresenter<IBudgetComponent>>>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            // Act
            var actualComponent = TestObject.ExecutedActivities.Component;

            // Assert
            Assert.AreSame(TestObject.Object, actualComponent);
        }


        public class BudgetComponentPresenterStub :
            BudgetComponentPresenterBase<
                IBudgetComponent,
                IPlannedResourceViewModel<IBudgetComponent, IPlannedResourcePresenter<IBudgetComponent>>,
                IPlannedResourcePresenter<IBudgetComponent>,
                IExecutedResourceViewModel<IBudgetComponent, IExecutedResourcePresenter<IBudgetComponent>>,
                IExecutedResourcePresenter<IBudgetComponent>,
                IPlannedActivityViewModel<IBudgetComponent, IPlannedActivityPresenter<IBudgetComponent>>,
                IPlannedActivityPresenter<IBudgetComponent>,
                IExecutedActivityViewModel<IBudgetComponent, IExecutedActivityPresenter<IBudgetComponent>>,
                IExecutedActivityPresenter<IBudgetComponent>>
        {
            public BudgetComponentPresenterStub()
            {
            }

            public BudgetComponentPresenterStub(IBudgetComponent budgetComponent)
                : base(budgetComponent)
            {
            }
        }
    }
}
