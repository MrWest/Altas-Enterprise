using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Implementation of the base contract <see cref="IExecutedResourcePresenter{TComponent}"/> representing the presenter view model
    /// decorating and impersonating in the UI a executed resource of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the decorated executed resource.</typeparam>
    /// <typeparam name="TServices">
    /// The type of the application services used to make the updates made to the decorated executed resource.
    /// </typeparam>
    public abstract class ExecutedResourcePresenterBase<TComponent, TServices> :
        BudgetComponentItemPresenterBase<IExecutedResource, TServices>,
        IExecutedResourcePresenter<TComponent>
        where TComponent : IBudgetComponent
        where TServices : class, IBudgetComponentItemManagerApplicationServices<IExecutedResource>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ExecutedResourcePresenterBase{TComponent, TServices}"/>.
        /// </summary>
        protected ExecutedResourcePresenterBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ExecutedResourcePresenterBase{TComponent, TServices}"/> given the
        /// executed resource.
        /// </summary>
        /// <param name="executedResource">
        /// The <see cref="IBudgetComponentItem"/> to decorate by the initializing
        /// <see cref="BudgetComponentItemPresenterBase{TItem, TComponent, TServices}"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="executedResource"/> is null.</exception>
        protected ExecutedResourcePresenterBase(IExecutedResource executedResource)
            : base(executedResource)
        {
        }


        /// <summary>
        /// Gets the message that is displayed to the user when the current executed resource presenter view model has changed.
        /// </summary>
        protected override string SucessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedExecutedResource; }
        }
    }
}
