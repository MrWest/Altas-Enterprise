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
    public class AtlasGenericModuleAccessDomainServices<TEntity>: DomainServicesBase<TEntity>, IAtlasGenericModuleAccessDomainServices<TEntity>
        where TEntity : class, IAtlasGenericModuleAccess
    {
        public override TEntity Create()
        {
            var entity = base.Create();
            entity.Name = Resources.NewEmptyAccess;
            return entity;
        }
    }
}
