using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services.Budget.WorkCapital
{
    /// <summary>
    ///     Implementation of the contract <see cref="IWorkCapitalPlannedResourceDomainServices" />, representing a domain
    ///     services ensuring that the business rules for the <see cref="IPlannedResource" /> of a certain
    ///     <see cref="IWorkCapitalComponent" /> are respected.
    /// </summary>
    public class WorkCapitalPlannedResourceDomainServices :
        PlannedResourceDomainServicesBase<IWorkCapitalComponent>,
        IWorkCapitalPlannedResourceDomainServices
    {
    }
}