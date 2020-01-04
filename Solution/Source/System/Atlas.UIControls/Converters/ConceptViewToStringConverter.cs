using System;
using System;
using System.Globalization;
using System.Windows.Data;
using CompanyName.Atlas.UIControls.Properties;

namespace CompanyName.Atlas.UIControls.Converters
{
    /// <summary>
    /// This is a value converter used to transform budget component items view types to string but not viceversa.
    /// </summary>
    public struct ConceptViewToStringConverter : IValueConverter
    {
        /// <summary>
        /// Converts from budget component item view to string.
        /// </summary>
        /// <param name="value">The <see cref="BudgetComponentItemViewType"/> to convert to string.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>A <see cref="string"/> according to the provided value at the argument <paramref name="value"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!Equals(value,null)&&value.GetType() == typeof (ConceptView))
            {
                var viewType = (ConceptView)value;

                switch (viewType)
                {
                    case ConceptView.Definition:
                        return Resources.Definition;
                    case ConceptView.Example:
                        return Resources.Example;
                    case ConceptView.RelatedConcept:
                        return Resources.Related;
                    default:
                        return Resources.Definition;
                }
            }



            return "";
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <exception cref="NotSupportedException">Always throws this exception.</exception>
        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
