using System;
using System.Globalization;
using System.Windows.Data;
using CompanyName.Atlas.Investments.Presentation.Views.ViewTypes;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Presentation.Views.Converters
{
    /// <summary>
    /// This is a value converter used to transform budget component items view types to string but not viceversa.
    /// </summary>
    public struct BudgetComponentItemViewToStringConverter : IValueConverter
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



            if (!Equals(value,null)&&value.GetType() == typeof (BudgetComponentItemViewType))
            {
              
                var viewType = (BudgetComponentItemViewType)value;

                switch (viewType)
                {
                    case BudgetComponentItemViewType.PlannedItems:
                        return Resources.Planned;
                    case BudgetComponentItemViewType.ExecutedItems:
                        return Resources.Executed;
                    case BudgetComponentItemViewType.Both:
                        return Resources.Both;
                    default:
                        return Resources.Planned;
                }
            }

            if (!Equals(value, null) && value.GetType() == typeof(PSDossificatiorViewType))
            {
                var viewType = (PSDossificatiorViewType)value;

                switch (viewType)
                {
                    case PSDossificatiorViewType.PriceSystem:
                        return Resources.PriceSystem;
                    case PSDossificatiorViewType.Dossificator:
                        return Resources.Dossificator;
                    case PSDossificatiorViewType.VariantLines:
                        return Resources.VariantLines;
                    case PSDossificatiorViewType.ConstructionComponenetPlanning:
                        return Resources.ConstructionComponent;
                    default:
                        return "";
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
