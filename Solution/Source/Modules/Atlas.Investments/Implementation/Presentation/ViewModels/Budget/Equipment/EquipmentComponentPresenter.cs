using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Equipment
{
    /// <summary>
    /// Implementation of the contract <see cref="IEquipmentComponentPresenter"/>, representing a presenter view model used to
    /// decorate and impersonate instances <see cref="IEquipmentComponent"/> in the UI.
    /// </summary>
    public class EquipmentComponentPresenter :
        BudgetComponentPresenterBase<
            IEquipmentComponent,
            IEquipmentPlannedSubSpecialityHolderViewModel,
             IEquipmentPlannedSubSpecialityHolderPresenter,
              IEquipmentExecutedSubSpecialityHolderViewModel,
             IEquipmentExecutedSubSpecialityHolderPresenter>,
        IEquipmentComponentPresenter
    {
    }
}
