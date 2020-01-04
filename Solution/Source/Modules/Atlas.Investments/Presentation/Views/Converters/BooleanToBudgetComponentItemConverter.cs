using System;
using System.Globalization;
using System.Windows.Data;
using CompanyName.Atlas.Investments.Presentation.Views.ViewTypes;

namespace CompanyName.Atlas.Investments.Presentation.Views.Converters
{
    /// <summary>
    /// A value converter to transform boolean values to budget component item view type ones.
    /// </summary>
    public class BooleanToBudgetComponentItemConverter : IValueConverter
    {
        /// <summary>
        /// Converts a budget component item view type to a boolean value one.
        /// </summary>
        /// <param name="value">The <see cref="BudgetComponentItemViewType"/> to convert to boolean.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">The <see cref="BudgetComponentItemViewType"/> to used in the convertion.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>
        /// A <see cref="Boolean"/> value saying whether the provided value at <paramref name="value"/> is equals to
        /// <paramref name="parameter"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (BudgetComponentItemViewType)value;
            var viewType = (BudgetComponentItemViewType)parameter;

            return boolValue == viewType;
        }

        /// <summary>
        /// Converts back a boolean value to a budget component item view type one.
        /// </summary>
        /// <param name="value">
        /// The <see cref="Nullable{T}"/> of <see cref="Boolean"/> to convert to <see cref="BudgetComponentItemViewType"/>.
        /// </param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">The <see cref="BudgetComponentItemViewType"/> used in the conversion.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>A <see cref="BudgetComponentItemViewType"/> converted from <paramref name="value"/>.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool?)value;
            var viewType = (BudgetComponentItemViewType)parameter;

            if (boolValue.HasValue && boolValue.Value)
                return viewType;

            return default(BudgetComponentItemViewType);
        }
    }
}
