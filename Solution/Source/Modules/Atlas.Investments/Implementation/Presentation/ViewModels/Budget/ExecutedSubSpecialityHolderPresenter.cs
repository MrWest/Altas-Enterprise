using System;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    public class ExecutedSubSpecialityHolderPresenter<TComponent> : SubSpecialityHolderPresenter<IExecutedSubSpecialityHolder,TComponent,IExecutedSubSpecialityHolderManagerApplicationServices>, IExecutedSubSpecialityHolderPresenter<TComponent>
         where TComponent : class, IBudgetComponent
        //where TExecuted : class, IExecutedActivityViewModel
        //where TPresenter : class, IExecutedActivityPresenter
    {
        private IExecutedActivityViewModel _executedActivities;
       
        /// <summary>
        /// Gets the crud view model used to manage the planned activities of the budget component contained in the current
        /// presenter.
        /// </summary>
        public IExecutedActivityViewModel ExecutedActivities
        {
            get { return GetOrInitialize(ref _executedActivities, x => x.SubSpecialityHolder = this); }
        }
        private IExecutedActivityViewModel GetOrInitialize(ref IExecutedActivityViewModel viewModel, Action<IExecutedActivityViewModel> initialize)

        {
            if (viewModel == null)
            {
                viewModel = ServiceLocator.Current.GetInstance<IExecutedActivityViewModel>();
                initialize(viewModel);
                viewModel.SubSpecialityHolder = this;
                viewModel.Load();
                viewModel.Raised += OnInteractionRequested;
            }

            return viewModel;
        }

        public override ICommand DeleteMySelfCommand
        {
            get
            {
                if (!Equals(BudgetComponent, null) && BudgetComponent.GetType().Implements<IEquipmentComponentPresenter>())
                {
                    return
                        (BudgetComponent as IEquipmentComponentPresenter).ExecutedSubSpecialityHolders
                        .DeleteCommand;
                }

                if (!Equals(BudgetComponent, null) && BudgetComponent.GetType().Implements<IConstructionComponentPresenter>())
                {
                    return
                        (BudgetComponent as IConstructionComponentPresenter).ExecutedSubSpecialityHolders
                        .DeleteCommand;
                }

                if (!Equals(BudgetComponent, null) && BudgetComponent.GetType().Implements<IOtherExpensesComponentPresenter>())
                {
                    return
                        (BudgetComponent as IOtherExpensesComponentPresenter).ExecutedSubSpecialityHolders
                        .DeleteCommand;
                }
                if (!Equals(BudgetComponent, null) && BudgetComponent.GetType().Implements<IWorkCapitalComponentPresenter>())
                {
                    return
                        (BudgetComponent as IWorkCapitalComponentPresenter).ExecutedSubSpecialityHolders
                        .DeleteCommand;
                }

                return null;

            }

        }

        public override ICrudViewModel Items
        {
            get { return ExecutedActivities; }
        }

        public override string NewText { get { return Resources.NewExecutedActivityName; } }
        protected override void RefreshCommand()
        {
            _executedActivities = null;
            OnPropertyChanged(() => Items);
            OnPropertyChanged(() => Items.AddCommand);
            OnPropertyChanged(() => Code);
        }

        public override void DoNotify()
        {
            base.DoNotify();
            BudgetComponent.Budget.InvestmentElement.DoNotify();
        }
    }
}