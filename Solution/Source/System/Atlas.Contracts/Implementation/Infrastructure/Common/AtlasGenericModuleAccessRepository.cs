using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    public abstract class  AtlasGenericModuleAccessRepository<TEntity>: Db4ORepositoryBase<TEntity>, IAtlasGenericModuleAccessRepository<TEntity>
        where TEntity : class, IAtlasGenericModuleAccess
    {
        public AtlasGenericModuleAccessRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                     GetName(x => x.User), GetName(x => x.Name)
                }).ToArray();
            }
        }
    }
}
