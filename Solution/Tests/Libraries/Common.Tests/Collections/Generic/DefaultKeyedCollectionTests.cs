using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests.Collections.Generic
{
    [TestClass, ExcludeFromCodeCoverage]
    public class DefaultKeyedCollectionTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenNullFunction_ThrowsException()
        {
            // Arrange
            new DefaultKeyedCollection<int, int>(null);
        }

        [TestMethod]
        public void Item_Always_PassesCallToGivenKeySelectionFunction()
        {
            // Arrange
            var expectedElement = new Dummy { Id = 1, Value = "A" };
            bool calledFunction = false;
            Func<Dummy, int> keySelection = dummy =>
            {
                calledFunction = true;
                return dummy.Id;
            };
            var collection = new DefaultKeyedCollection<int, Dummy>(keySelection) { expectedElement, new Dummy { Id = 2, Value = "B" } };
            
            // Act
            Dummy actualElement = collection[1];
            
            // Assert
            Assert.AreEqual(expectedElement, actualElement);
            Assert.IsTrue(calledFunction);
        }

        private struct Dummy
        {
            public string Value { get; set; }

            public int Id { get; set; }
        }
    }
}