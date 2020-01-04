using System;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Services
{
    [TestClass, ExcludeFromCodeCoverage]
    public class BudgetComponentItemDomainServicesBaseTests : DomainServicesTestsBase<IBudgetComponentItem, BudgetComponentItemDomainServicesBase<IBudgetComponentItem, IBudgetComponent>>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Component_IsNotSet_Throws()
        {
            Console.WriteLine(TestObject.Component);
        }

        [TestMethod]
        public void Component_IsSet_ResturnsIt()
        {
            // Arrange
            var component = Mock.Of<IBudgetComponent>();
            TestObject.Component = component;

            // Act
            var actualComponent = TestObject.Component;

            // Assert
            Assert.AreSame(component, actualComponent);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Component_IfGivenNull_Throws()
        {
            TestObject.Component = null;
        }


        [TestMethod]
        public void Create_ReturnsABudgetComponentItemWithCorrectData()
        {
            // Act
            IBudgetComponentItem item = TestObject.Create();

            // Assert
            TestMock.Protected().Verify("SetDataToNew", Times.Once(), item);
        }
    }
}
