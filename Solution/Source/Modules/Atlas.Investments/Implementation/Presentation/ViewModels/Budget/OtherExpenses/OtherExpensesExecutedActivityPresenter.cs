using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.OtherExpenses
{
    /// <summary>
    ///     Implementation of the presenter view model decorating and impersonating in the UI a executed activity of an
    ///     equipment budget component.
    /// </summary>
    public class OtherExpensesExecutedActivityPresenter :
        ExecutedActivityPresenterBase,
        IOtherExpensesExecutedActivityPresenter
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="OtherExpensesExecutedActivityPresenter" />.
        /// </summary>
        public OtherExpensesExecutedActivityPresenter()
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="OtherExpensesExecutedActivityPresenter" /> given a executed activity.
        /// </summary>
        /// <param name="executedActivity">The executed activity to decorate.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="executedActivity" />
        /// </exception>
        //public OtherExpensesExecutedActivityPresenter(IExecutedActivity executedActivity)
        //    : base(executedActivity)
        //{
        //}
    }
}