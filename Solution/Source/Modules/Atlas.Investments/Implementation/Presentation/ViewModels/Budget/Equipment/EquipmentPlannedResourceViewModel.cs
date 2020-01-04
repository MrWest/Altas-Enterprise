using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.Equipment;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Equipment
{
    /// <summary>
    /// Implementation of the contract <see cref="IEquipmentPlannedResourceViewModel"/> representing the crud view model used to manage
    /// the planned resources of an equipment budget component.
    /// </summary>
    public class EquipmentPlannedResourceViewModel :
        PlannedResourceViewModelBase<IEquipmentComponent, IEquipmentPlannedResourcePresenter, IEquipmentPlannedResourceManagerApplicationServices>,
        IEquipmentPlannedResourceViewModel
    {
    }
}
