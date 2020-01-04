using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Linq;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class AtlasModuleRolePresenter: EntityPresenterBase<IAtlasModuleRole, IAtlasModuleRoleManagerApplicationServices>, IAtlasModuleRolePresenter
        
    {
       public IAtlasGenericModuleAccessPresenter OwnerModuleAccess { get; set; }
        
        public object AllowedEntity
        {
            get
            {
                if(OwnerModuleAccess.Collection!=null&&Object.AllowedEntity!=null)
                {
                    var entity= OwnerModuleAccess.Collection.SingleOrDefault(x => ((IEntity)x.Object).Id.ToString()==(Object.AllowedEntity.ToString()));

                    var nomenclator = (INomenclator)entity;
                    if (nomenclator != null) return nomenclator.Name;
                }
                return null;
            }
            set
            {
                var presenter = value as IPresenter;
                if(presenter!=null)
                {
                    var newvalue = presenter.Object as IEntity;

                    if(newvalue!=null)
                    {
                        SetProperty(v => Object.AllowedEntity = v, newvalue.Id);
                        OnPropertyChanged(() => AllowedEntity);
                    }
                }
                
               
              
            }
        }
        //public string Name
        //{
        //    get
        //    {
        //        var service = ServiceLocator.Current.GetInstance<IEntityProviderManagerApplicationServices<INomenclator>>();
        //        var entity = service.GetEntity(Object.AllowedEntity);
        //        return entity?.Name;
        //    }
        //    set
        //    {
        //        OnPropertyChanged(() => Name);
        //    }
        //}
        public AtlasModulePermission Permission
        {
            get { return Object.ModulePermission; }
            set
            {
                SetProperty(v => Object.ModulePermission = v, value); 
                OnPropertyChanged(()=>Permission);
            }
        }

        protected override IAtlasModuleRoleManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.ModuleAccess = (OwnerModuleAccess as IPresenter)?.Object as IAtlasGenericModuleAccess;
            return service;
        }
    }
}

