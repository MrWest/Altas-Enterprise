using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.Equipment;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Equipment
{
    /// <summary>
    /// Implementation of the contract <see cref="IEquipmentPlannedActivityViewModel"/> representing the crud view model used to manage
    /// the planned activities of an equipment budget component.
    /// </summary>
    public class EquipmentPlannedActivityViewModel :
        PlannedActivityViewModelBase,
        IEquipmentPlannedActivityViewModel
    {
    }
}
