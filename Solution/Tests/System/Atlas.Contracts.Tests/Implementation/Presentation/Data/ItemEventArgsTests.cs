using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Presentation.Data
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ItemEventArgsTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenNullItem_ThrowsException()
        {
            new ItemEventArgs(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenNullItem_ThrowsExceptio2()
        {
            new ItemEventArgs<object>(null);
        }

        [TestMethod]
        public void Constructor_GivenItem_InitializesTheItemProperty()
        {
            // Arrange
            const int item = 90;

            // Act
            var args = new ItemEventArgs(item);

            // Assert
            Assert.AreEqual(item, args.Item);
        }

        [TestMethod]
        public void Constructor_GivenItem_InitializesTheItemProperty2()
        {
            // Arrange
            const int item = 90;

            // Act
            var args = new ItemEventArgs<int>(item);

            // Assert
            Assert.AreEqual(item, args.Item);
        }
    }
}
