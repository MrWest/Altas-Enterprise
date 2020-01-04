using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    public class ResourceProviderDomainServices:CodedNomenclatorDomainServicesBase<IResourceProvider>, IResourceProviderDomainServices
    {
        public override IResourceProvider Create()
        {
            var provider = base.Create();
            provider.Name = Resources.NewResourceProvider;
            provider.Description = Resources.NewResourceProviderDescription;
            return provider;
        }
    }
}