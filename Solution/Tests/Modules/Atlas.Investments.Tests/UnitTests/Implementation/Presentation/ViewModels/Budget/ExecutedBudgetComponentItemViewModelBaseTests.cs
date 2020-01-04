using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Presentation.ViewModels.Budget
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExecutedBudgetComponentItemViewModelBaseTests :
        CrudViewModelTestsBase<ExecutedBudgetComponentItemViewModelBaseTests.ViewModelStub, IExecutedBudgetComponentItem, IBudgetComponentItemPresenter<IExecutedBudgetComponentItem, IBudgetComponent>, IExecutedBudgetComponentItemManagerApplicationServices<IExecutedBudgetComponentItem, IBudgetComponent>>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            ApplicationServicesMock.SetupProperty(x => x.Component);

            TestObject.Component = Mock.Of<IBudgetComponent>();

            ServiceLocatorMock.Setup(x => x.GetInstance<IBudgetComponentItemPresenter<IExecutedBudgetComponentItem, IBudgetComponent>>()).Returns(() =>
            {
                var mock = new Mock<IBudgetComponentItemPresenter<IExecutedBudgetComponentItem, IBudgetComponent>>();
                mock.SetupAllProperties();
                return mock.Object;
            });
            ServiceLocatorMock.Setup(x => x.GetInstance<IBudgetComponentItemPresenter<IPlannedBudgetComponentItem, IBudgetComponent>>()).Returns(() =>
            {
                var mock = new Mock<IBudgetComponentItemPresenter<IPlannedBudgetComponentItem, IBudgetComponent>>();
                mock.SetupAllProperties();
                return mock.Object;
            });
        }


        [TestMethod]
        public void Constructor_InitializesExecutePlannedItemsCommand()
        {
            Assert.IsNotNull(TestObject.ExecutePlannedItemsCommand);
        }


        [TestMethod]
        public void ExecutePlannedItemsCommand_CanExecute_GivenNullList_ReturnsFalse()
        {
            // Act
            bool canExecute = TestObject.ExecutePlannedItemsCommand.CanExecute(null);

            // Assert
            Assert.IsFalse(canExecute);
        }

        [TestMethod]
        public void ExecutePlannedItemsCommand_CanExecute_GivenListOfNotAllPlannedBudgetComponentItems_ReturnFalse()
        {
            // Act
            bool canExecute = TestObject.ExecutePlannedItemsCommand.CanExecute(new object[] { Mock.Of<IPlannedBudgetComponentItem>(), 0 });

            // Assert
            Assert.IsFalse(canExecute);
        }

        [TestMethod]
        public void ExecutePlannedItemsCommand_CanExecute_GivenListOfNotAllPlannedBudgetComponentItems_PassesCallToServices()
        {
            // Arrange
            IPlannedBudgetComponentItem[] items = { Mock.Of<IPlannedBudgetComponentItem>() };
            IPresenter[] presenters =
            {
                Mock.Of<IPresenter>(x => x.Object == items[0])
            };
            ApplicationServicesMock.Setup(x => x.CanExecute(items)).Returns(true);

            // Act
            bool canExecute = TestObject.ExecutePlannedItemsCommand.CanExecute(presenters);

            // Assert
            Assert.IsTrue(canExecute);
        }

        [TestMethod]
        public void ExecutePlannedItemsCommand_Execute_PassesCallToServices()
        {
            // Arrange
            IPlannedBudgetComponentItem[] items = { Mock.Of<IPlannedBudgetComponentItem>() };
            IPresenter[] presenters =
            {
                Mock.Of<IPresenter>(x => x.Object == items[0])
            };

            // Act
            TestObject.ExecutePlannedItemsCommand.Execute(presenters);

            // Assert
            ApplicationServicesMock.Verify(x => x.Execute(items));
        }

        [TestMethod]
        public void ExecutePlannedItemsCommand_Execute_NotifiesTheUserOfTheExecutedItems()
        {
            // Arrange
            IPlannedBudgetComponentItem[] items = { Mock.Of<IPlannedBudgetComponentItem>() };
            IPresenter[] presenters =
            {
                Mock.Of<IPresenter>(x => x.Object == items[0])
            };
            ApplicationServicesMock
                .Setup(x => x.Execute(It.IsAny<IEnumerable<IPlannedBudgetComponentItem>>()))
                .Returns<IEnumerable<IPlannedBudgetComponentItem>>(
                plannedItems =>
                    (from plnItem in plannedItems where plnItem.Execution == null select plnItem)
                    .Aggregate(new List<IExecutedBudgetComponentItem>(), (list, item) =>
                    {
                        item.Execution = Mock.Of<IExecutedBudgetComponentItem>();
                        list.Add(item.Execution);
                        return list;
                    }));

            INotification notification = null;
            TestObject.Raised += (sender, e) => notification = e.Context;

            // Act
            TestObject.ExecutePlannedItemsCommand.Execute(presenters);

            // Assert
            Assert.IsNotNull(notification);
            Assert.AreEqual(Resources.ExecutionSummary, notification.Title);
            Assert.AreEqual(Resources.ExecutedThisManyItems.EasyFormat(1), notification.Content);
        }

        [TestMethod]
        public void ExecutePlannedItemsCommand_Execute_IntroducesTheItemsExecutionsIntoTheViewModel()
        {
            // Arrange
            var plannedItem1 = Mock.Of<IPlannedBudgetComponentItem>(x => x.Execution == Mock.Of<IExecutedBudgetComponentItem>());
            var plannedItem2 = Mock.Of<IPlannedBudgetComponentItem>();
            var plannedItem3 = Mock.Of<IPlannedBudgetComponentItem>();

            var pres1 = Mock.Of<IPresenter>(x => x.Object == plannedItem1);
            var pres2 = Mock.Of<IPresenter>(x => x.Object == plannedItem2);
            var pres3 = Mock.Of<IPresenter>(x => x.Object == plannedItem3);

            ApplicationServicesMock
                .Setup(x => x.Execute(It.IsAny<IEnumerable<IPlannedBudgetComponentItem>>()))
                .Returns<IEnumerable<IPlannedBudgetComponentItem>>(
                plannedItems =>
                    (from plnItem in plannedItems where plnItem.Execution == null select plnItem)
                    .Aggregate(new List<IExecutedBudgetComponentItem>(), (list, item) =>
                    {
                        item.Execution = Mock.Of<IExecutedBudgetComponentItem>();
                        list.Add(item.Execution);
                        return list;
                    }));

            // Act
            TestObject.ExecutePlannedItemsCommand.Execute(new[]
            {
                pres1, pres2, pres3
            });

            // Assert
            Assert.AreEqual(2, TestObject.Count());
        }


        public class ViewModelStub :
            ExecutedBudgetComponentItemViewModelBase<IExecutedBudgetComponentItem, IBudgetComponentItemPresenter<IExecutedBudgetComponentItem, IBudgetComponent>, IBudgetComponent, IExecutedBudgetComponentItemManagerApplicationServices<IExecutedBudgetComponentItem, IBudgetComponent>>
        {
        }
    }
}