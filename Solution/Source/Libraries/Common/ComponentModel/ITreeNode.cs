using System.Collections.Generic;

namespace System.ComponentModel
{
    /// <summary>
    /// Represents an element being a node of a tree of elements of the fulfilling the same contract
    ///  <see cref="ITreeNode"/>.
    /// </summary>
    public interface ITreeNode:INavigable
    {
        ///// <summary>
        /////     Gets or sets the name of the current <see cref="ITreeNode" />.
        ///// </summary>
        object Value { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="ITreeNode"/>.
        /// </summary>
        string Name { get; set; }

        ///// <summary>
        ///// Gets the parent (if any) of the current <see cref="ITreeNode"/>.
        ///// </summary>
        //ITreeNode Parent { get; }

        /// <summary>
        /// Gets the <see cref="IEnumerable{T}"/> containing the children elements of the current
        /// <see cref="ITreeNode"/>.
        /// </summary>
        IEnumerable<ITreeNode> Children { get; }
    }


    /// <summary>
    /// Represents the node of a tree containing a value.
    /// </summary>
    /// <typeparam name="T">The type of the value in the node.</typeparam>
    public interface ITreeNode<T>
    {
        /// <summary>
        /// Gets or sets the value
        /// </summary>
        object Value { get; set; }

        /// <summary>
        /// Gets the parent (if any) of the current <see cref="ITreeNode"/>.
        /// </summary>
       // new ITreeNode<T> Father { get; set; }

        /// <summary>
        /// Gets the <see cref="IEnumerable{T}"/> containing the children elements of the current
        /// <see cref="ITreeNode"/>.
        /// </summary>
        IEnumerable<ITreeNode<T>> Children { get; }
    }
}
