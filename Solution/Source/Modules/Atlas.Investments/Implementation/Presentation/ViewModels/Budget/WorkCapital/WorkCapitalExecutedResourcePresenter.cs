using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.WorkCapital
{
    /// <summary>
    ///     Implementation of the presenter view model decorating and impersonating in the UI a executed resource of a work
    ///     capital budget component.
    /// </summary>
    public class WorkCapitalExecutedResourcePresenter :
        ExecutedResourcePresenterBase<IWorkCapitalComponent, IWorkCapitalExecutedResourceManagerApplicationServices>,
        IWorkCapitalExecutedResourcePresenter
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="WorkCapitalExecutedResourcePresenter" />.
        /// </summary>
        public WorkCapitalExecutedResourcePresenter()
        {
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="WorkCapitalExecutedResourcePresenter" /> given a executed resource.
        /// </summary>
        /// <param name="executedResource">The executed resource to decorate.</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="executedResource" />
        /// </exception>
        public WorkCapitalExecutedResourcePresenter(IExecutedResource executedResource)
            : base(executedResource)
        {
        }
    }
}