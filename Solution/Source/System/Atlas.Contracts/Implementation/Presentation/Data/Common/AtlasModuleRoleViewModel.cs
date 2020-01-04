using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class AtlasModuleRoleViewModel : CrudViewModelBase<IAtlasModuleRole, IAtlasModuleRolePresenter, IAtlasModuleRoleManagerApplicationServices>, IAtlasModuleRoleViewModel
          
    {
       public IAtlasGenericModuleAccessPresenter OwnerModuleAccess { get; set; }

        protected override IAtlasModuleRolePresenter CreatePresenterFor(IAtlasModuleRole item)
        {
            var presenter = base.CreatePresenterFor(item);
            presenter.OwnerModuleAccess = OwnerModuleAccess;
            return presenter;
        }

        protected override IAtlasModuleRoleManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.ModuleAccess = (OwnerModuleAccess as IPresenter)?.Object as IAtlasGenericModuleAccess;
            return service;
        }
    }
}
