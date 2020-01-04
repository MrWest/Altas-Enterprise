using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Construction;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services.Budget.Construction
{
    /// <summary>
    /// Implementation of the contract <see cref="IConstructionPlannedResourceDomainServices"/>, representing a domain
    /// services ensuring that the business rules for the <see cref="IPlannedResource"/> of a certain
    /// <see cref="IConstructionComponent"/> are respected.
    /// </summary>
    public class ConstructionPlannedResourceDomainServices :
        PlannedResourceDomainServicesBase<IConstructionComponent>,
        IConstructionPlannedResourceDomainServices
    {
    }
}
