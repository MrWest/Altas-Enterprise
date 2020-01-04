using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application.Budget;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Contract of the presenter view model used to decorated and impersonate in the UI a component of an investment element's budget.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component decorated in the current presenter.</typeparam>
    /// <typeparam name="TPlannedResources">The type of the crud view model managing the current budget component's planned resources.</typeparam>
    /// <typeparam name="TPlannedResourcePresenter">The presenter view model to wrap the planned budget component resources.</typeparam>
    /// <typeparam name="TExecutedResources">The type of the crud view model managing the current budget component's executed resources.</typeparam>
    /// <typeparam name="TExecutedResourcePresenter">The presenter view model to wrap the executed budget component resources.</typeparam>
    /// <typeparam name="TPlannedActivities">The type of the crud view model managing the current budget component's planned activities.</typeparam>
    /// <typeparam name="TPlannedActivityPresenter">The presenter view model to wrap the planned budget component activities.</typeparam>
    /// <typeparam name="TExecutedActivities">The type of the crud view model managing the current budget component's executed activities.</typeparam>
    /// <typeparam name="TExecutedActivityPresenter">The presenter view model to wrap the executed budget component activities.</typeparam>
    public interface IBudgetComponentPresenter<TComponent,TPlannedSubSpecialityHolders, TPlannedSubSpecialityHolderPresenter, TExecutedSubSpecialityHolders, TExecutedSubSpecialityHolderPresenter> : IBudgetComponentPresenter<TComponent>
        where TComponent : class, IBudgetComponent
        //where TPlannedActivities : class, IPlannedActivityViewModel
        //////where TPlannedActivityPresenter : class, IPlannedActivityPresenter
        //where TExecutedActivities : class, IExecutedActivityViewModel
        //where TExecutedActivityPresenter : class, IExecutedActivityPresenter
        where TPlannedSubSpecialityHolders : class, IPlannedSubSpecialityHolderViewModel<TComponent, TPlannedSubSpecialityHolderPresenter>
        where TPlannedSubSpecialityHolderPresenter : class, IPlannedSubSpecialityHolderPresenter<TComponent>
        where TExecutedSubSpecialityHolders : class, IExecutedSubSpecialityHolderViewModel<TComponent, TExecutedSubSpecialityHolderPresenter>
        where TExecutedSubSpecialityHolderPresenter : class, IExecutedSubSpecialityHolderPresenter<TComponent>
    {
       

        /// <summary>
        /// Gets the crud view model used to manage the planned resources of the budget component contained in the current
        /// presenter.
        /// </summary>
       // TPlannedResources PalnnedResources { get; }

        /// <summary>
        /// Gets the crud view model used to manage the executed resources of the budget component contained in the current
        /// presenter.
        /// </summary>
      //  TExecutedResources ExecutedResources { get; }

        /// <summary>
        /// Gets the crud view model used to manage the planned activities of the budget component contained in the current
        /// presenter.
        /// </summary>
        IPlannedActivityViewModel PlannedActivities { get; }

        /// <summary>
        /// Gets the crud view model used to manage the executed activities of the budget component contained in the current
        /// presenter.
        /// </summary>
        IExecutedActivityViewModel ExecutedActivities { get; }

        TPlannedSubSpecialityHolders PlannedSubSpecialityHolders { get; }

        TExecutedSubSpecialityHolders ExecutedSubSpecialityHolders { get; }
        /// <summary>
        /// Gets the total of executed activities composing the current <see cref="IBudgetComponentPresenter"/>.
        /// </summary>
      //  decimal ExecutedActivitiesCost { get; }
        /// <summary>
        /// Gets the total of executed resource composing the current <see cref="IBudgetComponentPresenter"/>.
        /// </summary>
      //  decimal ExecutedResourceCost { get; }

        decimal BudgetByCurrencyAndPeriod(ICurrency currency, IPeriod period);

        bool HasActivities { get; }
      
    }

    /// <summary>
    /// simplify representation of <see cref="IBudgetComponentPresenter"/> to get jus what I want
    /// </summary>
    /// <typeparam name="?"></typeparam>
    public interface IBudgetComponentPresenter<TComponent> : IPresenter<TComponent>, IBudgetSummary,IPeriodCalculator, IBudgetComponentItemChangesSpreadder, ICosttable
         where TComponent : class, IBudgetComponent
    {
        /// <summary>
        /// Gets or sets the budget to which belong the current budget component.
        /// </summary>
        IBudgetPresenter Budget { get; set; }

        decimal PlannedCost { get; }
        decimal ExecutedCost { get; }

        void Notify();

        /// <summary>
        /// Notify changes to superior levels
        /// </summary>
        void NotifyUp();

       
    }
}
