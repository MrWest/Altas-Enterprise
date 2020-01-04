using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Application.Common
{
   public class AtlasModuleAccessManagerApplicationServices: AtlasGenericModuleAccessManagerApplicationServices<IAtlasModuleAccess,IAtlasModuleAccessRepository>, IAtlasModuleAccessManagerApplicationServices
    {
        public IAtlasGenericModuleAccess OwnerModuleAccess { get; set; }
        protected override IAtlasModuleAccessRepository Repository
        {
            get
            {
                var repo = base.Repository;
                repo.OwnerModuleAccess = OwnerModuleAccess;
                return repo;
            }
        }
    }
}
