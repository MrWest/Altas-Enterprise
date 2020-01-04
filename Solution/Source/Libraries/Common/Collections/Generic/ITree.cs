using System.Diagnostics.CodeAnalysis;

namespace System.Collections.Generic
{
    /// <summary>
    /// Describes the specification of a recursive tree, a non-lineal collection of subtrees where the root node of
    /// each tree has a value in it.
    /// </summary>
    /// <typeparam name="TValue">The root node's value type.</typeparam>
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public interface ITree<TValue> : IList<ITree<TValue>>
    {
        /// <summary>
        /// Gets or sets the value of the root node of this tree.
        /// </summary>
        TValue Value { get; set; }


        /// <summary>
        /// Gets the enumerable of subtress which is the result of traveling this tree in pre-order where there comes
        /// first the current tree value and then the same traveling process of each subtree of its.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        IEnumerable<ITree<TValue>> PrefixedOrder { get; }

        /// <summary>
        /// Gets the enumerable of subtress which is the result of traveling this tree in in-order where there comes
        /// the first subtree's in-order traveling, following the current tree vale and then the rest of the
        /// traveling process of each remaining subtree of its.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        IEnumerable<ITree<TValue>> MiddleOrder { get; }

        /// <summary>
        /// Gets the enumerable of subtress which is the result of traveling this tree in post-order where there comes
        /// first the post-order traveling process of the current tree and then its value.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        IEnumerable<ITree<TValue>> SuffixedOrder { get; }
    }
}