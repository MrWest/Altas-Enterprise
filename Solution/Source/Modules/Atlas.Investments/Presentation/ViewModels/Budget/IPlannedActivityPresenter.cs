using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Base contract for the presenter view models decorating and impersonating in the UI the planned activities of a certain
    /// budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the decorated planned activity.</typeparam>
    public interface IPlannedActivityPresenter :
        IBudgetComponentItemPresenter<IPlannedActivity>, IBudgetSummary, IPeriodCalculator, ITreeNode
    //where TComponent : class, IBudgetComponent
    {
        /// <summary>
        /// Gets or sets the budget component to which belong the underlying item.
        /// </summary>
        //IBudgetComponentPresenter<TComponent> Component { get; set; }
        /// <summary>
        /// Gets or sets the budget component to which belong the underlying item.
        /// </summary>
        ISubSpecialityHolderPresenter<IPlannedSubSpecialityHolder> SubSpecialityHolder { get; set; }

      
        //    /// <summary>
        //    /// Get or sets the Expense Concept id of the current <see cref="BudgetComponentItemBase"/>.
        //    /// </summary>
        ISubSpecialityPresenter SubSpeciality { get; set; }
    }

    //public interface IPlannedActivityPresenter : IBudgetSummary, IPeriodCalculator, INavigable
    //{
    //    /// <summary>
    //    /// Gets or sets the time interval (<see cref="IPeriod"/> for  the current <see cref="IBudgetComponentItem"/>.
    //    /// </summary>
    //    IPeriodPresenter Period { get; set; }

    //    /// <summary>
    //    /// Get or sets the Expense Concept id of the current <see cref="BudgetComponentItemBase"/>.
    //    /// </summary>
    //    ISubSpecialityPresenter SubSpeciality { get; set; }
    //}
}
