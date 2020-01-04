using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Base contract of the crud view models handling budget component items.
    /// </summary>
    /// <typeparam name="TItem">The type of the budget component items.</typeparam>
    /// <typeparam name="TComponent">The type of the budget component to the items belong.</typeparam>

    //public interface IBudgetComponentResourcePresenter<TItem, TComponentItem> :
    //    IBudgetComponentItemPresenter<TItem,TComponentItem>
    //    where TItem : class, IPlannedResource
    //    where TComponentItem:class ,IBudgetComponentItem
    //{
    //    /// <summary>
    //    ///     Gets or sets the budget component to which belong the items which business rules are enforced in the current
    //    ///     <see cref="IBudgetComponentItemDomainServices{TItem}" />.
    //    /// </summary>
    //     //TComponentItem BudgetComponentItem { get; set; }
       
    //}
}
