using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Domain.Specifications
{
    [TestClass, ExcludeFromCodeCoverage]
    public class NomenclatorHavingInItsNameSpecificationTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenNullNameSpecification_Throws()
        {
            new NomenclatorHavingInItsNameSpecification<INomenclator>(null);
        }


        [TestMethod]
        public void Predicate_IsCorrect()
        {
            // Arrange
            INomenclator[] nomenclators =
            {
                Mock.Of<INomenclator>(x => x.Name == "A1A" && x.Id == 1.ToString()),
                Mock.Of<INomenclator>(x => x.Name == "B1B" && x.Id == 2.ToString()),
                Mock.Of<INomenclator>(x => x.Name == "CCC" && x.Id == 3.ToString())
            };

            var specification = new NomenclatorHavingInItsNameSpecification<INomenclator>("1");

            // Act
            IEnumerable<INomenclator> actualYears = nomenclators.Where(x => specification.Predicate.Compile()(x));

            // Assert
            CollectionAssert.AreEquivalent(new[] { nomenclators[0], nomenclators[1] }, actualYears.ToArray());
        }
    }
}
