using System.Globalization;

namespace System.Windows.Data
{
    /// <summary>
    /// Converts from an Error message to a boolean value.
    /// </summary>
    public class ErrorMessageToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Converts a Error message to a boolean value.
        /// </summary>
        /// <returns>
        /// True if the if the error message is null; otherwise false.
        /// </returns>
        /// <param name="value">An Error message (or null).</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value as string;
            return stringValue == null;
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="value">Not used.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <exception cref="System.NotSupportedException">Always throws this exception.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}