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
   public class AtlasModuleAccessViewModel: NavigableViewModel<IAtlasModuleAccess, IAtlasModuleAccessPresenter, IAtlasModuleAccessManagerApplicationServices>, IAtlasModuleAccessViewModel
    {
       public IAtlasGenericModuleAccessPresenter OwnerModuleAccess { get; set; }


        protected override IAtlasModuleAccessPresenter CreatePresenterFor(IAtlasModuleAccess item)
        {
            var presenter = base.CreatePresenterFor(item);
            presenter.OwnerModuleAccess = OwnerModuleAccess;
            return presenter;
        }

        protected override IAtlasModuleAccessManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            service.OwnerModuleAccess = (OwnerModuleAccess as IPresenter)?.Object as IAtlasGenericModuleAccess;
            return service;
        }

        protected override INavigable Parent { get { return OwnerModuleAccess; } }
    }
}
