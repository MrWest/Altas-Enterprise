using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests.Collections.Generic
{
    /// <summary>
    /// Collection of tests to check the implementation of the <see cref="System.Collections.Generic.Tree&lt;TValue&gt;"/> class.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TreeTests
    {
        /// <summary>
        /// Must initialize correctly an instance of the tested class.
        /// </summary>
        [TestMethod]
        public void Tree_ConstructorTest()
        {
            var target = new Tree<int>(10);
            Assert.AreEqual(10, target.Value);
        }

        /// <summary>
        /// Must return the enumerable containing the subtrees of the tree in pre-order traveling way.
        /// </summary>
        [TestMethod]
        public void Tree_PreOrderMethodTests()
        {
            // Build a tree
            var node5 = new Tree<int>(5);
            var node6 = new Tree<int>(6);
            var node4 = new Tree<int>(4) { node5, node6 };
            var node3 = new Tree<int>(3);
            var node2 = new Tree<int>(2) { node4 };
            var target = new Tree<int>(1) { node2, node3 };

            // Check the pre-order travel
            CollectionAssert.AreEquivalent(new[] { 1, 2, 4, 5, 6, 3 }, (from tree in target.PrefixedOrder select tree.Value).ToArray());
        }

        /// <summary>
        /// Must return the enumerable containing the subtrees of the tree in in-order traveling way.
        /// </summary>
        [TestMethod]
        public void Tree_InOrderMethodTests()
        {
            // Build a tree
            var node5 = new Tree<int>(5);
            var node6 = new Tree<int>(6);
            var node4 = new Tree<int>(4) { node5, node6 };
            var node3 = new Tree<int>(3);
            var node2 = new Tree<int>(2) { node4 };
            var target = new Tree<int>(1) { node2, node3 };

            // Check the pre-order travel
            CollectionAssert.AreEquivalent(new[] { 5, 4, 6, 2, 1, 3 }, (from tree in target.MiddleOrder select tree.Value).ToArray());
        }

        /// <summary>
        /// Must return the enumerable containing the subtrees of the tree in post-order traveling way.
        /// </summary>
        [TestMethod]
        public void Tree_PostOrderMethodTests()
        {
            // Build a tree
            var node5 = new Tree<int>(5);
            var node6 = new Tree<int>(6);
            var node4 = new Tree<int>(4) { node5, node6 };
            var node3 = new Tree<int>(3);
            var node2 = new Tree<int>(2) { node4 };
            var target = new Tree<int>(1) { node2, node3 };

            // Check the pre-order travel
            CollectionAssert.AreEquivalent(new[] { 5, 6, 4, 2, 3, 1 }, (from tree in target.SuffixedOrder select tree.Value).ToArray());
        }
    }
}