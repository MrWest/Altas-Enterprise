using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Application.Budget.Equipment;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Equipment;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Equipment;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application.Budget.Equipment
{
    /// <summary>
    ///     Implementation of the application services handling the coming CRUD-operations from upper layers regarding to the
    ///     executed resources of an equipment budget component.
    /// </summary>
    public class EquipmentExecutedResourceManagerApplicationServices :
        ExecutedBudgetComponentItemManagerApplicationServicesBase<IExecutedResource, IEquipmentComponent, IEquipmentExecutedResourceRepository, IEquipmentExecutedResourceDomainServices>,
        IEquipmentExecutedResourceManagerApplicationServices
    {
        /// <summary>
        ///     Gets the instance of the equipment planned resources repository.
        /// </summary>
        protected override IRepository PlannedItemRepository
        {
            get { return ServiceLocator.Current.GetInstance<IEquipmentPlannedResourceRepository>(); }
        }
    }
}