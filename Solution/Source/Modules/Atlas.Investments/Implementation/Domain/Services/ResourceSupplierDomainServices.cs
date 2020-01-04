using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    public class ResourceSupplierDomainServices:CodedNomenclatorDomainServicesBase<IResourceSupplier>, IResourceSupplierDomainServices
    {
        public override IResourceSupplier Create()
        {
            var supplier = base.Create();
            supplier.Name = Resources.NewResourceSupplier;
            supplier.Description = Resources.NewResourceSupplierDescription;
            return supplier;
        }
    }
}