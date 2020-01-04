using System;
using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    /// <summary>
    /// Implements ICurrencyViewModel
    /// </summary>
    public class CurrencyViewModel : ConvertibleEntityViewModel<ICurrency, ICurrencyPresenter, ICurrencyManagerApplicationServices>, ICurrencyViewModel, ICurrencyProvider
    {
        private static ICurrencyProvider _currencyProvider;
        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        public static IEnumerable<ICurrencyPresenter> Currencies
        {
            get
            {
                if (_currencyProvider == null)
                    _currencyProvider = ServiceLocator.Current.GetInstance<ICurrencyProvider>();

                return _currencyProvider.Currencies;
            }
        }


        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        IEnumerable<ICurrencyPresenter> ICurrencyProvider.Currencies
        {
            get
            {
                if (!IsLoaded)
                    Load();
                return Items;
            }
        }

        protected override void OnAddedItem(object sender, EventArgs e)
        {
            base.OnAddedItem(sender, e);
           
            OnPropertyChanged("Currencies");
        }
        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was added an Category.
        /// </summary>
        /// <param name="addedPresenter">The added presenter which addition will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     added the given presenter element at <paramref name="addedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyAddedElementMessage(ICurrencyPresenter addedPresenter)
        {
            return Resources.SuccessfullyAddedCurrency.EasyFormat(addedPresenter);
        }

        /// <summary>
        ///     Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        ///     with the operation.
        /// </summary>
        /// <param name="presenter">The presenter view model that is about to be deleted.</param>
        protected override string GetDeleteConfirmationMessage(ICurrencyPresenter presenter)
        {
            return Resources.SureToDeleteCurrency.EasyFormat(presenter);
        }

        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was deleted an Category.
        /// </summary>
        /// <param name="deletedPresenter">The deleted presenter which deletion will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     deleted the given presenter element at <paramref name="deletedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyDeletedElementMessage(ICurrencyPresenter deletedPresenter)
        {
            return Resources.SuccessfullyDeletedCurrency.EasyFormat(deletedPresenter);
        }
    }
}
