using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.Equipment;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Equipment
{
    /// <summary>
    /// Implementation of the presenter view model decorating and impersonating in the UI a executed activity of an equipment budget
    /// component.
    /// </summary>
    public class EquipmentExecutedActivityPresenter :
        ExecutedActivityPresenterBase, IEquipmentExecutedActivityPresenter
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EquipmentExecutedActivityPresenter"/>.
        /// </summary>
        public EquipmentExecutedActivityPresenter()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EquipmentExecutedActivityPresenter"/> given a executed activity.
        /// </summary>
        /// <param name="executedActivity">The executed activity to decorate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="executedActivity"/></exception>
        //public EquipmentExecutedActivityPresenter(IExecutedActivity executedActivity)
        //    : base(executedActivity)
        //{
        //}
    }
}
