using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Investments.Presentation.Views.Converters
{
    /// <summary>
    /// Convert a color according to INavigable.Deep
    /// </summary>
    public class AnnoyingWidthAjustConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            


            try
            {
                if (parameter != null)
                    return (double) value;

                return (double) value - System.Convert.ToDouble(parameter?.ToString()) ;
            }
            catch (Exception e)
            {
                return value;
            }
                //  System.Convert.ToByte(navigable.Depth * 10);

               //new Thickness(10 * (int) value, 0, 0, 0);
           
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
