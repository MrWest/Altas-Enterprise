using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.Prism.Modularity;

namespace CompanyName.Atlas.UIControls.Converters
{
    public class AllowedModuleToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var module = (string) value;
            var selector = parameter as Selector;

            var user = selector.SelectedItem as IAtlasUserPresenter;
            
            if (module == null || user == null)
                return false;

            //TODO 
            //if necessary ask for the module file location to be sure.

          
            return user.AllowedModules.Any(x => x.ModuleName == module);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
            //var allowed = (bool)value ;
            //var user = parameter as IAtlasUser;

            //if(allowed&&)
            ////TODO 
            ////if necessary ask for the module file location to be sure.
            //return user.AllowedModules.Any(x => x.ModuleName == module.ModuleName);
        }
    }
}
