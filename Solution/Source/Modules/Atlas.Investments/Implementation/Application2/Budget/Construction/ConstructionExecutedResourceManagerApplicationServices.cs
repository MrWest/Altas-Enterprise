using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Application.Budget.Construction;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Construction;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Construction;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application.Budget.Construction
{
    /// <summary>
    ///     Implementation of the application services handling the coming CRUD-operations from upper layers regarding to the
    ///     executed resources of an construction budget component.
    /// </summary>
    public class ConstructionExecutedResourceManagerApplicationServices :
        ExecutedBudgetComponentItemManagerApplicationServicesBase<IExecutedResource, IConstructionComponent, IConstructionExecutedResourceRepository, IConstructionExecutedResourceDomainServices>,
        IConstructionExecutedResourceManagerApplicationServices
    {
        /// <summary>
        ///     Gets the instance of the construction planned resources repository.
        /// </summary>
        protected override IRepository PlannedItemRepository
        {
            get { return ServiceLocator.Current.GetInstance<IConstructionPlannedResourceRepository>(); }
        }
    }
}