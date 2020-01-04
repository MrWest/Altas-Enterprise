using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data.Common
{
    public interface IAtlasModuleSubjectRepository:IAtlasModuleGenericSubjectRepository<IAtlasModuleSubject>
    {
        IAtlasModuleGenericSubject OwnerSubject { get; set; }
    }
}
