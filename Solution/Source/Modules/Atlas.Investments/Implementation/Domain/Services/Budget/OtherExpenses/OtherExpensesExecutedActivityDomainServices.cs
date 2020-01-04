using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.OtherExpenses;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services.Budget.OtherExpenses
{
    /// <summary>
    ///     Implementation of the contract <see cref="IOtherExpensesExecutedActivityDomainServices" />, representing a domain
    ///     services ensuring that the business rules for the <see cref="IExecutedActivity" /> of a certain
    ///     <see cref="IOtherExpensesComponent" /> are respected.
    /// </summary>
    public class OtherExpensesExecutedActivityDomainServices :
        ExecutedActivityDomainServicesBase,
        IOtherExpensesExecutedActivityDomainServices
    {
    }
}