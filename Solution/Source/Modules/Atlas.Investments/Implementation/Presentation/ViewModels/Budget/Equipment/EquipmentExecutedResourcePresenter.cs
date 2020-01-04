using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.Equipment;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Equipment
{
    /// <summary>
    /// Implementation of the presenter view model decorating and impersonating in the UI a executed resource of an equipment budget
    /// component.
    /// </summary>
    public class EquipmentExecutedResourcePresenter :
        ExecutedResourcePresenterBase<IEquipmentComponent, IEquipmentExecutedResourceManagerApplicationServices>, IEquipmentExecutedResourcePresenter
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EquipmentExecutedResourcePresenter"/>.
        /// </summary>
        public EquipmentExecutedResourcePresenter()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="EquipmentExecutedResourcePresenter"/> given a executed resource.
        /// </summary>
        /// <param name="executedResource">The executed resource to decorate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="executedResource"/></exception>
        public EquipmentExecutedResourcePresenter(IExecutedResource executedResource)
            : base(executedResource)
        {
        }
    }
}
