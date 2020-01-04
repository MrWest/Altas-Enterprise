using System.ComponentModel;
using System.Enumable;

namespace System
{
    /// <summary>
    ///     Represents the different datetime scale.
    /// </summary>

     [TypeConverter(typeof(LocalizedEnumConverter))]
    public enum DateTimeScale
    {
        /// <summary>
        ///     This is a year date time scale.
        /// </summary>
         Yearly,

        /// <summary>
        ///     This is a month date time scale.
        /// </summary>
         Monthly,

        /// <summary>
        ///     This is a week date time scale.
        /// </summary>
        Weekly,
        /// <summary>
        ///     This is a day date time scale.
        /// </summary>
        Daily
    }

   
}