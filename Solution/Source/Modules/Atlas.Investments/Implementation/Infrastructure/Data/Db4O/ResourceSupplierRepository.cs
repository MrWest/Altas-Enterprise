using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    public class ResourceSupplierRepository: CodedNomenclatorRepositoryBase<IResourceSupplier>, IResourceSupplierRepository
    {
        public ResourceSupplierRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}