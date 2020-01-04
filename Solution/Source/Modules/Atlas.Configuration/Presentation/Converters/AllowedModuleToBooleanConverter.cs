using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Configuration.Presentation.Converters
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
            var module = (string)value;
            var selector = parameter as Selector;

            var user = selector.SelectedItem as IAtlasUserPresenter;

            if (module == null || user == null)
                return false;

            
           
        //    user.AllowedModules = GetValue(module, user.AllowedModules, user);
            //TODO 
            //if necessary ask for the module file location to be sure.


            return module;//user.AllowedModules.Any(x => x.ModuleName == module.ModuleName);

            ////ModuleInfo moduleInfo;
            ////IList<ModuleInfo> allowed;
            ////var final = GetValue(out moduleInfo, out allowed);
        }

        private static List<AtlasModuleInfo> GetValue( string moduleInfo, IAtlasModuleInfoViewModel allowed, IAtlasUserPresenter user)
        {
          
            var final = new List<AtlasModuleInfo>();
            var moduleCatalog = (ModuleCatalog)ServiceLocator.Current.GetInstance(typeof(ModuleCatalog));


            if ((moduleInfo != null && allowed != null))
            {
                foreach (AtlasModuleInfo allowedModule in allowed)
                {
                    final.Add(allowedModule);
                }
                if (allowed.All(m => m.ModuleName != moduleInfo))
                {
                    var module = moduleCatalog.Modules.Single(x => x.ModuleName == moduleInfo);
                    var atlasmodule = new AtlasModuleInfo()
                    {
                        ModuleType = module.ModuleType,
                        ModuleName = module.ModuleName,
                        Ref =  module.Ref,
                       // DependsOn = module.DependsOn,
                        InitializationMode = module.InitializationMode,
                        State = module.State,
                        AtlasUserId = user.Id
                    };

                    final.Add(atlasmodule);
                }
                   
            }
            return final;
        }
    }
}
