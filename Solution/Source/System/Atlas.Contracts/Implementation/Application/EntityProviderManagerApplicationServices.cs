using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;

namespace CompanyName.Atlas.Contracts.Implementation.Application
{
    public class EntityProviderManagerApplicationServices<TEntity> : ItemManagerApplicationServicesBase<TEntity, IRepository<TEntity>, IDomainServices<TEntity>>, IEntityProviderManagerApplicationServices<TEntity>
         where TEntity : class ,IEntity
    {
        
        public TEntity GetEntity(object Id)
        {
            
            return Id!=null?Repository.Entities.SingleOrDefault(x =>!Equals(x.Id,null) && x.Id.ToString() == Id.ToString()):null;
        }
    }
}
