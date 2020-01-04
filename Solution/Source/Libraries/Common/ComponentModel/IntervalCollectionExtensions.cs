using System.Linq;

namespace System.ComponentModel
{
    /// <summary>
    /// Contains some extension for the implementors or <see cref="System.ComponentModel.IIntervalCollection{TValue}"/>
    /// interface.
    /// </summary>
    public static class IntervalCollectionExtensions
    {
        /// <summary>
        /// Determines whether there are overlapping intervals in the given interval collection.
        /// </summary>
        /// <typeparam name="TValue">The type of limits of the intervals in the collection.</typeparam>
        /// <param name="collection">The interval collection to validate the limits of its periods.</param>
        /// <returns>A <see cref="System.Tuple{TValue, TValue}"/> which the two first
        /// <see cref="System.ComponentModel.IInterval{TValue}"/> with their limits overlaped in some
        /// way.</returns>
        public static Tuple<IInterval<TValue>, IInterval<TValue>> ValidateIntervals<TValue>(
            this IIntervalCollection<TValue> collection) where TValue : IComparable<TValue>
        {
            return (from interval in collection.Intervals
                let intervalStart = interval.Start
                let intervalEnd = interval.End
                from otherInterval in collection.Intervals.Except(new[] { interval })
                let otherStart = otherInterval.Start
                let otherEnd = otherInterval.End
                where
                    intervalStart.CompareTo(otherStart) == 0 || intervalEnd.CompareTo(otherEnd) == 0 ||
                    otherEnd.CompareTo(intervalStart) == 0 || intervalEnd.CompareTo(otherStart) == 0 ||
                    (intervalStart.CompareTo(otherStart) > 0 && intervalStart.CompareTo(otherEnd) < 0) ||
                    (intervalEnd.CompareTo(otherStart) > 0 && intervalEnd.CompareTo(otherEnd) < 0)
                select Tuple.Create(interval, otherInterval)).FirstOrDefault();
        }
    }
}