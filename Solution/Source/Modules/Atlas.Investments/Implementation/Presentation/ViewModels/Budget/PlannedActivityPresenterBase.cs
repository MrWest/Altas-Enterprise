using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Implementation of the base contract <see cref="IPlannedActivityPresenter{TComponent}"/> representing the presenter view model
    /// decorating and impersonating in the UI a planned resource of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the decorated planned resource.</typeparam>
    /// <typeparam name="TServices">
    /// The type of the application services used to make the updates made to the decorated planned resource.
    /// </typeparam>
    public abstract class PlannedActivityPresenterBase<TComponent, TServices> :
        BudgetComponentItemPresenterBase<IPlannedActivity, TComponent, TServices>,
        IPlannedActivityPresenter<TComponent>
        where TComponent : IBudgetComponent
        where TServices : class, IBudgetComponentItemManagerApplicationServices<IPlannedActivity>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PlannedActivityPresenterBase{TComponent, TServices}"/>.
        /// </summary>
        protected PlannedActivityPresenterBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="PlannedActivityPresenterBase{TComponent, TServices}"/> given the
        /// planned resource.
        /// </summary>
        /// <param name="plannedActivity">
        /// The <see cref="IBudgetComponentItem"/> to decorate by the initializing
        /// <see cref="BudgetComponentItemPresenterBase{TItem, TComponent, TServices}"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="plannedActivity"/> is null.</exception>
        protected PlannedActivityPresenterBase(IPlannedActivity plannedActivity)
            : base(plannedActivity)
        {
        }


        /// <summary>
        /// Gets the message that is displayed to the user when the current planned activity presenter view model has changed.
        /// </summary>
        protected override string SucessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedPlannedActivity; }
        }
    }
}
