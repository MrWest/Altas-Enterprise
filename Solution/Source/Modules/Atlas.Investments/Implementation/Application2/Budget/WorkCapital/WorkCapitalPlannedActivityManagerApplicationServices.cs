using CompanyName.Atlas.Investments.Application.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Implementation.Application.Budget.WorkCapital
{
    /// <summary>
    ///     Implementation of the application services handling the coming CRUD-operations from upper layers regarding to the
    ///     planned activities of an other expenses budget component.
    /// </summary>
    public class WorkCapitalPlannedActivityManagerApplicationServices :
        BudgetComponentActivityManagerApplicationServices<IPlannedActivity, IWorkCapitalComponent, IWorkCapitalPlannedActivityRepository, IWorkCapitalPlannedActivityDomainServices>,
        IWorkCapitalPlannedActivityManagerApplicationServices
    {
    }
}