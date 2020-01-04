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
    ///     Implementation of the contract <see cref="IWageScaleViewModel" /> being the CRUD view model to manage the CRUD
    ///     operations for Wage Scale instances in the UI.
    /// </summary>
    public class WageScaleViewModel :
        CrudViewModelBase<IWageScale, IWageScalePresenter, IWageScaleManagerApplicationServices>,
        IWageScaleViewModel, IWageScaleProvider
    {
        private static IWageScaleProvider _wageScaleProvider;


        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        public static IEnumerable<IWageScalePresenter> WageScales
        {
            get
            {
                if (_wageScaleProvider == null)
                    _wageScaleProvider = ServiceLocator.Current.GetInstance<IWageScaleProvider>();

                return _wageScaleProvider.WageScales;
            }
        }


        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        IEnumerable<IWageScalePresenter> IWageScaleProvider.WageScales
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
            OnPropertyChanged("WageScales");
        }
        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was added an Wage Scale.
        /// </summary>
        /// <param name="addedPresenter">The added presenter which addition will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     added the given presenter element at <paramref name="addedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyAddedElementMessage(IWageScalePresenter addedPresenter)
        {
            return Resources.SuccessfullyAddedWageScale.EasyFormat(addedPresenter);
        }

        /// <summary>
        ///     Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        ///     with the operation.
        /// </summary>
        /// <param name="presenter">The presenter view model that is about to be deleted.</param>
        protected override string GetDeleteConfirmationMessage(IWageScalePresenter presenter)
        {
            return Resources.SureToDeleteWageScale.EasyFormat(presenter);
        }

        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was deleted an Wage Scale.
        /// </summary>
        /// <param name="deletedPresenter">The deleted presenter which deletion will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     deleted the given presenter element at <paramref name="deletedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyDeletedElementMessage(IWageScalePresenter deletedPresenter)
        {
            return Resources.SuccessfullyDeletedWageScale.EasyFormat(deletedPresenter);
        }

        public IEnumerable<IWageScale> GetItems()
        {
            return base.GetItems(CreateServices());
        }
    }
}