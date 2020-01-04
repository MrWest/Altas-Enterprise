using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    /// <summary>
    /// wraps <see cref="IAtlasUser" /> for presentation
    /// </summary>
    public class AtlasUserPresenter:NomenclatorPresenterBase<IAtlasUser,IAtlasUserMangerApplicationService>,IAtlasUserPresenter
    {
        //private AtlasUserRol _rol;
        //private IList<ModuleInfo> _allowedModules;

        public string Password
        {
            get
            {
                return Object.Password;
            }
            set
            {
                SetProperty(v => Object.Password = v, value);
                OnPropertyChanged(() => Password);
            }
        }
        public AtlasUserRol Rol
        {
            get
            {
                return Object.Rol;
            }
            set
            {
                SetProperty(v => Object.Rol = v, value);
                OnPropertyChanged(()=>Rol);
            }
        }

        private IAtlasModuleInfoViewModel _allowedModules;
        public IAtlasModuleInfoViewModel AllowedModules
        {
            get
            {
                if (_allowedModules == null)
                {
                    _allowedModules = ServiceLocator.Current.GetInstance<IAtlasModuleInfoViewModel>();
                    _allowedModules.AtlasUser = this;
                    _allowedModules.Load();
                }

                return _allowedModules;
            }

        }
        private ModuleCatalog _moduleCatalog = (ModuleCatalog)ServiceLocator.Current.GetInstance(typeof(ModuleCatalog));

        public IList<AtlasUserAllowedModulePresenter> AtlasModuleList
        {
            get
            {
                var list = new List<AtlasUserAllowedModulePresenter>();

                foreach (ModuleInfo moduleInfo in _moduleCatalog.Modules)
                {
                    list.Add(new AtlasUserAllowedModulePresenter(moduleInfo.ModuleName,_moduleCatalog) {UserPresenter = this});
                   
                }

                return list;
               
            }
          
        }
    }



    public class AtlasModuleInfoPresenter :
        EntityPresenterBase<IAtlasModuleInfo, IAtlasModuleInfoMangerApplicationService>, IAtlasModuleInfoPresenter
    {
        public IAtlasUserPresenter AtlasUser { get; set; }
        public string ModuleName
        {
            get
            {
                return Object.ModuleName;
            }
            set
            {
                SetProperty(v => Object.ModuleName = v, value);
                OnPropertyChanged(() => ModuleName);
            }
        }
        public string ModuleType
        {
            get
            {
                return Object.ModuleType;
            }
            set
            {
                SetProperty(v => Object.ModuleType = v, value);
                OnPropertyChanged(() => ModuleType);
            }
        }
        public string Ref
        {
            get
            {
                return Object.Ref;
            }
            set
            {
                SetProperty(v => Object.Ref = v, value);
                OnPropertyChanged(() => Ref);
            }
        }
        public ModuleState State
        {
            get
            {
                return Object.State;
            }
            set
            {
                SetProperty(v => Object.State = v, value);
                OnPropertyChanged(() => State);
            }
        }

        protected override IAtlasModuleInfoMangerApplicationService CreateServices()
        {
            var service = base.CreateServices();
            service.AtlasUser = AtlasUser.Object;
            return service;
        }
    }

}
