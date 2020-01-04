using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Implementation.Application;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Application
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExecutedBudgetComponentItemManagerApplicationServicesBaseTests :
        ItemManagerApplicationServicesTestsBase<ExecutedBudgetComponentItemManagerApplicationServicesBaseTests.ApplicationServicesStub, IExecutedBudgetComponentItem, IBudgetComponentItemRepository<IExecutedBudgetComponentItem, IBudgetComponent>, IExecutedBudgetComponentItemDomainServices<IExecutedBudgetComponentItem, IBudgetComponent>, IUnitOfWork>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            RepositoryMock.SetupProperty(x => x.Component);
            DomainServicesMock.SetupProperty(x => x.Component);

            TestObject.Component = Mock.Of<IBudgetComponent>();
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CanExecute_GivenNullPlannedItems_Throws()
        {
            TestObject.CanExecute<IPlannedBudgetComponentItem>(null);
        }

        [TestMethod]
        public void CanExecute_GivenPlannedItems_PassesCallToServices()
        {
            // Arrange
            IPlannedBudgetComponentItem[] items = { Mock.Of<IPlannedBudgetComponentItem>(x => (int)x.Id == 1) };
            IPlannedBudgetComponentItem[] repositoryItems = { Mock.Of<IPlannedBudgetComponentItem>(x => (int)x.Id == 1) };
            DomainServicesMock.Setup(x => x.CanExecute(repositoryItems)).Returns(true);

            Mock<IRepository> plannedItemRepository = Mock.Get((IRepository)TestObjectInternals.GetProperty("PlannedItemRepository"));
            plannedItemRepository.Setup(x => x.FindById(1)).Returns(repositoryItems);

            // Act
            bool canExecuted = TestObject.CanExecute(items);

            // Assert
            Assert.IsTrue(canExecuted);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Execute_GivenNullPlannedItems_Throws()
        {
            TestObject.Execute<IPlannedBudgetComponentItem>(null);
        }

        [TestMethod]
        public void Execute_GivenPlannedItems_PassesCallToServices()
        {
            // Arrange
            IPlannedBudgetComponentItem[] items =
            {
                Mock.Of<IPlannedBudgetComponentItem>(x => x.Execution == Mock.Of<IExecutedBudgetComponentItem>() && (int)x.Id == 1),
                Mock.Of<IPlannedBudgetComponentItem>(x => (int)x.Id == 2),
                Mock.Of<IPlannedBudgetComponentItem>(x => (int)x.Id == 3)
            };
            var dbPlannedItems = new MockRepository(MockBehavior.Default);
            IPlannedBudgetComponentItem[] repositoryItems =
            {
                dbPlannedItems.OneOf<IPlannedBudgetComponentItem>(x => x.Execution == Mock.Of<IExecutedBudgetComponentItem>()),
                dbPlannedItems.OneOf<IPlannedBudgetComponentItem>(),
                dbPlannedItems.OneOf<IPlannedBudgetComponentItem>()
            };
            repositoryItems.Where(x => x.Execution == null).ToList().ForEach(rep => Mock.Get(rep).Setup(x => x.Execution).Verifiable());
            
            DomainServicesMock
                .Setup(x => x.Execute(repositoryItems))
                .Returns<IEnumerable<IPlannedBudgetComponentItem>>(
                plannedItems => 
                    plannedItems.Where(x => x.Execution == null).Aggregate(new List<IExecutedBudgetComponentItem>(),
                    (list, item) =>
                    {
                        Mock.Get(item).SetupGet(x => x.Execution).Returns(Mock.Of<IExecutedBudgetComponentItem>());
                        list.Add(item.Execution);
                        return list;
                    }));

            Mock<IRepository> plannedItemRepository = Mock.Get((IRepository)TestObjectInternals.GetProperty("PlannedItemRepository"));
            plannedItemRepository.Setup(x => x.FindById(1, 2, 3)).Returns(repositoryItems);

            // Act
            IEnumerable<IExecutedBudgetComponentItem> executed = TestObject.Execute(items);

            // Assert
            CollectionAssert.AreEquivalent(repositoryItems.Skip(1).Select(x => x.Execution).ToArray(), executed.ToArray());
            dbPlannedItems.Verify();
        }

        [TestMethod]
        public void Execute_GivenPlannedItems_CommandsRepositoryToAddExecutedItems()
        {
            // Arrange
            IPlannedBudgetComponentItem[] items =
            {
                Mock.Of<IPlannedBudgetComponentItem>(x => (int)x.Id == 1),
                Mock.Of<IPlannedBudgetComponentItem>(x => (int)x.Id == 2),
                Mock.Of<IPlannedBudgetComponentItem>(x => (int)x.Id == 3)
            };
            IPlannedBudgetComponentItem[] repositoryItems =
            {
                Mock.Of<IPlannedBudgetComponentItem>(x => x.Execution == Mock.Of<IExecutedBudgetComponentItem>()),
                Mock.Of<IPlannedBudgetComponentItem>(),
                Mock.Of<IPlannedBudgetComponentItem>()
            };
            
            Mock<IRepository> plannedItemRepository = Mock.Get((IRepository)TestObjectInternals.GetProperty("PlannedItemRepository"));
            plannedItemRepository.Setup(x => x.FindById(1, 2, 3)).Returns(repositoryItems);
            DomainServicesMock
                .Setup(x => x.Execute(repositoryItems))
                .Returns<IEnumerable<IPlannedBudgetComponentItem>>(
                plannedItems => plannedItems.Aggregate(new List<IExecutedBudgetComponentItem>(), (list, item) =>
                {
                    item.Execution = Mock.Of<IExecutedBudgetComponentItem>();
                    list.Add(item.Execution);
                    return list;
                }));

            // Act
            TestObject.Execute(items);

            // Assert
            RepositoryMock.Verify(x => x.Add(repositoryItems[1].Execution));
            RepositoryMock.Verify(x => x.Add(repositoryItems[2].Execution));
        }


        public class ApplicationServicesStub :
            ExecutedBudgetComponentItemManagerApplicationServicesBase<IExecutedBudgetComponentItem, IBudgetComponent, IBudgetComponentItemRepository<IExecutedBudgetComponentItem, IBudgetComponent>, IExecutedBudgetComponentItemDomainServices<IExecutedBudgetComponentItem, IBudgetComponent>>
        {
            private readonly IRepository _plannedItemRepository = Mock.Of<IRepository>();


            protected override IRepository PlannedItemRepository
            {
                get { return _plannedItemRepository; }
            }
        }
    }
}