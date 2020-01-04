using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.WorkCapital
{
    /// <summary>
    ///     Implementation of the presenter view model decorating and impersonating in the UI a executed activity of an
    ///     equipment budget component.
    /// </summary>
    public class WorkCapitalExecutedActivityPresenter :
        ExecutedActivityPresenterBase,
        IWorkCapitalExecutedActivityPresenter
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="WorkCapitalExecutedActivityPresenter" />.
        /// </summary>
        public WorkCapitalExecutedActivityPresenter()
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="WorkCapitalExecutedActivityPresenter" /> given a executed activity.
        /// </summary>
        /// <param name="executedActivity">The executed activity to decorate.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="executedActivity" />
        /// </exception>
        //public WorkCapitalExecutedActivityPresenter(IExecutedActivity executedActivity)
        //    : base(executedActivity)
        //{
        //}
    }
}