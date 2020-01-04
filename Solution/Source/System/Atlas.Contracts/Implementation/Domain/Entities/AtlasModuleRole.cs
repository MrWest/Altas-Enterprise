using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
    public class AtlasModuleRole:EntityBase, IAtlasModuleRole
    {
        public AtlasModuleRole()
        {
            ModulePermission = AtlasModulePermission.Read;
        }
        public IAtlasGenericModuleAccess ModuleAccess { get; set; }
        public object AllowedEntity { get; set; }
        public AtlasModulePermission ModulePermission { get; set; }
    }
}
