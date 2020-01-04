using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.OtherExpenses
{
    /// <summary>
    ///     Implementation of the presenter view model decorating and impersonating in the UI a planned resource of an other
    ///     expenses budget component.
    /// </summary>
    public class OtherExpensesPlannedResourcePresenter :
        PlannedResourcePresenterBase<IOtherExpensesComponent, IOtherExpensesPlannedResourceManagerApplicationServices>,
        IOtherExpensesPlannedResourcePresenter
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="OtherExpensesPlannedResourcePresenter" />.
        /// </summary>
        public OtherExpensesPlannedResourcePresenter()
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="OtherExpensesPlannedResourcePresenter" /> given a planned resource.
        /// </summary>
        /// <param name="plannedResource">The planned resource to decorate.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="plannedResource" />
        /// </exception>
        public OtherExpensesPlannedResourcePresenter(IPlannedResource plannedResource)
            : base(plannedResource)
        {
        }
    }
}