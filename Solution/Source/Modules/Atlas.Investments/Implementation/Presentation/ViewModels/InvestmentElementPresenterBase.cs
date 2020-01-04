using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Reporting;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Reporting;
using CompanyName.Atlas.Investments.Properties;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Input;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Presentation.Views.ViewTypes;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Rerporting;
using CompanyName.Atlas.Investments.Application;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    /// <summary>
    ///     Default implementation of the contract <see cref="IInvestmentElementPresenter" />, representing the contract of
    ///     presenter view models used to impersonate investment elements in the UI.
    /// </summary>
    public abstract class InvestmentElementPresenterBase<T, TServices> :
        NavigableNomenclator<T, TServices>,
        IInvestmentElementPresenter<T>
        where T : class, IInvestmentElement
        where TServices : IInvestmentElementManagerApplicationServices<T>
    {
        private IBudgetPresenter _budget;
        private bool _isExpanded;
      //  private ITreeNode _parent;
        private IInvestmentComponentViewModel<T> _elements;

        private IMeasurementUnitViewModel _measurementUnitViewModel;
        private ICurrencyViewModel _currencyViewModel;
        private ICategoryViewModel _categoryViewModel;
        private IExpenseConceptViewModel _expenseConceptViewModel;


        ///// <summary>
        /////     Initializes a new instance of a <see cref="InvestmentElementPresenterBase{T,TServices}" /> given the investment
        /////     element to impersonate in the UI.
        ///// </summary>
        ///// <param name="investmentElement">
        /////     The <see cref="IInvestmentElement" /> that will be decorated and impersonated by the
        /////     <see cref="InvestmentElementPresenterBase{T,TServices}" /> currently being initialized.
        ///// </param>
        ///// <exception cref="ArgumentNullException"><paramref name="investmentElement" /> is null.</exception>
        //protected InvestmentElementPresenterBase(T investmentElement)
        //    : base(investmentElement)
        //{
        //}


        /// <summary>
        ///     Gets or sets the budget of the <see cref="IInvestmentElement" /> contained in the current
        ///     <see cref="InvestmentElementPresenterBase{T,TServices}" />.
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
                 Object.Budget = value.Object;
                _budget = value;
            }
        }

        /// <summary>
        ///     Gets or sets the crud view model handling the investment components presenters being children of the current investment element presenter.
        /// </summary>
        public IInvestmentComponentViewModel<T> Elements
        { 
            get 
            {
                if (_elements == null)
                {
                    _elements = ServiceLocator.Current.GetInstance<IInvestmentComponentViewModel<T>>();
                    _elements.InvestmentElement =  this;
                    _elements.Load();
                    _elements.Raised += OnInteractionRequested;
                }

                return _elements;
            }
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [StringLengthValidator(1, 100, MessageTemplateResourceType = typeof(Resources), MessageTemplateResourceName = "InvestmentElementMustHaveAName")]
        public override string Name
        {
            get { return base.Name; }
            set
            {
                base.Name = value;
                OnPropertyChanged(() => ShortName);
                OnPropertyChanged(() => EvenShortterName);
                OnPropertyChanged(() => LimitedName);
                OnPropertyChanged(() => Name);
            }
        }

       

        //public override ICommand DeleteMySelfCommand {

        //    get {
            
        //        return ((IInvestmentElementPresenter)Parent) != null?((IInvestmentElementPresenter)Parent).Elements.DeleteCommand:null;

        //    }
        //}

        public override ICrudViewModel Items => Elements;
        

        /// <summary>
        ///     Gets or sets the period  of the current <see cref="IInvestmentElementPresenter" />.
        /// </summary>
        public IPeriodPresenter Period { get; set; }
        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string ShortName
        {
            get { return Name != null ? (Name.Length > 20 ? Name.Substring(0, 20) + "..." : Name) : string.Empty; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => EvenShortterName);
                OnPropertyChanged(() => LimitedName);
                OnPropertyChanged(() => ShortName);
            }
        }
        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string EvenShortterName
        {
            get { return Name != null ? (Name.Length > 2 ? Name.Substring(0, 2) + "..." : Name) : string.Empty; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => ShortName);
                OnPropertyChanged(() => LimitedName);
                OnPropertyChanged(() => EvenShortterName);
            }
        }
        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string LimitedName
        {
            get { return Name != null ? (Name.Length > 25 ? Name.Substring(0, 25) + "..." : Name) : string.Empty; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
                OnPropertyChanged(() => ShortName);
                OnPropertyChanged(() => EvenShortterName);
                OnPropertyChanged(() => LimitedName);
            }
        }

        ///// <summary>
        /////     Gets the data representing the geometry specification of the icon corresponding to the current
        /////     <see cref="InvestmentElementPresenterBase{T,TServices}" /> according to its depth.
        ///// </summary>
        //[ExcludeFromCodeCoverage]
        //public override string IconData
        //{
        //    get
        //    {
        //        switch (Depth)
        //        {
        //            case 0:
        //                return
        //                    "F1 M 25,27L 46,19L 46,22.25L 28.5,29L 31.75,31.25L 51,23.75L 51,48.5L 31.75,57L 25,52L 25,27 Z ";
        //            default:
        //                return "F1 M 24,19L 40,19L 40,31L 52,31L 52,57L 24,57L 24,19 Z M 41,30L 41,19L 52,30L 41,30 Z ";
        //        }
        //    }
        //}

        /// <summary>
        ///     Gets the data representing the geometry specification of the icon corresponding to the current
        ///     <see cref="InvestmentElementPresenterBase{T,TServices}" /> according to its depth.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public override string IconData
        {
            get
            {
                if (HasItems)
                {
                    if (IsExpanded)
                        return "F1 M 0,12 0,8 20,8 20,12 Z ";

                    return "F1 M 0,12 0,8 8,8 8,0 12,0 12,8 20,8 20,12 12,12 12,20 8,20 8,12 Z ";
                }

                return "";
            }

        }

        /// <summary>
        ///     Gets the data representing the state icon corresponding to the current
        ///     <see cref="IInvestmentElementPresenter" /> according to its vissual state (has childrens/colapssed/expanded).
        /// </summary>
        public string StateIconData {
            get
            {
                switch (IsExpanded)
                {
                    case false:
                        return
                            "F1 M 25,27L 46,19L 46,22.25L 28.5,29L 31.75,31.25L 51,23.75L 51,48.5L 31.75,57L 25,52L 25,27 Z ";
                    default:
                        return "F1 M 24,19L 40,19L 40,31L 52,31L 52,57L 24,57L 24,19 Z M 41,30L 41,19L 52,30L 41,30 Z ";
                }
            }
        }

        public override Thickness DeepThickness
        {
            get
            {
                if (Parent != null)
                {
                   
                    if (Parent.DeepThickness.Left + 13>180-(13*2))
                        return new Thickness(Parent.DeepThickness.Left, 0, 0, 0);
                    return new Thickness(Parent.DeepThickness.Left + 13, 0, 0, 0);
                }
                   

                return new Thickness(0, 0, 0, 0);
            }

        }
        ///// <summary>
        /////     Gets or sets whether the node rendering the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> data
        /////     is expanded or not.
        ///// </summary>
        //public bool IsExpanded
        //{
        //    get { return _isExpanded; }
        //    set
        //    {
        //        if (_isExpanded == value)
        //            return;

        //        _isExpanded = value;
        //        OnPropertyChanged(() => IsExpanded);
        //    }
        //}

        /// <summary>
        ///     Gets or sets the code of the underlying investment.
        /// </summary>
        public string Code
        {
            get { return Object.Code; }
            set { SetProperty(v => Object.Code = v, value); }
        }

        /// <summary>
        ///     Gets or sets the location of the underlying investment.
        /// </summary>
        public string Location
        {
            get { return Object.Location; }
            set { SetProperty(v => Object.Location = v, value); }
        }

        /// <summary>
        ///     Gets or sets the constructor of the underlying investment.
        /// </summary>
        public string Constructor
        {
            get { return Object.Constructor; }
            set { SetProperty(v => Object.Constructor = v, value); }
        }

        /// <summary>
        ///     Gets or sets the objective of the underlying investment.
        /// </summary>
        public string Objective
        {
            get { return Object.Objective; }
            set { SetProperty(v => Object.Objective = v, value); }
        }

        /// <summary>
        ///     Gets or sets the scope of the underlying investment.
        /// </summary>
        public string Scope
        {
            get { return Object.Scope; }
            set { SetProperty(v => Object.Scope = v, value); }
        }

        ///// <summary>
        /////     Gets or sets the value of the current <see cref="ITreeNode{T}" />.
        ///// </summary>
        //public ITreeNode Value
        //{

        //    get { return this; }
           
        //}

        ///// <summary>
        /////     When overridden in a deriver it gets or sets the depth the current
        /////     <see cref="InvestmentElementPresenterBase{T,TServices}" /> is with respect the root of the tree formed by the root
        /////     <see cref="InvestmentElementPresenterBase{T,TServices}" /> containing directly or indirectly the current one.
        /////     Returns 0 if the current one is the root of the tree, 1 if it is a child of the tree's root, and so on.
        ///// </summary>
        //public override int Depth => 0;

        /// <summary>
        ///     When overridden in a deriver it gets or sets the parent (if any) of the current <see cref="ITreeNode{T}" />.
        /// </summary>
    //    public abstract ITreeNode Parent { get; set; }


        /// <summary>
        /// Gets the investment element being represented in the user interface by this presenter view model.
        /// </summary>
        //IInvestmentElement IPresenter<IInvestmentElement>.Object
        //{
        //    get { return Object; }
        //    set { Object = (T)value; }
        //}
        public DateTime Start
        {
            get { return Period.Starts; }
            set
            {

               
                SetProperty(v => Period.Starts = v, value);
               // Period.Starts = value;
                //OnPropertyChanged(()=>Period);
                OnPropertyChanged(() => Start);
                OnPropertyChanged(() => End);


            }
        }


        ///// <summary>
        /////     Gets or sets the value of the current <see cref="ITreeNode<T>" />.
        ///// </summary>
        //object ITreeNode<ILifeline>.Value
        //{
        //    get { return this; }
        //    set { ; }
        //}

        public DateTime End
        {
            get { return Period.Ends; }
            set
            {
               
                SetProperty(v => Period.Ends = v, value);
               // Period.Ends = value;
               // OnPropertyChanged(() => Period);
                OnPropertyChanged(() => Start);
                OnPropertyChanged(() => End);
            }
        }
        ///// <summary>
        /////     Gets the <see cref="IEnumerable{T}" /> containing the children elements of the current <see cref="InvestmentPresenter"/>.
        ///// </summary>
        //public IEnumerable<ITreeNode> Children
        //{
        //    get { return Elements; }
        //}
        
        //IEnumerable<ITreeNode<ILifeline>> ITreeNode<ILifeline>.Children
        //{
        //    get { return Elements.Items; }
        //}

        /// <summary>
        /// Gets a <see cref="IMeasurementUnitViewModel"/> to use for presentation purposses.
        /// </summary>
        public IEnumerable<IMeasurementUnitPresenter> MeasurementUnits
        {
            get
            {
                if (_measurementUnitViewModel == null)
                {
                    _measurementUnitViewModel = ServiceLocator.Current.GetInstance<IMeasurementUnitViewModel>();
                    _measurementUnitViewModel.Load();

                }
                return _measurementUnitViewModel.Items;
            }
        }

        /// <summary>
        /// Gets a <see cref="ICurrencyViewModel"/> to use for presentation purposses.
        /// </summary>
        public IEnumerable<ICurrencyPresenter> Currencies
        {
            get
            {
                if (_currencyViewModel == null)
                {
                    _currencyViewModel = ServiceLocator.Current.GetInstance<ICurrencyViewModel>();
                    _currencyViewModel.Load();

                }
                return _currencyViewModel.Items;
            }
        }

        /// <summary>
        /// Gets a  <see cref="ICategoryViewModel"/> to use for presentation purposses.
        /// </summary>
        public IEnumerable<ICategoryPresenter> Categories {
            get
            {
                if (_categoryViewModel == null)
                {
                    _categoryViewModel = ServiceLocator.Current.GetInstance<ICategoryViewModel>();
                    _categoryViewModel.Load();

                }
                return _categoryViewModel.Items;
            }

  
        }

        /// <summary>
        /// Gets a  <see cref="IExpenseConceptViewModel"/> to use for presentation purposses.
        /// </summary>
        public IEnumerable<IExpenseConceptPresenter> ExpenseConcepts
        {
            get
            {
                if (_expenseConceptViewModel == null)
                {
                    _expenseConceptViewModel = ServiceLocator.Current.GetInstance<IExpenseConceptViewModel>();
                    _expenseConceptViewModel.Load();

                }
                return _expenseConceptViewModel.Items;
            }
        }

        private IMetroChartPresenter _metroChartPresenter;

        public IMetroChartPresenter MetroChartPresenter
        {
            get
            {
                if (_metroChartPresenter == null)
                {
                    _metroChartPresenter = ServiceLocator.Current.GetInstance<IMetroChartPresenter>();
                    _metroChartPresenter.ObjectToShow = Budget;
                }

                return _metroChartPresenter;
            }
        }

        private IBudgetSummaryPresenter _budgetSummaryPresenter;
        private object _secondView = BudgetViewType.All;
        private object _view;
        private IInvestmentElementPresenter _investmentElementPresenterImplementation;

        public IBudgetSummaryPresenter BudgetSummaryPresenter
        {
            get
            {
                if (_budgetSummaryPresenter == null)
                {
                    _budgetSummaryPresenter = ServiceLocator.Current.GetInstance<IBudgetSummaryPresenter>();
                    _budgetSummaryPresenter.ObjectToShow = Budget;
                }

                return _budgetSummaryPresenter;
            }
        }
        public int Percent
        {
            get { return GetPercent(); }
        }

        public int StartPercent
        {
            get { return GetStartPercent(); }
        }

        public void TellChange()
        {
             OnPropertyChanged(() => Percent);
             OnPropertyChanged(() => StartPercent);
        }

        protected virtual int GetPercent()
        {
            
            return 0;
        }

        protected virtual int GetStartPercent()
        {
           
            return 0;
        }

        public DateTime StartDate()
        {
            if (!StartCalculated)
            { 
                if(!Budget.HasActivities&& Elements.Items.Count==0)
                          LastCalculatedStartDate = Period.OriStart();
                else
                {
                    bool first = true;
                    foreach (IInvestmentElementPresenter investmentElement in Elements.Items)
                    {
                        investmentElement.SecondView = Budget.SecondView;
                        investmentElement.StartCalculated = false;
                        if (first)
                        {
                            LastCalculatedStartDate = investmentElement.StartDate();
                            first = false;
                        }
                        else
                        {
                            var date = investmentElement.StartDate();
                            if (IsDateLater(LastCalculatedFinishDate, date))
                                LastCalculatedFinishDate = date;
                        }
                    }

                    if (((BudgetViewType)Budget.SecondView == BudgetViewType.All && Budget.HasActivities) ||
                        ((BudgetViewType)Budget.SecondView == BudgetViewType.Equipment && Budget.EquipmentComponent.HasActivities) ||
                        ((BudgetViewType)Budget.SecondView == BudgetViewType.Construction && Budget.ConstructionComponent.HasActivities) ||
                        ((BudgetViewType)Budget.SecondView == BudgetViewType.Others && Budget.OtherExpensesComponent.HasActivities)||
                        ((BudgetViewType)Budget.SecondView == BudgetViewType.WorkCapital && Budget.WorkCapitalComponent.HasActivities))
                    {
                        if (first)
                        {

                            LastCalculatedStartDate = Budget.StartDate();
                            first = false;
                        }
                        else
                        {
                            var date = Budget.StartDate();
                            if (IsDateLater(LastCalculatedFinishDate, date))
                                LastCalculatedFinishDate = date;
                        }
                    }
                    else
                    {
                        if (Elements.Items.Count == 0)
                            LastCalculatedStartDate = Period.OriStart();
                    }
                 

                    StartCalculated = true;
                }

               
            }
          
            return LastCalculatedStartDate;
        }

        private bool IsDateEarlier(DateTime first, DateTime second)
        {
            if (first.Year < second.Year)
                return true;
            if (first.Year > second.Year)
                return false;

            if (first.Month < second.Month)
                return true;
            if (first.Month > second.Month)
                return false;

            if (first.Day < second.Day)
                return true;
            
                return false;
        }

        private bool IsDateLater(DateTime first, DateTime second)
        {
            if (first.Year > second.Year)
                return true;
            if (first.Year < second.Year)
                return false;

            if (first.Month > second.Month)
                return true;
            if (first.Month < second.Month)
                return false;

            if (first.Day > second.Day)
                return true;

            return false;
        }
        public DateTime FinishDate()
        {
                    if (!EndCalculated)
                    {
                         if (!Budget.HasActivities && Elements.Items.Count == 0)
                                LastCalculatedFinishDate = Period.OriEnd();
                         else
                         {
                    bool first = true;
                    foreach (IInvestmentElementPresenter investmentElement in Elements)
                    {
                        investmentElement.SecondView = Budget.SecondView;
                        investmentElement.EndCalculated = false;
                        if (first)
                        {
                            LastCalculatedFinishDate = investmentElement.FinishDate();
                            first = false;
                        }
                        else
                        {
                            var date = investmentElement.FinishDate();
                           
                            if (IsDateEarlier(LastCalculatedFinishDate,date))
                                LastCalculatedFinishDate = date;
                        }
                    }

                             if (((BudgetViewType) Budget.SecondView == BudgetViewType.All && Budget.HasActivities) ||
                                 ((BudgetViewType) Budget.SecondView == BudgetViewType.Equipment &&
                                  Budget.EquipmentComponent.HasActivities) ||
                                 ((BudgetViewType) Budget.SecondView == BudgetViewType.Construction &&
                                  Budget.ConstructionComponent.HasActivities) ||
                                 ((BudgetViewType) Budget.SecondView == BudgetViewType.Others &&
                                  Budget.OtherExpensesComponent.HasActivities) ||
                                 ((BudgetViewType) Budget.SecondView == BudgetViewType.WorkCapital &&
                                  Budget.WorkCapitalComponent.HasActivities))
                             {

                                 if (first)
                                 {
                                     LastCalculatedFinishDate = Budget.FinishDate();
                                     // first = false;
                                 }
                                 else
                                 {
                                     var date = Budget.FinishDate();
                            if (IsDateEarlier(LastCalculatedFinishDate, date))
                                LastCalculatedFinishDate = date;

                        }
                             }
                             else
                             {
                                    if (Elements.Items.Count == 0)
                                        LastCalculatedFinishDate = Period.OriEnd();
                             }
                           
                             EndCalculated = true;
                }

              
              }
            

            return LastCalculatedFinishDate;
        }
        public object View
        {
            get { return _view; }
            set
            {
                if (value != null)
                    _view = (BudgetComponentItemViewType)value;

                //OnPropertyChanged(() => FilterCommand);
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

                   Budget.SecondView =  (BudgetViewType)value;
                    if(Budget.LastCalculatedStartDate != LastCalculatedStartDate)
                    StartCalculated = false;
                    if (Budget.LastCalculatedFinishDate != LastCalculatedFinishDate)
                        EndCalculated = false;
                    if (Budget.LastCalculatedStartDate != LastCalculatedStartDate && Budget.LastCalculatedFinishDate != LastCalculatedFinishDate)
                        Period.Notify();
                }
                OnPropertyChanged(() => SecondView);
                OnPropertyChanged(() => Period);
            }
        }

        public override void DoNotify()
        {

            Period.Notify();
            OnPropertyChanged(() => Period);
           // Elements.Load();
            OnPropertyChanged(() => Elements);
            OnPropertyChanged(() => Items);
            OnPropertyChanged(() => ExecutedCost);
            OnPropertyChanged(() => ExecutionPercent);
            base.DoNotify();
            Parent?.DoNotify();

        }


        ///// <summary>
        ///// Gets the investment element being represented in the user interface by this presenter view model.
        ///// </summary>
        public IEntity CopiedObject { get; set; }
        //public ICopyPasteable ObjectToPasteOn { get; set; }
        public void Paste()
        {
            if (CopiedObject == null)
                throw new NullReferenceException("investmentComponent");
            if (Elements.Items.All(x => x.Id.ToString() != CopiedObject.Id.ToString()))
            {
                var newService = ServiceLocator.Current.GetInstance<IInvestmentComponentManagerApplicationServices>();
                newService.InvestmentElement = Object;
                newService.CopiedObject = CopiedObject;
                newService.Paste();
                Elements.Load();
                IsExpanded = true;
                DoNotify();
            }

        }

        public ICommand FilterCommand { get { return Elements.FilterCommand; } }

       // private bool _endCalculated = false;

        public bool EndCalculated
        {
           

            get { return Object.EndCalculated; }
            set
            {
                if (value != null && value != Object.EndCalculated)
                {
                    bool change = Object.EndCalculated;
                    Object.EndCalculated = value;
                    CreateServices().Update(Object);
                    if (!Object.EndCalculated && change)
                    {
                        OnPropertyChanged(() => Period);
                        if (Parent != null && Parent.GetType().Implements<IPeriodCalculator>())
                            ((IPeriodCalculator)Parent).EndCalculated = Object.EndCalculated;
                        //if (Budget.EndCalculated)
                        //    Budget.EndCalculated = _endCalculated;
                    }

                    //  OnPropertyChanged(() => Executor);
                }
            }
        }
     //   private bool _startCalculated = false;

        public bool StartCalculated
        {
            get { return Object.StartCalculated; }
            set
            {
                if (value != null && value != Object.StartCalculated)
                {
                    bool change = Object.StartCalculated; ;
                    Object.StartCalculated = value;

                    CreateServices().Update(Object);
                    //SetProperty(v => Object.StartCalculated = v, _startCalculated);
                    if (!Object.StartCalculated && change )
                    {
                        Period.Notify();
                        if (Parent != null && Parent.GetType().Implements<IPeriodCalculator>())
                            ((IPeriodCalculator)Parent).StartCalculated = Object.StartCalculated;;
                        //if (Budget.StartCalculated)
                        //    Budget.StartCalculated = _startCalculated;
                    }
                   
                    //  OnPropertyChanged(() => Executor);
                }
            }
           
        }
        public DateTime LastCalculatedFinishDate
        {
            get { return Object.LastCalculatedFinishDate; }
            set
            {
                if (value != null && value != Object.LastCalculatedFinishDate)
                {
                    
                    Object.LastCalculatedFinishDate =  value;
                    CreateServices().Update(Object);
                    //  OnPropertyChanged(() => Executor);
                }
            }
        }

        public DateTime LastCalculatedStartDate
        {
            get { return Object.LastCalculatedStartDate; }
            set
            {
                if (value != null && value != Object.LastCalculatedStartDate)
                {
                    Object.LastCalculatedStartDate =  value;
                    CreateServices().Update(Object);
                    //  OnPropertyChanged(() => Executor);
                }
            }
        }

        public decimal PlannedCost { get { return Budget.PlannedCost + Elements.Sum(x => x.PlannedCost); } }
        public decimal ExecutedCost { get { return Budget.ExecutedCost + Elements.Sum(x => x.ExecutedCost); } }
        public decimal ExecutionPercent { get { return PlannedCost!=0? Math.Round(ExecutedCost * 100 / PlannedCost,2):0; } }

        public decimal BudgetByCurrencyAndPeriod(ICurrency currency, IPeriod period)
        {
            return Budget.BudgetByCurrencyAndPeriod(currency,period) + Elements.Sum(x => x.BudgetByCurrencyAndPeriod(currency,period));
        }

        public IList<IInvestmentElementPresenter> MyElements { get { return Elements.Items.Cast<IInvestmentElementPresenter>().ToList(); } }



        public decimal GoDeepOnBudgetComponent(Type budgetComponent, ICurrency currency, IPeriod period)
        {
            if (budgetComponent.Implements<IEquipmentComponentPresenter>())
            {
                var equimentValue = Budget.EquipmentComponent.BudgetByCurrencyAndPeriod(currency, period);
               
                if (!RemenberList.Any(
                        x =>
                            x.Type.Implements<IEquipmentComponentPresenter>() && x.Currency == currency &&
                            x.Period == period))
                    RemenberList.Add(new Componenttable() {Type = budgetComponent,Currency = currency,Period = period , Value = equimentValue });
                else
                {
                    var eq = RemenberList.Single(
                        x =>
                            x.Type.Implements<IEquipmentComponentPresenter>() && x.Currency == currency &&
                            x.Period == period);
                    eq.Value = equimentValue;
                }

                return equimentValue + MyElements.Sum(x => x.GoDeepOnBudgetComponent(budgetComponent, currency, period)); 
            }
            if (budgetComponent.Implements<IConstructionComponentPresenter>())
            {
                var construcValue = Budget.ConstructionComponent.BudgetByCurrencyAndPeriod(currency, period);

                if (!RemenberList.Any(
                        x =>
                            x.Type.Implements<IConstructionComponentPresenter>() && x.Currency == currency &&
                            x.Period == period))
                    RemenberList.Add(new Componenttable() { Type = budgetComponent, Currency = currency, Period = period, Value = construcValue });
                else
                {
                    var cons = RemenberList.Single(
                        x =>
                            x.Type.Implements<IConstructionComponentPresenter>() && x.Currency == currency &&
                            x.Period == period);
                    cons.Value = construcValue;
                }

                return construcValue +
                          MyElements.Sum(x => x.GoDeepOnBudgetComponent(budgetComponent, currency, period));
            }
            if (budgetComponent.Implements<IOtherExpensesComponentPresenter>())
            {
                var otherValue = Budget.OtherExpensesComponent.BudgetByCurrencyAndPeriod(currency, period);

                if (!RemenberList.Any(
                        x =>
                            x.Type.Implements<IOtherExpensesComponentPresenter>() && x.Currency == currency &&
                            x.Period == period))
                    RemenberList.Add(new Componenttable() { Type = budgetComponent, Currency = currency, Period = period, Value = otherValue });
                else
                {
                    var other = RemenberList.Single(
                        x =>
                            x.Type.Implements<IOtherExpensesComponentPresenter>() && x.Currency == currency &&
                            x.Period == period);
                    other.Value = otherValue;
                }

                return otherValue +
                          MyElements.Sum(x => x.GoDeepOnBudgetComponent(budgetComponent, currency, period));
            }
            if (budgetComponent.Implements<IWorkCapitalComponentPresenter>())
            {
                var workValue = Budget.WorkCapitalComponent.BudgetByCurrencyAndPeriod(currency, period);

                if (!RemenberList.Any(
                        x =>
                            x.Type.Implements<IWorkCapitalComponentPresenter>() && x.Currency == currency &&
                            x.Period == period))
                    RemenberList.Add(new Componenttable() { Type = budgetComponent, Currency = currency, Period = period, Value = workValue });
                else
                {
                    var work = RemenberList.Single(
                        x =>
                            x.Type.Implements<IWorkCapitalComponentPresenter>() && x.Currency == currency &&
                            x.Period == period);
                    work.Value = workValue;
                }

                return workValue +
                         MyElements.Sum(x => x.GoDeepOnBudgetComponent(budgetComponent, currency, period));
            }

            if (budgetComponent.Implements<IFixedCapitalPresenter>())
            {
                var fixedCapital = RemenberList.Where(x =>
                           (x.Type.Implements<IEquipmentComponentPresenter>()|| x.Type.Implements<IConstructionComponentPresenter>()|| x.Type.Implements<IOtherExpensesComponentPresenter>())
                           && x.Currency == currency &&
                            x.Period == period).Sum(x=>x.Value);
                return fixedCapital + MyElements.Sum(x => x.GoDeepOnBudgetComponent(budgetComponent, currency, period));
            }

            if (budgetComponent.Implements<IBudgetPresenter>())
            {
                var totalCapital = RemenberList.Where(x =>
                          x.Currency == currency &&
                            x.Period == period).Sum(x => x.Value);
                return totalCapital + MyElements.Sum(x => x.GoDeepOnBudgetComponent(budgetComponent, currency, period));
            }

            return 0;
        }

        private  IList<Componenttable> RemenberList = new List<Componenttable>();
        public Thickness TimeLineThickness { get; set; }
        public void SpreadChanges(IBudgetComponentItem toSpread)
        {
           Budget.SpreadChanges(toSpread);
        }

        public virtual decimal Cost
        {
            get
            {
                if (!IsCostCalculated)
                {
                    decimal cost = 0;
                    //if (Currency != null)
                    foreach (INavigable val in Items)
                    {
                        cost += val.Cost;
                        //if (val.Currency != null)
                        //    if (val.Currency.Id.ToString() == Currency.Id.ToString())
                        //    {
                        //        cost += val.Cost;
                        //    }
                        //    else
                        //    {
                        //        var curren = val.Currency.ConvertionFactorFor(Currency.Object);

                        //        cost += val.Cost * curren;

                        //    }

                    }

                    cost += Budget.Cost;
                    _calculatedCost = cost;
                    IsCostCalculated = true;
                }

                //if (Currency!=null)
                return _calculatedCost;
            }
        }

        private decimal _calculatedCost;

        private bool _isCostCalculated;
        public override bool IsCostCalculated
        {
            get { return _isCostCalculated; }
            set
            {
                _isCostCalculated = value;
                if (!_isCostCalculated && Parent != null)
                {
                    OnPropertyChanged(() => Cost);
                    Parent.IsCostCalculated = false;
                }

            }
        }

        private int _inDeep;

        public int InDeep
        {
            get
            {
                if (_inDeep == 0)
                    _inDeep = CreateServices().InDeep(Object);
                return _inDeep;
            }
        }

        public Brush BackgroundColorBrush { get; set; }
    }

    public struct Componenttable
    {
        public Type Type;
        public decimal Value;
        public ICurrency Currency;
        public IPeriod Period;

    }
}