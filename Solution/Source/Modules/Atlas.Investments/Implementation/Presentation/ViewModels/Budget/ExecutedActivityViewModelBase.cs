using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Microsoft.Practices.Prism.Commands;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Implementation of the base contract <see cref="IExecutedActivityViewModel{TComponent, TPresenter}"/> representing the crud view
    /// model used to manage the executed activities of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the executed activities managed here.</typeparam>
    /// <typeparam name="TPresenter">The type of the presenter view models decorating the executed activities.</typeparam>
    /// <typeparam name="TServices">
    /// The type of the application services used the send to the data operations generated in the current crud view model.
    /// </typeparam>
    public  class ExecutedActivityViewModelBase:
        BudgetComponentItemViewModelBase<IExecutedActivity, IExecutedActivityPresenter, IExecutedActivityItemManagerApplicationServices>,
        IExecutedActivityViewModel
        //where TComponent : class, IBudgetComponent
        //where TPresenter : class, IExecutedActivityPresenter<TComponent>
        //where TServices : class, IExecutedBudgetComponentItemManagerApplicationServices<TComponent>
    {
        protected BackgroundWorker _BackgroundWorkerMine;

      
        /// <summary>
        /// Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        /// with the operation.
        /// </summary>
        /// <param name="executedActivity">The executed activity that is about to be deleted.</param>
        /// <returns>
        /// A <see cref="string"/> representing the message confirming with the user whether he/she wants to continue with the deletion
        /// of executed activity or not.
        /// </returns>
        protected override string GetDeleteConfirmationMessage(IExecutedActivityPresenter executedActivity)
        {
            return Resources.SureToDeleteExecutedActivity.EasyFormat(executedActivity);
        }

        /// <summary>
        /// Gets the confirmation message that is show when there is added a new executed activity.
        /// </summary>
        /// <param name="executedActivity">
        /// The executed activity containing the added executed activity.</param>
        /// <returns>
        /// A <see cref="string"/> representing the message notifying that a new executed activity has been added.
        /// </returns>
        protected override string GetSuccessfullyAddedElementMessage(IExecutedActivityPresenter executedActivity)
        {
            return Resources.SuccessfullyAddedExecutedActivity.EasyFormat(executedActivity);
        }

        /// <summary>
        /// Gets the confirmation message that is show when there is deleted a new executed activity.
        /// </summary>
        /// <param name="executedActivity">
        /// The executed activity containing the deleted executed activity.</param>
        /// <returns>
        /// A <see cref="string"/> representing the message notifying that a new executed activity has been deleted.
        /// </returns>
        protected override string GetSuccessfullyDeletedElementMessage(IExecutedActivityPresenter executedActivity)
        {
            return Resources.SuccessfullyDeletedExecutedActivity.EasyFormat(executedActivity);
        }

        protected override IExecutedActivityItemManagerApplicationServices CreateServices()
        {
            IExecutedActivityItemManagerApplicationServices service = base.CreateServices();
            //service.Component = Component.Object;
            service.SubSpecialityHolder = SubSpecialityHolder?.Object;
            return service;
            
        }

        protected override IExecutedActivityPresenter CreatePresenterFor(IExecutedActivity budgetComponentItem)
        {
            var presenter = base.CreatePresenterFor(budgetComponentItem);
            //presenter.Component = Component;
            presenter.SubSpecialityHolder = SubSpecialityHolder;
            var period = ServiceLocator.Current.GetInstance<IPeriodPresenter>();
            period.Object = budgetComponentItem.Period;
            period.Holder = presenter;
            presenter.Period = period;

            return presenter;
        }

        public ISubSpecialityHolderPresenter<IExecutedSubSpecialityHolder> SubSpecialityHolder { get; set; }

        public override bool CanAdd(IExecutedActivityPresenter presenter)
        {
            //if (!Equals(SubSpecialityHolder, null) && Equals(SubSpecialityHolder.SubSpeciality, null))
            //    return true;

            return Equals(SubSpecialityHolder, null)|| !Equals(SubSpecialityHolder.SubSpeciality, null);
        }

        /// <summary>
        ///     Initializes a new instance of
        ///     <see cref="ExecutedBudgetComponentItemViewModelBase{TItem,TPresenter,TComponent,TServices}" />.
        /// </summary>
        public ExecutedActivityViewModelBase()
        {
            ExecutePlannedItemsCommand = new DelegateCommand<IList>(ExecutePlannedItemsCommand_Executed, ExecutePlannedItemsCommand_CanExecute);
            _BackgroundWorkerMine = new BackgroundWorker();
            _BackgroundWorkerMine.DoWork += ExecuteOnDoWork;
            _BackgroundWorkerMine.RunWorkerCompleted += ExecuteOnRunWorkerCompleted;
        }

        private void ExecuteOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IEnumerable<IExecutedActivity> executedItems = (IEnumerable < IExecutedActivity >)e.Result;
            StatusBarServices.SignalText(Resources.ExecutedThisManyItems.EasyFormat(executedItems.Count()));
        }

        private void ExecuteOnDoWork(object sender, DoWorkEventArgs e)
        {
            IPlannedActivity[] plannedItems = (IPlannedActivity[]) e.Argument;
            // Command to execute them
            IEnumerable<IExecutedActivity> executedItems = ExecuteUsingServices(services => services.BeExecuted(plannedItems)).ToArray();

            // Then, create for each of the executed item a presenter and add them to the current crud view model
            foreach (var executedItem in executedItems)
            {
                IExecutedActivityPresenter presenter = CreatePresenterFor(executedItem);
                presenter.PropertyChanged += OnPresenterPropertyChanged;
                Items.Add(presenter);
            }

            e.Result = executedItems;
        }


        /// <summary>
        ///     Gets the command that allows to executed specified planned items.
        /// </summary>
        public ICommand ExecutePlannedItemsCommand { get; private set; }


        private bool ExecutePlannedItemsCommand_CanExecute(IList list)
        {
            IPlannedActivity[] plannedItems;
            return TryGetPlannedItems(list, out plannedItems) && ExecuteUsingServices(services => services.CanBeExecute(plannedItems));
        }

        private void ExecutePlannedItemsCommand_Executed(IList list)
        {
            IPlannedActivity[] plannedItems;
            if (!TryGetPlannedItems(list, out plannedItems))
                return;
            StatusBarServices.SignalWaitOperation();

           
            _BackgroundWorkerMine.RunWorkerAsync(plannedItems);
           
            // And notify the user

          
                //, Resources.ExecutionSummary);
        }


        private static bool TryGetPlannedItems(IList arg, out IPlannedActivity[] plannedItems)
        {
            plannedItems =
                (from presenter in (arg ?? new object[0]).OfType<IPresenter>()
                 select presenter.Object)
                    .OfType<IPlannedActivity>()
                    .ToArray();

            return plannedItems.Any() && arg != null && plannedItems.Count() == arg.Count;
        }

        protected override INavigable Parent { get { return SubSpecialityHolder; } }
    }
}
