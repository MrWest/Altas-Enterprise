using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses
{
    /// <summary>
    ///     Contract of the presenter view model used to decorate and impersonate an OtherExpenses component element of a
    ///     certain budget in the UI.
    /// </summary>
    public interface IOtherExpensesComponentPresenter :
        IBudgetComponentPresenter<
            IOtherExpensesComponent,
           IOtherExpensesPlannedSubSpecialityHolderViewModel,
             IOtherExpensesPlannedSubSpecialityHolderPresenter,
              IOtherExpensesExecutedSubSpecialityHolderViewModel,
             IOtherExpensesExecutedSubSpecialityHolderPresenter>
    {
    }
}