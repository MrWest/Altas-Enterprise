namespace System
{
    /// <summary>
    ///     Contains extensions and helpers to aid the work with datetime.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        ///     Determines the month delta there is between two dates.
        /// </summary>
        /// <param name="from">The datetime to calculate the delta from.</param>
        /// <param name="to">The datetime to calculate the delta to.</param>
        /// <returns>
        ///     The quantity of months there are from <paramref name="from" /> to the datetime given at <paramref name="to" />.
        /// </returns>
        public static int GetMonthDelta(this DateTime from, DateTime to)
        {
            if (to < from)
                throw new ArgumentException("to");

            int fromYear = from.Year, toYear = to.Year, toMonth = to.Month,
                fromDelta = (fromYear < toYear ? 12 : toMonth) - from.Month,
                toDelta = to.Month;

            return (fromYear < toYear)
                ? (toYear - (fromYear + 1)) * 12 + fromDelta + toDelta
                : fromDelta;
        }
    }
}