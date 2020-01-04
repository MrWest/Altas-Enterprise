using System;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.OtherExpenses
{
    /// <summary>
    ///     Implementation of the presenter view model decorating and impersonating in the UI a planned activity of an other
    ///     expenses budget component.
    /// </summary>
    public class OtherExpensesPlannedActivityPresenter :
        PlannedActivityPresenter,
        IOtherExpensesPlannedActivityPresenter
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="OtherExpensesPlannedActivityPresenter" />.
        /// </summary>
        public OtherExpensesPlannedActivityPresenter()
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="OtherExpensesPlannedActivityPresenter" /> given a planned activity.
        /// </summary>
        /// <param name="plannedActivity">The planned activity to decorate.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="plannedActivity" />
        /// </exception>
        public OtherExpensesPlannedActivityPresenter(IPlannedActivity plannedActivity)
            : base()
        {
        }
    }
}