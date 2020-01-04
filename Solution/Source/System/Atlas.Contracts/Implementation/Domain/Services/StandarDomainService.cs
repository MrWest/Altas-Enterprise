using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
    public class StandarDomainService<TEntity> : DomainServicesBase<TEntity>, IDomainServices<TEntity>
        where TEntity : class ,IEntity
    {
    }
}
