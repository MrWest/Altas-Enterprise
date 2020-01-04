using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.Equipment;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Equipment
{
    /// <summary>
    /// Implementation of the presenter view model decorating and impersonating in the UI a planned resource of an equipment budget
    /// component.
    /// </summary>
    public class EquipmentPlannedResourcePresenter :
        PlannedResourcePresenterBase<IEquipmentComponent, IEquipmentPlannedResourceManagerApplicationServices>, IEquipmentPlannedResourcePresenter
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EquipmentPlannedResourcePresenter"/>.
        /// </summary>
        public EquipmentPlannedResourcePresenter()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EquipmentPlannedResourcePresenter"/> given a planned resource.
        /// </summary>
        /// <param name="plannedResource">The planned resource to decorate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="plannedResource"/></exception>
        public EquipmentPlannedResourcePresenter(IPlannedResource plannedResource)
            : base(plannedResource)
        {
        }
    }
}
