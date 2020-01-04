using System.Collections.Generic;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services
{
    public interface IActivityDomainServices<TActivity>: IBudgetComponentItemDomainServices<TActivity>
        where TActivity:class ,IActivity
    {
        //TComponent Component { get; set; }

        ISubSpecialityHolder SubSpecialityHolder { get; set; }
        /// <summary>
        ///     Determines whether there can be executed any of the given planned items.
        /// </summary>
        /// <typeparam name="TPlanned">The type of the planned items to determine whether there can executed or not.</typeparam>
        /// <param name="plannedItems">
        ///     A <see cref="IEnumerable{T}" /> of planned items to determine whether there can be executed at least one of them.
        /// </param>
        /// <returns>True if there is at least one planned item unexecuted; false otherwise.</returns>
        bool CanExecute(IEnumerable<IPlannedActivity> plannedItems);

        /// <summary>
        ///     Executes all the planned items still not executed.
        /// </summary>
        /// <typeparam name="TPlanned">The type of the planned items to execute.</typeparam>
        /// <param name="plannedItems">
        ///     The <see cref="IEnumerable{T}" /> of planned items to executed the unexecuted ones among them.
        /// </param>
        /// <returns>An <see cref="int" /> representing how many planned items have been actually executed.</returns>
        IEnumerable<IExecutedActivity> Execute(IEnumerable<IPlannedActivity> plannedItems);
    }
}