using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment
{
    /// <summary>
    /// Contract of the presenter view model used to decorate and impersonate an equipment component element of a certain budget in the UI.
    /// </summary>
    public interface IEquipmentComponentPresenter :
        IBudgetComponentPresenter<
            IEquipmentComponent,
            IEquipmentPlannedSubSpecialityHolderViewModel,
             IEquipmentPlannedSubSpecialityHolderPresenter,
              IEquipmentExecutedSubSpecialityHolderViewModel,
             IEquipmentExecutedSubSpecialityHolderPresenter>
    {
    }
}
