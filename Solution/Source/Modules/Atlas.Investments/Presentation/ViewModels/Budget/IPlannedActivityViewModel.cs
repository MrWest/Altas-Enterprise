using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Base contract of the crud view model used to manage the planned activity of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the planned activities managed here.</typeparam>
    /// <typeparam name="TPresenter">The type of the presenter view models decorating the planned activities.</typeparam>
    public interface IPlannedActivityViewModel :
        IBudgetComponentItemViewModel<IPlannedActivity, IPlannedActivityPresenter>
        //where TComponent : class, IBudgetComponent
        //where TPresenter : class, IPlannedActivityPresenter<TComponent>
    {
        ///// <summary>
        /////     Gets the budget component containing the items managed in the current
        /////     <see cref="IBudgetComponentItemViewModel{TItem, TPresenter, TComponent}" />.
        ///// </summary>
        //IBudgetComponentPresenter<TComponent> Component { get; set; }
        /// <summary>
        /// Gets or sets the budget component to which belong the underlying item.
        /// </summary>
        ISubSpecialityHolderPresenter<IPlannedSubSpecialityHolder> SubSpecialityHolder { get; set; }
    }

    ///// <summary>
    ///// Base contract of the crud view model used to manage the planned activity of a certain budget component.
    ///// </summary>
    ///// <typeparam name="TComponent">The type of the budget component to which belong the planned activities managed here.</typeparam>
    ///// <typeparam name="TPresenter">The type of the presenter view models decorating the planned activities.</typeparam>
    //public interface IPlannedActivityViewModel<TComponent> :
    //    IBudgetComponentItemViewModel<IPlannedActivity, IPlannedActivityPresenter<TComponent>>
    //    where TComponent : class, IBudgetComponent
    //{
    //    /// <summary>
    //    ///     Gets the budget component containing the items managed in the current
    //    ///     <see cref="IBudgetComponentItemViewModel{TItem, TPresenter, TComponent}" />.
    //    /// </summary>
    //    ISubSpecialityHolderPresenter<TComponent> Component { get; set; }
    //}
}
