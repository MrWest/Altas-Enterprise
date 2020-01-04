using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services.Budget.WorkCapital
{
    /// <summary>
    ///     Implementation of the contract <see cref="IWorkCapitalExecutedActivityDomainServices" />, representing a domain
    ///     services ensuring that the business rules for the <see cref="IExecutedActivity" /> of a certain
    ///     <see cref="IWorkCapitalComponent" /> are respected.
    /// </summary>
    public class WorkCapitalExecutedActivityDomainServices :
        ExecutedActivityDomainServicesBase,
        IWorkCapitalExecutedActivityDomainServices
    {
    }
}