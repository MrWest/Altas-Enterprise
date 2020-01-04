using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.Views;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Implementation of the contract IBudgetComponentPresenter{...}, representing the presenter view model used to decorated
    /// and impersonate a budget component in the UI.
    /// </summary>
    public abstract class BudgetComponentPresenterBase<
            TComponent,
            TPlannedSubSpecialityHolders,
            TPlannedSubSpecialityHolderPresenter,
            TExecutedSubSpecialityHolders,
            TExecutedSubSpecialityHolderPresenter> :
        EntityPresenterBase<TComponent, IItemManagerApplicationServices<TComponent>>,
        IBudgetComponentPresenter<
            TComponent,
            TPlannedSubSpecialityHolders,
            TPlannedSubSpecialityHolderPresenter,
            TExecutedSubSpecialityHolders,
            TExecutedSubSpecialityHolderPresenter>, IView
        where TComponent : class, IBudgetComponent
        //where TPlannedActivities : class, IPlannedActivityViewModel
        //where TPlannedActivityPresenter : class, IPlannedActivityPresenter
        //where TExecutedActivities : class, IExecutedActivityViewModel
        //where TExecutedActivityPresenter : class, IExecutedActivityPresenter
       where TPlannedSubSpecialityHolders : class, IPlannedSubSpecialityHolderViewModel<TComponent, TPlannedSubSpecialityHolderPresenter>
        where TPlannedSubSpecialityHolderPresenter : class, IPlannedSubSpecialityHolderPresenter<TComponent>
        where TExecutedSubSpecialityHolders : class, IExecutedSubSpecialityHolderViewModel<TComponent, TExecutedSubSpecialityHolderPresenter>
        where TExecutedSubSpecialityHolderPresenter : class, IExecutedSubSpecialityHolderPresenter<TComponent>
    {
        private IBudgetPresenter _budget;
        private IPlannedActivityViewModel _plannedActivities;
        private IExecutedActivityViewModel _executedActivities;

        private TPlannedSubSpecialityHolders _plannedSubSpecialityHolders;
        private TExecutedSubSpecialityHolders _executedSubSpecialityHolders;
        /// <summary>
        /// Initializes a new instance of a presenter view model decorating a domain budget component but without such budget component.
        /// </summary>
        public BudgetComponentPresenterBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of a presenter view model decorating a domain budget component.
        /// </summary>
        /// <param name="budgetComponent">The domain budget component to present.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponent"/> is null.</exception>
        public BudgetComponentPresenterBase(TComponent budgetComponent)
            : base(budgetComponent)
        {
        }


        /// <summary>
        /// Gets or sets the budget to which belong the current budget component.
        /// </summary>
        public IBudgetPresenter Budget
        {
            get
            {
                if (_budget == null)
                    throw new InvalidOperationException(Resources.InitializeBudgetBeforeUsingIt);

                return _budget;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _budget = value;
            }
        }


        ///// <summary>
        ///// Gets the crud view model used to manage the planned activities of the budget component contained in the current
        ///// presenter.
        ///// </summary>
        public IPlannedActivityViewModel PlannedActivities
        {
            get { return GetOrInitialize(ref _plannedActivities); }
        }

        /////// <summary>
        /////// Gets the crud view model used to manage the executed activities of the budget component contained in the current
        /////// presenter.
        /////// </summary>
        public IExecutedActivityViewModel ExecutedActivities
        {
            get { return GetOrInitialize(ref _executedActivities); }
        }

        public TPlannedSubSpecialityHolders PlannedSubSpecialityHolders { get { return GetOrInitialize(ref _plannedSubSpecialityHolders, x => x.BudgetComponent = this); } }
        public TExecutedSubSpecialityHolders ExecutedSubSpecialityHolders { get { return GetOrInitialize(ref _executedSubSpecialityHolders, x => x.BudgetComponent = this); } }


        public virtual decimal BudgetByCurrencyAndPeriod(ICurrency currency, IPeriod period)
        {
          
            return PlannedActivities.Items
                .Sum(x => x.BudgetByCurrencyAndPeriod(currency, period));
        }

        public virtual decimal PlannedCost { get
        {
            return PlannedActivities.Items.Sum(x => x.Cost);
        } 
        }

        public decimal ExecutedCost
        {
            get { return ExecutedActivities.Items.Sum(x => x.Cost); }
        }

        public decimal ExecutionPercent
        {
            get
            {
                return PlannedCost!=0?((ExecutedCost * 100) / PlannedCost):0;
            }
        }
        private IPlannedActivityViewModel GetOrInitialize(ref IPlannedActivityViewModel viewModel)

        {
            if (viewModel == null)
            {
                viewModel = ServiceLocator.Current.GetInstance<IPlannedActivityViewModel>();
                //initialize(viewModel);
                //viewModel.Component = this;
                foreach (TPlannedSubSpecialityHolderPresenter plannedSubSpecialityHolder in PlannedSubSpecialityHolders.Items)
                {
                    viewModel.SubSpecialityHolder = plannedSubSpecialityHolder;
                    foreach (IPlannedActivityPresenter plannedActivityPresenter in plannedSubSpecialityHolder.PlannedActivities)
                    viewModel.Items.Add(plannedActivityPresenter);
                }

               // viewModel.Load();
                viewModel.Raised += OnInteractionRequested;
            }
            else if (viewModel.Items.Count != PlannedSubSpecialityHolders.Items.Sum(x => x.PlannedActivities.Items.Count))
            {
                viewModel.Items.Clear();
                foreach (TPlannedSubSpecialityHolderPresenter plannedSubSpecialityHolder in PlannedSubSpecialityHolders.Items)
                {
                    viewModel.SubSpecialityHolder = plannedSubSpecialityHolder;
                    foreach (IPlannedActivityPresenter plannedActivityPresenter in plannedSubSpecialityHolder.PlannedActivities)
                        viewModel.Items.Add(plannedActivityPresenter);
                }

            }

            return viewModel;
        }

        private IExecutedActivityViewModel GetOrInitialize(ref IExecutedActivityViewModel viewModel)

        {
            if (viewModel == null)
            {
                viewModel = ServiceLocator.Current.GetInstance<IExecutedActivityViewModel>();
                //initialize(viewModel);
                //viewModel.Component = this;
                foreach (TExecutedSubSpecialityHolderPresenter executedSubSpecialityHolderPresenter in ExecutedSubSpecialityHolders.Items)
                {
                    viewModel.SubSpecialityHolder = executedSubSpecialityHolderPresenter;
                    foreach (IExecutedActivityPresenter executedActivityPresenter in executedSubSpecialityHolderPresenter.ExecutedActivities)
                        viewModel.Items.Add(executedActivityPresenter);
                }

                // viewModel.Load();
                viewModel.Raised += OnInteractionRequested;
            }

            return viewModel;
        }


        private TPlannedSubSpecialityHolders GetOrInitialize(ref TPlannedSubSpecialityHolders viewModel, Action<TPlannedSubSpecialityHolders> initialize)

        {
            if (viewModel == null)
            {
                viewModel = ServiceLocator.Current.GetInstance<TPlannedSubSpecialityHolders>();
                initialize(viewModel);
                viewModel.BudgetComponent = this;
                viewModel.Load();
                viewModel.Raised += OnInteractionRequested;
            }

            return viewModel;
        }

        private TExecutedSubSpecialityHolders GetOrInitialize(ref TExecutedSubSpecialityHolders viewModel, Action<TExecutedSubSpecialityHolders> initialize)

        {
            if (viewModel == null)
            {
                viewModel = ServiceLocator.Current.GetInstance<TExecutedSubSpecialityHolders>();
                initialize(viewModel);
                viewModel.BudgetComponent = this;
                viewModel.Load();
                viewModel.Raised += OnInteractionRequested;
            }

            return viewModel;
        }

        public virtual void Notify()
        {


            //_plannedActivities.LoadOnAdd();
            //_executedActivities.LoadOnAdd();
            
            OnPropertyChanged(() => PlannedSubSpecialityHolders);
            OnPropertyChanged(() => ExecutedSubSpecialityHolders);
           



            //  Object.PlannedResources.ForEach(x=> x.Component = Object);

        }

        public void NotifyUp()
        {
            //Budget.Notify();
        }

        /// <summary>
        /// Invoked when the datacontext for the current view has been changed. Makes sure that the interactions channels
        /// between this view and the new datacontext are wired up.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The event arguments containing the details about the datacontext change.</param>
        protected virtual void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.SetupInteractionWithDataContext(e, OnInteractionRequested);
        }

        /// <summary>
        /// Invoked when the current view's datacontext has requested an interaction with the current view.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The event arguments containing the details about the interaction request.</param>
        protected virtual void OnInteractionRequested(object sender, InteractionRequestedEventArgs e)
        {
            this.Execute(e);
        }

        /// <summary>
        /// Gets the total of planned activities composing the current <see cref="IBudgetComponentPresenter"/>.
        /// </summary>
        //public decimal PlannedActivitiesCost
        //{
        //    get { return PlannedActivities.Sum(x => x.Cost); }
        //}

       
        /// <summary>
        /// Gets the total of executed activities composing the current <see cref="IBudgetComponentPresenter"/>.
        /// </summary>
        //public decimal ExecutedActivitiesCost
        //{
        //    get { return ExecutedActivities.Sum(x => x.Cost); }
        //}


        public object DataContext { get; set; }

        public DateTime StartDate()
        {
            if (!StartCalculated)
            {
              //  LastCalculatedStartDate = Budget.InvestmentElement.Period.OriStart();
                bool first = true;
                foreach (IPlannedActivityPresenter plannedActivity in PlannedActivities)
                {
                    if (first)
                    {
                        LastCalculatedStartDate = plannedActivity.StartDate();
                        first = false;
                    }
                    else
                    {
                        if (LastCalculatedStartDate.CompareTo(plannedActivity.StartDate()) > 0)
                            LastCalculatedStartDate = plannedActivity.StartDate();
                    }
                }

                StartCalculated = true;
            }




            return LastCalculatedStartDate;
        }

        public DateTime FinishDate()
        {
            if (!EndCalculated)
            {
               // LastCalculatedFinishDate = Budget.InvestmentElement.Period.OriEnd();
                bool first = true;
                foreach (IPlannedActivityPresenter plannedActivity in PlannedActivities)
                {
                    if (first)
                    {
                        LastCalculatedFinishDate = plannedActivity.FinishDate();
                        first = false;
                    }
                    else
                    {
                        if (LastCalculatedFinishDate.CompareTo(plannedActivity.FinishDate()) < 0)
                            LastCalculatedFinishDate = plannedActivity.FinishDate();
                    }
                }
                EndCalculated = true;
            }
           
            return LastCalculatedFinishDate;
        }

      //  private bool _startCalculated = false;

        public bool StartCalculated
        {
            get { return Object.StartCalculated; }
            set
            {
                bool change = Object.StartCalculated;
                Object.StartCalculated = value;
                if (!value )
                {
                   // OnPropertyChanged(() => Period);
                    if (Budget != null )
                        Budget.StartCalculated = Object.StartCalculated;
                    //foreach (IPlannedActivityPresenter plannedActivityPresenter in PlannedActivities)
                    //{
                    //    plannedActivityPresenter.StartCalculated = _startCalculated;
                    //}
                }
            }
        }
      //  private bool _endCalculated = false;

        public bool EndCalculated
        {
            get { return Object.EndCalculated; }
            set
            {
                bool change = Object.EndCalculated;
                Object.EndCalculated = value;
                if (!Object.EndCalculated && change )
                {
                    // OnPropertyChanged(() => Period);
                    if (Budget != null)
                        Budget.EndCalculated = Object.EndCalculated;
                    //foreach (IPlannedActivityPresenter plannedActivityPresenter in PlannedActivities)
                    //{
                    //    plannedActivityPresenter.EndCalculated = _endCalculated;
                    //}
                }
            }
        }

        public DateTime LastCalculatedFinishDate
        {
            get { return Object.LastCalculatedFinishDate; }
            set { Object.LastCalculatedFinishDate = value; }
        }

        public DateTime LastCalculatedStartDate
        {
            get { return Object.LastCalculatedStartDate; }
            set { Object.LastCalculatedStartDate = value; }
        }

        public bool HasActivities => PlannedActivities.Items.Count > 0;
        public void SpreadChanges(IBudgetComponentItem toSpread)
        {
            foreach (TPlannedSubSpecialityHolderPresenter plannedSubSpecialityHolderPresenter in PlannedSubSpecialityHolders.Items)
            {
               plannedSubSpecialityHolderPresenter.SpreadChanges(toSpread);
            }
        }

        public virtual decimal Cost
        {
            get
            {
                if (!IsCostCalculated)
                {
                    decimal cost = 0;
                    //if (Currency != null)
                    //    foreach (INavigable val in Items)
                    //    {
                    //        if (val.Currency != null)
                    //            if (val.Currency.Id.ToString() == Currency.Id.ToString())
                    //            {
                    //                cost += val.Cost;
                    //            }
                    //            else
                    //            {
                    //                var curren = val.Currency.ConvertionFactorFor(Currency.Object);

                    //                cost += val.Cost * curren;

                    //            }

                    //    }
                    _calculatedCost = PlannedCost;
                    IsCostCalculated = true;
                }

                //if (Currency!=null)
                return _calculatedCost;
            }
        }

        private decimal _calculatedCost;

        private bool _isCostCalculated;
        public virtual bool IsCostCalculated
        {
            get { return _isCostCalculated; }
            set
            {
                _isCostCalculated = value;
                if (!_isCostCalculated && Budget != null)
                {
                    OnPropertyChanged(() => Cost);
                    Budget.IsCostCalculated = false;
                }

            }
        }
    }
}
