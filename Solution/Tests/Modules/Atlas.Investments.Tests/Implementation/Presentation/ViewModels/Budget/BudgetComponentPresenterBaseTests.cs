using System;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Presentation.ViewModels.Budget
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


        //[TestMethod]
        //public void PlannedResources_ReturnsCorrectInstanceOfDesiredCrudViewModel()
        //{
        //    TestReturnsCorrectInnerCrudViewModelInstance(x => x.PlannedResources);
        //}

        //[TestMethod]
        //public void PlannedResources_ReturnsLoadedInstanceOfDesiredCrudViewModel()
        //{
        //    TestReturnedCrudViewModelInstanceGetsLoaded(x => x.PlannedResources);
        //}

        [TestMethod]
        public void PlannedSubSpecialityHolder_ReturnsInstanceOfDesiredCrudViewModelWithComponentReferenceInitialized()
        {
            // Arrange
            ServiceLocatorMock.Setup(x => x.GetInstance<IPlannedSubSpecialityHolderViewModel<IBudgetComponent, IPlannedSubSpecialityHolderPresenter<IBudgetComponent>>>()).Returns(() =>
            {
                var mock = new Mock<IPlannedSubSpecialityHolderViewModel<IBudgetComponent, IPlannedSubSpecialityHolderPresenter<IBudgetComponent>>>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            // Act
            var actualComponent = TestObject.PlannedSubSpecialityHolders.BudgetComponent;

            // Assert
            Assert.AreSame(TestObject.Object, actualComponent);
        }


        [TestMethod]
        public void ExecutedSubSpecialityHolder_ReturnsCorrectInstanceOfDesiredCrudViewModel()
        {
            TestReturnsCorrectInnerCrudViewModelInstance(x => x.ExecutedSubSpecialityHolders);
        }

        [TestMethod]
        public void ExecutedSubSpecialityHolder_ReturnsLoadedInstanceOfDesiredCrudViewModel()
        {
            // Arrange
            ServiceLocatorMock.Setup(x => x.GetInstance<IExecutedSubSpecialityHolderViewModel<IBudgetComponent, IExecutedSubSpecialityHolderPresenter<IBudgetComponent>>>()).Returns(() =>
            {
                var mock = new Mock<IExecutedSubSpecialityHolderViewModel<IBudgetComponent, IExecutedSubSpecialityHolderPresenter<IBudgetComponent>>>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            TestReturnedCrudViewModelInstanceGetsLoaded(x => x.ExecutedSubSpecialityHolders);
        }

        [TestMethod]
        public void ExecutedSubSpecialityHolder_ReturnsInstanceOfDesiredCrudViewModelWithComponentReferenceInitialized()
        {
            // Arrange
            ServiceLocatorMock.Setup(x => x.GetInstance<IExecutedSubSpecialityHolderViewModel<IBudgetComponent, IExecutedSubSpecialityHolderPresenter<IBudgetComponent>>>()).Returns(() =>
            {
                var mock = new Mock<IExecutedSubSpecialityHolderViewModel<IBudgetComponent, IExecutedSubSpecialityHolderPresenter<IBudgetComponent>>>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            // Act
            var actualComponent = TestObject.ExecutedSubSpecialityHolders.BudgetComponent;

            // Assert
            Assert.AreSame(TestObject.Object, actualComponent);
        }


        [TestMethod]
        public void PlannedSubSpecialityHolder_ReturnsCorrectInstanceOfDesiredCrudViewModel()
        {
            TestReturnsCorrectInnerCrudViewModelInstance(x => x.PlannedSubSpecialityHolders);
        }

        [TestMethod]
        public void PlannedSubSpecialityHolder_ReturnsLoadedInstanceOfDesiredCrudViewModel()
        {
            // Arrange
            ServiceLocatorMock.Setup(x => x.GetInstance<IPlannedSubSpecialityHolderViewModel<IBudgetComponent, IPlannedSubSpecialityHolderPresenter<IBudgetComponent>>>()).Returns(() =>
            {
                var mock = new Mock<IPlannedSubSpecialityHolderViewModel<IBudgetComponent, IPlannedSubSpecialityHolderPresenter<IBudgetComponent>>>();
                mock.SetupAllProperties();

                return mock.Object;
            });

            TestReturnedCrudViewModelInstanceGetsLoaded(x => x.PlannedActivities);
        }

        //[TestMethod]
        //public void PlannedActivities_ReturnsInstanceOfDesiredCrudViewModelWithComponentReferenceInitialized()
        //{
        //    // Arrange
        //    ServiceLocatorMock.Setup(x => x.GetInstance<IPlannedActivityViewModel<IBudgetComponent, IPlannedActivityPresenter<IBudgetComponent>>>()).Returns(() =>
        //    {
        //        var mock = new Mock<IPlannedActivityViewModel<IBudgetComponent, IPlannedActivityPresenter<IBudgetComponent>>>();
        //        mock.SetupAllProperties();

        //        return mock.Object;
        //    });

        //    // Act
        //    var actualComponent = TestObject.PlannedActivities.Component;

        //    // Assert
        //    Assert.AreSame(TestObject.Object, actualComponent);
        //}


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

        //[TestMethod]
        //public void ExecutedActivities_ReturnsInstanceOfDesiredCrudViewModelWithComponentReferenceInitialized()
        //{
        //    // Arrange
        //    ServiceLocatorMock.Setup(x => x.GetInstance<IExecutedActivityViewModel<IBudgetComponent, IExecutedActivityPresenter<IBudgetComponent>>>()).Returns(() =>
        //    {
        //        var mock = new Mock<IExecutedActivityViewModel<IBudgetComponent, IExecutedActivityPresenter<IBudgetComponent>>>();
        //        mock.SetupAllProperties();

        //        return mock.Object;
        //    });

        //    // Act
        //    var actualComponent = TestObject.ExecutedActivities.Component;

        //    // Assert
        //    Assert.AreSame(TestObject.Object, actualComponent);
        //}


        public class BudgetComponentPresenterStub :
            BudgetComponentPresenterBase<
                IBudgetComponent,
                IPlannedSubSpecialityHolderViewModel<IBudgetComponent, IPlannedSubSpecialityHolderPresenter<IBudgetComponent>>,
                IPlannedSubSpecialityHolderPresenter<IBudgetComponent>,
                IExecutedSubSpecialityHolderViewModel<IBudgetComponent,IExecutedSubSpecialityHolderPresenter<IBudgetComponent>>,
                IExecutedSubSpecialityHolderPresenter<IBudgetComponent>>
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
