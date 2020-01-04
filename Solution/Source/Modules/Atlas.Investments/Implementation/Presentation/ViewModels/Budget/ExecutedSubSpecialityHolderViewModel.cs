using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.Prism.Commands;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    public class ExecutedSubSpecialityHolderViewModel<TComponent,TSubPresenter> : SubSpecialityHolderViewModel<IExecutedSubSpecialityHolder,TComponent, TSubPresenter,IExecutedSubSpecialityHolderManagerApplicationServices>, IExecutedSubSpecialityHolderViewModel<TComponent, TSubPresenter>
        where TComponent : class, IBudgetComponent
        where TSubPresenter : class, IExecutedSubSpecialityHolderPresenter<TComponent>
        //where TExecuted : class, IExecutedActivityViewModel
        //where TPresenter : class, IExecutedActivityPresenter
    {
        protected BackgroundWorker _BackgroundWorkerMine;
        public ExecutedSubSpecialityHolderViewModel()
        {
            ExecutePlannedItemsCommand = new DelegateCommand<IList>(ExecutePlannedItemsCommand_Executed, ExecutePlannedItemsCommand_CanExecute);
            _BackgroundWorkerMine = new BackgroundWorker();
            _BackgroundWorkerMine.DoWork += ExecuteOnDoWork;
            _BackgroundWorkerMine.RunWorkerCompleted += ExecuteOnRunWorkerCompleted;
        }

        private void ExecuteOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IEnumerable<IExecutedSubSpecialityHolder> executedItems = (IEnumerable < IExecutedSubSpecialityHolder > )e.Result;
            // Then, create for each of the executed item a presenter and add them to the current crud view model
            foreach (var executedItem in executedItems)
            {
                TSubPresenter presenter = CreatePresenterFor(executedItem);
                presenter.PropertyChanged += OnPresenterPropertyChanged;
                Items.Add(presenter);
            }

            Filter("");
            //Load();
            // And notify the user
            StatusBarServices.SignalText(Resources.ExecutedThisManyItems.EasyFormat(executedItems.Count()));//, Resources.ExecutionSummary);

            BudgetComponent.Notify();
        }

        private void ExecuteOnDoWork(object sender, DoWorkEventArgs e)
        {
            IPlannedSubSpecialityHolder[] plannedItems = (IPlannedSubSpecialityHolder[])e.Argument;
            // Command to execute them
            IEnumerable<IExecutedSubSpecialityHolder> executedItems = ExecuteUsingServices(services => services.BeExecuted(plannedItems)).ToArray();
            e.Result = executedItems;
        }


        /// <summary>
        ///     Gets the command that allows to executed specified planned items.
        /// </summary>
        public ICommand ExecutePlannedItemsCommand { get; private set; }


        private bool ExecutePlannedItemsCommand_CanExecute(IList list)
        {
            IPlannedSubSpecialityHolder[] plannedItems;
            return TryGetPlannedItems(list, out plannedItems) && ExecuteUsingServices(services => services.CanBeExecute(plannedItems));
        }

        private void ExecutePlannedItemsCommand_Executed(IList list)
        {
            IPlannedSubSpecialityHolder[] plannedItems;
            if (!TryGetPlannedItems(list, out plannedItems))
                return;
            StatusBarServices.SignalWaitOperation();

            _BackgroundWorkerMine.RunWorkerAsync(plannedItems);

           
           
        }


        private static bool TryGetPlannedItems(IList arg, out IPlannedSubSpecialityHolder[] plannedItems)
        {
            plannedItems =
                (from presenter in (arg ?? new object[0]).OfType<IPresenter>()
                 select presenter.Object)
                    .OfType<IPlannedSubSpecialityHolder>()
                    .ToArray();

            return plannedItems.Any() && arg != null && plannedItems.Count() == arg.Count;
        }
    }
}