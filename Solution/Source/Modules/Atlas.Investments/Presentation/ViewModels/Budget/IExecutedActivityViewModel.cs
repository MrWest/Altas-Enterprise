using System.Windows.Input;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Base contract of the crud view model used to manage the executed resources of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the executed resources managed here.</typeparam>
    /// <typeparam name="TPresenter">The type of the presenter view models decorating the executed resources.</typeparam>
    public interface IExecutedActivityViewModel:
        IBudgetComponentItemViewModel<IExecutedActivity, IExecutedActivityPresenter>
        //where TComponent : class ,IBudgetComponent
        //where TPresenter : class, IExecutedActivityPresenter
    {
        /// <summary>
        ///     Gets the budget component containing the items managed in the current
        ///     <see cref="IBudgetComponentItemViewModel{TItem, TPresenter, TComponent}" />.
        /// </summary>
        //IBudgetComponentPresenter<TComponent> Component { get; set; }

        /// <summary>
        /// Gets or sets the budget component to which belong the underlying item.
        /// </summary>
        ISubSpecialityHolderPresenter<IExecutedSubSpecialityHolder> SubSpecialityHolder { get; set; }
        /// <summary>
        ///     Gets the command that allows to executed specified planned items.
        /// </summary>
        ICommand ExecutePlannedItemsCommand { get; }
    }
}
