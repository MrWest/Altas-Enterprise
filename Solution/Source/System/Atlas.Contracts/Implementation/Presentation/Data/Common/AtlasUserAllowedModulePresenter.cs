using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class AtlasUserAllowedModulePresenter:BindableBase
    {
        public IAtlasUserPresenter UserPresenter { get; set; }

        private ModuleCatalog _moduleCatalog;
        private string _moduleName;
        public AtlasUserAllowedModulePresenter(string moduleName)
        {
            _moduleName = moduleName;

        }
        public AtlasUserAllowedModulePresenter(string moduleName, ModuleCatalog moduleCatalog)
        {
            _moduleName = moduleName;
            _moduleCatalog = moduleCatalog;
        }
        public string ModuleName
        {
            get { return _moduleName; }
        }

        public bool Allowed
        {
            get
            {
                
                return (UserPresenter!=null&&_moduleCatalog!=null&& (UserPresenter.AllowedModules.Items.Any(x=>x.ModuleName==ModuleName)));
            }

            set
            {
                if (value && !Allowed)
                {
                    //var final = new List<AtlasModuleInfo>();
                    //foreach (AtlasModuleInfo allowedModule in UserPresenter.AllowedModules)
                    //{
                    //    final.Add(allowedModule);
                    //}

                    var module = _moduleCatalog.Modules.Single(x => x.ModuleName == ModuleName);
                    var atlasmodule = new AtlasModuleInfo()
                    {
                        ModuleType = module.ModuleType,
                        ModuleName = module.ModuleName,
                        Ref = module.Ref,
                      //  DependsOn = module.DependsOn,
                        InitializationMode = module.InitializationMode,
                        State = module.State,
                        AtlasUserId = UserPresenter.Id

                    };

                    UserPresenter.AllowedModules.AddFromScratch(atlasmodule);
                   // final.Add(atlasmodule);
                   //// final.Add(_moduleCatalog.Modules.Single(x=>x.ModuleName == ModuleName));
                   // UserPresenter.AllowedModules = final;
                }
                else if (!value && Allowed)
                {
                    var final = new List<AtlasModuleInfo>();
                    foreach (AtlasModuleInfo allowedModule in UserPresenter.AllowedModules)
                    {
                        final.Add(allowedModule);
                    }

                    UserPresenter.AllowedModules.Delete(UserPresenter.AllowedModules.Items.Single(x=> x.ModuleName == ModuleName));

                    //final.Remove(final.Single(x => x.ModuleName == ModuleName));
                    //UserPresenter.AllowedModules = final;
                }
               
                OnPropertyChanged(() => Allowed);
            }
        }
    }
}