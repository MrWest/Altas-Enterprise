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
            IWorkCapitalPlannedActivityViewModel,
            IWorkCapitalPlannedActivityPresenter,
            IWorkCapitalExecutedActivityViewModel,
            IWorkCapitalExecutedActivityPresenter>
    {
    }
}