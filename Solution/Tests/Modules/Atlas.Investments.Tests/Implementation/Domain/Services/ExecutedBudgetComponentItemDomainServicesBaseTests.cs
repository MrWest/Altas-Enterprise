using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Domain.Services
{
    //[TestClass, ExcludeFromCodeCoverage]
    //public class ExecutedBudgetComponentItemDomainServicesBaseTests : DomainServicesTestsBase<IExecutedBudgetComponentItem, ExecutedBudgetComponentItemDomainServicesBase<IExecutedBudgetComponentItem, IBudgetComponent>>
    //{
    //    [TestInitialize]
    //    public override void Initialize()
    //    {
    //        base.Initialize();

    //        ServiceLocatorMock.Setup(x => x.GetInstance<IExecutedBudgetComponentItem>()).Returns(() =>
    //        {
    //            var mock = new Mock<IExecutedBudgetComponentItem>();
    //            mock.SetupAllProperties();

    //            return mock.Object;
    //        });
    //    }


    //    [TestMethod, ExpectedException(typeof(ArgumentNullException))]
    //    public void CanExecute_GivenNullEnumerableOfPlannedItems_Throws()
    //    {
    //        TestObject.CanExecute<IPlannedBudgetComponentItem>(null);
    //    }

    //    [TestMethod]
    //    public void CanExecute_GivenEnumerableOfPlannedItemsWhereAtLeastOneIsUnexecuted_ReturnsTrue()
    //    {
    //        // Arrange
    //        IPlannedBudgetComponentItem[] items =
    //        {
    //            Mock.Of<IPlannedBudgetComponentItem>(),
    //            Mock.Of<IPlannedBudgetComponentItem>(x => x.Execution == Mock.Of<IExecutedBudgetComponentItem>())
    //        };

    //        // Act
    //        bool canExecute = TestObject.CanExecute(items);

    //        // Assert
    //        Assert.IsTrue(canExecute);
    //    }

    //    [TestMethod]
    //    public void CanExecute_GivenEnumerableOfPlannedItemsWhereAllAreexecuted_ReturnsFalse()
    //    {
    //        // Arrange
    //        IPlannedBudgetComponentItem[] items =
    //        {
    //            Mock.Of<IPlannedBudgetComponentItem>(x => x.Execution == Mock.Of<IExecutedBudgetComponentItem>()),
    //            Mock.Of<IPlannedBudgetComponentItem>(x => x.Execution == Mock.Of<IExecutedBudgetComponentItem>())
    //        };

    //        // Act
    //        bool canExecute = TestObject.CanExecute(items);

    //        // Assert
    //        Assert.IsFalse(canExecute);
    //    }


    //    [TestMethod, ExpectedException(typeof(ArgumentNullException))]
    //    public void Execute_GivenNullEnumerableOfPlannedItems_Throws()
    //    {
    //        TestObject.Execute<IPlannedBudgetComponentItem>(null);
    //    }

    //    [TestMethod]
    //    public void Execute_GivenEnumerableOfPlannedItems_LinqsANewExecutedItemToUnexecutedOnes()
    //    {
    //        // Arrange
    //        IPlannedBudgetComponentItem[] plannedItems =
    //        {
    //            Mock.Of<IPlannedBudgetComponentItem>(x => x.Execution == Mock.Of<IExecutedBudgetComponentItem>()),
    //            Mock.Of<IPlannedBudgetComponentItem>()
    //        };

    //        // Act
    //        TestObject.Execute(plannedItems);

    //        // Arrange
    //        Assert.IsNotNull(plannedItems[1].Execution);
    //        Assert.AreSame(plannedItems[1], plannedItems[1].Execution.Planification);
    //    }

    //    [TestMethod]
    //    public void Execute_GivenEnumerableOfPlannedItems_ReturnsTheNumberOfExecutedPlannedItems()
    //    {
    //        // Arrange
    //        IPlannedBudgetComponentItem[] plannedItems =
    //        {
    //            Mock.Of<IPlannedBudgetComponentItem>(x => x.Execution == Mock.Of<IExecutedBudgetComponentItem>()),
    //            Mock.Of<IPlannedBudgetComponentItem>()
    //        };

    //        // Act
    //        IEnumerable<IExecutedBudgetComponentItem> executed = TestObject.Execute(plannedItems);

    //        // Arrange
    //        CollectionAssert.AreEquivalent(plannedItems.Skip(1).Select(x => x.Execution).ToArray(), executed.ToArray());
    //    }
    //}
}
