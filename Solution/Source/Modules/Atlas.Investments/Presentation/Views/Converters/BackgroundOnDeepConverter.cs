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
    public class BackgroundOnDeepConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var navigable  = value as INavigable;

            if (navigable != null)
            {
                var brush = parameter as SolidColorBrush;
                Color color =new Color();
                if (brush != null)
                {
                    color = brush.Color;

                    //color.R = (color.R - System.Convert.ToByte(navigable.Depth * 10)) > System.Convert.ToByte(25) ? (color.R - System.Convert.ToByte(navigable.Depth * 10)):
                  //  var newColor = Color.FromRgb(10, 10, 10);

                    for (int i = 0; i < navigable.Depth; i++)
                    {


                        if (color.R < 15 && color.G < 15 && color.B < 15)
                            break;
                        if(color.R>=15)
                        color.R -= 15;
                        if (color.G >= 15)
                            color.G -= 15;
                        if (color.B >= 15)
                            color.B -= 15;
                    }

                  //  brush.Color = color;
                    return new SolidColorBrush(color);
                }
                   
                //  System.Convert.ToByte(navigable.Depth * 10);

               //new Thickness(10 * (int) value, 0, 0, 0);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
