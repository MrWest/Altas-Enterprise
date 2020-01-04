using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Gets or sets the budget component to which belong the underlying item.
    /// </summary>
    public class PlannedActivityPresenter : BudgetComponentItemPresenterBase<IPlannedActivity, IPlannedActivityManagerApplicationServices>,
       IPlannedActivityPresenter
      //where TComponent : class, IBudgetComponent
      //  where TServices : class, IPlannedtActivityManagerApplicationServices
    {

        //private IBudgetComponentPresenter<TComponent> _component;
        //public IBudgetComponentPresenter<TComponent> Component
        //{
        //    get
        //    {
        //        if (_component == null)
        //            throw new InvalidOperationException(Resources.InitializeComponentReferenceBeforeUsingIt);

        //        return _component;
        //    }
        //    set
        //    {
        //        if (value == null)
        //            throw new ArgumentNullException("value");

        //        _component = value;
        //    }
        //}

        public ISubSpecialityHolderPresenter<IPlannedSubSpecialityHolder> SubSpecialityHolder { get; set; }

        public decimal PlannedCost { get; private set; }
        public decimal ExecutedCost { get; private set; }
        public decimal ExecutionPercent { get; private set; }

        public decimal BudgetByCurrencyAndPeriod(ICurrency currency, IPeriod period)
        {
            decimal rstl = 0;
            if (currency != null && period != null && Object.Currency!=null)
            {
                //if (!HasItems)
                return (currency.Id.ToString() == Object.Currency.ToString() && period.IsContained(Period.Object)) ? Cost : rstl;

            }
            if (currency != null && Object.Currency != null)
            {
                return (currency.Id.ToString() == Object.Currency.ToString()) ? Cost : rstl;
            }
            if (period != null)
            {
                var comparringPeriod = ServiceLocator.Current.GetInstance<IPeriod>();
                comparringPeriod.Starts = LastCalculatedFinishDate;
                comparringPeriod.Ends = LastCalculatedFinishDate;
                comparringPeriod.Holder = Object;
                return (period.IsContained(comparringPeriod)) ? CostByDaysPercent(Cost,period,comparringPeriod) : rstl;
            }

            return rstl;

        }

        private decimal CostByDaysPercent(decimal cost, IPeriod currentPeriod, IPeriod activityPeriod)
        {

            if (currentPeriod.Ends.CompareTo(activityPeriod.Ends) <= 0)
            {
                var aux = ServiceLocator.Current.GetInstance<IPeriod>();
                aux.Starts = activityPeriod.Starts;
                aux.Ends = currentPeriod.Ends;

                int currenDays = aux.Days;
                int activityDays = activityPeriod.Days;

                return (((currenDays * 100) / activityDays) / 100) * cost;
            }

             

            return cost;
        }
        protected ISubSpecialityPresenter _SubSpeciality;
        /// <summary>
        /// Get or sets the Expense Concept of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        public ISubSpecialityPresenter SubSpeciality
        {
            get
            {
                if (_SubSpeciality == null)
                {
                    _SubSpeciality = ServiceLocator.Current.GetInstance<ISubSpecialityPresenter>();
                    var provider = ServiceLocator.Current.GetInstance<IEntityProviderManagerApplicationServices<ISubSpeciality>>();

                    var entity = provider.GetEntity(Object.SubSpeciality);
                    //entity = ServiceLocator.Current.GetInstance<ISubExpenseConcept>();
                    //entity.Name = "ANTIMETA";
                    if (entity != null)
                        _SubSpeciality.Object = entity;
                }
                //  return Object.SubExpenseConcept != null ? ServiceLocator.Current.GetInstance<IExpenseConceptProvider>().ExpenseConcepts.FirstOrDefault(x => true) : null;
                return _SubSpeciality;
            }
            set
            {
                if (value != null)
                {
                    SetProperty(v => Object.SubSpeciality = v.Object.Id, value);
                    OnPropertyChanged(() => SubExpenseConcept);
                }

            }

        }


        /// <summary>
        /// returns the kind of the current budget item
        /// </summary>
        public override String Kind
        {
            get { return "PlannedActivity"; }
        }

        /// <summary>
        /// Get or sets the Category of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        public virtual IActivityExecutorPresenter Executor
        {

            get
            {
                return Object.Executor != null
                    ? ServiceLocator.Current.GetInstance<IActivityExecutorProvider>()
                        .Executors.SingleOrDefault(x => x.Id.ToString() == Object.Executor.ToString())
                    : null;
            }
            set
            {
                if (value != null)
                {
                    SetProperty(v => Object.Executor = v.Id, value);
                    OnPropertyChanged(() => Executor);
                }
            }
        }

        public override ICommand DeleteMySelfCommand
        {
            get
            {
                if (!Equals(SubSpecialityHolder,null)&& SubSpecialityHolder.GetType().Implements<IEquipmentPlannedSubSpecialityHolderPresenter>())
                {
                    return
                        (SubSpecialityHolder as IEquipmentPlannedSubSpecialityHolderPresenter).PlannedActivities
                        .DeleteCommand;
                }

                if (!Equals(SubSpecialityHolder, null) && SubSpecialityHolder.GetType().Implements<IConstructionPlannedSubSpecialityHolderPresenter>())
                {
                    return
                        (SubSpecialityHolder as IConstructionPlannedSubSpecialityHolderPresenter).PlannedActivities
                        .DeleteCommand;
                }

                if (!Equals(SubSpecialityHolder, null) && SubSpecialityHolder.GetType().Implements<IOtherExpensesPlannedSubSpecialityHolderPresenter>())
                {
                    return
                        (SubSpecialityHolder as IOtherExpensesPlannedSubSpecialityHolderPresenter).PlannedActivities
                        .DeleteCommand;
                }
                if (!Equals(SubSpecialityHolder, null) && SubSpecialityHolder.GetType().Implements<IWorkCapitalPlannedSubSpecialityHolderPresenter>())
                {
                    return
                        (SubSpecialityHolder as IWorkCapitalPlannedSubSpecialityHolderPresenter).PlannedActivities
                        .DeleteCommand;
                }

                return null;
              
            }
        }

       


        /// <summary>
        /// Notify changes to superior levels
        /// </summary>
        public override void NotifyUp()
        {
            //OnPropertyChanged(() => Quantity);
            //OnPropertyChanged(() => UnitaryCost);
            //IsCostCalculated = false;
            //OnPropertyChanged(() => Cost);
            //Parent.DoNotify();
           
        }

        public override INavigable Parent
        {
            get { return SubSpecialityHolder; }
        }

        protected override IPlannedActivityManagerApplicationServices CreateServices()
        {
            IPlannedActivityManagerApplicationServices services =  base.CreateServices();
            //services.Component = Component.Object;
            services.SubSpecialityHolder = SubSpecialityHolder?.Object;
            return services;
        }

        public DateTime StartDate()
        {
            
                if (!StartCalculated)
                {
                    //  LastCalculatedStartDate = Budget.InvestmentElement.Period.OriStart();

                    LastCalculatedStartDate = CreateServices().StartDate(Object);
                   

                    StartCalculated = true;
                }

          



            return LastCalculatedStartDate;
           // return Period.OriStart();
        }

        public DateTime FinishDate()
        {
           
                if (!EndCalculated)
                {
                    LastCalculatedFinishDate = CreateServices().FinishDate(Object);
                    EndCalculated = true;
                }

                return LastCalculatedFinishDate;
           
        }

        public override void DoNotify()
        {
            base.DoNotify();
          //  StartCalculated = false;
            EndCalculated = false;
           // Period.Notify();
          //  OnPropertyChanged(()=>Period);
        }

       // private bool _endCalculated = false;

        public bool EndCalculated
        {
            get { return Object.EndCalculated;  }
            set
            {
                if (value != null && !Equals(Object.EndCalculated, value))
                {
                    bool change = Object.EndCalculated;


                    Object.EndCalculated = value;
                    CreateServices().FreeUpdate(Object);

                    if (change)
                    {

                        Period.Notify();
                        if (SubSpecialityHolder != null)
                            SubSpecialityHolder.EndCalculated = value;
                    }
                }
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
                    CreateServices().FreeUpdate(Object);

                    if (change)
                    {

                        if (SubSpecialityHolder != null)
                            SubSpecialityHolder.StartCalculated = value;
                        EndCalculated = value;
                        Period.Notify();
                    }
                }
            }
        }

        public DateTime LastCalculatedFinishDate
        {
            get { return Object.LastCalculatedFinishDate; }
            set
            {
                if (value != null && !Equals(Object.LastCalculatedFinishDate, value))
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
                if (value != null && !Equals(Object.LastCalculatedStartDate, value))
                {
                    Object.LastCalculatedStartDate = value;
                    CreateServices().FreeUpdate(Object);
                    //SetProperty(v => Object.LastCalculatedStartDate = v, value);
                    //  OnPropertyChanged(() => Executor);
                }
            }
        }

        public override void SpreadChanges(IBudgetComponentItem toSpread)
        {
            if(toSpread.GetType().Implements<IPlannedResource>())
                foreach (IPlannedResourcePresenter<IPlannedActivity> plannedResourcePresenter in PlannedResources.Items)
                plannedResourcePresenter.SpreadChanges(toSpread);


            else
            {
                if (Code == toSpread.Code)
                {
                    if (toSpread.GetType().Implements<IPlannedActivity>())
                        base.SpreadChanges(toSpread);
                    if (toSpread.GetType().Implements<IUnderGroupActivity>())
                        CreateServices().AdquireUnderProperties(Object, toSpread as IUnderGroupActivity);

                    Items.Load();
                    OnPropertyChanged(() => Items);
                    OnPropertyChanged(() => Name);
                    OnPropertyChanged(() => Description);
                    OnPropertyChanged(() => Code);
                    OnPropertyChanged(() => MeasurementUnit);
                    OnPropertyChanged(() => UnitaryCost);
                    OnPropertyChanged(() => Currency);
                    OnPropertyChanged(() => SubExpenseConcept);
                    OnPropertyChanged(() => Category);
                    OnPropertyChanged(() => Quantity);
                }
            }
        }

        public override void Notify()
        {

            base.Notify();

            var executor = Object.Executor;
            Object.Executor = null;
            OnPropertyChanged(() => Executor);
            Object.Executor = executor;
            OnPropertyChanged(() => Executor);

        }

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
        public Thickness TimeLineThickness { get; set; }
        public Brush BackgroundColorBrush { get; set; }
    }
}
