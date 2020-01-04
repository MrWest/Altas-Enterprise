using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data.Budget
{
    /// <summary>
    /// Base contract of the repositories used to performs the data operations over items of a certain budget component to ensure that
    /// their managed safely.
    /// </summary>
    /// <typeparam name="TItem">The budget component items which data operations will be handled here.</typeparam>
    /// <typeparam name="TComponent">The budget component to which they belong.</typeparam>
    public interface IBudgetComponentItemRepository<TItem> : IRepository<TItem> where TItem : class, IBudgetComponentItem
       
    {

        /// <summary>
        /// Gets or sets the budget component to which belong the items which data operations are made in the current
        /// <see cref="IBudgetComponentItemRepository{T, TComponent}"/>.
        /// </summary>
        //  TComponent Component { get; set; }
        /// <summary>
        /// Gets the budget component items which names match the given specification.
        /// </summary>
        /// <param name="nameSpecification">The specification that the names of the returned budget component items must match.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> of budgte component items matching the given name specification.</returns>
        IEnumerable<TItem> FilterByName(string nameSpecification);
    }
}
