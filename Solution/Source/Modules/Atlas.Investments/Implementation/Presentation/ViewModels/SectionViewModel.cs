using System;
using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{

    /// <summary>
    ///     Implementation of the contract <see cref="ISectionViewModel" /> being the CRUD view model to manage the CRUD
    ///     operations for Section instances in the UI.
    /// </summary>
    public class SectionViewModel : CrudViewModelBase<ISection, ISectionPresenter, ISectionManagerApplicationService>, ISectionViewModel
    {
        private IGenericSectionPresenter _aboveSection;



        public IGenericSectionPresenter AboveSection
        {
            get
            {
                if (_aboveSection == null)
                    throw new InvalidOperationException(Resources.InitializeInvestmentComponentViewModelParentBeforeUsingIt);

                return _aboveSection;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _aboveSection = value;
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
        protected override string GetSuccessfullyAddedElementMessage(ISectionPresenter addedPresenter)
        {
            return Resources.SuccessfullyAddedSection.EasyFormat(addedPresenter);
        }

        /// <summary>
        ///     Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        ///     with the operation.
        /// </summary>
        /// <param name="presenter">The presenter view model that is about to be deleted.</param>
        protected override string GetDeleteConfirmationMessage(ISectionPresenter presenter)
        {
            return Resources.SureToDeleteSection.EasyFormat(presenter);
        }

        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was deleted an Category.
        /// </summary>
        /// <param name="deletedPresenter">The deleted presenter which deletion will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     deleted the given presenter element at <paramref name="deletedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyDeletedElementMessage(ISectionPresenter deletedPresenter)
        {
            return Resources.SuccessfullyDeletedSection.EasyFormat(deletedPresenter);
        }

        //public override bool CanAdd(ISectionPresenter presenter)
        //{
        //    return true;
        //}

        //public override bool CanDelete(ISectionPresenter presenter)
        //{
        //    return true;
        //}

        /// <summary>
        ///     Gets the application services used to send the data operations originated in the current
        ///     <see cref="BudgetComponentItemViewModelBase{TItem,TPresenter,TComponent,TServices}" />.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="TServices" />.</returns>
        protected override ISectionManagerApplicationService CreateServices()
        {
            ISectionManagerApplicationService services = base.CreateServices();
            services.AboveSection = AboveSection.Object as IPriceSystem;

            return services;
        }

        /// <summary>
        ///     Creates a new presenter view model decorating the given investment component.
        /// </summary>
        /// <param name="investmentComponent">The <see cref="IInvestmentComponent" /> to wrap into an presenter view model.</param>
        /// <returns>
        ///     A new instance of <see cref="IInvestmentComponentPresenter" /> decorating
        ///     <paramref name="investmentComponent" />.
        /// </returns>
        protected override ISectionPresenter CreatePresenterFor(ISection sectionPresenter)
        {
            ISectionPresenter component = base.CreatePresenterFor(sectionPresenter);
            component.AboveSection = AboveSection;

            return component;
        }


        
    }
}
