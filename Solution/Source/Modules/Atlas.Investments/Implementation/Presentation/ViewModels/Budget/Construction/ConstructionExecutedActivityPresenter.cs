using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.Construction;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Construction
{
    /// <summary>
    /// Implementation of the presenter view model decorating and impersonating in the UI a executed activity of an equipment budget
    /// component.
    /// </summary>
    public class ConstructionExecutedActivityPresenter :
        ExecutedActivityPresenterBase, IConstructionExecutedActivityPresenter
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConstructionExecutedActivityPresenter"/>.
        /// </summary>
        public ConstructionExecutedActivityPresenter()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ConstructionExecutedActivityPresenter"/> given a executed activity.
        /// </summary>
        /// <param name="executedActivity">The executed activity to decorate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="executedActivity"/></exception>
        //public ConstructionExecutedActivityPresenter(IExecutedActivity executedActivity)
        //    : base(executedActivity)
        //{
        //}
    }
}
