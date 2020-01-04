using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Application.Common
{
    public class AtlasGenericModuleAccessManagerApplicationServices<TEntity,TRepository>: ItemManagerApplicationServicesBase<TEntity, TRepository,IAtlasGenericModuleAccessDomainServices<TEntity>>, IAtlasGenericModuleAccessManagerApplicationServices<TEntity>
        where TEntity : class, IAtlasGenericModuleAccess
         where TRepository : class, IAtlasGenericModuleAccessRepository<TEntity>
    {

    }
}
