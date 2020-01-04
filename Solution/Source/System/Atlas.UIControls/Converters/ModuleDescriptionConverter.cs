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
using System.Reflection;
using System.IO;

namespace CompanyName.Atlas.UIControls.Converters
{
    public class ModuleDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string assemblyFile = value as string;


                Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(a => !a.IsDynamic && a.CodeBase == assemblyFile);


                if (assembly != null)
                {
                    AssemblyDescriptionAttribute attr = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyDescriptionAttribute));

                    return attr.Description;
                }


              
                return null;

            }
            catch (Exception e)
            {
                return null;
            }

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
