using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace System
{
    /// <summary>
    ///     Utilities container class. The utilities here are used when working with dates and elements related to it, like
    ///     weeks, months, etc.
    /// </summary>
    public static class DateTimeUtilities
    {
        /// <summary>
        ///     Gets all the names already localized of the year.
        /// </summary>
        public static IEnumerable<string> MonthNames
        {
            get
            {
                DateTimeFormatInfo monthFormat = DateTimeFormatInfo.CurrentInfo ?? DateTimeFormatInfo.InvariantInfo;
                return monthFormat.MonthNames
                    .Take(12)
                    .Aggregate(new List<string>(), (list, month) =>
                    {
                        list.Add(month.Capitalize());
                        return list;
                    });
            }
        }
    }
}