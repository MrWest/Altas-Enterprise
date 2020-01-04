using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompanyName.Atlas.Contracts.Implementation.Domain;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Domain
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExpressionBuilderTests
    {
        /// <summary>
        ///     Check it throws the proper exceptions when getting nulls at the calling time of the methods expose by the tested
        ///     class.
        /// </summary>
        [TestMethod, ExpectedException(typeof (ArgumentNullException))]
        public void And_FirstOperadIsNull_ThrowsException()
        {
            ExpressionBuilder.And<object>(null, null);
        }

        [TestMethod, ExpectedException(typeof (ArgumentNullException))]
        public void And_SecondOperationIsNull_ThrowsException()
        {
            ExpressionBuilder.And<Func<object, bool>>(o => true, null);
        }

        [TestMethod, ExpectedException(typeof (ArgumentNullException))]
        public void Or_FirstOperandIsNull_ThrowsException()
        {
            ExpressionBuilder.Or<object>(null, null);
        }

        [TestMethod, ExpectedException(typeof (ArgumentNullException))]
        public void Or_SecondOperandIsNull_ThrowsException()
        {
            ExpressionBuilder.Or<Func<object, bool>>(o => true, null);
        }

        [TestMethod, ExpectedException(typeof (ArgumentNullException))]
        public void Not_OperandIsNull_ThrowsException()
        {
            ExpressionBuilder.Not<object>(null);
        }

        [TestMethod, ExpectedException(typeof (ArgumentNullException))]
        public void Compose_FirstArgIsNull_ThrowsException()
        {
            ExpressionBuilder.Compose<object>(null, null, null);
        }

        [TestMethod, ExpectedException(typeof (ArgumentNullException))]
        public void Compose_SecondArgIsNull_ThrowsException()
        {
            ExpressionBuilder.Compose<Func<object, bool>>(o => true, null, null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Compose_ThirdArgIsNull_ThrowsException()
        {
            ExpressionBuilder.Compose<Func<object, bool>>(o => true, o => true, null);
        }

        /// <summary>
        ///     Checks whether the tested class is able to compose two expressions in one by using the And function (&).
        /// </summary>
        [TestMethod]
        public void ComposesExpressionsUsingAndFunction()
        {
            // Arrange
            IQueryable<string> samples = new[] { "Samples", "ore", "Siro", "Aries" }.AsQueryable();
            Expression<Func<string, bool>> expression1 = s => s.Length == 4;
            Expression<Func<string, bool>> expression2 = t => t.StartsWith("S");

            // Act
            Expression<Func<string, bool>> resultExpression = expression1.And(expression2);

            // Assert
            Assert.AreEqual("Siro", samples.Where(resultExpression).Single());
        }

        /// <summary>
        ///     Checks whether the tested class is able to compose two expressions in one by using the given merging function.
        /// </summary>
        [TestMethod]
        public void ComposesExpressionsUsingMergingFunction()
        {
            // Arrange
            IQueryable<string> samples = new[] { "Samples", "ore", "Siro", "Aries" }.AsQueryable();
            Expression<Func<string, bool>> expression1 = s => s.Length == 4;
            Expression<Func<string, bool>> expression2 = t => t.StartsWith("S");

            // Act
            Expression<Func<string, bool>> resultExpression = expression1.Compose(expression2, Expression.And);

            // Assert
            Assert.AreEqual("Siro", samples.Where(resultExpression).Single());
        }

        /// <summary>
        ///     Checks whether the tested class is able to compose two expressions in one by using the Or function (!).
        /// </summary>
        [TestMethod]
        public void ComposesExpressionsUsingNotFunction()
        {
            // Arrange
            IQueryable<string> samples = new[] { "Samples", "ore", "Siro", "Aries" }.AsQueryable();
            Expression<Func<string, bool>> expression1 = s => s.Length == 4;

            // Act
            Expression<Func<string, bool>> resultExpression = expression1.Not();

            // Assert
            string[] expected = { "Samples", "Aries", "ore" };
            string[] actual = samples.Where(resultExpression).ToArray();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        /// <summary>
        ///     Checks whether the tested class is able to compose two expressions in one by using the Or function (|).
        /// </summary>
        [TestMethod]
        public void ComposesExpressionsUsingOrFunction()
        {
            // Arrange
            IQueryable<string> samples = new[] { "Samples", "ore", "Siro", "Aries" }.AsQueryable();
            Expression<Func<string, bool>> expression1 = s => s.Length == 4;
            Expression<Func<string, bool>> expression2 = t => t.StartsWith("S");

            // Act
            Expression<Func<string, bool>> resultExpression = expression1.Or(expression2);

            // Assert
            string[] expected = samples.Where(resultExpression).ToArray();
            string[] actual = samples.Where(resultExpression).ToArray();
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}