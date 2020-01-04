using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.Practices.Prism.Modularity;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
   public class AtlasMainModuleAccess : AtlasGenericModuleAccess, IAtlasMainModuleAccess
    {
        public String AssemblyName { get; set; }
    }
}
