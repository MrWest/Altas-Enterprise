using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.Prism.Modularity;

namespace CompanyName.Atlas.Configuration.Presentation.Converters
{
    public class UserAllowedModulesConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var moduleInfo = (ModuleInfo) parameter;
            var allowed = (IList<ModuleInfo>) parameter;
            return (moduleInfo!=null&& allowed != null) && allowed.Any(m => m.ModuleName == moduleInfo.ModuleName);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var moduleInfo = (ModuleInfo)parameter;
            var allowed = (IList<ModuleInfo>)parameter;
            var final = new List<ModuleInfo>();
            if ((moduleInfo != null && allowed != null))
            {

                foreach (ModuleInfo allowedModule in allowed)
                {
                    final.Add(allowedModule);
                }
                if(allowed.All(m => m.ModuleName != moduleInfo.ModuleName))
                final.Add(moduleInfo);

            }
            return final;
        }
    }
}