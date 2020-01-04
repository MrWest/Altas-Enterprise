using System;
using System.Collections.Generic;
using CompanyName.Atlas.Investments.Domain.Entities;
using Db4objects.Db4o.TA;

namespace CompanyName.Atlas.Investments.Application
{
    public interface IActivityManagerApplicationServices<TActivity> : IBudgetComponentItemManagerApplicationServices<TActivity>
        where TActivity:class,IActivity
         //where TComponent : class, IBudgetComponent
    {
        /// <summary>
        /// Gets or sets the budget component to which belong the items managed here.
        /// </summary>
        //TComponent Component { get; set; }
        ISubSpecialityHolder SubSpecialityHolder { get; set; }

        bool CanExecute(IEnumerable<IPlannedActivity> plannedItems);

        /// <summary>
        ///     Executes the given planned items.
        /// </summary>
        /// <typeparam name="TPlanned">The type of the planned items to try executing.</typeparam>
        /// <param name="plannedItems">The <see cref="IEnumerable{T}" /> of planned items to execute.</param>
        /// <returns>
        ///     The count of <see cref="IPlannedBudgetComponentItem" /> in <paramref name="plannedItems" /> that were actually
        ///     executed.
        /// </returns>
        IEnumerable<IExecutedActivity> Execute(IEnumerable<IPlannedActivity> plannedItems);

      //  DateTime FinishDate(TActivity activity);

    }
}