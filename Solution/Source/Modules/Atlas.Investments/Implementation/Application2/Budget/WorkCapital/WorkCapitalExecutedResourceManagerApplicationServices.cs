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
    ///     executed resources of an other expenses budget component.
    /// </summary>
    public class WorkCapitalExecutedResourceManagerApplicationServices :
        ExecutedBudgetComponentItemManagerApplicationServicesBase<IExecutedResource, IWorkCapitalComponent, IWorkCapitalExecutedResourceRepository, IWorkCapitalExecutedResourceDomainServices>,
        IWorkCapitalExecutedResourceManagerApplicationServices
    {
        /// <summary>
        ///     Gets the instance of the work capital planned resources repository.
        /// </summary>
        protected override IRepository PlannedItemRepository
        {
            get { return ServiceLocator.Current.GetInstance<IWorkCapitalPlannedResourceRepository>(); }
        }
    }
}