using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.Construction;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Construction
{
    /// <summary>
    /// Implementation of the presenter view model decorating and impersonating in the UI a executed resource of an equipment budget
    /// component.
    /// </summary>
    public class ConstructionExecutedResourcePresenter :
        ExecutedResourcePresenterBase<IConstructionComponent, IConstructionExecutedResourceManagerApplicationServices>, IConstructionExecutedResourcePresenter
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConstructionExecutedResourcePresenter"/>.
        /// </summary>
        public ConstructionExecutedResourcePresenter()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ConstructionExecutedResourcePresenter"/> given a executed resource.
        /// </summary>
        /// <param name="executedResource">The executed resource to decorate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="executedResource"/></exception>
        public ConstructionExecutedResourcePresenter(IExecutedResource executedResource)
            : base(executedResource)
        {
        }
    }
}
