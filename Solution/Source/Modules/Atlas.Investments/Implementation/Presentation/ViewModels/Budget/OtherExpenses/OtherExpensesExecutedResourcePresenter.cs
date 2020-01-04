using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.OtherExpenses
{
    /// <summary>
    ///     Implementation of the presenter view model decorating and impersonating in the UI a executed resource of an other
    ///     expenses budget component.
    /// </summary>
    public class OtherExpensesExecutedResourcePresenter :
        ExecutedResourcePresenterBase<IOtherExpensesComponent, IOtherExpensesExecutedResourceManagerApplicationServices>,
        IOtherExpensesExecutedResourcePresenter
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="OtherExpensesExecutedResourcePresenter" />.
        /// </summary>
        public OtherExpensesExecutedResourcePresenter()
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="OtherExpensesExecutedResourcePresenter" /> given a executed resource.
        /// </summary>
        /// <param name="executedResource">The executed resource to decorate.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="executedResource" />
        /// </exception>
        public OtherExpensesExecutedResourcePresenter(IExecutedResource executedResource)
            : base(executedResource)
        {
        }
    }
}