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
    public class AtlasUserMangerApplicationService : ItemManagerApplicationServicesBase<IAtlasUser, IAtlasUserRepository, IAtlasUserDomainService>, IAtlasUserMangerApplicationService
    {
    }

    public class AtlasModuleInfoMangerApplicationService : ItemManagerApplicationServicesBase<IAtlasModuleInfo, IAtlasModuleInfoRepository, IAtlasModuleInfoDomainService>, IAtlasModuleInfoMangerApplicationService
    {
        public IAtlasUser AtlasUser { get; set; }

        protected override IAtlasModuleInfoDomainService DomainServices
        {
            get
            {
                var domains = base.DomainServices;
                domains.AtlasUser = AtlasUser;
                return domains;
            }
        }

        protected override IAtlasModuleInfoRepository Repository
        {
            get
            {
                var repo = base.Repository;
                repo.AtlasUser = AtlasUser;

                return repo;

            }
        }
    }

    
}
