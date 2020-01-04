using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses
{
    /// <summary>
    ///     Contract of the crud view model managing the planned resources of an other expenses budget component.
    /// </summary>
    public interface IOtherExpensesPlannedResourceViewModel :
        IPlannedResourceViewModel<IBudgetComponentItem, IOtherExpensesPlannedResourcePresenter>
    {
    }
}