using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Application;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
    public class CommonDomainService<TEntity> : DomainServicesBase<TEntity>, ICommonDomainService<TEntity>
        where TEntity : class,IEntity
    {
        public override TEntity Create()
        {
            var entity = base.Create();
            if (entity.GetType().Implements<INomenclator>())
            {
                var newEntity = entity as INomenclator;
                newEntity.Name = "Nuevo";

                return newEntity as TEntity;
            }
            return base.Create();
        }
    }
}
