using System;
using System.Linq;
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
    public class PlannedSubSpecialityHolderPresenter<TComponent> : SubSpecialityHolderPresenter<IPlannedSubSpecialityHolder,TComponent,IPlannedSubSpecialityHolderManagerApplicationServices>, IPlannedSubSpecialityHolderPresenter<TComponent>
         where TComponent : class, IBudgetComponent
        //where TPlanned : class, IPlannedActivityViewModel
        //where TPresenter : class, IPlannedActivityPresenter
    {
        private IPlannedActivityViewModel _plannedActivities;
       
        /// <summary>
        /// Gets the crud view model used to manage the planned activities of the budget component contained in the current
        /// presenter.
        /// </summary>
        public IPlannedActivityViewModel PlannedActivities
        {
            get { return GetOrInitialize(ref _plannedActivities, x => x.SubSpecialityHolder = this); }
        }
        private IPlannedActivityViewModel GetOrInitialize(ref IPlannedActivityViewModel viewModel, Action<IPlannedActivityViewModel> initialize)

        {
            if (viewModel == null)
            {
                viewModel = ServiceLocator.Current.GetInstance<IPlannedActivityViewModel>();
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
                if (!Equals(BudgetComponent, null) &&
                    BudgetComponent.GetType().Implements<IEquipmentComponentPresenter>())
                {
                    return
                        (BudgetComponent as IEquipmentComponentPresenter).PlannedSubSpecialityHolders
                        .DeleteCommand;
                }

                if (!Equals(BudgetComponent, null) &&
                    BudgetComponent.GetType().Implements<IConstructionComponentPresenter>())
                {
                    return
                        (BudgetComponent as IConstructionComponentPresenter).PlannedSubSpecialityHolders
                        .DeleteCommand;
                }

                if (!Equals(BudgetComponent, null) &&
                    BudgetComponent.GetType().Implements<IOtherExpensesComponentPresenter>())
                {
                    return
                        (BudgetComponent as IOtherExpensesComponentPresenter).PlannedSubSpecialityHolders
                        .DeleteCommand;
                }
                if (!Equals(BudgetComponent, null) &&
                    BudgetComponent.GetType().Implements<IWorkCapitalComponentPresenter>())
                {
                    return
                        (BudgetComponent as IWorkCapitalComponentPresenter).PlannedSubSpecialityHolders
                        .DeleteCommand;
                }

                return null;

            }
        }

        public override ICrudViewModel Items => PlannedActivities;

        public override string NewText { get { return Resources.NewPlannedActivityName; } }
        protected override void RefreshCommand()
        {
            _plannedActivities = null;
            OnPropertyChanged(() => Items);
            OnPropertyChanged(() => Items.AddCommand);
            OnPropertyChanged(() => Code);
        }

        public void SpreadChanges(IBudgetComponentItem toSpread)
        {
            foreach (IPlannedActivityPresenter plannedActivityPresenter in PlannedActivities.Items)
            {
                plannedActivityPresenter.SpreadChanges(toSpread);
            }
        }

        public virtual bool IsCostCalculated
        {
            get { return __isCostCalculated; }
            set
            {
                __isCostCalculated = value;
                if (!__isCostCalculated && Parent != null)
                {
                    OnPropertyChanged(() => Cost);
                    BudgetComponent.IsCostCalculated = false;
                }

            }
        }

        public override DateTime StartDate()
        {
           return BudgetComponent.Budget.InvestmentElement.Start;
            //if (!StartCalculated)
            //{
            //    //  LastCalculatedStartDate = Budget.InvestmentElement.Period.OriStart();
            //    bool first = true;
            //    foreach (IPlannedActivityPresenter plannedActivity in PlannedActivities)
            //    {
            //        if (first)
            //        {
            //            LastCalculatedStartDate = plannedActivity.StartDate();
            //            first = false;
            //        }
            //        else
            //        {
            //            if (LastCalculatedStartDate.CompareTo(plannedActivity.StartDate()) > 0)
            //                LastCalculatedStartDate = plannedActivity.StartDate();
            //        }
            //    }

            //    StartCalculated = true;
            //}




            //return LastCalculatedStartDate;
        }

        public override DateTime FinishDate()
        {
            return BudgetComponent.Budget.InvestmentElement.End;
            //if (!EndCalculated)
            //{
            //    // LastCalculatedFinishDate = Budget.InvestmentElement.Period.OriEnd();
            //    bool first = true;
            //    foreach (IPlannedActivityPresenter plannedActivity in PlannedActivities)
            //    {
            //        if (first)
            //        {
            //            LastCalculatedFinishDate = plannedActivity.FinishDate();
            //            first = false;
            //        }
            //        else
            //        {
            //            if (LastCalculatedFinishDate.CompareTo(plannedActivity.FinishDate()) < 0)
            //                LastCalculatedFinishDate = plannedActivity.FinishDate();
            //        }
            //    }
            //    EndCalculated = true;
            //}

            //return LastCalculatedFinishDate;
        }
    }
}