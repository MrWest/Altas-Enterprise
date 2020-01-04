using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Implementation of the base contract <see cref="IExecutedResourceViewModel{TComponent, TPresenter}"/> representing the crud view
    /// model used to manage the executed resources of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the executed resources managed here.</typeparam>
    /// <typeparam name="TPresenter">The type of the presenter view models decorating the executed resources.</typeparam>
    /// <typeparam name="TServices">
    /// The type of the application services used the send to the data operations generated in the current crud view model.
    /// </typeparam>
    public abstract class ExecutedResourceViewModelBase<TComponent, TPresenter, TServices> :
        ExecutedBudgetComponentItemViewModelBase<IExecutedResource, TPresenter, TComponent, TServices>,
        IExecutedResourceViewModel<TComponent, TPresenter>
        where TComponent : class, IBudgetComponent
        where TPresenter : class, IBudgetComponentItemPresenter<IExecutedResource>
        where TServices : class, IExecutedBudgetComponentItemManagerApplicationServices<IExecutedResource>
    {

        /// <summary>
        /// Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        /// with the operation.
        /// </summary>
        /// <param name="executedResource">The executed resource that is about to be deleted.</param>
        /// <returns>
        /// A <see cref="string"/> representing the message confirming with the user whether he/she wants to continue with the deletion
        /// of executed resource or not.
        /// </returns>
        protected override string GetDeleteConfirmationMessage(TPresenter executedResource)
        {
            return Resources.SureToDeleteExecutedResource.EasyFormat(executedResource);
        }

        /// <summary>
        /// Gets the confirmation message that is show when there is added a new executed resource.
        /// </summary>
        /// <param name="executedResource">
        /// The executed resource containing the added executed resource.</param>
        /// <returns>
        /// A <see cref="string"/> representing the message notifying that a new executed resource has been added.
        /// </returns>
        protected override string GetSuccessfullyAddedElementMessage(TPresenter executedResource)
        {
            return Resources.SuccessfullyAddedExecutedResource.EasyFormat(executedResource);
        }

        /// <summary>
        /// Gets the confirmation message that is show when there is deleted a new executed resource.
        /// </summary>
        /// <param name="executedResource">
        /// The executed resource containing the deleted executed resource.</param>
        /// <returns>
        /// A <see cref="string"/> representing the message notifying that a new executed resource has been deleted.
        /// </returns>
        protected override string GetSuccessfullyDeletedElementMessage(TPresenter executedResource)
        {
            return Resources.SuccessfullyDeletedExecutedResource.EasyFormat(executedResource);
        }
    }
}
