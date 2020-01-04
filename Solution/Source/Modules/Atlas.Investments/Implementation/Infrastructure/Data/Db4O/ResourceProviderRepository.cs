using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    public class ResourceProviderRepository: CodedNomenclatorRepositoryBase<IResourceProvider>, IResourceProviderRepository
    {
        public ResourceProviderRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}