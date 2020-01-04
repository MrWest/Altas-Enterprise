using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Base class of those presenter view models decorating and impersonating in the UI the items of a certain budget component.
    /// </summary>
    /// <typeparam name="TItem">The type of the budget component item being decorated.</typeparam>
    /// <typeparam name="TComponent">The type of the budget component containing the item.</typeparam>
    /// <typeparam name="TServices">The application services used to make the updates of the budget component item.</typeparam>
    public abstract class BudgetComponentItemPresenterBase<TItem, TServices> :
        NavigableNomenclator<TItem, TServices>,
        IBudgetComponentItemPresenter<TItem>
        where TItem : class, IBudgetComponentItem
        //where TComponent : class, IEntity
        where TServices : class, IBudgetComponentItemManagerApplicationServices<TItem>

    {

        // private TComponent _component;

        protected IPlannedResourceViewModel<TItem> _plannedResources;

        //private IEntity _budgetComponentItemParent;
        /// <summary>
        /// Initializes a new instance of <see cref="BudgetComponentItemPresenterBase{TItem, TComponent, TServices}"/>.
        /// </summary>
        //public BudgetComponentItemPresenterBase()
        //{
        // //   IsExpanded = true;
        //}
        protected BackgroundWorker _BackgroundWorker;

        public BudgetComponentItemPresenterBase()
        {
            _BackgroundWorker = new BackgroundWorker();
            _BackgroundWorker.DoWork += DoNotifyOnDoWork;
            _BackgroundWorker.RunWorkerCompleted += GetrealUnitaryOnRunWorkerCompleted;
        }


        private bool save;
        private void GetrealUnitaryOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            //OnPropertyChanged(() => Quantity);

            //// PlannedResources.Load();
            //OnPropertyChanged(() => UnitaryCost);
            ////  base.DoNotify();  //
            //OnPropertyChanged(() => Cost);
            _isAwaiting = save;

            AllowUdpateNotifications = true;
            if (AllowUdpateNotifications)
                StatusBarServices.SignalText(SucessfullyUpdatedMessage.EasyFormat(Object));

            //StatusBarServices.SignalReady();

        }

        private void DoNotifyOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
           
            DoNotify();
        }

        public IPeriodPresenter Period
        {
            get; set;
        }


        public object BudgetComponent
        {
            get { return this.ToString(); }
            set
            {
                _objectValue = value;
                if (_service == null)
                    _service = ServiceLocator.Current.GetInstance<TServices>();
                _objectValue = _service.Find(value);
                AdquireOnThis(_objectValue as TItem);
                OnPropertyChanged(() => BudgetComponent);


            }
        }

        ///// <summary>
        ///// Initializes a new instance of <see cref="BudgetComponentItemPresenterBase{TItem, TComponent, TServices}"/> given the
        ///// budget component item.
        ///// </summary>
        ///// <param name="budgetComponentItem">
        ///// The <see cref="IBudgetComponentItem"/> to decorate by the initializing
        ///// <see cref="BudgetComponentItemPresenterBase{TItem, TComponent, TServices}"/>.
        ///// </param>
        ///// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        //public BudgetComponentItemPresenterBase(TItem budgetComponentItem)
        //    : base(budgetComponentItem)
        //{
        //  //  IsExpanded = false;
        //   // AddResourceCommand = PlannedResources.AddCommand;

        //}




        /// <summary>
        /// Gets or sets an identifier to differentiate the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/> 
        /// with respect to others.
        /// </summary>
        public virtual decimal Quantity
        {
            get { return Math.Round(Object.Quantity, 2);  }
            set
            {
                AllowUdpateNotifications = false;
                StatusBarServices.SignalWaitOperation();
                save = _isAwaiting;
                _isAwaiting = true;

                // SetProperty(v => Object.Quantity = v, value);
                //OnPropertyChanged(() => Quantity);
                SetProperty(v => Object.Quantity = v, value);



                _BackgroundWorker.RunWorkerAsync(value);
              //  DoNotify();
               

            }
        }

        /// <summary>
        /// Gets or sets the measurement unit
        /// </summary>
        public virtual IMeasurementUnitPresenter MeasurementUnit
        {
            get
            {
                return Object.MeasurementUnit != null
                    ? ServiceLocator.Current.GetInstance<IMeasurementUnitProvider>()
                        .MeasurementUnits.SingleOrDefault(x => x.Id.ToString() == Object.MeasurementUnit.ToString())
                    : null;
            }
            set
            {
                if (value != null)
                {

                    SetProperty(v => Object.MeasurementUnit = v.Id, value);
                    OnPropertyChanged(() => MeasurementUnit);
                }
            }
        }

        ///// <summary>
        ///// Gets or sets the currency
        ///// </summary>
        public override ICurrencyPresenter Currency
        {
            get
            {
                return Object.Currency != null
                    ? ServiceLocator.Current.GetInstance<ICurrencyProvider>()
                        .Currencies.SingleOrDefault(x => x.Id.ToString() == Object.Currency.ToString())
                    : null;
            }
            set
            {
                if (value != null)
                {

                    SetProperty(v => Object.Currency = v.Id, value);
                    OnPropertyChanged(() => Currency);
                    OnPropertyChanged(() => Cost);
                }
            }
        }

       

        /// <summary>
        /// Get or sets the Category of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        public virtual ICategoryPresenter Category
        {

            get
            {
                return Object.Category != null
                    ? ServiceLocator.Current.GetInstance<ICategoryProvider>()
                        .Categories.SingleOrDefault(x => x.Id.ToString() == Object.Category.ToString())
                    : null;
            }
            set
            {
                if (value != null)
                {
                    SetProperty(v => Object.Category = v.Id, value);
                    OnPropertyChanged(() => Category);
                }
            }
        }


        //public static DependencyProperty SubLevelProperty = DependencyProperty.Register("SubExpenseConcept", typeof(object), typeof(BudgetComponentItemBase), new PropertyMetadata(OnSubLevelChanged));

        //private static void OnSubLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var sublevel = e.NewValue;
        //}

        protected ISubExpenseConceptPresenter _subExpenseConcept;

        /// <summary>
        /// Get or sets the Expense Concept of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        public virtual ISubExpenseConceptPresenter SubExpenseConcept
        {
            get
            {
                if (_subExpenseConcept == null)
                {
                    _subExpenseConcept = ServiceLocator.Current.GetInstance<ISubExpenseConceptPresenter>();
                    var provider =
                        ServiceLocator.Current
                            .GetInstance<IEntityProviderManagerApplicationServices<ISubExpenseConcept>>();

                    var entity = provider.GetEntity(Object.SubExpenseConcept);
                    //entity = ServiceLocator.Current.GetInstance<ISubExpenseConcept>();
                    //entity.Name = "ANTIMETA";
                    if (entity != null)
                        _subExpenseConcept.Object = entity;
                }
                //  return Object.SubExpenseConcept != null ? ServiceLocator.Current.GetInstance<IExpenseConceptProvider>().ExpenseConcepts.FirstOrDefault(x => true) : null;
                return _subExpenseConcept;
            }
            set
            {
                if (value != null)
                {
                    SetProperty(v => Object.SubExpenseConcept = v.Object.Id, value);
                    _subExpenseConcept = null;
                    OnPropertyChanged(() => SubExpenseConcept);
                }

            }

        }

        /// <summary>
        ///     Gets the shortened version of the current <see cref="InvestmentElementPresenterBase{T,TServices}" /> name.
        /// </summary>
        public string ShortName
        {
            get { return Name != null ? (Name.Length > 40 ? Name.Substring(0, 40) + "..." : Name) : string.Empty; }
            set
            {
                SetProperty(v => Object.Name = v, value);
                OnPropertyChanged(() => Name);
               
                OnPropertyChanged(() => ShortName);
            }
        }
        /// <summary>
        /// Notify changes to superior levels
        /// </summary>
        public abstract void NotifyUp();

        /// <summary>
        /// Notify changes to inferior levels
        /// </summary>
        public virtual void NotifyDown()
        {
            if (_isAwaiting)
            {
                _realUnitaryCost = -1;

                isUnitaryPriceCalculated = false;
            }

            OnPropertyChanged(() => Quantity);
                OnPropertyChanged(() => UnitaryCost);
                OnPropertyChanged(() => Cost);
                foreach (IPlannedResourcePresenter<TItem> plannedResource in PlannedResources.Items)
                {
                    plannedResource.NotifyDown();
                }
           
                
        }



    /// <summary>
        /// Gets or sets an identifier to differentiate the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
        /// </summary>
        public virtual string Code
        {
            get { return Object.Code; }
            set { SetProperty(v => Object.Code = v, value); }
        }



        /// <summary>
        /// Gets or sets the UnitaryCost for the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
        /// </summary>
        public virtual decimal UnitaryCost
        {
            get
            {

                // var crap = Math.Round(Object.UnitaryCost, 2);
                return  Math.Round(RealUnitaryCost, 2);
                // return PlannedResources.Items;
                //return (HasItems && Quantity > 0 && Cost > 0 ) ? Cost / Quantity :  Math.Round(Object.UnitaryCost, 2);
            }
            set
            {
                SetProperty(v => Object.UnitaryCost = v, Math.Round(value,2));

                SetProperty(v => Object.isUnitaryPriceCalculated = v, false);
                OnPropertyChanged(() => UnitaryCost);
                OnPropertyChanged(() => Cost);
                // NotifyDown();
                NotifyUp();
            }
        }

        private decimal _realUnitaryCost = -1;

        private  decimal RealUnitaryCost
        {
            get
            {
                if (isUnitaryPriceCalculated)
                    return CalculatedUnitaryPrice;

                if (_realUnitaryCost >= 0 )
                    return _realUnitaryCost;
                // var crap = Math.Round(Object.UnitaryCost, 2);
                // GetRealUnitaryCost();
               // StatusBarServices.ForceSignalLoading();
                if (Quantity > 0 && Currency != null)
                {
                    //if (!_isAwaiting)
                    //{
                    //    AsyncSignalText();
                    //    Thread thread = new Thread(AwaitTextShowing);
                    //    thread.SetApartmentState(ApartmentState.STA);
                    //    thread.Priority = ThreadPriority.Highest;
                    //  //  thread.Start();
                      
                    //}
                        
                    _realUnitaryCost = CreateServices().GetMyCost(Object, Currency.Object) / Quantity; ;
                    //StatusBarServices.SignalReady();
                }
                  
                else
                    _realUnitaryCost =  Math.Round(Object.UnitaryCost, 2);

                CalculatedUnitaryPrice = _realUnitaryCost;
                isUnitaryPriceCalculated = true;

                // StatusBarServices.SignalReady();

                return _realUnitaryCost;
                //return PlannedResources.Items;
                //return (HasItems && Quantity > 0 && Cost > 0 ) ? Cost / Quantity :  Math.Round(Object.UnitaryCost, 2);
            }
        }

        private  void AwaitTextShowing()
        {
        //    AllowUdpateNotifications = false;
            _isAwaiting = true;
            SetRealUnitaryCost(null);
            //   AllowUdpateNotifications = true;
            _isAwaiting = false;
            CalculatedUnitaryPrice = _realUnitaryCost;
            isUnitaryPriceCalculated = true;
            OnPropertyChanged(() => UnitaryCost);
            OnPropertyChanged(() => Cost);
            // NotifyDown();
            NotifyUp();
            StatusBarServices.SignalReady();
          
        }

        private void Action()
        {
            throw new NotImplementedException();
        }


        private decimal Function()
        {
            return CreateServices().GetMyCost(Object, Currency.Object) / Quantity;
            
        }

        private  void SetRealUnitaryCost(Task task)
        {
            _realUnitaryCost =  CreateServices().GetMyCost(Object, Currency.Object) / Quantity;
           

        }

       

        /// <summary>
        /// Gets or sets the UnitaryCost for the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
        /// </summary>
        public  bool isUnitaryPriceCalculated
        {
            get { return  Object.isUnitaryPriceCalculated; }
            set
            {
                Object.isUnitaryPriceCalculated = value;

               CreateServices().FreeUpdate(Object);
              // SetProperty(v => Object.isUnitaryPriceCalculated = v, value);

            }
        }

        /// <summary>
        /// Gets or sets the UnitaryCost for the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
        /// </summary>
        public  decimal CalculatedUnitaryPrice
        {
            get
            {
                return Object.CalculatedUnitaryPrice;
            }
            set
            {
                Object.CalculatedUnitaryPrice = value;

                CreateServices().FreeUpdate(Object);
              //  SetProperty(v => Object.CalculatedUnitaryPrice = v, value);
                
            }
        }
        /// <summary>
        /// Gets or sets the Cost for the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
        /// </summary>
        public override decimal Cost
        {
            get
            {

                return  Math.Round(RealUnitaryCost * Quantity, 2);
            }
        }

        /// <summary>
        /// Creates the application services used to make the updates made to the current
        /// <see cref="BudgetComponentItemPresenterBase{TItem, TComponent, TServices}"/>.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="TServices"/>.</returns>
        protected override TServices CreateServices()
        {
            TServices services = base.CreateServices();
           //services.Component = Component ;

            return services;
        }

        /// <summary>
        /// Gets the crud view model used to manage the planned resources of the budget component contained in the current
        /// presenter.
        /// </summary>
        public IPlannedResourceViewModel<TItem> PlannedResources
        {
            get { return GetOrInitialize(ref _plannedResources, x => x.Component = this ); }
        }
        protected TViewModel GetOrInitialize<TViewModel>(ref TViewModel viewModel, Action<TViewModel> initialize)
           where TViewModel : IPlannedResourceViewModel<TItem> 
        {
            if (viewModel == null)
            {
                viewModel = ServiceLocator.Current.GetInstance<TViewModel>();
                initialize(viewModel);
              //  viewModel.Component = this;
             
                viewModel.Load();
          
                viewModel.Raised += OnInteractionRequested;
                
            }

            return viewModel;
        }



        public override string NewText { get { return Resources.NewPlannedResourceName; } }


        ///// <summary>
        ///// Gets if the current budget component item has any planned resources
        ///// </summary>
        //public override bool HasItems {
        //    get
        //    {
        //        return _plannedResources != null && _plannedResources.Items.Count > 0;
        //    }
        //}

        //private bool _expanded ;
        private TServices _service;
        private object _objectValue;
        protected object _isVisible;

        ///// <summary>
        ///// Gets or sets the visual state for the  current budget component item
        ///// </summary>
        //public override bool IsExpanded {
        //    get { return _expanded; }
        //    set
        //    {
        //        if (_expanded != value)
        //        {
        //          _expanded = value;
        //            OnPropertyChanged(() => IsExpanded);
        //            OnPropertyChanged(() => IconData);
        //        }
              
        //    }
        //}

        //public virtual Thickness DeepThickness
        //{
        //    get
        //    {
                
        //            return new Thickness(0,0,0,0);
        //    }
            
        //}

        //public virtual string ItemKind { get; private set; }

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

        //public abstract void Notify();

        ///// <summary>
        ///// Invoked when the datacontext for the current view has been changed. Makes sure that the interactions channels
        ///// between this view and the new datacontext are wired up.
        ///// </summary>
        ///// <param name="sender">The object sending the event invoking this method.</param>
        ///// <param name="e">The event arguments containing the details about the datacontext change.</param>
        //protected virtual void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    this.SetupInteractionWithDataContext(e, OnInteractionRequested);
        //}

        ///// <summary>
        ///// Invoked when the current view's datacontext has requested an interaction with the current view.
        ///// </summary>
        ///// <param name="sender">The object sending the event invoking this method.</param>
        ///// <param name="e">The event arguments containing the details about the interaction request.</param>
        //protected virtual void OnInteractionRequested(object sender, InteractionRequestedEventArgs e)
        //{
        //    this.Execute(e);
        //}

        private TItem _toadquire;
        protected async void AdquireOnThis(TItem toadquire)
        {
          //  Name = toadquire.Name;

            _toadquire = toadquire;

            AllowUdpateNotifications = false;
            await SignalWaitOperation().ContinueWith(DoAdquire);
            AllowUdpateNotifications = true;
            StatusBarServices.SignalReady();
            //  DoAdquire(null);
            //NotifyUp();
            //NotifyDown();
            NotifyAquire();

        }

     

        private async void DoAdquire(Task task)
        {
            ExecuteUsingServices(services =>
            {
                services.AdquireProperties(Object, _toadquire);
                services.Dispose();
                return;
            });

          
        }

        private void NotifyAquire()
        {
            OnPropertyChanged(() => Code);
            OnPropertyChanged(() => Name);
            OnPropertyChanged(() => Description);
            OnPropertyChanged(() => MeasurementUnit);
            OnPropertyChanged(() => Currency);
            PlannedResources.Load();
            OnPropertyChanged(() => PlannedResources);
            DoNotify();
        }


        private async Task SignalWaitOperation()
        {
            StatusBarServices.SignalWaitOperation();

        }

        private async void ShowAwaitMessage()
        {
            await SignalWaitOperation();

           

        }
        public  override  void DoNotify()
        {

            if (!_isAwaiting)
            {
                _realUnitaryCost = -1;

                isUnitaryPriceCalculated = false;
            }
           
            //OnPropertyChanged(() => Name);
            //OnPropertyChanged(() => Description);
            //OnPropertyChanged(() => Code);
            //OnPropertyChanged(() => MeasurementUnit);
            //OnPropertyChanged(() => UnitaryCost);
            //OnPropertyChanged(() => Currency);
            OnPropertyChanged(() => SubExpenseConcept);
            OnPropertyChanged(() => Category);
            OnPropertyChanged(() => Quantity);

            // PlannedResources.Load();
            Period?.Notify();
            base.DoNotify();  // OnPropertyChanged(() => Cost);
            OnPropertyChanged(() => UnitaryCost);
            NotifyDown();
            NotifyUp();
            // OnPropertyChanged(() => Cost);
        }

        //public virtual void AdquireProperties(IBudgetComponentItemPresenter toAdquire)
        //{
        //    ExecuteUsingServices(services =>
        //    {
        //        services.AdquireProperties(Object, toAdquire.Object as IBudgetComponentItem);
        //        services.Dispose();
        //        return;
        //    });

        //    NotifyUp();
        //    NotifyUp();
        //    DoNotify();
        //    //{
        //    //// Do nothing if the entity cannot be updated or there cannot be executed the update altogether
        //    //if (!Update || !services.CanUpdate(Object))
        //    //    return false;

        //    //try
        //    //{
        //    //    // Update the entity
        //    //    services.Update(Object);
        //    //    return true;
        //    //}
        //    //catch (ValidationException exception)
        //    //{
        //    //    // In case of validation errors, register them
        //    //    errors.AddRange(exception.Errors);
        //    //}

        //    //return false;
        //    //}
        //    //);
        //}


        public override void Notify()
        {
            //_realUnitaryCost = -1;

           // isUnitaryPriceCalculated = false;

            base.Notify();

            var measurement = Object.MeasurementUnit;
            Object.MeasurementUnit = null;
               OnPropertyChanged(() => MeasurementUnit);
            Object.MeasurementUnit = measurement;
            OnPropertyChanged(() => MeasurementUnit);

            var currency = Object.Currency;
            Object.Currency = null;
            OnPropertyChanged(() => Currency);
                 Object.Currency = currency;
            OnPropertyChanged(() => Currency);

            var category = Object.Category;
            Object.Category = null;
            OnPropertyChanged(() => Category);
            Object.Category = category;
            OnPropertyChanged(() => Category);

            var subExpenseConcept = Object.SubExpenseConcept;
            Object.SubExpenseConcept = null;
            OnPropertyChanged(() => SubExpenseConcept);
            Object.SubExpenseConcept = subExpenseConcept;
            OnPropertyChanged(() => SubExpenseConcept);
          

              
        }
        public override ICrudViewModel Items
        {
            get { return PlannedResources; }
        }

        public virtual void SpreadChanges(IBudgetComponentItem toSpread)
        {
            if(toSpread.Code==Code && !Equals(toSpread,Object))
             BudgetComponent = toSpread.Id;
        }

        public bool ExistPlannedResource(string code)
        {
            var resourceViewModel = ServiceLocator.Current.GetInstance<IPlannedResourceViewModel<TItem>>();
            resourceViewModel.Component = this;
            return resourceViewModel.ExistPlannedResource(code);
        }

        public IPlannedResourcePresenter<TItem> GetPlannedResource(string code)
        {
            var resourceViewModel = ServiceLocator.Current.GetInstance<IPlannedResourceViewModel<TItem>>();
            resourceViewModel.Component = this;
            return resourceViewModel.GetPlannedResource(code);
        }

        public void AddFromScratch(string code, string name, string desc, string muId, string cuId, decimal norm, decimal price,
            int kind, string muwId, decimal wv)
        {
            var resourceViewModel = ServiceLocator.Current.GetInstance<IPlannedResourceViewModel<TItem>>();
            resourceViewModel.Component = this;
            resourceViewModel.AddFromScratch(code, name, desc, muId, cuId, norm, price, kind, muwId, wv);
        }
    }
}
