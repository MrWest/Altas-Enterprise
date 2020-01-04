using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Implementation.Application;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Application.Budget
{
    [TestClass, ExcludeFromCodeCoverage]
    public class BudgetComponentItemManagerApplicationServicesBaseTests :
        ItemManagerApplicationServicesTestsBase<BudgetComponentItemManagerApplicationServicesBaseTests.BudgetComponentItemManagerApplicationServicesStub, IBudgetComponentItem, IBudgetComponentItemRepository<IBudgetComponentItem, IBudgetComponent>, IBudgetComponentItemDomainServices<IBudgetComponentItem, IBudgetComponent>, IUnitOfWork>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            RepositoryMock.SetupProperty(x => x.Component);
            DomainServicesMock.SetupProperty(x => x.Component);

            TestObject.Component = Mock.Of<IBudgetComponent>();
        }


        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Component_IsNotSet_Throws()
        {
            // Arrange
            var obj = new Mock<BudgetComponentItemManagerApplicationServicesStub> { CallBase = true };

            // Act
            Console.WriteLine(obj.Object.Component);
        }

        [TestMethod]
        public void Component_IsSet_ResturnsIt()
        {
            // Arrange
            var component = Mock.Of<IBudgetComponent>();
            TestObject.Component = component;

            // Act
            IBudgetComponent actualComponent = TestObject.Component;

            // Assert
            Assert.AreSame(component, actualComponent);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Component_IfGivenNull_Throws()
        {
            TestObject.Component = null;
        }


        [TestMethod]
        public void Repository_IsReturnsWithTheEquipmentComponentInitialized()
        {
            // Arrange
            TestObject.Component = Mock.Of<IEquipmentComponent>();

            // Act
            IBudgetComponentItemRepository<IBudgetComponentItem, IBudgetComponent> repository = TestObject.GetRepository();

            // Assert
            Assert.AreSame(TestObject.Component, repository.Component);
        }


        [TestMethod]
        public void DomainServices_IsReturnsWithTheEquipmentComponentInitialized()
        {
            // Arrange
            TestObject.Component = Mock.Of<IEquipmentComponent>();

            // Act
            IBudgetComponentItemDomainServices<IBudgetComponentItem, IBudgetComponent> domainServices = TestObject.GetDomainServices();

            // Assert
            Assert.AreSame(TestObject.Component, domainServices.Component);
        }


        [TestMethod]
        public void GetKeyFor_ReturnsKeyContainingParentId()
        {
            // Arrange
            var component = Mock.Of<IBudgetComponent>(x => (int)x.Id == 1);
            TestObject.Component = component;

            // Act
            MethodBase addMethod = TestObject.GetType().GetMethod("Add");
            string key = TestObject.GetKeyFor(addMethod, Mock.Of<IInvestmentElement>(x => (int)x.Id == 2));

            // Assert
            Assert.IsTrue(key.StartsWith("{0}->".EasyFormat(component.Id)));
        }


        [TestMethod]
        public void CanFilter_GivenAnyNameSpecification_ReturnsTrue()
        {
            Assert.IsTrue(TestObject.CanFilter("A"));
        }


        [TestMethod]
        public void Filter_GivenAnyNameSpecification_PassesCallToRepository()
        {
            // Arrange
            IBudgetComponentItem[] expectedItems =
            {
                Mock.Of<IBudgetComponentItem>(), Mock.Of<IBudgetComponentItem>()
            };
            RepositoryMock.Setup(x => x.FilterByName("AA")).Returns(expectedItems);

            // Act
            IBudgetComponentItem[] actualItems = TestObject.Filter("AA").ToArray();

            // Assert
            CollectionAssert.AreEquivalent(expectedItems, actualItems);
        }


        public class BudgetComponentItemManagerApplicationServicesStub :
            BudgetComponentItemManagerApplicationServicesBase<IBudgetComponentItem, IBudgetComponent, IBudgetComponentItemRepository<IBudgetComponentItem, IBudgetComponent>, IBudgetComponentItemDomainServices<IBudgetComponentItem, IBudgetComponent>>
        {
            public IBudgetComponentItemRepository<IBudgetComponentItem, IBudgetComponent> GetRepository()
            {
                return Repository;
            }

            public IBudgetComponentItemDomainServices<IBudgetComponentItem, IBudgetComponent> GetDomainServices()
            {
                return DomainServices;
            }
        }
    }
}