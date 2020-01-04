using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Implementation.Domain;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Domain
{
    [TestClass, ExcludeFromCodeCoverage]
    public class SpecificationTests
    {
        static Specification<IEntity> CreateSpecification(Expression<Predicate<IEntity>> condition)
        {
            return new Specification<IEntity>(condition);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AndOperator_FirstSpecificationNull_ThrowsException()
        {
            // Arrange
            Specification<IEntity> other = CreateSpecification(e => true);
            // Act
            Specification<IEntity> newSpec = null & other;
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AndOperator_SecondSpecificationNull_ThrowsException()
        {
            // Arrange
            Specification<IEntity> other = CreateSpecification(e => true);
            // Act
            Specification<IEntity> newSpec = other & null;
        }

        [TestMethod]
        public void AndOperator_WithOtherSpecification_ReturnsSpecificationBeingAndBetweenTheTwo()
        {
            // Arrange
            Specification<IEntity> specificationA = CreateSpecification(e => Convert.ToInt32(e.Id) >= 1);
            Specification<IEntity> specificationB = CreateSpecification(e => Convert.ToInt32(e.Id) <= 2);
            // Act
            Specification<IEntity> mergedSpecification = specificationA & specificationB;
            // Assert
            var entity = Mock.Of<IEntity>(x => x.Id == 1.ToString());
            Assert.IsTrue(mergedSpecification.Predicate.Compile().Invoke(entity));
        }


        [TestMethod]
        public void Cosntructor_IsDefault_InitializesPredicateToATrueCondition()
        {
            // Act
            var specification = new Specification<IEntity>();

            // Assert
            Assert.IsTrue(specification.Predicate.Compile()(Mock.Of<IEntity>()));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_PredicateNull_ThrowsException()
        {
            // Act
            CreateSpecification(null);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void NotOperator_SecondSpecificationNull_ThrowsException()
        {
            // Arrange
            Specification<IEntity> nullSpec = null;
            // Act
            Specification<IEntity> newSpec = !nullSpec;
        }

        [TestMethod]
        public void NotOperator_WithOtherSpecification_ReturnsSpecificationBeingOrBetweenTheTwo()
        {
            // Arrange
            Specification<IEntity> specificationA = CreateSpecification(e => e.Id == 1.ToString());
            // Act
            Specification<IEntity> mergedSpecification = !specificationA;
            // Assert
            var entity = Mock.Of<IEntity>(x => x.Id == 2.ToString());
            Assert.IsTrue(mergedSpecification.Predicate.Compile().Invoke(entity));
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void OrOperator_FirstSpecificationNull_ThrowsException()
        {
            // Arrange
            Specification<IEntity> other = CreateSpecification(e => true);
            // Act
            Specification<IEntity> newSpec = null | other;
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void OrOperator_SecondSpecificationNull_ThrowsException()
        {
            // Arrange
            Specification<IEntity> other = CreateSpecification(e => true);
            // Act
            Specification<IEntity> newSpec = other | null;
        }

        [TestMethod]
        public void OrOperator_WithOtherSpecification_ReturnsSpecificationBeingOrBetweenTheTwo()
        {
            // Arrange
            Specification<IEntity> specificationA = CreateSpecification(e => e.Id == 1.ToString());
            Specification<IEntity> specificationB = CreateSpecification(e => e.Id == 2.ToString());
            // Act
            Specification<IEntity> mergedSpecification = specificationA | specificationB;
            // Assert
            var entity = Mock.Of<IEntity>(x => x.Id == 2.ToString());
            Assert.IsTrue(mergedSpecification.Predicate.Compile().Invoke(entity));
        }
    }
}