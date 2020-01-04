using System.Windows.Input;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    /// <summary>
    ///     This is the base contract of a crud view model managing executed items.
    /// </summary>
    public interface IExecutedBudgetComponentItemViewModel 
    {
        /// <summary>
        ///     Gets the command that allows to executed specified planned items.
        /// </summary>
        ICommand ExecutePlannedItemsCommand { get; }
    }


    /// <summary>
    ///     This is the base contract of a crud view model managing executed items.
    /// </summary>
    public interface IExecutedBudgetComponentItemViewModel<TPresenter, TComponent> :IBudgetComponentItemViewModel<IExecutedActivity,TPresenter>, IExecutedBudgetComponentItemViewModel

        //where T : class, IExecutedBudgetComponentItem
        where TPresenter : class, IBudgetComponentItemPresenter<IExecutedActivity>
        where TComponent : class, IBudgetComponent
    {
        IBudgetComponentPresenter<TComponent> Component { get; set; }
    }
}