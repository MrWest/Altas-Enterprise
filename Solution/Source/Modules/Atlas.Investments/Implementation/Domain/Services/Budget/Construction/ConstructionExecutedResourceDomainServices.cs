using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Construction;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services.Budget.Construction
{
    /// <summary>
    /// Implementation of the contract <see cref="IConstructionExecutedResourceDomainServices"/>, representing a domain
    /// services ensuring that the business rules for the <see cref="IExecutedResource"/> of a certain
    /// <see cref="IConstructionComponent"/> are respected.
    /// </summary>
    public class ConstructionExecutedResourceDomainServices :
        ExecutedResourceDomainServicesBase<IConstructionComponent>,
        IConstructionExecutedResourceDomainServices
    {
    }
}
