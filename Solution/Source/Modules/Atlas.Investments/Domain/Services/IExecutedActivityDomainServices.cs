using System.Collections.Generic;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services
{
    /// <summary>
    ///     Base contract of the doamin services ensuring there are applied the business rules for the items of a certain
    ///     budget component.
    /// </summary>
    /// <typeparam name="TItem">The type of the budget component items which business rules are enforced.</typeparam>
    /// <typeparam name="TComponent">The budget component to which the item belong.</typeparam>
    public interface IExecutedActivityDomainServices : IActivityDomainServices<IExecutedActivity>
        //where TItem : class, IBudgetComponentItem
        //where TComponent : class, IBudgetComponent
    {
       // TComponent Component { get; set; }
        /// <summary>
        ///     Determines whether there can be executed any of the given planned items.
        /// </summary>
        /// <typeparam name="TPlanned">The type of the planned items to determine whether there can executed or not.</typeparam>
        /// <param name="plannedItems">
        ///     A <see cref="IEnumerable{T}" /> of planned items to determine whether there can be executed at least one of them.
        /// </param>
        /// <returns>True if there is at least one planned item unexecuted; false otherwise.</returns>
     //   bool CanExecute<TPlanned>(IEnumerable<TPlanned> plannedItems) where TPlanned : class, IPlannedActivity;

        /// <summary>
        ///     Executes all the planned items still not executed.
        /// </summary>
        /// <typeparam name="TPlanned">The type of the planned items to execute.</typeparam>
        /// <param name="plannedItems">
        ///     The <see cref="IEnumerable{T}" /> of planned items to executed the unexecuted ones among them.
        /// </param>
        /// <returns>An <see cref="int" /> representing how many planned items have been actually executed.</returns>
      //  IEnumerable<IExecutedActivity> Execute<TPlanned>(IEnumerable<TPlanned> plannedItems) where TPlanned : class, IPlannedActivity;
   
    }
}