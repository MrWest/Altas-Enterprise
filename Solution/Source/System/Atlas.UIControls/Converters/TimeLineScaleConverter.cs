using System;
using System.Globalization;
using System.Windows.Data;

namespace CompanyName.Atlas.UIControls.Converters
{
    public class TimeLineScaleConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           
            switch ((DateTimeScale)value)
            {
                case DateTimeScale.Yearly:
                    return DateTimeScale.Daily;
                case  DateTimeScale.Monthly:
                    return DateTimeScale.Daily;
                default:
                    return (DateTimeScale) value;

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTimeScale.Yearly;
        }
    }
}
