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
    /// implements <see cref="IMeasurementUnitViewModel"/>
    /// </summary>
    public class MeasurementUnitViewModel : ConvertibleEntityViewModel<IMeasurementUnit, IMeasurementUnitPresenter, IMeasurementUnitManagerApplicationServices>, IMeasurementUnitViewModel, IMeasurementUnitProvider
    {
        private static IMeasurementUnitProvider _measurementUnitProvider;
        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        public static IEnumerable<IMeasurementUnitPresenter> MeasurementUnits
        {
            get
            {
                if (_measurementUnitProvider == null)
                    _measurementUnitProvider = ServiceLocator.Current.GetInstance<IMeasurementUnitProvider>();

               

                return _measurementUnitProvider.MeasurementUnits;
            }
        }


        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        IEnumerable<IMeasurementUnitPresenter> IMeasurementUnitProvider.MeasurementUnits
        {
            get
            {
                if(!IsLoaded)
                Load();
              
                return Items;
            }
        }

        protected override void OnAddedItem(object sender, EventArgs e)
        {
            base.OnAddedItem(sender, e);
            OnPropertyChanged("MeasurementUnits");
        }
        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was added an Category.
        /// </summary>
        /// <param name="addedPresenter">The added presenter which addition will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     added the given presenter element at <paramref name="addedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyAddedElementMessage(IMeasurementUnitPresenter addedPresenter)
        {
            return Resources.SuccessfullyAddedMeasurementUnit.EasyFormat(addedPresenter);
        }

        /// <summary>
        ///     Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        ///     with the operation.
        /// </summary>
        /// <param name="presenter">The presenter view model that is about to be deleted.</param>
        protected override string GetDeleteConfirmationMessage(IMeasurementUnitPresenter presenter)
        {
            return Resources.SureToDeleteMeasurementUnit.EasyFormat(presenter);
        }

        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was deleted an Category.
        /// </summary>
        /// <param name="deletedPresenter">The deleted presenter which deletion will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     deleted the given presenter element at <paramref name="deletedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyDeletedElementMessage(IMeasurementUnitPresenter deletedPresenter)
        {
            return Resources.SuccessfullyDeletedMeasurementUnit.EasyFormat(deletedPresenter);
        }
    }
}
