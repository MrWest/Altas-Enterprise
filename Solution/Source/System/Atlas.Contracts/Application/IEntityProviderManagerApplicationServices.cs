using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Application
{
    public interface IEntityProviderManagerApplicationServices<TEntity>:IItemManagerApplicationServices<TEntity>
        where TEntity:class ,IEntity
    {
        TEntity GetEntity(object Id);
    }
}
