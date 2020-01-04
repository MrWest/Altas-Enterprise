using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Presentation.Views.Converters
{
    /// <summary>
    ///     Represents a converter tranforming <see cref="ExpenseConceptType" /> values into strings and viceversa.
    /// </summary>
    public class ExpenseConceptTypeToStringConverter : IValueConverter
    {
        private readonly IDictionary<string, ExpenseConceptType> _reverseTranslationDictionary = new Dictionary<string, ExpenseConceptType>
        {
            { Resources.Direct, ExpenseConceptType.Direct },
            { Resources.Indirect, ExpenseConceptType.Indirect },
        };

        private readonly IDictionary<ExpenseConceptType, string> _translationDictionary = new Dictionary<ExpenseConceptType, string>
        {
            { ExpenseConceptType.Direct, Resources.Direct },
            { ExpenseConceptType.Indirect, Resources.Indirect },
        };


        /// <summary>
        ///     Converts a <see cref="ExpenseConceptType" /> value into a string.
        /// </summary>
        /// <returns>
        ///     The string equivalent of the given <see cref="ExpenseConceptType" /> in the argument <paramref name="value" />.
        /// </returns>
        /// <param name="value">The <see cref="ExpenseConceptType" /> value produced by the binding source.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var expenseConceptType = (ExpenseConceptType)value;
                return _translationDictionary[expenseConceptType];
            }
            catch (InvalidCastException)
            {
            }
            
            IEnumerable<ExpenseConceptType> expenseConceptTypes = ((Array)value).Cast<ExpenseConceptType>();
            return expenseConceptTypes.Aggregate(new List<string>(), (list, ectype) =>
            {
                list.Add(_translationDictionary[ectype]);
                return list;
            });
        }

        /// <summary>
        ///     Converts a <see cref="string" /> value into a <see cref="ExpenseConceptType" />.
        /// </summary>
        /// <returns>
        ///     A <see cref="ExpenseConceptType" /> converted value.
        /// </returns>
        /// <param name="value">The <see cref="string" /> value that is produced by the binding target.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Not used.</param>
        /// <param name="culture">Not used.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var stringValue = (string)value;
                return _reverseTranslationDictionary[stringValue];
            }
            catch (InvalidCastException)
            {
                throw new NotSupportedException();
            }
        }
    }
}