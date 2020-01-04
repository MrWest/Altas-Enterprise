using System;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Implementation of the base contract <see cref="IPlannedResourceViewModel{TComponent, TPresenter}"/> representing the crud view
    /// model used to manage the planned resources of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the planned resources managed here.</typeparam>
    /// <typeparam name="TPresenter">The type of the presenter view models decorating the planned resources.</typeparam>
    /// <typeparam name="TServices">
    /// The type of the application services used the send to the data operations generated in the current crud view model.
    /// </typeparam>
    [Obsolete]
    public  class PlannedResourceViewModelBase<TComponent, TPresenter, TServices> :
        BudgetComponentItemViewModelBase<IPlannedResource, TPresenter,TComponent, TServices>,
        IPlannedResourceViewModel<TComponent>
        where TComponent : class, IBudgetComponentItem
        where TPresenter : class, IBudgetComponentResourcePresenter<IPlannedResource, TComponent>
        where TServices : class, IBudgetComponentResourceManagerApplicationServices<IPlannedResource, TComponent>
    {

        /// <summary>
        /// Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        /// with the operation.
        /// </summary>
        /// <param name="plannedResource">The planned resource that is about to be deleted.</param>
        /// <returns>
        /// A <see cref="string"/> representing the message confirming with the user whether he/she wants to continue with the deletion
        /// of planned resource or not.
        /// </returns>
        protected override string GetDeleteConfirmationMessage(TPresenter plannedResource)
        {
            return Resources.SureToDeletePlannedResource.EasyFormat(plannedResource);
        }

        /// <summary>
        /// Gets the confirmation message that is show when there is added a new planned resource.
        /// </summary>
        /// <param name="plannedResource">
        /// The planned resource containing the added planned resource.</param>
        /// <returns>
        /// A <see cref="string"/> representing the message notifying that a new planned resource has been added.
        /// </returns>
        protected override string GetSuccessfullyAddedElementMessage(TPresenter plannedResource)
        {
            return Resources.SuccessfullyAddedPlannedResource.EasyFormat(plannedResource);
        }

        /// <summary>
        /// Gets the confirmation message that is show when there is deleted a new planned resource.
        /// </summary>
        /// <param name="plannedResource">
        /// The planned resource containing the deleted planned resource.</param>
        /// <returns>
        /// A <see cref="string"/> representing the message notifying that a new planned resource has been deleted.
        /// </returns>
        protected override string GetSuccessfullyDeletedElementMessage(TPresenter plannedResource)
        {
            return Resources.SuccessfullyDeletedPlannedResource.EasyFormat(plannedResource);
        }
    }
}
