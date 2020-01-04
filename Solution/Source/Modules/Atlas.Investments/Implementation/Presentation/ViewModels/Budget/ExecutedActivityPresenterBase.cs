using System;
using System.Linq;
using System.Windows.Input;
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
    /// Implementation of the base contract <see cref="IExecutedActivityPresenter{TComponent}"/> representing the presenter view model
    /// decorating and impersonating in the UI a executed resource of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the decorated executed resource.</typeparam>
    /// <typeparam name="TServices">
    /// The type of the application services used to make the updates made to the decorated executed resource.
    /// </typeparam>
    public  class ExecutedActivityPresenterBase :
        BudgetComponentItemPresenterBase<IExecutedActivity, IExecutedActivityItemManagerApplicationServices>,
        IExecutedActivityPresenter
        ////where TComponent : class, IBudgetComponent
        ////where TServices : class, IExecutedBudgetComponentItemManagerApplicationServices
    {

        /// <summary>
        /// Gets or sets the budget component to which belong the underlying item.
        /// </summary>
        //public IBudgetComponentPresenter<TComponent> Component { get; set; }

        public ISubSpecialityHolderPresenter<IExecutedSubSpecialityHolder> SubSpecialityHolder { get; set; }

        private IExecutionViewModel _executionLog;
        //private IPlannedActivityViewModel<TComponent> _plannedActivityViewModel;

    

        /// <summary>
        /// Initializes a new instance of <see cref="ExecutedActivityPresenterBase{TComponent, TServices}"/> given the
        /// executed resource.
        /// </summary>
        /// <param name="executedActivity">
        /// The <see cref="IBudgetComponentItem"/> to decorate by the initializing
        /// <see cref="BudgetComponentItemPresenterBase{TItem, TComponent, TServices}"/>.
        /// </param>
        ///// <exception cref="ArgumentNullException"><paramref name="executedActivity"/> is null.</exception>
        //public ExecutedActivityPresenterBase(IExecutedActivity executedActivity)
        //    : base(executedActivity)
        //{
        //}


        /// <summary>
        /// Gets the message that is displayed to the user when the current executed activity presenter view model has changed.
        /// </summary>
        protected override string SucessfullyUpdatedMessage
        {
            get { return Resources.SuccessfullyUpdatedExecutedActivity; }
        }

        /// <summary>
        /// Gets the crud view model used to manage the planned resources of the budget component contained in the current
        /// presenter.
        /// </summary>
        public IExecutionViewModel ExecutionLog
        {
            get
            {
                if (_executionLog == null)
                {
                    _executionLog = ServiceLocator.Current.GetInstance<IExecutionViewModel>();
                    //  Actioner(x => x.SuperiorCategory = Object);
                    _executionLog.ExecutedActivity = this;
                    _executionLog.Load();

                    _executionLog.Raised += OnInteractionRequested;

                }
                return _executionLog;
            }
        }

        public decimal ExecutedQuantity
        {
            get
            {
                return (ExecutionLog != null && ExecutionLog.Items.Count > 0) ? ExecutionLog.Sum(x => x.Amount) : base.Quantity;
            }

        }

        /// <summary>
        /// Gets or sets the quantity of the current <see cref="ExecutedBudgetComponentItemBase"/> if its is unplanned. If there is
        /// a planification set for it (Planification is not null) then its quantity is returned, and any new value will be rejected.
        /// </summary>
        public override decimal Quantity
        {
            get { return ExecutionLog.Items.Count>0  ? ExecutedQuantity : base.Quantity; }
            set
            {
                base.Quantity = ExecutionLog.Items.Count > 0 ? ExecutedQuantity : value;
                  
            }
        }
        public IPeriodPresenter Period
        {
            get; set;
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


        public override void Notify()
        {

            base.Notify();

            var executor = Object.Executor;
            Object.Executor = null;
            OnPropertyChanged(() => Executor);
            Object.Executor = executor;
            OnPropertyChanged(() => Executor);
           

            GetOrInitialize(ref _plannedResources, x => x.Component = this);
            OnPropertyChanged(() => HasItems);
            OnPropertyChanged(() => Quantity);
            OnPropertyChanged(() => UnitaryCost);
            OnPropertyChanged(() => Cost);
            NotifyDown();
            NotifyUp();

            //  Object.PlannedResources.ForEach(x=> x.Component = Object);

        }

     
        

        /// <summary>
        /// Notify changes to superior levels
        /// </summary>
        public override void NotifyUp()
        {
            OnPropertyChanged(() => Quantity);
            OnPropertyChanged(() => UnitaryCost);
            OnPropertyChanged(() => Cost);
            Parent.DoNotify();
        }

        public override void NotifyDown()
        {
            //OnPropertyChanged(() => Quantity);
            //OnPropertyChanged(() => UnitaryCost);
            //OnPropertyChanged(() => Cost);
            foreach (IPlannedResourcePresenter<IExecutedActivity> plannedResource in PlannedResources)
            {

                plannedResource.NotifyDown();

            }
        }

        protected override IExecutedActivityItemManagerApplicationServices CreateServices()
        {
            var service = base.CreateServices();
            //service.Component = Component.Object;
            service.SubSpecialityHolder = SubSpecialityHolder?.Object;
            return service;
        }

        public override ICommand DeleteMySelfCommand
        {
            get
            {

               
                if (!Equals(SubSpecialityHolder, null) && SubSpecialityHolder.GetType().Implements<IEquipmentExecutedSubSpecialityHolderPresenter>())
                {
                    return
                        (SubSpecialityHolder as IEquipmentExecutedSubSpecialityHolderPresenter).ExecutedActivities
                        .DeleteCommand;
                }

                if (!Equals(SubSpecialityHolder, null) && SubSpecialityHolder.GetType().Implements<IConstructionExecutedSubSpecialityHolderPresenter>())
                {
                    return
                        (SubSpecialityHolder as IConstructionExecutedSubSpecialityHolderPresenter).ExecutedActivities
                        .DeleteCommand;
                }

                if (!Equals(SubSpecialityHolder, null) && SubSpecialityHolder.GetType().Implements<IOtherExpensesExecutedSubSpecialityHolderPresenter>())
                {
                    return
                        (SubSpecialityHolder as IOtherExpensesExecutedSubSpecialityHolderPresenter).ExecutedActivities
                        .DeleteCommand;
                }
                if (!Equals(SubSpecialityHolder, null) && SubSpecialityHolder.GetType().Implements<IWorkCapitalExecutedSubSpecialityHolderPresenter>())
                {
                    return
                        (SubSpecialityHolder as IWorkCapitalExecutedSubSpecialityHolderPresenter).ExecutedActivities
                        .DeleteCommand;
                }

                return null;

            }
        }

        public override INavigable Parent
        {
            get { return SubSpecialityHolder; }
        }
        public override string NewText { get { return Resources.NewExecutedResourceName; } }

        /// <summary>
        /// returns the kind of the current budget item
        /// </summary>
        public override String Kind
        {
            get { return "ExecutedActivity"; }
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

    }
}
