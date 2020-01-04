using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital
{
    /// <summary>
    ///     Contract of the presenter view model used to decorate and impersonate an WorkCapital component element of a certain
    ///     budget in the UI.
    /// </summary>
    public interface IWorkCapitalComponentPresenter :
        IBudgetComponentPresenter<
            IWorkCapitalComponent,
            IWorkCapitalPlannedSubSpecialityHolderViewModel,
            IWorkCapitalPlannedSubSpecialityHolderPresenter,
            IWorkCapitalExecutedSubSpecialityHolderViewModel,
            IWorkCapitalExecutedSubSpecialityHolderPresenter>
    {
        /// <summary>
        /// Cash flow for work capital
        /// </summary>
        IWorkCapitalCashFlowPresenter PlannedWorkCapitalCashFlow { get; set; }
        /// Cash flow for work capital
        /// </summary>
        IWorkCapitalCashFlowPresenter ExecutedWorkCapitalCashFlow { get; set; }
    }
}