using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
    public class AtlasModuleSubjectDomainServices : AtlasModuleGenericSubjectDomainServices<IAtlasModuleSubject>, IAtlasModuleSubjectDomainServices
    {
        public override IAtlasModuleSubject Create()
        {
            var entity = base.Create();
            entity.OwnerSubject = OwnerSubject;
            return entity;
        }

        public IAtlasModuleGenericSubject OwnerSubject { get; set; }
    }
}
