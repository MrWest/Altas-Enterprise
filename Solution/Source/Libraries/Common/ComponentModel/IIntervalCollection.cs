using System.Collections.Generic;

namespace System.ComponentModel
{
    /// <summary>
    ///     Describes the behavior of an object containing a collection of
    ///     <see cref="System.ComponentModel.IInterval{TValue}" /> objects.
    /// </summary>
    /// <typeparam name="TValue">
    ///     The type of values representing the limits of the contained intervals.
    /// </typeparam>
    public interface IIntervalCollection<TValue> where TValue : IComparable<TValue>
    {
        /// <summary>
        ///     Gets the collection of <see cref="System.ComponentModel.IInterval{TValue}" /> contained in this
        ///     collection.
        /// </summary>
        ICollection<IInterval<TValue>> Intervals { get; }
    }
}