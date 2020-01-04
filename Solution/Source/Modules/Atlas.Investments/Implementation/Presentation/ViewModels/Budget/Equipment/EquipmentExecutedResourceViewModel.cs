using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.Equipment;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Equipment
{
    /// <summary>
    /// Implementation of the contract <see cref="IEquipmentExecutedResourceViewModel"/> representing the crud view model used to manage
    /// the executed resources of an equipment budget component.
    /// </summary>
    public class EquipmentExecutedResourceViewModel :
        ExecutedResourceViewModelBase<IEquipmentComponent, IEquipmentExecutedResourcePresenter, IEquipmentExecutedResourceManagerApplicationServices>,
        IEquipmentExecutedResourceViewModel
    {
    }
}
