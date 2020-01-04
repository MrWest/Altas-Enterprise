using System;
using System.Globalization;
using System.Windows.Data;

namespace CompanyName.Atlas.UIControls.Converters
{
    /// <summary>
    ///     This a converter that allows to take a <see cref="DateTimeScale" /> value and transform it into a nulleable
    ///     boolean.
    /// </summary>
    public class IsCheckedToDateTimeScaleConverter : IValueConverter
    {
        /// <summary>
        ///     Converts a date time scale value into a boolean, according to a provided expected date time scale value.
        /// </summary>
        /// <param name="value">The <see cref="DateTimeScale" /> value to convert.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">
        ///     The <see cref="DateTimeScale" /> value that this method is expected to return true if
        ///     <paramref name="value" /> is equals to it.
        /// </param>
        /// <param name="culture">Not used.</param>
        /// <returns>True if <paramref name="value" /> and <paramref name="parameter" /> are equals, false otherwise.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var scale = (DateTimeScale) value;
            var expectedScale = (DateTimeScale) parameter;

            return scale == expectedScale;
        }

        /// <summary>
        ///     Converts a boolean value into a date time scale one, according to a provided expected date time scale value.
        /// </summary>
        /// <param name="value">The <see cref="bool" /> value to convert.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">
        ///     The <see cref="DateTimeScale" /> value that this method is expected to return if
        ///     <paramref name="value" /> is true.
        /// </param>
        /// <param name="culture">Not used.</param>
        /// <returns>A <see cref="DateTimeScale" /> equals to <paramref name="parameter" /> if <paramref name="value" /> is true.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var @checked = (bool) value;
            var expectedScale = (DateTimeScale) parameter;

            return @checked
                ? expectedScale
                : (expectedScale == default(DateTimeScale) ? DateTimeScale.Weekly : default(DateTimeScale));
        }
    }
}