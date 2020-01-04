using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    public interface IAtlasModuleRole:IEntity
    {
        
        IAtlasGenericModuleAccess ModuleAccess { get; set; }
        object AllowedEntity { get; set; }
        AtlasModulePermission ModulePermission { get; set; }
    }
}
