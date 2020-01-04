using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Construction;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services.Budget.Construction
{
    /// <summary>
    ///     Implementation of the contract <see cref="IConstructionExecutedActivityDomainServices" />, representing a domain
    ///     services ensuring that the business rules for the <see cref="IExecutedActivity" /> of a certain
    ///     <see cref="IConstructionComponent" /> are respected.
    /// </summary>
    public class ConstructionExecutedActivityDomainServices :
        ExecutedActivityDomainServicesBase,
        IConstructionExecutedActivityDomainServices
    {
    }
}