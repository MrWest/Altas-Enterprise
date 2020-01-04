using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class AtlasForMainModuleAccessViewModel: AtlasGenericModuleAccessViewModel<IAtlasMainModuleAccess>, IAtlasForMainModuleAccessViewModel
    {
       public IEnumerable<IPresenter> Collection { get; set; }
        protected override IAtlasGenericModuleAccessPresenter<IAtlasMainModuleAccess> CreatePresenterFor(IAtlasModuleAccess item)
        {
            var presenter = base.CreatePresenterFor(item);
            presenter.OwnerModuleAccess = OwnerModuleAccess;           
            return presenter;
        }

    }
}
