using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Application.Budget.Construction;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Construction;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Construction;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application.Budget.Construction
{
    /// <summary>
    ///     Implementation of the application services handling the coming CRUD-operations from upper layers regarding to the
    ///     planned activities of an construction budget component.
    /// </summary>
    public class ConstructionExecutedActivityManagerApplicationServices :
        ExecutedBudgetComponentItemManagerApplicationServicesBase<IExecutedActivity, IConstructionComponent, IConstructionExecutedActivityRepository, IConstructionExecutedActivityDomainServices>,
        IConstructionExecutedActivityManagerApplicationServices
    {
        /// <summary>
        ///     Gets the instance of the construction planned activities repository.
        /// </summary>
        protected override IRepository PlannedItemRepository
        {
            get { return ServiceLocator.Current.GetInstance<IConstructionPlannedActivityRepository>(); }
        }
    }
}