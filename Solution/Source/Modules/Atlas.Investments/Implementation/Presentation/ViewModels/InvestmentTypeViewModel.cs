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
    ///     Implementation of the contract <see cref="IInvestmentTypeViewModel" /> being the CRUD view model to manage the CRUD
    ///     operations for Investment Type instances in the UI.
    /// </summary>
    public class InvestmentTypeViewModel : CrudViewModelBase<IInvestmentType, IInvestmentTypePresenter, IInvestmentTypeManagerApplicationServices>, IInvestmentTypeViewModel, IInvestmentTypeProvider
    {
        private static IInvestmentTypeProvider _phaseProvider;
        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        public static IEnumerable<IInvestmentTypePresenter> InvestmentTypes
        {
            get
            {
                if (_phaseProvider == null)
                    _phaseProvider = ServiceLocator.Current.GetInstance<IInvestmentTypeProvider>();

                return _phaseProvider.InvestmentTypes;
            }
        }


        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        IEnumerable<IInvestmentTypePresenter> IInvestmentTypeProvider.InvestmentTypes
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
            OnPropertyChanged("InvestmentTypes");
        }
        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was added an OSDE.
        /// </summary>
        /// <param name="addedPresenter">The added presenter which addition will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     added the given presenter element at <paramref name="addedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyAddedElementMessage(IInvestmentTypePresenter addedPresenter)
        {
            return Resources.SuccessfullyAddedInvestmentType.EasyFormat(addedPresenter);
        }

        /// <summary>
        ///     Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        ///     with the operation.
        /// </summary>
        /// <param name="presenter">The presenter view model that is about to be deleted.</param>
        protected override string GetDeleteConfirmationMessage(IInvestmentTypePresenter presenter)
        {
            return Resources.SureToDeleteInvestmentType.EasyFormat(presenter);
        }

        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was deleted an OSDE.
        /// </summary>
        /// <param name="deletedPresenter">The deleted presenter which deletion will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     deleted the given presenter element at <paramref name="deletedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyDeletedElementMessage(IInvestmentTypePresenter deletedPresenter)
        {
            return Resources.SuccessfullyDeletedInvestmentType.EasyFormat(deletedPresenter);
        }
    }
}