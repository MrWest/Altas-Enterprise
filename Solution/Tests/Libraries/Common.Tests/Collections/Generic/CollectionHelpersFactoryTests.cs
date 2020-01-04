using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests.Collections.Generic
{
    /// <summary>
    ///     Collection of tests to check the implementation of
    ///     <see cref="System.Collections.Generic.CollectionHelpersFactory" />.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CollectionHelpersFactoryTests
    {
        static List<string> GetList()
        {
            return new List<string>
            {
                "A",
                "B"
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetCollectionAdapterFor_NullCollection_ThrowsException()
        {
            // Act
            CollectionHelpersFactory.GetCollectionAdapterFor<Stream, FileStream>(null);
        }

        [TestMethod]
        public void GetCollectionAdapterFor_GivenCollection_ReturnsOtherCollection()
        {
            // Assert
            string[] collection = { };
            // Act
            var otherCollection = CollectionHelpersFactory.GetCollectionAdapterFor<object, string>(collection);
            // Assert
            CollectionAssert.AreEquivalent(otherCollection.ToArray(), collection);
        }

        [TestMethod]
        public void GetCollectionAdapterFor_GivenCollection_ReturnedCollectionCountMatchesOriginalCount()
        {
            // Assert
            var collection = GetList();
            var otherCollection = CollectionHelpersFactory.GetCollectionAdapterFor<string, string>(collection);
            // Act
            var actualCount = otherCollection.Count;
            // Assert
            Assert.AreEqual(actualCount, collection.Count());
        }

        [TestMethod]
        public void GetCollectionAdapterFor_GivenCollection_ReturnedCollectionIsReadOnlyValueMatchesOriginalIsReadOnly()
        {
            // Assert
            var collection = (ICollection<string>)GetList();
            var otherCollection = CollectionHelpersFactory.GetCollectionAdapterFor<string, string>(collection);
            // Act
            bool isReadOnly = otherCollection.IsReadOnly;
            // Assert
            Assert.IsFalse(isReadOnly);
        }

        [TestMethod]
        public void GetCollectionAdapterFor_GivenCollection_ReturnedCollectionGetEnumeratorResultMatchesOriginalGetEnumerator()
        {
            // Assert
            var collection = GetList();
            var otherCollection = CollectionHelpersFactory.GetCollectionAdapterFor<string, string>(collection);
            // Act
            var items = otherCollection.ToArray();
            // Assert
            CollectionAssert.AreEqual(items, collection.ToArray());
        }

        [TestMethod]
        public void GetCollectionAdapterFor_GivenCollection_ReturnedCollectionGetEnumeratorResultMatchesOriginalGetEnumerator2()
        {
            // Assert
            var collection = GetList();
            var otherCollection = CollectionHelpersFactory.GetCollectionAdapterFor<string, string>(collection);
            // Act
            var items = ((IEnumerable)otherCollection).Cast<string>().ToArray();
            // Assert
            CollectionAssert.AreEqual(items, collection.ToArray());
        }

        [TestMethod]
        public void GetCollectionAdapterFor_GivenCollection_ReturnedCollectionAddItemsInOriginal()
        {
            // Assert
            var collection = GetList();
            var otherCollection = CollectionHelpersFactory.GetCollectionAdapterFor<string, string>(collection);
            // Act
            otherCollection.Add("C");
            // Assert
            CollectionAssert.AreEqual(otherCollection.ToArray(), collection.ToArray());
        }

        [TestMethod]
        public void GetCollectionAdapterFor_GivenCollection_ReturnedCollectionClearItemsInOriginal()
        {
            // Assert
            var collection = GetList();
            var otherCollection = CollectionHelpersFactory.GetCollectionAdapterFor<string, string>(collection);
            // Act
            otherCollection.Clear();
            // Assert
            Assert.AreEqual(0, collection.Count());
        }

        [TestMethod]
        public void GetCollectionAdapterFor_GivenCollection_ReturnedCollectionDeterminesWhetherOriginalContainsAnItem()
        {
            // Assert
            var collection = GetList();
            var otherCollection = CollectionHelpersFactory.GetCollectionAdapterFor<string, string>(collection);
            // Act
            var containsB = otherCollection.Contains("B");
            // Assert
            Assert.IsTrue(containsB);
        }

        [TestMethod]
        public void GetCollectionAdapterFor_GivenCollection_ReturnedCollectionDeterminesWhetherOriginalDoesNotContainsAnItem()
        {
            // Assert
            var collection = GetList();            
            var otherCollection = CollectionHelpersFactory.GetCollectionAdapterFor<string, string>(collection);
            // Act
            var doesNotContainsC = !otherCollection.Contains("C");
            // Assert
            Assert.IsTrue(doesNotContainsC);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetCollectionAdapterFor_GivenCollectionAndCopiedToNullArray_ThrowsException()
        {
            // Assert
            var collection = GetList();
            var otherCollection = CollectionHelpersFactory.GetCollectionAdapterFor<string, string>(collection);
            var array = new string[collection.Count];
            // Act
            otherCollection.CopyTo(null, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCollectionAdapterFor_GivenCollectionAndCopiedToArrayWithDifferentLength_ThrowsException()
        {
            // Assert
            var collection = GetList();
            var otherCollection = CollectionHelpersFactory.GetCollectionAdapterFor<string, string>(collection);
            var array = new string[collection.Count + 1];
            // Act
            otherCollection.CopyTo(array, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetCollectionAdapterFor_GivenCollectionAndCopiedToArrayInIndexLesserThan0_ThrowsException()
        {
            // Assert
            var collection = GetList();
            var otherCollection = CollectionHelpersFactory.GetCollectionAdapterFor<string, string>(collection);
            var array = new string[collection.Count];
            // Act
            otherCollection.CopyTo(array, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetCollectionAdapterFor_GivenCollectionAndCopiedToArrayInIndexGreaterOriginalCollectionCount_ThrowsException()
        {
            // Assert
            var collection = GetList();
            var otherCollection = CollectionHelpersFactory.GetCollectionAdapterFor<string, string>(collection);
            var array = new string[collection.Count];
            // Act
            otherCollection.CopyTo(array, 4);
        }

        [TestMethod]
        public void GetCollectionAdapterFor_GivenCollection_ReturnedCollectionCopiesOriginalToAnArray()
        {
            // Assert
            var collection = GetList();
            var otherCollection = CollectionHelpersFactory.GetCollectionAdapterFor<string, string>(collection);
            var array = new string[collection.Count];
            // Act
            otherCollection.CopyTo(array, 1);
            // Assert
            CollectionAssert.AreEquivalent(new[] { null, "B" }, array);
        }

        [TestMethod]
        public void GetCollectionAdapterFor_GivenCollection_ReturnedCollectionRemovesItemFromOriginal()
        {
            // Assert
            var collection = GetList();
            var otherCollection = CollectionHelpersFactory.GetCollectionAdapterFor<string, string>(collection);
            // Act
            otherCollection.Remove("A");
            // Assert
            CollectionAssert.AreEquivalent(new[] { "B" }, collection);
        }
    }
}