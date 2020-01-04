using System;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.WorkCapital
{
    /// <summary>
    ///     Implementation of the presenter view model decorating and impersonating in the UI a planned activity of a work
    ///     capital budget component.
    /// </summary>
    public class WorkCapitalPlannedActivityPresenter :
        PlannedActivityPresenter,
        IWorkCapitalPlannedActivityPresenter
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="WorkCapitalPlannedActivityPresenter" />.
        /// </summary>
        public WorkCapitalPlannedActivityPresenter()
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="WorkCapitalPlannedActivityPresenter" /> given a planned activity.
        /// </summary>
        /// <param name="plannedActivity">The planned activity to decorate.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="plannedActivity" />
        /// </exception>
        public WorkCapitalPlannedActivityPresenter(IPlannedActivity plannedActivity)
            : base()
        {
        }
    }
}