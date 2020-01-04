using System;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Presentation.Views;
using CompanyName.Atlas.Investments.Presentation.Views.ViewTypes;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Implementation of the contract <see cref="IBudgetPresenter"/>, representing the presenter view model used to decorate and
    /// impersonate an investment element's budget in the UI.
    /// </summary>
    public class BudgetPresenter : EntityPresenterBase<IBudget, IItemManagerApplicationServices<IBudget>>, IBudgetPresenter
    {
        private IInvestmentElementPresenter _investmentElement;
        private IEquipmentComponentPresenter _equipmentComponent;
        private IConstructionComponentPresenter _constructionComponent;
        private IOtherExpensesComponentPresenter _otherExpensesComponent;
        private IWorkCapitalComponentPresenter _workCapitalComponent;

        /// <summary>
        /// Initializes a new instance of <see cref="BudgetPresenter"/> decorating a domain budget but without such budget.
        /// </summary>
        public BudgetPresenter()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BudgetPresenter"/> decorating a domain budget.
        /// </summary>
        /// <param name="budget">The <see cref="IBudget"/> to present.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budget"/> is null.</exception>
        public BudgetPresenter(IBudget budget)
            : base(budget)
        {
        }


        /// <summary>
        /// Gets or sets the presenter view model containing the investment element to which belong the budget decorated by the current
        /// budget presenter.
        /// </summary>
        public IInvestmentElementPresenter InvestmentElement
        {
            get
            {
                if (_investmentElement == null)
                    throw new InvalidOperationException(Resources.InitializeInvestmentElementBeforeUsingIt);

                return _investmentElement;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _investmentElement = value;
            }
        }

        /// <summary>
        /// Gets the presenter view model containing the equipment component of the budget containing in the current presenter.
        /// </summary>
        public IEquipmentComponentPresenter EquipmentComponent
        {
            get
            {
                if (_equipmentComponent == null)
                    throw new InvalidOperationException(Resources.InitializeEquipmentComponentBeforeUsingIt);

                return _equipmentComponent;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _equipmentComponent = value;
            }
        }

        /// <summary>
        /// Gets the presenter view model containing the construction component of the budget containing in the current presenter.
        /// </summary>
        public IConstructionComponentPresenter ConstructionComponent
        {
            get
            {
                if (_constructionComponent == null)
                    throw new InvalidOperationException(Resources.InitializeConstructionComponentBeforeUsingIt);

                return _constructionComponent;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _constructionComponent = value;
            }
        }
        /// <summary>
        /// Gets the presenter view model containing the other expenses component of the budget containing in the current presenter.
        /// </summary>
        public IOtherExpensesComponentPresenter OtherExpensesComponent
        {
            get
            {
                if (_otherExpensesComponent == null)
                    throw new InvalidOperationException(Resources.InitializeConstructionComponentBeforeUsingIt);

                return _otherExpensesComponent;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _otherExpensesComponent = value;
            }
        }
        /// <summary>
        /// Gets the presenter view model containing the other expenses component of the budget containing in the current presenter.
        /// </summary>
        public IWorkCapitalComponentPresenter WorkCapitalComponent
        {
            get
            {
                if (_workCapitalComponent == null)
                    throw new InvalidOperationException(Resources.InitializeConstructionComponentBeforeUsingIt);

                return _workCapitalComponent;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _workCapitalComponent = value;
            }
        }

        public decimal BudgetByCurrencyAndPeriod(ICurrency currency, IPeriod period)
        {
            return EquipmentComponent.BudgetByCurrencyAndPeriod(currency , period) +
                ConstructionComponent.BudgetByCurrencyAndPeriod(currency, period)+
                OtherExpensesComponent.BudgetByCurrencyAndPeriod(currency , period)+
                WorkCapitalComponent.BudgetByCurrencyAndPeriod(currency, period);
        }

        public decimal PlannedCost
        {
            get
            {
                return EquipmentComponent.PlannedCost + ConstructionComponent.PlannedCost +
                       OtherExpensesComponent.PlannedCost + WorkCapitalComponent.PlannedCost;
            }
           
        }

        public decimal ExecutedCost
        {
            get
            {
                return EquipmentComponent.ExecutedCost + ConstructionComponent.ExecutedCost +
                       OtherExpensesComponent.ExecutedCost + WorkCapitalComponent.ExecutedCost;
            }
        }

        public decimal ExecutionPercent
        {
            get { 
                return  PlannedCost>0?((ExecutedCost*100)/ PlannedCost):0;
            }
        }

        public DateTime StartDate()
        {
            if (!StartCalculated)
            {
                StartCalculated = true;

                switch ((BudgetViewType)SecondView)
                {
                    default:
                        {
                            if (EquipmentComponent.HasActivities)
                                LastCalculatedStartDate = EquipmentComponent.StartDate();

                            DateTime date = LastCalculatedStartDate;

                            if (ConstructionComponent.HasActivities)
                            {
                                    date = ConstructionComponent.StartDate();
                            if (LastCalculatedStartDate.CompareTo(date) > 0 || !EquipmentComponent.HasActivities)
                                LastCalculatedStartDate = date;

                            }

                            if (OtherExpensesComponent.HasActivities)
                            {
                                date = OtherExpensesComponent.StartDate();
                                                            if (LastCalculatedStartDate.CompareTo(date) > 0 || (!EquipmentComponent.HasActivities&& !ConstructionComponent.HasActivities))
                                                                LastCalculatedStartDate = date;
                            }

                            if (WorkCapitalComponent.HasActivities)
                            {
                                date = WorkCapitalComponent.StartDate();
                                  if (LastCalculatedStartDate.CompareTo(date) > 0 || (!EquipmentComponent.HasActivities && !ConstructionComponent.HasActivities && !OtherExpensesComponent.HasActivities))
                                       LastCalculatedStartDate = date;
                            }
                           

                            return LastCalculatedStartDate;
                        }
                    case BudgetViewType.Equipment:
                        {
                            LastCalculatedStartDate = EquipmentComponent.StartDate();
                            return LastCalculatedStartDate;
                        }
                    case BudgetViewType.Construction:
                        {
                            LastCalculatedStartDate = ConstructionComponent.StartDate();
                            return LastCalculatedStartDate;
                        }
                    case BudgetViewType.Others:
                        {
                            LastCalculatedStartDate = OtherExpensesComponent.StartDate();
                            return LastCalculatedStartDate;
                        }

                    case BudgetViewType.WorkCapital:
                        {
                            LastCalculatedStartDate = WorkCapitalComponent.StartDate();
                            return LastCalculatedStartDate;
                        }
                }
               
            }

            return LastCalculatedStartDate;

        }

        public DateTime FinishDate()

        {

            if (!EndCalculated)
            {
                EndCalculated = true;

                   switch ((BudgetViewType)SecondView)
                    {
                        default:
                            {
                               if(EquipmentComponent.HasActivities)
                                LastCalculatedFinishDate = EquipmentComponent.FinishDate();

                                DateTime date = LastCalculatedFinishDate;

                                if (ConstructionComponent.HasActivities)
                                {
                                     date = ConstructionComponent.FinishDate();
                                if (LastCalculatedFinishDate.CompareTo(date) < 0 )
                                        LastCalculatedFinishDate = date;
                                }


                            if (OtherExpensesComponent.HasActivities)
                            {
                             date = OtherExpensesComponent.FinishDate();
                                                            if (LastCalculatedFinishDate.CompareTo(date) < 0)
                                                                    LastCalculatedFinishDate = date;
                            }

                            if (WorkCapitalComponent.HasActivities)
                            {
                                 date = WorkCapitalComponent.FinishDate();
                                                                if (LastCalculatedFinishDate.CompareTo(date) < 0)
                                                                        LastCalculatedFinishDate = date;
                            }
                           

                                return LastCalculatedFinishDate;
                            }
                           case BudgetViewType.Equipment:
                            {
                                    LastCalculatedFinishDate = EquipmentComponent.FinishDate();
                                return LastCalculatedFinishDate;
                            }
                            case BudgetViewType.Construction:
                            {
                                    LastCalculatedFinishDate = ConstructionComponent.FinishDate();
                                return LastCalculatedFinishDate;
                            }
                            case BudgetViewType.Others:
                            {
                                    LastCalculatedFinishDate = OtherExpensesComponent.FinishDate();
                                return LastCalculatedFinishDate;
                            }

                            case BudgetViewType.WorkCapital:
                            {
                                    LastCalculatedFinishDate = WorkCapitalComponent.FinishDate();
                                return LastCalculatedFinishDate;
                            }
                    }
           
            }

            return LastCalculatedFinishDate;

        }


        public ICommand FilterCommand
        {
            get
            {
                switch ((BudgetViewType)SecondView)
                {
                    default:
                    {
                            if (BudgetComponentItemViewType.PlannedItems == (BudgetComponentItemViewType)View)
                                return EquipmentComponent.PlannedSubSpecialityHolders.FilterCommand;
                            if (BudgetComponentItemViewType.ExecutedItems == (BudgetComponentItemViewType)View)
                                return EquipmentComponent.ExecutedSubSpecialityHolders.FilterCommand;

                            return EquipmentComponent.PlannedSubSpecialityHolders.FilterCommand;
                   }
                        case BudgetViewType.Construction:
                        {
                            if (BudgetComponentItemViewType.PlannedItems == (BudgetComponentItemViewType)View)
                                return ConstructionComponent.PlannedSubSpecialityHolders.FilterCommand;
                            if (BudgetComponentItemViewType.ExecutedItems == (BudgetComponentItemViewType)View)
                                return ConstructionComponent.ExecutedSubSpecialityHolders.FilterCommand;

                            return ConstructionComponent.PlannedSubSpecialityHolders.FilterCommand;
                        }
                    case BudgetViewType.Others:
                        {
                            if (BudgetComponentItemViewType.PlannedItems == (BudgetComponentItemViewType)View)
                                return OtherExpensesComponent.PlannedSubSpecialityHolders.FilterCommand;
                            if (BudgetComponentItemViewType.ExecutedItems == (BudgetComponentItemViewType)View)
                                return OtherExpensesComponent.ExecutedSubSpecialityHolders.FilterCommand;

                            return OtherExpensesComponent.PlannedSubSpecialityHolders.FilterCommand;
                        }

                    case BudgetViewType.WorkCapital:
                        {
                            if (BudgetComponentItemViewType.PlannedItems == (BudgetComponentItemViewType)View)
                                return WorkCapitalComponent.PlannedSubSpecialityHolders.FilterCommand;
                            if (BudgetComponentItemViewType.ExecutedItems == (BudgetComponentItemViewType)View)
                                return WorkCapitalComponent.ExecutedSubSpecialityHolders.FilterCommand;

                            return WorkCapitalComponent.PlannedSubSpecialityHolders.FilterCommand;
                        }
                }
                //if (BudgetComponentItemViewType.PlannedItems == (BudgetComponentItemViewType)View)
                //    return PlannedActivities.FilterCommand;
                //if (BudgetComponentItemViewType.ExecutedItems == (BudgetComponentItemViewType)View)
                //    return ExecutedActivities.FilterCommand;

                //return PlannedActivities.FilterCommand;// + ExecutedActivities.FilterCommand;
            }

        }

        private BudgetComponentItemViewType _view = BudgetComponentItemViewType.PlannedItems;
        private BudgetViewType _secondView = BudgetViewType.All;
        private int _selectedComponet = 0;

        public object View
        {
            get { return _view; }
            set
            {
                if (value != null)
                    _view = (BudgetComponentItemViewType)value;

                OnPropertyChanged(()=>FilterCommand);
                OnPropertyChanged(() => View);
            }
        }

        public string FilterCriteria { get; set; }

        public object SecondView
        {
            get { return _secondView; }
            set
            {
                if (value != null && !Equals(_secondView, value))
                {
                    _secondView = (BudgetViewType)value;

                    InvestmentElement.SecondView = (BudgetViewType)value;
                    //StartCalculated = false;
                    //EndCalculated = false;
                 
                }


                OnPropertyChanged(() => FilterCommand);
                OnPropertyChanged(() => SecondView);
            }
        }

        public int SelectedComponent
        {
            get { return _selectedComponet; }
            set
            {
                _selectedComponet = value;
                OnPropertyChanged(()=>SelectedComponent);
            }
         }

       // private bool _startCalculated = false;

        public bool StartCalculated
        {
            get { return Object.StartCalculated; }
            set
            {
                if (value != null && !Equals(Object.StartCalculated, value))
                {
                    bool change = Object.StartCalculated;
                    Object.StartCalculated = value;
                    if (!Object.StartCalculated && change != Object.StartCalculated)
                    {
                        //OnPropertyChanged(() => InvestmentElement);
                        if (InvestmentElement != null)
                            InvestmentElement.StartCalculated = Object.StartCalculated;

                        //EquipmentComponent.StartCalculated = _startCalculated;
                        //ConstructionComponent.StartCalculated = _startCalculated;
                        //OtherExpensesComponent.StartCalculated = _startCalculated;
                        //WorkCapitalComponent.StartCalculated = _startCalculated;
                    }
                }
            }
        }
     //   private bool _endCalculated = false;

        public bool EndCalculated
        {
            get { return Object.EndCalculated; }
            set
            {
                if (value != null && !Equals(Object.EndCalculated, value))
                {
                    bool change = Object.EndCalculated;
                    Object.EndCalculated = value;
                    if (!Object.EndCalculated && change != Object.EndCalculated)
                    {
                        //OnPropertyChanged(() => InvestmentElement);
                        if (InvestmentElement != null)
                            InvestmentElement.EndCalculated = Object.EndCalculated;

                        //EquipmentComponent.EndCalculated = _endCalculated;
                        //ConstructionComponent.EndCalculated = _endCalculated;
                        //OtherExpensesComponent.EndCalculated = _endCalculated;
                        //WorkCapitalComponent.EndCalculated = _endCalculated;
                    }
                }
            }
        }

        public DateTime LastCalculatedFinishDate
        {
            get
            {
                return Object.LastCalculatedFinishDate;
            }
            set { Object.LastCalculatedFinishDate = value; }
        }

        public DateTime LastCalculatedStartDate
        {
            get
            {
                return Object.LastCalculatedStartDate;
            }
            set { Object.LastCalculatedStartDate = value; }
        }

        public bool HasActivities => EquipmentComponent.HasActivities || ConstructionComponent.HasActivities
                                     || OtherExpensesComponent.HasActivities || WorkCapitalComponent.HasActivities;

        public void SpreadChanges(IBudgetComponentItem toSpread)
        {
            EquipmentComponent.SpreadChanges(toSpread);
            ConstructionComponent.SpreadChanges(toSpread);
            OtherExpensesComponent.SpreadChanges(toSpread);
            WorkCapitalComponent.SpreadChanges(toSpread);
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

                    cost += EquipmentComponent.Cost;
                    cost += ConstructionComponent.Cost;
                    cost += OtherExpensesComponent.Cost;
                    cost += WorkCapitalComponent.Cost;

                    _calculatedCost = Math.Round(cost, 2);
                    IsCostCalculated = true;
                }

                //if (Currency!=null)
                return _calculatedCost;
            }
        }

        private decimal _calculatedCost;

        private bool _isCostCalculated;
        public bool IsCostCalculated
        {
            get { return _isCostCalculated; }
            set
            {
                _isCostCalculated = value;
                if (!_isCostCalculated && InvestmentElement != null)
                {
                    OnPropertyChanged(() => Cost);
                    InvestmentElement.IsCostCalculated = false;
                }
            }
        }
    }
}
