using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.Construction;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Construction
{
    /// <summary>
    /// Implementation of the presenter view model decorating and impersonating in the UI a planned resource of an equipment budget
    /// component.
    /// </summary>
    public class ConstructionPlannedResourcePresenter :
        PlannedResourcePresenterBase<IConstructionComponent, IConstructionPlannedResourceManagerApplicationServices>, IConstructionPlannedResourcePresenter
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConstructionPlannedResourcePresenter"/>.
        /// </summary>
        public ConstructionPlannedResourcePresenter()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ConstructionPlannedResourcePresenter"/> given a planned resource.
        /// </summary>
        /// <param name="plannedResource">The planned resource to decorate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="plannedResource"/></exception>
        public ConstructionPlannedResourcePresenter(IPlannedResource plannedResource)
            : base(plannedResource)
        {
        }
    }
}
