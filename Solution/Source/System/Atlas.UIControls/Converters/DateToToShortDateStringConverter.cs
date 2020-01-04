using System;
using System.Globalization;
using System.Windows.Data;

namespace CompanyName.Atlas.UIControls.Converters
{
    public class DateToToShortDateStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            return date.ToShortDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDateTime(value);
        }
    }
}
