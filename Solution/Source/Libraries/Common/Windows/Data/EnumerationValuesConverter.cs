using System.Globalization;

namespace System.Windows.Data
{
    /// <summary>
    /// Helper converter to be used in combo boxes where the items are extracted from the values of an enumeration.
    /// </summary>
    public class EnumerationValuesConverter : IValueConverter
    {
        /// <summary>
        /// Returns an enumeration's values names in a list.
        /// </summary>
        /// <returns>
        /// The list of strings conforming the values of an enumeration.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.GetValues(value.GetType());
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}