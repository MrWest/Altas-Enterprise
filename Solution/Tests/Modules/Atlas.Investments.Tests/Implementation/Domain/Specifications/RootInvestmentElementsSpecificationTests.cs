using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Domain.Specifications
{
    [TestClass, ExcludeFromCodeCoverage]
    public class RootInvestmentElementsSpecificationTests
    {
        //[TestMethod]
        //public void Condition_HasCorrectPredicateReturningTheIndependentInvestmentElements()
        //{
        //    // Arrange
        //    IInvestmentElement parent1 = Mock.Of<IInvestmentElement>(), parent2 = Mock.Of<IInvestmentElement>();
        //    IInvestmentElement child1 = Mock.Of<IInvestmentElement>(x => x.Parent == parent1),
        //        child2 = Mock.Of<IInvestmentElement>(x => x.Parent == parent2);
        //    IInvestmentElement[] elements = { parent1, child1, parent2, child2 };
            
        //    var specification = new RootInvestmentElementsSpecification();

        //    // Act
        //    IInvestmentElement[] queryResult = elements.AsQueryable().Where(x => specification.Predicate.Compile()(x)).ToArray();
        //    CollectionAssert.AreEquivalent(new[] { parent1, parent2 }, queryResult);
        //}
    }
}
