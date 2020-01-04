using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace CompanyName.Atlas.UIControls.Converters
{
    /// <summary>
    ///     This is a value converter allowing to take doubles values corresponding to several parameters and tranform them
    ///     into a column width.
    /// </summary>
    public struct TimelineColumnWidthConverter : IMultiValueConverter
    {
        /// <summary>
        ///     Convertes the doubles passed as values into a grid length.
        /// </summary>
        /// <param name="values">The <see cref="Array" /> containing the parameters.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>A <see cref="GridLength" /> instance being the calculated column width.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double width = Math.Round(values.Aggregate(1.0, (x, y) => x * (double)y));

            return new GridLength(width, GridUnitType.Pixel);
        }

        /// <summary>
        ///     Not supported.
        /// </summary>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}