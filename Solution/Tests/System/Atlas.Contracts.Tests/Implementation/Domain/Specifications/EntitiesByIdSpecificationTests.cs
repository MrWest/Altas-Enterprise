using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Domain.Specifications
{
    [TestClass, ExcludeFromCodeCoverage]
    public class EntitiesByIdSpecificationTests
    {
        [TestMethod, ExpectedException(typeof (ArgumentNullException))]
        public void Constructor_GivenNullArray_Throws()
        {
            new EntitiesByIdSpecification<IEntity>(null);
        }

        [TestMethod]
        public void Constructor_GivenArray_InitializesPredicate()
        {
            // Arrange
            var specification = new EntitiesByIdSpecification<IEntity>(1);

            // Act
            Expression<Predicate<IEntity>> predicate = specification.Predicate;

            // Assert
            Assert.IsNotNull(predicate);
        }


        [TestMethod]
        public void Predicate_ContainsTheCorrectCriteria()
        {
            // Arrange
            IEntity[] entitiesPool =
            {
                Mock.Of<IEntity>(x => x.Id == 1.ToString()), Mock.Of<IEntity>(x => x.Id == 2.ToString()),
                Mock.Of<IEntity>(x => x.Id == 3.ToString()), Mock.Of<IEntity>(x => x.Id == 4.ToString())
            };
            var specification = new EntitiesByIdSpecification<IEntity>(1, 4);

            // Act
            Predicate<IEntity> predicate = specification.Predicate.Compile();
            IEnumerable<IEntity> result = entitiesPool.Where(x => predicate(x));
            CollectionAssert.AreEquivalent(new[] { entitiesPool[0], entitiesPool[3] }, result.ToArray());
        }
    }
}