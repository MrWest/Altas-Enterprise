using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.OtherExpenses
{
    /// <summary>
    ///     Implementation of the contract <see cref="IOtherExpensesComponentPresenter" />, representing a presenter view model
    ///     used to decorate and impersonate instances <see cref="IOtherExpensesComponent" /> in the UI.
    /// </summary>
    public class OtherExpensesComponentPresenter :
        BudgetComponentPresenterBase<
            IOtherExpensesComponent,
            IOtherExpensesPlannedSubSpecialityHolderViewModel,
             IOtherExpensesPlannedSubSpecialityHolderPresenter,
              IOtherExpensesExecutedSubSpecialityHolderViewModel,
             IOtherExpensesExecutedSubSpecialityHolderPresenter>,
        IOtherExpensesComponentPresenter
    {
    }
}