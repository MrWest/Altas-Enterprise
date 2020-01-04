using System;
using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;

namespace CompanyName.Atlas.Investments.Application
{
    /// <summary>
    /// Base contract of the application services manging budget component items.
    /// </summary>
    /// <typeparam name="TItem">The type of the budget component items managed by the current application services.</typeparam>
    /// <typeparam name="TComponent">The type of the budget component to which belong the items managed here.</typeparam>
    public interface IBudgetComponentItemManagerApplicationServices<TItem> : IItemManagerApplicationServices<TItem>
        where TItem : class, IBudgetComponentItem
        //where TComponent :class , IEntity
    {
        /// <summary>
        /// Gets or sets the budget component to which belong the items managed here.
        /// </summary>
       // TComponent Component { get; set; }

        /// <summary>
        /// Determines whether there can be narrowed the set of the budget component items by leaving only the ones with the given
        /// specification in their names.
        /// </summary>
        /// <param name="nameSpecification">
        /// A <see cref="string"/> being the criteria that must match the name of the budget component items in order to be
        /// returned.
        /// </param>
        /// <returns>True if there can be filtered the budget component items; false otherwise.</returns>
        bool CanFilter(string nameSpecification);

        /// <summary>
        /// Gets the budget component items which names match the given specification.
        /// </summary>
        /// <param name="nameSpecification">The criteria to be matched by the budget component items in order to be returned.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> matching <paramref name="nameSpecification"/>.</returns>
        IEnumerable<TItem> Filter(string nameSpecification);

        /// <summary>
        /// convert the cost given <see cref="IBudgetComponentItem"/> to match the actual one based on the currency convert factor
        /// </summary>
        /// <param name="budgetComponentItem"></param>
        /// <returns></returns>
        decimal CurrencyConvert(ICurrency currency, ICurrenciable currenciable, decimal Cost = Decimal.MinValue);

        TItem AdquireProperties(TItem onAdquiring, TItem toAdquire);
            //where TOther: class , IBudgetComponentItem;

        decimal GetMyCost(TItem item, ICurrency currency);

        TItem ExportRelated(IDatabaseContext exportDatabaseContext, TItem item);

        TItem Export(IDatabaseContext exportDatabaseContext, TItem item);
        void FreeUpdate(TItem buItem);
        // bool ExistPlannedResource(string code);
    }
}
