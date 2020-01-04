using System;
using System.Collections.Generic;
using System.Drawing;
using System.Enumable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Investments.Application.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Presentation.Views.Converters;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;
using Binding = System.Windows.Data.Binding;


namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.WorkCapital
{
    public class WorkCapitalCashFlowPresenter : EntityPresenterBase<IWorkCapitalCashFlow, IWorkCapitalCashFlowItemManagerApplicationServices>, IWorkCapitalCashFlowPresenter, IView
    {
        //private IWorkCapitalCashFlowCashMovementCategoryViewModel<ICashEntry> _cashEntries;
        //private IWorkCapitalCashFlowCashMovementCategoryViewModel<ICashOutgoing> _cashOutgoings;
        private IList<ICashMovementCategoryPresenter<ICashEntry>> _allCashEntries;
        private IList<ICashMovementCategoryPresenter<ICashOutgoing>> _allCashOutgoings;
        //private bool _showliquity;

        protected override IWorkCapitalCashFlowItemManagerApplicationServices CreateServices()
        {
            var serv = base.CreateServices();
            serv.WorkCapitalComponent = WorkCapitalComponent.Object;
            return serv;
        }

        public IWorkCapitalComponentPresenter WorkCapitalComponent { get; set; }
      
        /// <summary>
        /// Initializes a new instance of <see cref="BudgetPresenter"/> decorating a domain budget but without such budget.
        /// </summary>
        public WorkCapitalCashFlowPresenter()
        {
            Period = ServiceLocator.Current.GetInstance<IPeriod>();
            Period.Starts = Starts;
            Period.Ends = Ends;
            Period.PeriodKind = DateTimeScale;
            PeriodKind = DateTimeScale;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BudgetPresenter"/> decorating a domain budget.
        /// </summary>
        /// <param name="budget">The <see cref="IBudget"/> to present.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budget"/> is null.</exception>
        public WorkCapitalCashFlowPresenter(IWorkCapitalCashFlow budget)
            : base(budget)
        {
            Period = ServiceLocator.Current.GetInstance<IPeriod>();
            Period.Starts = Starts;
            Period.Ends = Ends;
            Period.PeriodKind = DateTimeScale;
            PeriodKind = DateTimeScale;
        }

        /// <summary>
        /// Gets the crud view model used to manage the planned resources of the budget component contained in the current
        /// presenter.
        /// </summary>
        public IWorkCapitalCashFlowCashMovementCategoryPresenter<ICashEntry> CashEntries { get; set; } //get
           
        /// <summary>
        /// Invoked when the current view's datacontext has requested an interaction with the current view.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The event arguments containing the details about the interaction request.</param>
        protected virtual void OnInteractionRequested(object sender, InteractionRequestedEventArgs e)
        {
            this.Execute(e);
        }
        private void Actioner(Func<ICashMovementCategory, object> method)
        {
            Actioner((services =>
            {
                return services.SubCategories.Count == 0?null:
                method(services);
                
            }));
        }
        /// <summary>
        /// Gets the crud view model used to manage the planned resources of the budget component contained in the current
        /// presenter.
        /// </summary>
        public IWorkCapitalCashFlowCashMovementCategoryPresenter<ICashOutgoing> CashOutgoings { get; set;}

      
        public IList<ICashMovementCategoryPresenter<ICashEntry>> CashEntriesInDeep
        {
            get
            {
                if(CashEntries.SubCategories.Items.Count==0)
                    CashEntries.SubCategories.Load();
                IList<ICashMovementCategoryPresenter<ICashEntry>> cashEntries = new List<ICashMovementCategoryPresenter<ICashEntry>>();
                InDeepList(ref cashEntries);
                return cashEntries;
            }
          
        }

        private void InDeepList(ref IList<ICashMovementCategoryPresenter<ICashEntry>> cashEntries) 
        {
            

            foreach (ICashMovementCategoryPresenter<ICashEntry> cashmovement in CashEntries.SubCategories.Items)
            {
                cashEntries.Add(cashmovement);
                if (cashmovement.IsExpanded)
                cashmovement.InDeepList(ref cashEntries);
            }
        }
        private void InDeepList(ref IList<ICashMovementCategoryPresenter<ICashOutgoing>> cashEntries) 
        {


            foreach (ICashMovementCategoryPresenter<ICashOutgoing> cashmovement in CashOutgoings.SubCategories.Items)
            {
                cashEntries.Add(cashmovement);
                if (cashmovement.IsExpanded)
                    cashmovement.InDeepList(ref cashEntries);
            }
        }
      

        public IList<ICashMovementCategoryPresenter<ICashOutgoing>> CashOutgoingsInDeep
        {
            get
            {
                if (CashOutgoings.SubCategories.Items.Count == 0)
                    CashOutgoings.SubCategories.Load();
                IList<ICashMovementCategoryPresenter<ICashOutgoing>> cashOutgoing = new List<ICashMovementCategoryPresenter<ICashOutgoing>>();
                InDeepList(ref cashOutgoing);
                return cashOutgoing;
            }
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


        public object DataContext { get; set; }

        private IPeriod _period;
        public IPeriod Period
        {
            get
            {
                if (_period == null || _period.Starts != Starts || _period.Ends != Ends)
                {
                    _period = ServiceLocator.Current.GetInstance<IPeriod>();
                    _period.Starts = Starts;
                    _period.Ends = Ends;
                    _period.PeriodKind = DateTimeScale;
                }
               
                return _period;
            }
            set { _period = value; }
        }

        public DateTime Starts 
        {
            get { return Object.Starts; }
            set
            {

                SetProperty(v => Object.Starts = v, value);
                Period.Starts = value;
                OnPropertyChanged(() => DataGridColumns);
                OnPropertyChanged(() => DataGridColumnsBuiltin);
                OnPropertyChanged(() => Ends);
                OnPropertyChanged(() => Starts);
                OnPropertyChanged(() => WorkCapital);
                OnPropertyChanged(() => BuiltinCash);
                
            }
        }
        public DateTime Ends
        {
            get { return Object.Ends; }
            set
            {
                SetProperty(v => Object.Ends = v, value);
                Period.Ends = value;
                OnPropertyChanged(() => DataGridColumns);
                OnPropertyChanged(() => DataGridColumnsBuiltin);
                OnPropertyChanged(() => Ends);
                OnPropertyChanged(() => Starts);
                OnPropertyChanged(() => WorkCapital);
                OnPropertyChanged(() => BuiltinCash);
                
            }
        }

        public DateTimeScale DateTimeScale
        {

            get { return Object.DateTimeScale; }
            set
            {
                SetProperty(v => Object.DateTimeScale = v, value);
                PeriodKind = value;
               

            }
        }
        public DateTimeScale PeriodKind
        {
            get { return Period.PeriodKind; }
            set
            {
                Period.PeriodKind = value;
                OnPropertyChanged(() => DataGridColumns);
                OnPropertyChanged(() => DataGridColumnsBuiltin);
                OnPropertyChanged(() => WorkCapital);
                OnPropertyChanged(() => CashPeriod);
                OnPropertyChanged(() => BuiltinCash);

            }
            
        }

        private IList<DataGridTextColumn> _dataGridColumns;

        public IList<ICashMovementCategoryPresenter<ICashOutgoing>> BuiltinCash
        {
            get
            {
                var _allCashMovements = ServiceLocator.Current.GetInstance<ICashMovementCategoryPresenter<ICashOutgoing>>();
                _allCashMovements.Object = ServiceLocator.Current.GetInstance<ICashOutgoing>();

                _allCashMovements.Object.SuperiorCategory = this.Object;


                IList<ICashMovement> fulllist = new List<ICashMovement>();

                foreach (var VARIABLE in CashEntries.AllCashMovements)
                    fulllist.Add(VARIABLE);


                var myService = CreateServices();
                //foreach (var period in Period.Periods)
                //{

                //    var aux = ServiceLocator.Current.GetInstance<ICashMovement>();
                //    aux.CashMovementCategory = VARIABLE.CashMovementCategory;
                //    aux.Date = VARIABLE.Date;
                //    aux.Amount = VARIABLE.Amount * -1;
                //    fulllist.Add(aux);
                //}


                _allCashMovements.SetMovementsList(myService.GeRemainingCashHistory(Period,CashEntries.Object,CashOutgoings.Object));



                var _allCashMovementList = new List<ICashMovementCategoryPresenter<ICashOutgoing>>();
                _allCashMovementList.Add(_allCashMovements);
                return _allCashMovementList;
            }
        }

        public IList<DataGridTextColumn> DataGridColumns
        {
            get { return TransformDataGridColumns(Period, "MovementsList",false); }
            set { _dataGridColumns = value; }
            
        }
        public IList<DataGridTextColumn> DataGridColumnsBuiltin
        {
            get { return TransformDataGridColumns(Period, "MovementsList", true); }
            set { _dataGridColumns = value; }

        }

        public IList<ICashMovementCategoryPresenter<ICashEntry>> AllCashEntries
        {
            get
            {
                if (_allCashEntries == null)
                {
                    _allCashEntries = new List<ICashMovementCategoryPresenter<ICashEntry>>();
                    _allCashEntries.Add(CashEntries);
                }
                    

                return _allCashEntries;
            }
        }

        public IList<ICashMovementCategoryPresenter<ICashOutgoing>> AllCashOutgoings
        {
            get
            {
                if (_allCashOutgoings == null)
                {
                    _allCashOutgoings = new List<ICashMovementCategoryPresenter<ICashOutgoing>>();
                    _allCashOutgoings.Add(CashOutgoings);
                }
                return _allCashOutgoings;
            }
        }

        public IList<ICashMovementCategoryPresenter<ICashOutgoing>> AllCashMovements
        {
            get
            {

                var _allCashMovements = ServiceLocator.Current.GetInstance<ICashMovementCategoryPresenter<ICashOutgoing>>();
               _allCashMovements.Object = ServiceLocator.Current.GetInstance<ICashOutgoing>();

                _allCashMovements.Object.SuperiorCategory = this.Object;
               

                IList<ICashMovement> fulllist = new List<ICashMovement>();
               
                    foreach (var VARIABLE in CashEntries.AllCashMovements)
                        fulllist.Add(VARIABLE);
                  
                        
               
                    foreach (var VARIABLE in CashOutgoings.AllCashMovements)
                    {
                        
                        var aux = ServiceLocator.Current.GetInstance<ICashMovement>();
                        aux.CashMovementCategory = VARIABLE.CashMovementCategory;
                        aux.Date = VARIABLE.Date;
                        aux.Amount = VARIABLE.Amount * -1;
                        fulllist.Add(aux);
                    }
                        
                  
                _allCashMovements.SetMovementsList(fulllist);

              

               var _allCashMovementList = new List<ICashMovementCategoryPresenter<ICashOutgoing>>();
               _allCashMovementList.Add(_allCashMovements);
               return _allCashMovementList;
            }
        }


        //public bool ShowLiquity
        //{
        //    get
        //    {
        //        return CashEntries.ShowLiquity;
        //    }
        //    set
        //    {
        //        CashEntries.ShowLiquity = value;
        //        CashEntries.SubCategories.Load();
        //        OnPropertyChanged(() => WorkCapital);
        //        OnPropertyChanged(() => AllCashEntries);
        //        if(AllCashEntries.Count>0)
        //        AllCashEntries[0].TellYourFather();
        //        OnPropertyChanged(() => AllCashMovements);
        //        if (AllCashMovements.Count > 0)
        //            AllCashMovements[0].TellYourFather();
        //        OnPropertyChanged(() => CashEntriesInDeep);
        //    }
        //}

        private IList<DataGridTextColumn> TransformDataGridColumns(IPeriod mainperiod, String binding,bool buitin)
        {
            IList<DataGridTextColumn> returnColumns = new List<DataGridTextColumn>();

            if(buitin)
            returnColumns.Add(new DataGridTextColumn()
            {
                Header = "Total",
                Width = new DataGridLength(25, DataGridLengthUnitType.Star),
                Binding = new Binding(binding) { Converter = new PeriodtoValueConverter(), ConverterParameter = mainperiod.Periods.LastOrDefault() }
                 ,
                CellStyle = CellStyle,
                IsReadOnly = true
            });
            else
                returnColumns.Add(new DataGridTextColumn()
                {
                    Header = "Total",
                    Width = new DataGridLength(25, DataGridLengthUnitType.Star),
                    Binding = new Binding(binding) { Converter = new PeriodtoValueConverter(), ConverterParameter = mainperiod }
                    , CellStyle = CellStyle , IsReadOnly = true

                });
          

            foreach (IPeriod period in mainperiod.Periods)
            {
                if (mainperiod.Periods.Count > 31)
                {
                    returnColumns.Add(new DataGridTextColumn()
                    {
                        Header = period.Name,
                        Width = DataGridLength.Auto,
                        Binding = new Binding(binding) { Converter = new PeriodtoValueConverter(), ConverterParameter = period }
                         ,
                        CellStyle = CellStyle
                    });
                }
                else
                {
                   
                        returnColumns.Add(new DataGridTextColumn()
                        {
                            Header = period.Name,
                            Width = new DataGridLength(25, DataGridLengthUnitType.Star),
                            Binding = new Binding(binding) { Converter = new PeriodtoValueConverter(), ConverterParameter = period },
                             
                            CellStyle = CellStyle


                        });
             
                   
                }



            }
            //OnPropertyChanged(() => WorkCapital);
            //var shit = WorkCapital;
            return returnColumns;
        }

        public bool isWorkCapitalCalculated
        {
            get { return Object.IsWorkCapitalCalculated; }
            set
            {
                Object.IsWorkCapitalCalculated = value;
                CreateServices().Update(Object);
                if(!value)
                    OnPropertyChanged(() => WorkCapital);
            }
        }

        public decimal CalculatedWorkCapital
        {
            get { return Object.CalculatedWorkCapital; }
            set
            {
                Object.CalculatedWorkCapital = value;
                CreateServices().Update(Object);
            }
        }
        public decimal WorkCapital
        {
            get
            {
                //if (!isVisible)
                //    return 0;

                if (!isWorkCapitalCalculated)
                {
                    CalculatedWorkCapital = CreateServices().GetWorkCapital(Period, CashEntries.Object, CashOutgoings.Object);
                    isWorkCapitalCalculated = true;
                }
                   

                return CalculatedWorkCapital;



            }
           
        }

        public override void Notify()
        {
            base.Notify();
            OnPropertyChanged(()=> WorkCapital);
        }

        public string CashPeriod
        {
            get
            {
                return Resources.CashAmount + " " + ResourceEnumConverter.ConvertToString(Period.PeriodKind);
            }
        }

        public  void TellYourFather()
        {
            OnPropertyChanged(() => CashEntriesInDeep);
            OnPropertyChanged(() => CashOutgoingsInDeep);
           // OnPropertyChanged(() => WorkCapital);
            OnPropertyChanged(() => AllCashMovements);
            OnPropertyChanged(() => BuiltinCash);
            
        }

        public Style CellStyle { get; set; }

        private bool _isvisible = false;

        public bool isVisible
        {
            get { return _isvisible; }
            set
            {
                _isvisible = value;
                if(_isvisible)
                    OnPropertyChanged(() => WorkCapital);
            }
        }
    }
}
