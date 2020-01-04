using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    /// <summary>
    /// Implementation of the contract <see cref="IConvertibleEntityPresenter{TEntity}" /> being the CRUD view model to manage the CRUD
    ///     operations for Category instances in the UI.
    /// </summary>
    public class ConvertibleEntityViewModel<TEntity> : CrudViewModelBase<TEntity, IConvertibleEntityPresenter<TEntity>, IConvertibleEntityManagerApplicationServices<TEntity>>,
        IConvertibleEntityViewModel<TEntity>
        where TEntity : class ,IConvertibleEntity
    {

        /// <summary>
        /// When overridden in a deriver it allows it handle the AddedItem event.
        /// </summary>
        /// <param name="sender">The object raising the event.</param>
        /// <param name="e">The information generated in the event with details about the addition.</param>
        protected override void OnAddedItem(object sender, EventArgs e)
        {
            base.OnAddedItem(sender, e);
            ItemEventArgs<IConvertibleEntityPresenter<TEntity>> arguments;
            if (!CheckIsItemEventArgs(e, out arguments))
                return;


        }

        /// <summary>
        /// When overridden in a deriver it allows it handle the DeletedItem event.
        /// </summary>
        /// <param name="sender">The object raising the event.</param>
        /// <param name="e">The information generated in the event with details about the deletion.</param>
        protected override void OnDeletedItem(object sender, EventArgs e)
        {
            base.OnDeletedItem(sender, e);
            ItemEventArgs<IConvertibleEntityPresenter<TEntity>> arguments;
            if (!CheckIsItemEventArgs(e, out arguments))
                return;



        }
        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was added an Category.
        /// </summary>
        /// <param name="addedPresenter">The added presenter which addition will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     added the given presenter element at <paramref name="addedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyAddedElementMessage(IConvertibleEntityPresenter<TEntity> addedPresenter)
        {
            return Resources.SuccessfullyAddedConvertible.EasyFormat(addedPresenter);
        }

        /// <summary>
        ///     Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        ///     with the operation.
        /// </summary>
        /// <param name="presenter">The presenter view model that is about to be deleted.</param>
        protected override string GetDeleteConfirmationMessage(IConvertibleEntityPresenter<TEntity> presenter)
        {
            return Resources.SureToDeleteConvertible.EasyFormat(presenter);
        }

        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was deleted an Category.
        /// </summary>
        /// <param name="deletedPresenter">The deleted presenter which deletion will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     deleted the given presenter element at <paramref name="deletedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyDeletedElementMessage(IConvertibleEntityPresenter<TEntity> deletedPresenter)
        {
            return Resources.SuccessfullyDeletedConvertible.EasyFormat(deletedPresenter);
        }

        public IEnumerable<TEntity> GetItems()
        {
            return base.GetItems(CreateServices());
        }
    }

    /// <summary>
    /// Implementation of the contract <see cref="IConvertibleEntityPresenter" /> being the CRUD view model to manage the CRUD
    ///     operations for Category instances in the UI.
    /// </summary>
    public class ConvertibleEntityViewModel<TEntity, TPresenter, TService> : CrudViewModelBase<TEntity, TPresenter, TService>,
        IConvertibleEntityViewModel<TEntity,TPresenter>
        where TEntity : class ,IConvertibleEntity
        where TPresenter : class ,IConvertibleEntityPresenter<TEntity>
        where TService : class , IConvertibleEntityManagerApplicationServices<TEntity>
    {
        protected override void OnAddedItem(object sender, EventArgs e)
        {
            base.OnAddedItem(sender, e);
            _isLoaded = false;
        }

        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was added an Category.
        /// </summary>
        /// <param name="addedPresenter">The added presenter which addition will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     added the given presenter element at <paramref name="addedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyAddedElementMessage(TPresenter addedPresenter)
        {
            return Resources.SuccessfullyAddedConvertible.EasyFormat(addedPresenter);
        }

        /// <summary>
        ///     Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        ///     with the operation.
        /// </summary>
        /// <param name="presenter">The presenter view model that is about to be deleted.</param>
        protected override string GetDeleteConfirmationMessage(TPresenter presenter)
        {
            return Resources.SureToDeleteConvertible.EasyFormat(presenter);
        }

        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was deleted an Category.
        /// </summary>
        /// <param name="deletedPresenter">The deleted presenter which deletion will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     deleted the given presenter element at <paramref name="deletedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyDeletedElementMessage(TPresenter deletedPresenter)
        {
            return Resources.SuccessfullyDeletedConvertible.EasyFormat(deletedPresenter);
        }

        public IEnumerable<TEntity> GetItems()
        {
            return base.GetItems(CreateServices());
        }

        public void AddFromScratch(string muname, string s)
        {
            ExecuteUsingServices(services =>
            {
                services.AddFromScratch(muname, s);
            });

          //  Load();
        }

        public IConvertibleEntityPresenter<TEntity> GetConvertible(string letters)
        {
            var convertible = CreateServices().Items.SingleOrDefault(x =>
                string.Equals(x.Letters.ToLower(), letters.ToLower(),
                    StringComparison.Ordinal));

            return CreatePresenterFor(convertible);
        }

        public bool ExistsConvertible(string letters)
        {
            return CreateServices().Items.Any(x =>
                 string.Equals(x.Letters.ToLower(), letters.ToLower(),
                     StringComparison.Ordinal));
        }
    }
   
}
