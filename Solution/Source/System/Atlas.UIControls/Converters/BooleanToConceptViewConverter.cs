using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CompanyName.Atlas.UIControls.Converters
{
   public class BooleanToConceptViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (ConceptView)value;
            var viewType = (ConceptView)parameter;

            return boolValue == viewType;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool?)value;
            var viewType = (ConceptView)parameter;

            if (boolValue.HasValue && boolValue.Value)
                return viewType;

            return default(ConceptView);
        }
    }
}
