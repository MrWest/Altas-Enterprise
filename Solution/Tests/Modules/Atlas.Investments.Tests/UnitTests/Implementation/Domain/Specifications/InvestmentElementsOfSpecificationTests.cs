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
    public class InvestmentElementsOfSpecificationTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenNullInvestmentElement_Throws()
        {
            new InvestmentElementsOfSpecification(null);
        }


        [TestMethod]
        public void Condition_HasCorrectPredicateReturningTheIndependentInvestmentElements()
        {
            // Arrange
            IInvestmentElement parent1 = Mock.Of<IInvestmentElement>(x => (int)x.Id == 1),
                parent2 = Mock.Of<IInvestmentElement>(x => (int)x.Id == 2);
            IInvestmentComponent child1 = Mock.Of<IInvestmentComponent>(x => x.Parent == parent1),
                child2 = Mock.Of<IInvestmentComponent>(x => x.Parent == parent2);
            IInvestmentElement[] elements = { parent1, child1, parent2, child2 };

            var specification = new InvestmentElementsOfSpecification(parent2);

            // Act
            IInvestmentElement[] queryResult = elements
                .AsQueryable()
                .Where(x => x is IInvestmentComponent
                    && specification.Predicate.Compile()((IInvestmentComponent)x)).ToArray();
            CollectionAssert.AreEquivalent(new[] { child2 }, queryResult);
        }
    }
}
