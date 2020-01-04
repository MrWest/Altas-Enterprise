using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Application.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.WorkCapital;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application.Budget.WorkCapital
{
    /// <summary>
    ///     Implementation of the application services handling the coming CRUD-operations from upper layers regarding to the
    ///     planned activities of an WorkCapital budget component.
    /// </summary>
    public class WorkCapitalExecutedActivityManagerApplicationServices :
        ExecutedBudgetComponentItemManagerApplicationServicesBase<IExecutedActivity, IWorkCapitalComponent, IWorkCapitalExecutedActivityRepository, IWorkCapitalExecutedActivityDomainServices>,
        IWorkCapitalExecutedActivityManagerApplicationServices
    {
        /// <summary>
        ///     Gets the instance of the work capital planned activity repository.
        /// </summary>
        protected override IRepository PlannedItemRepository
        {
            get { return ServiceLocator.Current.GetInstance<IWorkCapitalPlannedActivityRepository>(); }
        }
    }
}