using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction
{
    /// <summary>
    /// Contract of the presenter view model used to decorate and impersonate an construction component element of a certain budget in the UI.
    /// </summary>
    public interface IConstructionComponentPresenter :
        IBudgetComponentPresenter<
            IConstructionComponent,
          IConstructionPlannedSubSpecialityHolderViewModel,
             IConstructionPlannedSubSpecialityHolderPresenter,
              IConstructionExecutedSubSpecialityHolderViewModel,
             IConstructionExecutedSubSpecialityHolderPresenter>
    {
    }
}
