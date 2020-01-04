using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface IAtlasModuleRolePresenter :IPresenter<IAtlasModuleRole>
        
    {
        IAtlasGenericModuleAccessPresenter OwnerModuleAccess { get; set; }
        //String Name { get; set; }
        AtlasModulePermission Permission { get; set; }
        object AllowedEntity { get; set; }
    }
}
