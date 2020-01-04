using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Equipment;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.WorkCapital
{
    /// <summary>
    ///     Implementation of the presenter view model decorating and impersonating in the UI a planned resource of a work
    ///     capital budget component.
    /// </summary>
    public class WorkCapitalPlannedResourcePresenter :
        PlannedResourcePresenterBase<IWorkCapitalComponent, IWorkCapitalPlannedResourceManagerApplicationServices>,
        IWorkCapitalPlannedResourcePresenter
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="WorkCapitalPlannedResourcePresenter" />.
        /// </summary>
        public WorkCapitalPlannedResourcePresenter()
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="EquipmentPlannedResourcePresenter" /> given a planned resource.
        /// </summary>
        /// <param name="plannedResource">The planned resource to decorate.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="plannedResource" />
        /// </exception>
        public WorkCapitalPlannedResourcePresenter(IPlannedResource plannedResource)
            : base(plannedResource)
        {
        }
    }
}