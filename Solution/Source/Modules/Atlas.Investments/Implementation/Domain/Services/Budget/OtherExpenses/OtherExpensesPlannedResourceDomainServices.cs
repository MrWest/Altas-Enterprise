using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.OtherExpenses;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services.Budget.OtherExpenses
{
    /// <summary>
    ///     Implementation of the contract <see cref="IOtherExpensesPlannedResourceDomainServices" />, representing a domain
    ///     services ensuring that the business rules for the <see cref="IPlannedResource" /> of a certain
    ///     <see cref="IOtherExpensesComponent" /> are respected.
    /// </summary>
    public class OtherExpensesPlannedResourceDomainServices :
        PlannedResourceDomainServicesBase<IOtherExpensesComponent>,
        IOtherExpensesPlannedResourceDomainServices
    {
    }
}