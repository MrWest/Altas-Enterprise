using CompanyName.Atlas.Investments.Application.Budget.Equipment;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Equipment;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Equipment;

namespace CompanyName.Atlas.Investments.Implementation.Application.Budget.Equipment
{
    /// <summary>
    ///     Implementation of the application services handling the coming CRUD-operations from upper layers regarding to the
    ///     planned activities of an equipment budget component.
    /// </summary>
    public class EquipmentPlannedActivityManagerApplicationServices :
        BudgetComponentItemManagerApplicationServicesBase<IPlannedActivity, IEquipmentComponent, IEquipmentPlannedActivityRepository, IEquipmentPlannedActivityDomainServices>,
        IEquipmentPlannedActivityManagerApplicationServices
    {
    }
}