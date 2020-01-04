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

namespace CompanyName.Atlas.UIControls.Converters
{
    /// <summary>
    /// Convert a color according to INavigable.Deep
    /// </summary>
    public class BackgroundOnDeepConverterReverse : IValueConverter
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

                    //    int depth = GetNewDepth(navigable);
                    int depth = navigable.Depth;
                    for (int i = 0; i < depth; i++)
                    {


                        if (color.R > 247 && color.G > 247 && color.B > 247)
                            break;
                        if (color.R <= 247)
                            color.R += 8;
                        if (color.G <= 247)
                            color.G += 8;
                        if (color.B <= 247)
                            color.B += 8;
                    }

                  //  brush.Color = color;
                    return new SolidColorBrush(color);
                }
                   
                //  System.Convert.ToByte(navigable.Depth * 10);

               //new Thickness(10 * (int) value, 0, 0, 0);
            }

            return value;
        }

        private int GetNewDepth(INavigable navigable)
        {
            if (Equals(navigable.Parent, null))
                return 0;
            if (navigable.Parent.Kind == navigable.Kind)
                return GetNewDepth(navigable.Parent)+1;
            return 0;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
