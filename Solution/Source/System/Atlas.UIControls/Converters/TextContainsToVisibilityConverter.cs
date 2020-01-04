using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.UIControls.Converters
{
    /// <summary>
    /// converts a combobox selected index value from an string Id
    /// </summary>
    public class TextContainsToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var text = (string) value;

            return text != null && parameter != null && ((TextBlock)parameter).Text.Contains(text)
                ? Visibility.Visible
                : Visibility.Collapsed;

         
            //  return value == null ? null : (value as IEntity).Id;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            //var main = ServiceLocator.Current.GetInstance(parameter as Type, null);

            //(main as ICrudViewModel).Load();

            //int index = System.Convert.ToInt32(value);

            //IEntity test = null;
            //if (index >= 0)
            //    test = (main as ICrudViewModel).Items[index] as IEntity;



            //return test == null ? null : test.Id;

            return null;
        }
    }
}
