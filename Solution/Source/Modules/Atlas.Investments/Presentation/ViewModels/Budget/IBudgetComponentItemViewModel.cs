using System;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    /// <summary>
    ///     Base contract of the crud view models handling budget component items.
    /// </summary>
    public interface IBudgetComponentItemViewModel 
    {
        //IEntity BudgetComponentItemParent { get; set; }
        /// <summary>
        ///     Gets the command that allows to filter the set of budget component items managed in the current crud view model.
        /// </summary>
        ICommand FilterCommand { get; }

    }


    /// <summary>
    ///     Base contract of the crud view models handling budget component items.
    /// </summary>
    /// <typeparam name="TItem">The type of the budget component items.</typeparam>
    /// <typeparam name="TPresenter">The type of presenter view model decorating the budget component items.</typeparam>
    /// <typeparam name="TComponent">The type of the budget component of the items managed in the current crud view model.</typeparam>
    public interface IBudgetComponentItemViewModel<TItem,TPresenter>:INavigableViewModel<TItem,TPresenter>//,IBudgetComponentItemViewModel
        where TItem : class ,IBudgetComponentItem
        where TPresenter : class, IBudgetComponentItemPresenter<TItem>
    {
        
        
    }
}