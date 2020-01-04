using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Collections.Generic
{
    /// <summary>
    /// Provides the default implementation of a recursive tree. Contains three properties that allows to gets the
    /// enumerable of trees composing the result of the three classic tree travels ways: pre-order, in-order,
    /// post-order.
    /// </summary>
    /// <typeparam name="TValue">The type of the node's value.</typeparam>
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public class Tree<TValue> : Collection<ITree<TValue>>, ITree<TValue>
    {
        /// <summary>
        /// Initializes a new instance of a tree with its root node's value.
        /// </summary>
        /// <param name="value">The value of the root node.</param>
        public Tree(TValue value)
        {
            Value = value;
        }


        /// <summary>
        /// Gets or sets the value of the root node of this tree.
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// Gets the enumerable of subtress which is the result of traveling this tree in pre-order.
        /// </summary>
        public IEnumerable<ITree<TValue>> PrefixedOrder
        {
            get
            {
                // First goes the root of the current tree
                yield return this;

                // Following the pre-order traversing of that tree's subtrees
                foreach (var preOrder in this.SelectMany(subtree => subtree.PrefixedOrder)) yield return preOrder;
            }
        }

        /// <summary>
        /// Gets the enumerable of subtress which is the result of traveling this tree in in-order.
        /// </summary>
        public IEnumerable<ITree<TValue>> MiddleOrder
        {
            get
            {
                // First goes the root of the current tree
                if (Count > 0) foreach (var subtree in this[0].MiddleOrder) yield return subtree;
                
                // Then goes the current tree
                yield return this;
                
                // Following the reaining subtree's in-order travesing
                foreach (var subtree in this.Skip(1).SelectMany(subtree => subtree.MiddleOrder)) yield return subtree;
            }
        }

        /// <summary>
        /// Gets the enumerable of subtress which is the result of traveling this tree in post-order.
        /// </summary>
        public IEnumerable<ITree<TValue>> SuffixedOrder
        {
            get
            {
                // First goes the sub-tree's suffix order traversing
                foreach (var subtree in this.SelectMany(subtree => subtree.SuffixedOrder)) yield return subtree;
                
                // And then this tree's root
                yield return this;
            }
        }
    }
}