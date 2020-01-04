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
    ///     Implementation of the contract <see cref="IExpenseConceptViewModel" /> being the CRUD view model to manage the CRUD
    ///     operations for Expense Concept instances in the UI.
    /// </summary>
    public class ExpenseConceptViewModel : CrudViewModelBase<IExpenseConcept, IExpenseConceptPresenter, IExpenseConceptManagerApplicationServices>, IExpenseConceptViewModel, IExpenseConceptProvider
    {
        private static IExpenseConceptProvider _expenseConceptProvider;
        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        public static IEnumerable<IExpenseConceptPresenter> ExpenseConcepts
        {
            get
            {
                if (_expenseConceptProvider == null)
                    _expenseConceptProvider = ServiceLocator.Current.GetInstance<IExpenseConceptProvider>();

                return _expenseConceptProvider.ExpenseConcepts;
            }
        }


        /// <summary>
        ///     Gets the <see cref="IEnumerable{T}" /> containing the presenter view models (<see cref="IWageScalePresenter" />)
        ///     containing the <see cref="IWageScale" /> domain entities in the system.
        /// </summary>
        IEnumerable<IExpenseConceptPresenter> IExpenseConceptProvider.ExpenseConcepts
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
            OnPropertyChanged("ExpenseConcepts");
        }
        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was added an Expense Concept.
        /// </summary>
        /// <param name="addedPresenter">The added presenter which addition will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     added the given presenter element at <paramref name="addedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyAddedElementMessage(IExpenseConceptPresenter addedPresenter)
        {
            return Resources.SuccessfullyAddedExpenseConcept.EasyFormat(addedPresenter);
        }

        /// <summary>
        ///     Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        ///     with the operation.
        /// </summary>
        /// <param name="presenter">The presenter view model that is about to be deleted.</param>
        protected override string GetDeleteConfirmationMessage(IExpenseConceptPresenter presenter)
        {
            return Resources.SureToDeleteExpenseConcept.EasyFormat(presenter);
        }

        /// <summary>
        ///     Gets the message that should be displayed in the status component when there was deleted an Expense Concept.
        /// </summary>
        /// <param name="deletedPresenter">The deleted presenter which deletion will be notified.</param>
        /// <returns>
        ///     A string representing the full message to display in order to notify the user that there was successfully
        ///     deleted the given presenter element at <paramref name="deletedPresenter" />.
        /// </returns>
        protected override string GetSuccessfullyDeletedElementMessage(IExpenseConceptPresenter deletedPresenter)
        {
            return Resources.SuccessfullyDeletedExpenseConcept.EasyFormat(deletedPresenter);
        }

        /// <summary>
        /// Just what it says
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IExpenseConcept> GetItems()
        {
            return base.GetItems(CreateServices());
        }
    }
}