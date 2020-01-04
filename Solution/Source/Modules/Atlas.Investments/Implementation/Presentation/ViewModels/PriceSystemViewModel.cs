using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Implementation of the contract <see cref="IPriceSystemViewModel" /> being the CRUD view model to manage the CRUD
    ///     operations for price system instances in the UI.
    /// </summary>
    public  class PriceSystemViewModel : CrudViewModelBase<IPriceSystem, IPriceSystemPresenter, IPriceSystemManagerApplicationService>, IPriceSystemViewModel, IPriceSystemProvider
    {
        private static IPriceSystemProvider _priceSystemProvider;

       
        public override void Load()
        {
            base.Load();
            if (Items.Count > 0 && Items.All(x => !x.IsActive))
            {
                SelectedItem = Items[0];
            }
            if (Items.Count > 0 && Items.Any(x => x.IsActive))
            {
                SelectedItem = Items.Single(x => x.IsActive);
            }
        }

        public override IPriceSystemPresenter SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (!Equals(_selectedItem, null))
                    _selectedItem.Object.IsActive = false;

                SetProperty(ref _selectedItem, value);
                if (!Equals(_selectedItem, null))
                    _selectedItem.IsActive = true;
            }
        }

        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        public static IEnumerable<IPriceSystemPresenter> PriceSystems
        {
            get
            {
                if (_priceSystemProvider == null)
                    _priceSystemProvider = ServiceLocator.Current.GetInstance<IPriceSystemProvider>();

                return _priceSystemProvider.PriceSystems;
            }
        }


        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        IEnumerable<IPriceSystemPresenter> IPriceSystemProvider.PriceSystems
        {
            get
            {
                if(!IsLoaded)
                Load();
                return Items;
            }
        }
        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was added an Category.
        /// </summary>
        /// <param name="addedPresenter">The added presenter which addition will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     added the given presenter element at <paramref name="addedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyAddedElementMessage(IPriceSystemPresenter addedPresenter)
        {
            return Resources.SuccessfullyAddedPriceSystem.EasyFormat(addedPresenter);
        }

        /// <summary>
        ///     Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        ///     with the operation.
        /// </summary>
        /// <param name="presenter">The presenter view model that is about to be deleted.</param>
        protected override string GetDeleteConfirmationMessage(IPriceSystemPresenter presenter)
        {
            return Resources.SureToDeletePriceSystem.EasyFormat(presenter);
        }

        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was deleted an Category.
        /// </summary>
        /// <param name="deletedPresenter">The deleted presenter which deletion will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     deleted the given presenter element at <paramref name="deletedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyDeletedElementMessage(IPriceSystemPresenter deletedPresenter)
        {
            return Resources.SuccessfullyDeletedPriceSystem.EasyFormat(deletedPresenter);
        }

        public override bool CanAdd(IPriceSystemPresenter presenter)
        {
            return true;
        }

        public override bool CanDelete(IPriceSystemPresenter presenter)
        {
            return true;
        }

       
      
    }
}
