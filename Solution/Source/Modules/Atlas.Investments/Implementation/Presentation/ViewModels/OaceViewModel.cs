using System;
using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Implementation of the contract <see cref="IOaceViewModel" /> being the CRUD view model to manage the CRUD
    ///     operations for OACE instances in the UI.
    /// </summary>
    public class OaceViewModel : CrudViewModelBase<IOace, IOacePresenter, IOaceManagerApplicationServices>, IOaceViewModel, IOaceProvider
    {
        private static IOaceProvider _oaceProvider;

        public OaceViewModel() 
        {
            
        }
        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        public static IEnumerable<IOacePresenter> Oaces
        {
            get
            {
                if (_oaceProvider == null)
                    _oaceProvider = ServiceLocator.Current.GetInstance<IOaceProvider>();
                var check = ServiceLocator.Current.GetAllInstances<IOaceProvider>();
                return _oaceProvider.Oaces;
            }
        }


        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        IEnumerable<IOacePresenter> IOaceProvider.Oaces
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
            OnPropertyChanged("Oaces");
        }
        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was added an OACE.
        /// </summary>
        /// <param name="addedPresenter">The added presenter which addition will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     added the given presenter element at <paramref name="addedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyAddedElementMessage(IOacePresenter addedPresenter)
        {
            return Resources.SuccessfullyAddedOace.EasyFormat(addedPresenter);
        }

        /// <summary>
        ///     Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        ///     with the operation.
        /// </summary>
        /// <param name="presenter">The presenter view model that is about to be deleted.</param>
        protected override string GetDeleteConfirmationMessage(IOacePresenter presenter)
        {
            return Resources.SureToDeleteOace.EasyFormat(presenter);
        }

        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was deleted an OACE.
        /// </summary>
        /// <param name="deletedPresenter">The deleted presenter which deletion will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     deleted the given presenter element at <paramref name="deletedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyDeletedElementMessage(IOacePresenter deletedPresenter)
        {
            return Resources.SuccessfullyDeletedOace.EasyFormat(deletedPresenter);
        }
        public IEnumerable<IOace> GetItems()
        {
            return base.GetItems(CreateServices());
        }
    }
}