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
    public class ModuleIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string assemblyFile = value as string;
                Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(a => !a.IsDynamic && a.CodeBase.ToLower() == assemblyFile.ToLower());


                if (assembly != null)
                {
                    AssemblyMetadataAttribute attr = (AssemblyMetadataAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyMetadataAttribute));

                    
                    return attr.Value;
                }

               
                return null;

            }
            catch (Exception e)
            {
                return "No Name";
            }
           
            //TODO 
            //if necessary ask for the module file location to be sure.

          
           
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
