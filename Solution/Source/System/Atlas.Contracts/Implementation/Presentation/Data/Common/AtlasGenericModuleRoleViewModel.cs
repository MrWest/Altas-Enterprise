using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class AtlasGenericModuleRoleViewModel<TEntity> : CrudViewModelBase<IAtlasModuleRole, IAtlasModuleRolePresenter<TEntity>, IAtlasModuleRoleManagerApplicationServices>, IAtlasGenericModuleRoleViewModel<TEntity>
         where TEntity : class, IAtlasModuleAccess
    {
       public IAtlasCommonModuleAccessPresenter<TEntity> OwnerModuleAccess { get; set; }

       
        protected override IAtlasModuleRolePresenter<TEntity> CreatePresenterFor(IAtlasModuleRole item)
        {
            var presenter = base.CreatePresenterFor(item);
            presenter.OwnerModuleAccess = OwnerModuleAccess;
            
            return presenter;
        }

        protected override IAtlasModuleRoleManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.ModuleAccess = OwnerModuleAccess.Object;
            return service;
        }
    }
}
