namespace System.ComponentModel
{
    /// <summary>
    ///     Describes the behavior of an interval. It's an object containing two limits defined by two values and all times
    ///     the second value is greater than the first.
    /// </summary>
    /// <typeparam name="TValue">The type of the values serving as interval limits.</typeparam>
    public interface IInterval<TValue> where TValue : IComparable<TValue>
    {
        /// <summary>
        ///     Gets or sets the lower limit of the interval.
        /// </summary>
        TValue Start { get; set; }

        /// <summary>
        ///     Gets or sets the upper limit of the interval.
        /// </summary>
        TValue End { get; set; }
    }
}