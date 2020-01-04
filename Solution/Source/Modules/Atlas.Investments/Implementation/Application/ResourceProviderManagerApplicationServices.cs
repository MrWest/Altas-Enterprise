using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    public class ResourceProviderManagerApplicationServices:ItemManagerApplicationServicesBase<IResourceProvider, IResourceProviderRepository, IResourceProviderDomainServices>, IResourceProviderManagerApplicationServices
    {
        
    }
}