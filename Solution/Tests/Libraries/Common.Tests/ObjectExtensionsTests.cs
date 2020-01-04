using System;
using System.Diagnostics.CodeAnalysis;
using Common.Tests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ObjectExtensionsTests
    {
        [TestMethod]
        public void EqualsByMembers_DifferentPropertiesValues_ReturnsFalse()
        {
            // Arrange
            var o1 = new ComparandStub
            {
                Word = "A",
                Number = 1
            };
            var o2 = new ComparandStub
            {
                Word = "B",
                Number = 1
            };

            // Act
            bool comparisonResult = o1.EqualsByMembers(o2);

            // Assert
            Assert.IsFalse(comparisonResult);
        }

        [TestMethod]
        public void EqualsByMembers_EqualPropertiesValues_ReturnsTrue()
        {
            // Arrange
            var referencedStub = new ComparandStub
            {
                Word = "A",
                Number = 1
            };
            var o1 = new ComparandStub
            {
                Word = "B",
                Number = 1,
                Stub = referencedStub
            };
            var o2 = new ComparandStub
            {
                Word = "B",
                Number = 1,
                Stub = referencedStub
            };

            // Act
            bool comparisonResult = o1.EqualsByMembers(o2);

            // Assert
            Assert.IsTrue(comparisonResult);
        }

        [TestMethod]
        public void EqualsByMembers_EqualsUnignoredProperties_ReturnsTrue()
        {
            // Arrange
            var referencedStub = new ComparandStub
            {
                Word = "A",
                Number = 1
            };
            var o1 = new ComparandStub
            {
                Word = "B",
                Number = 1,
                Stub = referencedStub
            };
            var o2 = new ComparandStub
            {
                Word = "A",
                Number = 1,
                Stub = referencedStub
            };

            // Act
            bool comparisonResult = o1.EqualsByMembers(o2, "Word");

            // Assert
            Assert.IsTrue(comparisonResult);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void EqualsByMembers_EnumerableComparands_ThrowsException()
        {
            Assert.IsFalse("1".EqualsByMembers("2"));
        }

        [TestMethod]
        public void EqualsByMembers_NullObj1_ReturnFalse()
        {
            Assert.IsFalse(ObjectExtensions.EqualsByMembers(null, string.Empty));
        }

        [TestMethod]
        public void EqualsByMembers_NullObj2_ReturnFalse()
        {
            Assert.IsFalse(string.Empty.EqualsByMembers(null));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateProperties_SourceObjectIsNull_ThrowsException()
        {
            // Act
            ObjectExtensions.UpdateProperties(null, new object());
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void UpdateProperties_DestinationObjectIsNull_ThrowsException()
        {
            // Act
            new object().UpdateProperties(null);
        }

        [TestMethod]
        public void UpdateProperties_GivenTwoObjects_PassesValuesOfPropertiesOfFirstToSecond()
        {
            // Arrange
            Stub object1 = new Stub { Name = "A", Age = 10 }, object2 = new Stub { Name = "B", Age = 20 };
            // Act
            object1.UpdateProperties(object2);
            // Assert
            Assert.AreEqual(object1.Name, object2.Name);
            Assert.AreEqual(object1.Age, object1.Age);
        }


        class Stub
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}