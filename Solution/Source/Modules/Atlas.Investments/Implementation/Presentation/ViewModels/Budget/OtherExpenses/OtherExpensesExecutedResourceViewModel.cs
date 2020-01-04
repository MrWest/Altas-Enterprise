using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.OtherExpenses
{
    /// <summary>
    ///     Implementation of the contract <see cref="IOtherExpensesExecutedResourceViewModel" /> representing the crud view
    ///     model used to manage the executed resources of an other expenses budget component.
    /// </summary>
    public class OtherExpensesExecutedResourceViewModel :
        ExecutedResourceViewModelBase<IOtherExpensesComponent, IOtherExpensesExecutedResourcePresenter, IOtherExpensesExecutedResourceManagerApplicationServices>,
        IOtherExpensesExecutedResourceViewModel
    {
    }
}