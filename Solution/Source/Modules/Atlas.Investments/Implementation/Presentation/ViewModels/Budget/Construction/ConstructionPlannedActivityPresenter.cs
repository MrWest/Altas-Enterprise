using System;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.Construction;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Construction
{
    /// <summary>
    /// Implementation of the presenter view model decorating and impersonating in the UI a planned activity of an equipment budget
    /// component.
    /// </summary>
    public class ConstructionPlannedActivityPresenter :
        PlannedActivityPresenter, IConstructionPlannedActivityPresenter
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConstructionPlannedActivityPresenter"/>.
        /// </summary>
        public ConstructionPlannedActivityPresenter()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ConstructionPlannedActivityPresenter"/> given a planned activity.
        /// </summary>
        /// <param name="plannedActivity">The planned activity to decorate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="plannedActivity"/></exception>
        public ConstructionPlannedActivityPresenter(IPlannedActivity plannedActivity)
            : base()
        {
        }
    }
}
