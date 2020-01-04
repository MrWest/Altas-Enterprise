using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment
{
    /// <summary>
    /// Contract of the crud view model managing the executed resources of an equipment budget component.
    /// </summary>
    public interface IEquipmentExecutedResourceViewModel : IExecutedResourceViewModel<IEquipmentComponent, IEquipmentExecutedResourcePresenter>
    {
    }
}
