using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Specifications
{
    [TestClass, ExcludeFromCodeCoverage]
    public class BudgetComponentItemsOfSpecificationTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenNullBudgetComponent_Throws()
        {
            new BudgetComponentItemsOfSpecification<IBudgetComponentItem>(null);
        }


        [TestMethod]
        public void Condition_HasCorrectPredicateReturningTheItemsOfTheBudgetComponent()
        {
            // Arrange
            IBudgetComponent component1 = Mock.Of<IBudgetComponent>(x => (int)x.Id == 1),
                component2 = Mock.Of<IBudgetComponent>(x => (int)x.Id == 2);
            IBudgetComponentItem child1 = Mock.Of<IBudgetComponentItem>(x => x.Component == component1),
                child2 = Mock.Of<IBudgetComponentItem>(x => x.Component == component2);
            IBudgetComponentItem[] elements = { child1, child2 };

            var specification = new BudgetComponentItemsOfSpecification<IBudgetComponentItem>(component2);

            // Act
            IBudgetComponentItem[] queryResult = elements.AsQueryable().Where(x => specification.Predicate.Compile()(x)).ToArray();
            CollectionAssert.AreEquivalent(new[] { child2 }, queryResult);
        }
    }
}
