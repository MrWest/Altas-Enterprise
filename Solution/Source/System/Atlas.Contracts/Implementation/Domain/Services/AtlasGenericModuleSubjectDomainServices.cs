using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
    public class AtlasModuleGenericSubjectDomainServices<TEntity> : DomainServicesBase<TEntity>, IAtlasModuleGenericSubjectDomainServices<TEntity>
        where TEntity: class ,IAtlasModuleGenericSubject
    {
        public override TEntity Create()
        {
               var entity =  base.Create();
               entity.Name = Resources.NewSubject;
               entity.Description = Resources.NewSubjectDescription;
            return entity;
        }
    }
}
