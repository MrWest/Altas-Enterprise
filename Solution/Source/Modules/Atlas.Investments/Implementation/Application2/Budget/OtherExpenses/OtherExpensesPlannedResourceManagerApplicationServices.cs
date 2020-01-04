using CompanyName.Atlas.Investments.Application.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.OtherExpenses;

namespace CompanyName.Atlas.Investments.Implementation.Application.Budget.OtherExpenses
{
    /// <summary>
    ///     Implementation of the application services handling the coming CRUD-operations from upper layers regarding to the
    ///     planned resources of an other expenses budget component.
    /// </summary>
    public class OtherExpensesPlannedResourceManagerApplicationServices :
        BudgetComponentItemManagerApplicationServicesBase<IPlannedResource, IOtherExpensesComponent, IOtherExpensesPlannedResourceRepository, IOtherExpensesPlannedResourceDomainServices>,
        IOtherExpensesPlannedResourceManagerApplicationServices
    {
    }
}