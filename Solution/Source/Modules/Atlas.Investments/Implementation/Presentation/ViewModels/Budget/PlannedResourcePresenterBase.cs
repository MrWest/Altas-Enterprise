using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Implementation of the base contract <see cref="IPlannedResourcePresenter{TComponent}"/> representing the presenter view model
    /// decorating and impersonating in the UI a planned resource of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the decorated planned resource.</typeparam>
    /// <typeparam name="TServices">
    /// The type of the application services used to make the updates made to the decorated planned resource.
    /// </typeparam>
    public  class PlannedResourcePresenterBase<TComponent> :
        BudgetComponentItemPresenterBase<IPlannedResource, IBudgetComponentResourceManagerApplicationServices<TComponent>>,
        IPlannedResourcePresenter<TComponent>
        where TComponent : class ,IBudgetComponentItem
       
    {
        private IBudgetComponentItemPresenter<TComponent> _component;
        public IBudgetComponentItemPresenter<TComponent> Component
        {
            get
            {
                if (_component == null)
                    throw new InvalidOperationException(Resources.InitializeComponentReferenceBeforeUsingIt);

                return _component;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _component = value;
            }
        }


        /// <summary>
        /// Gets or sets an identifier to differentiate the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/> 
        /// with respect to others.
        /// </summary>
        public override decimal Quantity
        {
            get { return Norm == 0 || Component == null ? Math.Round(Object.Quantity, 2) : Math.Round(Component.Quantity * Norm, 2);  }
            set
            {
                SetProperty(v => Object.Quantity = v, value);
               // OnPropertyChanged(() => Quantity);
                DoNotify();

            }
        }

      
       
        /// <summary>
        /// Gets or sets the Norm for the current <see cref="IBudgetComponentItemPresenter{TItem, TComponent}"/>.
        /// </summary>
        public decimal Norm
        {
            get
            {
                return Object.Norm;
            }
            set
            {
                SetProperty(v => Object.Norm = v, value);
                OnPropertyChanged(() => Norm);
                DoNotify();
            }
        }

        public IWeightPresenter Weight { get; set; }
        public IVolumePresenter Volume { get; set; }

        public ResourceKind ResourceKind
        {
            get { return Object.ResourceKind; }
            set
            {
                
                SetProperty(v => Object.ResourceKind = v, value);
                OnPropertyChanged(()=>ResourceKind);
                OnPropertyChanged(() => MenNumber);

            }
        }

        public decimal WasteCoefficient
        {
            get
            {
                return Object.WasteCoefficient;
            }
            set
            {
                SetProperty(v => Object.WasteCoefficient = v, value);
                
            }
            
        }

        public IWageScalePresenter WageScale
        {
            get
            {
               
                return Object.WageScale != null ? ServiceLocator.Current.GetInstance<IWageScaleProvider>().WageScales.SingleOrDefault(x => !Equals(x.Id,null)&& x.Id.ToString() == Object.WageScale.ToString()) : null;

            }
            set
            {
                if (value != null)
                {
                    SetProperty(v => Object.WageScale = v.Id, value);
                    OnPropertyChanged(() => WageScale);
                    OnPropertyChanged(() => UnitaryCost);
                    OnPropertyChanged(() => Cost);
                }
            } 
        }

        public int MenNumber
        {
            get
            {
                return Object.MenNumber;
            }
            set
            {
                SetProperty(v => Object.MenNumber = v, value);
                NotifyUp();
                //OnPropertyChanged(() => UnitaryCost);
                //OnPropertyChanged(() => Cost);
            }
            
        }

        /// <summary>
        /// Get or sets the Category of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        public virtual IResourceSupplierPresenter Supplier
        {

            get
            {
                return Object.Supplier != null
                    ? ServiceLocator.Current.GetInstance<IResourceSupplierProvider>()
                        .Suppliers.SingleOrDefault(x => x.Id.ToString() == Object.Supplier.ToString())
                    : null;
            }
            set
            {
                if (value != null)
                {
                    SetProperty(v => Object.Supplier = v.Id, value);
                    OnPropertyChanged(() => Supplier);
                }
            }
        }
        /// <summary>
        /// Get or sets the Category of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        public virtual IResourceProviderPresenter Provider
        {

            get
            {
                return Object.Provider != null
                    ? ServiceLocator.Current.GetInstance<IResourceProviderProvider>()
                        .Providers.SingleOrDefault(x => x.Id.ToString() == Object.Provider.ToString())
                    : null;
            }
            set
            {
                if (value != null)
                {
                    SetProperty(v => Object.Provider = v.Id, value);
                    OnPropertyChanged(() => Provider);
                }
            }
        }


        /// <summary>
        /// returns the kind of the current budget item
        /// </summary>
        public override String Kind
        {
            get { return "Resource"; }
        }
        public virtual string DeleteText
        {
            get { return Resources.DeleteResourse; }
        }
        public override Thickness DeepThickness
        {
            get { return new Thickness(Component.DeepThickness.Left + 8, 0, 0, 0); }
        }

        /// <summary>
        /// Gets the message that is displayed to the user when the current planned resource presenter view model has changed.
        /// </summary>
        protected override string SucessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedPlannedResource; }
        }

       

        /// <summary>
        /// Notify changes to superior levels
        /// </summary>
        public override void NotifyUp()
        {
            
            OnPropertyChanged(() => Quantity);
            OnPropertyChanged(() => UnitaryCost);
            OnPropertyChanged(() => Cost);
            Component.DoNotify();
        }

        public override void NotifyDown()
        {
           
            base.NotifyDown();
            if(ResourceKind == ResourceKind.Activity)
             EndCalculated = false;




        }
        public override void DoNotify()
        {
           

            OnPropertyChanged(() => WageScale);
            OnPropertyChanged(() => WasteCoefficient);
            OnPropertyChanged(() => Volume);
            OnPropertyChanged(() => Weight);
            OnPropertyChanged(() => Norm);
            OnPropertyChanged(() => ResourceKind);

            //OnPropertyChanged(() => Category);
            // OnPropertyChanged(() => Quantity);

            // PlannedResources.Load();

            base.DoNotify();
           
            Period.Notify();
            OnPropertyChanged(() => Period);

            Parent.IsCostCalculated = false;

            
            if (ResourceKind == ResourceKind.MenLabor)
            {
                var periodCalc =    (Parent as IPeriodCalculator);
                if (periodCalc != null) periodCalc.EndCalculated = false;
            }
               



        }
        protected override IBudgetComponentResourceManagerApplicationServices<TComponent> CreateServices()
        {
            IBudgetComponentResourceManagerApplicationServices<TComponent> services = base.CreateServices();
            services.Component = Component.Object as TComponent;
            return services;
        }

        public override INavigable Parent
        {
            get { return Component; }
        }

        public override void SpreadChanges(IBudgetComponentItem toSpread)
        {

            base.SpreadChanges(toSpread);
            foreach (IPlannedResourcePresenter<IPlannedResource> plannedResourcePresenter in PlannedResources.Items)
            {
                plannedResourcePresenter.SpreadChanges(toSpread);
            }
        }

        public override void Notify()
        {

            base.Notify();


            var provider = Object.Provider;
            Object.Provider = null;
            OnPropertyChanged(() => Provider);
            Object.Provider = provider;
            OnPropertyChanged(() => Provider);


            var supplier = Object.Supplier;
            Object.Supplier = null;
            OnPropertyChanged(() => Supplier);
            Object.Supplier = supplier;
            OnPropertyChanged(() => Supplier);
          

            Volume.Notify();
            Weight.Notify();

            StartCalculated = false;
            EndCalculated = false;
            Period.Notify();
            OnPropertyChanged(() => Period);


        }

        public DateTime StartDate()
        {
            if (!StartCalculated)
            {
                LastCalculatedStartDate = Period.OriStart();
                StartCalculated = true;
            }
            return LastCalculatedStartDate;
        }

        public DateTime FinishDate()
        {
            if (!EndCalculated)
            {
                //if (PlannedResources.Items.Any() && PlannedResources.Items.All(x => x.ResourceKind == ResourceKind.Activity))
                //{

                //}
                LastCalculatedFinishDate = ExecuteUsingServices(service =>
                {
                    return service.FinishDate(Object);
                });

                EndCalculated = true;
            }
            return LastCalculatedFinishDate;
            //// get all the menlabor resources

            //var plannedResources = new List<IPlannedResourcePresenter>();

            //foreach (IPlannedResourcePresenter plannedResourcePresenter in PlannedResources.Items)
            //{
            //    if (plannedResourcePresenter.ResourceKind == ResourceKind.MenLabor)
            //        plannedResources.Add(plannedResourcePresenter);
            //}

            ////get all hours quantity already has the hours needed to finish (by one man) TODO arrange that
            //var hours = new List<decimal>();
            //foreach (IBudgetComponentItemPresenter plannedResource in plannedResources)
            //{
            //    hours.Add(plannedResource.Quantity / (((IPlannedResourcePresenter)plannedResource).MenNumber));
            //}

            ////if nothing here
            //if (hours.Count == 0)
            //    return Period.OriEnd();
            ////get max hours amount
            //var maxhours = hours.Max();

            //int days = (int)maxhours / 8;

            //if ((int) maxhours % 8 > 0)
            //    days++;
            //return Period.Starts.AddDays(days);
        }


        //private bool _endCalculated = false;

        public bool EndCalculated
        {
            get { return Object.EndCalculated; }
            set
            {
                bool change = Object.EndCalculated;
               

                Object.EndCalculated = value;
                CreateServices().FreeUpdate(Object);
                if (change )
                {
                    Period.Notify();
                    if (Component  as IPeriodCalculator != null)
                        (Component as IPeriodCalculator).EndCalculated = Object.EndCalculated;
                }
            }
        }
       // private bool _startCalculated = false;

        public bool StartCalculated
        {
            get { return Object.StartCalculated; }
            set
            {
                bool change = Object.StartCalculated;
                
                Object.StartCalculated = value;
                CreateServices().FreeUpdate(Object);
                if (change )
                {
                   
                    if ((Component as IPeriodCalculator) != null)
                    {
                        (Component as IPeriodCalculator).StartCalculated = Object.StartCalculated;
                        
                    }

                    EndCalculated = value;
                    Period.Notify();

                }
            }
        }
        public DateTime LastCalculatedFinishDate
        {
            get { return Object.LastCalculatedFinishDate; }
            set
            {
                if (value != null)
                {
                    Object.LastCalculatedFinishDate = value;
                    CreateServices().FreeUpdate(Object);
                    //SetProperty(v => Object.LastCalculatedFinishDate = v, value);
                    //  OnPropertyChanged(() => Executor);
                }
            }
        }

        public DateTime LastCalculatedStartDate
        {
            get { return Object.LastCalculatedStartDate; }
            set
            {
                if (value != null)
                {
                    Object.LastCalculatedStartDate = value;
                    CreateServices().FreeUpdate(Object);
                    //SetProperty(v => Object.LastCalculatedStartDate = v, value);
                    //  OnPropertyChanged(() => Executor);
                }
            }
        }
    }
}
