using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services
{
    /// <summary>
    ///     Base contract of the doamin services ensuring there are applied the business rules for the items of a certain
    ///     budget component.
    /// </summary>
    /// <typeparam name="TItem">The type of the budget component items which business rules are enforced.</typeparam>
    /// <typeparam name="TComponent">The budget component to which the item belong.</typeparam>
    public interface IBudgetComponentItemDomainServices<TItem>: IDomainServices<TItem>
        where TItem : class, IBudgetComponentItem
        //where TComponent : class, IEntity
    {
        /// <summary>
        ///     Gets or sets the budget component to which belong the items which business rules are enforced in the current
        ///     <see cref="IBudgetComponentItemDomainServices{T, TComponent}" />.
        /// </summary>
       // TComponent Component { get; set; }

      


    }
}