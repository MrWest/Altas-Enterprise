using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Contracts.Domain
{
    public interface IAtlasUserDomainService:IDomainServices<IAtlasUser>
    {
    }

    public interface IAtlasModuleInfoDomainService : IDomainServices<IAtlasModuleInfo>
    {
        IAtlasUser AtlasUser { get; set; }
    }
}
