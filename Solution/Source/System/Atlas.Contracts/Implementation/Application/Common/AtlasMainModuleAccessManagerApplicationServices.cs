using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Application.Common
{
    public class AtlasMainModuleAccessManagerApplicationServices: AtlasGenericModuleAccessManagerApplicationServices<IAtlasMainModuleAccess, IAtlasMainModuleAccessRepository>, IAtlasMainModuleAccessManagerApplicationServices
    {
        public string AssemblyName { get; set; }

        protected override IAtlasMainModuleAccessRepository Repository
        {
            get
            {
                var repo = base.Repository;
                repo.AssemblyName = AssemblyName;
                return repo;
            }
        }

      
    }
}
