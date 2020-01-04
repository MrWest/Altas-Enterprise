using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses
{
    /// <summary>
    ///     Contract of the crud view model managing the executed resources of an other expenses budget component.
    /// </summary>
    public interface IOtherExpensesExecutedResourceViewModel :
        IExecutedResourceViewModel<IOtherExpensesComponent, IOtherExpensesExecutedResourcePresenter>
    {
    }
}