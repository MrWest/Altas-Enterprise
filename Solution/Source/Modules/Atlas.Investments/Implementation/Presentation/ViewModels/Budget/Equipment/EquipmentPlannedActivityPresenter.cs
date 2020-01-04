using System;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.Equipment;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Equipment
{
    /// <summary>
    /// Implementation of the presenter view model decorating and impersonating in the UI a planned activity of an equipment budget
    /// component.
    /// </summary>
    public class EquipmentPlannedActivityPresenter :
        PlannedActivityPresenter, IEquipmentPlannedActivityPresenter
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EquipmentPlannedActivityPresenter"/>.
        /// </summary>
        public EquipmentPlannedActivityPresenter()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EquipmentPlannedActivityPresenter"/> given a planned activity.
        /// </summary>
        /// <param name="plannedActivity">The planned activity to decorate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="plannedActivity"/></exception>
        public EquipmentPlannedActivityPresenter(IPlannedActivity plannedActivity)
            : base()
        {
        }
    }
}
