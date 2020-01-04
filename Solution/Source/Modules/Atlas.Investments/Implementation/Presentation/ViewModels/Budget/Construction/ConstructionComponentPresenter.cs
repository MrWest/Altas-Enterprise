using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Construction
{
    /// <summary>
    /// Implementation of the contract <see cref="IConstructionComponentPresenter"/>, representing a presenter view model used to
    /// decorate and impersonate instances <see cref="IConstructionComponent"/> in the UI.
    /// </summary>
    public class ConstructionComponentPresenter :
        BudgetComponentPresenterBase<
            IConstructionComponent,
            IConstructionPlannedSubSpecialityHolderViewModel,
             IConstructionPlannedSubSpecialityHolderPresenter,
              IConstructionExecutedSubSpecialityHolderViewModel,
             IConstructionExecutedSubSpecialityHolderPresenter>,
        IConstructionComponentPresenter
    {
    }
}
