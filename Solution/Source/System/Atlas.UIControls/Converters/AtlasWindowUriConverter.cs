using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace CompanyName.Atlas.UIControls.Converters
{
    /// <summary>
    ///     Represents a converter that extracts the uri formed by the selected navigation path in an
    ///     <see cref="AtlasWindow" /> instance, the path is formed by whatever section and category the user has selected.
    /// </summary>
    public class AtlasWindowUriConverter : IMultiValueConverter
    {
        /// <summary>
        ///     Converts the given pieces into an Uri.
        /// </summary>
        /// <param name="values">
        ///     The values from the visual components being the navigation levels which combination represents the final
        ///     path.
        /// </param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>
        ///     A <see cref="string" /> representing the final path (Uri) the user has selected os far; or an empty string
        ///     if the user is at the root of the navigation map of the <see cref="AtlasWindow" />.
        /// </returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null && !values.Any())
                return string.Empty;

            var firstNavItem = (values[0] as FirstLevelNavItem);
            SecondLevelNavItem secondNavItem = values.Length >= 2 ? (values[1] as SecondLevelNavItem) : null;

            string firstNavItemText = firstNavItem != null ? firstNavItem.Text : string.Empty;
            string secondNavItemText = secondNavItem != null ? secondNavItem.Text : string.Empty;

            return string.Join("/", firstNavItemText, secondNavItemText);
        }

        /// <summary>
        ///     Not supported.
        /// </summary>
        /// <exception cref="NotSupportedException">Always throw this exception.</exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}