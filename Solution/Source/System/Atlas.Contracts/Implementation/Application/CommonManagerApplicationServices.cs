using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;

namespace CompanyName.Atlas.Contracts.Implementation.Application
{
    public class CommonManagerApplicationServices<TEntity> : ItemManagerApplicationServicesBase<TEntity,ICommonRepository<TEntity> ,ICommonDomainService<TEntity>>, ICommonManagerApplicationServices<TEntity>
        where TEntity : class,IEntity
    {
    }
}
