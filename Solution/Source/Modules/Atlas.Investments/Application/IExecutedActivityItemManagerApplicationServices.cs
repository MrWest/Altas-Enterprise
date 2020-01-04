using System.Collections.Generic;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;

namespace CompanyName.Atlas.Investments.Application
{
    /// <summary>
    ///     Base contract of the application services used to respond to the incoming requests from the upper layers regarding
    ///     to the
    ///     CRUD-operations implying executed budget component items.
    /// </summary>
    /// <typeparam name="TItem">The type of the executed budget component items managed by the current application services.</typeparam>
    /// <typeparam name="TComponent">The type of the budget component to which belong the executed items managed here.</typeparam>
    public interface IExecutedActivityItemManagerApplicationServices :
        IBudgetComponentItemManagerApplicationServices<IExecutedActivity>
        //where TItem : class, IBudgetComponentItem
        //where TComponent : class, IBudgetComponent

    {

        //TComponent Component { get; set; }

        ISubSpecialityHolder SubSpecialityHolder { get; set; }
        ///     Determines whether there can be executed the given planned items.
        /// </summary>
        /// <typeparam name="TPlanned">The type of the planned items to try executing.</typeparam>
        /// <param name="plannedItems">
        ///     An <see cref="IEnumerable{T}" /> of planned items being the ones to determine whether there can
        ///     be executed.
        /// </param>
        /// <returns>True when there is at least one of <see cref="plannedItems" /> that can be executed; false otherwise.</returns>
        bool CanBeExecute<TPlanned>(IEnumerable<TPlanned> plannedItems)
            where TPlanned : class, IPlannedActivity;

      
        /// <summary>
        ///     Executes the given planned items.
        /// </summary>
        /// <param name="plannedItems">The <see cref="IEnumerable{T}" /> of planned items to execute.</param>
        /// <returns>
        ///     The count of <see cref="IPlannedBudgetComponentItem" /> in <paramref name="plannedItems" /> that were actually
        ///     executed.
        /// </returns>
        IEnumerable<IExecutedActivity> BeExecuted<TPlanned>(IEnumerable<TPlanned> plannedItems)
            where TPlanned : class, IPlannedActivity;

      //  IPlannedActivityRepository PlannedItemRepository { get; set; }


    }
}