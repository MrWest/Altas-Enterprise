using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Properties;
using Microsoft.Practices.Prism.Modularity;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
    public class AtlasUserDomainService : DomainServicesBase<IAtlasUser>, IAtlasUserDomainService
    {
        public IAtlasUser Create()
        {
            var user = base.Create();
            user.Name = Resources.New_AtlasUser;
            user.Description = Resources.AtlasUserDescription;
            user.Rol = AtlasUserRol.User;
           // user.AllowedModules = new List<ModuleInfo>();

            return user;
        }
    }

    public class AtlasModuleInfoDomainService : DomainServicesBase<IAtlasModuleInfo>, IAtlasModuleInfoDomainService
    {
        public IAtlasModuleInfo Create()
        {
            var moduleinfo = base.Create();

          //  moduleinfo.AtlasUser = AtlasUser;
            moduleinfo.AtlasUserId = AtlasUser.Id;
            return moduleinfo;
        }

        public IAtlasUser AtlasUser { get; set; }
    }
}
