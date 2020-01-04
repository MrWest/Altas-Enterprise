using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application.Budget;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Contract of the presenter view model used to decorate and impersonta the budget of a certain investment element in the UI.
    /// </summary>
    public interface IBudgetPresenter : IPresenter<IBudget>, IBudgetSummary,IPeriodCalculator,IFiltrable,IBudgetComponentItemChangesSpreadder,ICosttable
    {
        /// <summary>
        /// Gets or sets the presenter view model containing the investment element to which belong the budget decorated by the current
        /// budget presenter.
        /// </summary>
        IInvestmentElementPresenter InvestmentElement { get; set; }

        /// <summary>
        /// Gets the presenter view model containing the equipment component of the budget containing in the current presenter.
        /// </summary>
        IEquipmentComponentPresenter EquipmentComponent { get; set; }

        /// <summary>
        /// Gets the presenter view model containing the construction component of the budget containing in the current presenter.
        /// </summary>
        IConstructionComponentPresenter ConstructionComponent { get; set; }

        /// <summary>
        /// Gets the presenter view model containing the construction component of the budget containing in the current presenter.
        /// </summary>
        IOtherExpensesComponentPresenter OtherExpensesComponent { get; set; }

        /// <summary>
        /// Gets the presenter view model containing the construction component of the budget containing in the current presenter.
        /// </summary>
        IWorkCapitalComponentPresenter WorkCapitalComponent { get; set; }

        bool HasActivities { get; }


    }
}
