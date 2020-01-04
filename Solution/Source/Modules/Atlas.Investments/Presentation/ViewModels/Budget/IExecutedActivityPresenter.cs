using System.Notifications;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Contracts.Presentation.Common;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Base contract for the presenter view models decorating and impersonating in the UI the executed activity of a certain
    /// budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the decorated executed activity.</typeparam>
    public interface IExecutedActivityPresenter :
        IBudgetComponentItemPresenter<IExecutedActivity>
        ////where TComponent : class,IBudgetComponent
    {
        /// <summary>
        /// Gets or sets the budget component to which belong the underlying item.
        /// </summary>
        ////IBudgetComponentPresenter<TComponent> Component { get; set; }

        /// <summary>
        /// Gets or sets the budget component to which belong the underlying item.
        /// </summary>
        ISubSpecialityHolderPresenter<IExecutedSubSpecialityHolder> SubSpecialityHolder { get; set; }

        IExecutionViewModel ExecutionLog { get; }
        decimal ExecutedQuantity { get; }
        IPeriodPresenter Period { get; set;}
        //IPlannedActivityPresenter<TComponent> PlannedActivity { get; }
    }
}
