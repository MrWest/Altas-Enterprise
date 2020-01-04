using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Implementation of the base contract <see cref="IPlannedActivityViewModel{TComponent, TPresenter}"/> representing the crud view
    /// model used to manage the planned activities of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the planned activities managed here.</typeparam>
    /// <typeparam name="TPresenter">The type of the presenter view models decorating the planned activities.</typeparam>
    /// <typeparam name="TServices">
    /// The type of the application services used the send to the data operations generated in the current crud view model.
    /// </typeparam>
    public  class PlannedActivityViewModelBase :
        BudgetComponentItemViewModelBase<IPlannedActivity, IPlannedActivityPresenter, IPlannedActivityManagerApplicationServices>,
        IPlannedActivityViewModel
        //where TComponent : class, IBudgetComponent
        //where TPresenter : class,  IPlannedActivityPresenter<TComponent>
        //where TServices : class, IPlannedActivityManagerApplicationServices<TComponent>
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

        public PlannedActivityViewModelBase()
        {
            AddPlannedItemsCommand = new DelegateCommand<IList>(AddPlannedItemsCommand_Executed, AddPlannedItemsCommand_CanExecute);
        }
        /// <summary>
        ///     Gets the command that allows to executed specified planned items.
        /// </summary>
        public ICommand AddPlannedItemsCommand { get; private set; }


        private bool AddPlannedItemsCommand_CanExecute(IList list)
        {


            IList<IPlannedActivity> budgetComponentItems;
            var rslt = TryGetPlannedItems(list, out budgetComponentItems);

            return rslt && ExecuteUsingServices(services => services.CanExecute(budgetComponentItems));
        }



        private void AddPlannedItemsCommand_Executed(IList list)
        {

            IList<IPlannedActivity> budgetComponentItems;
            if (!TryGetPlannedItems(list, out budgetComponentItems))
                return;

            // Command to execute them
            IEnumerable<IExecutedActivity> executedItems = ExecuteUsingServices(services => services.Execute(budgetComponentItems)).ToArray();

            // Then, create for each of the executed item a presenter and add them to the current crud view model
            //foreach (var executedItem in executedItems)
            //{
            //    TPresenter presenter = CreatePresenterFor(executedItem);
            //    presenter.PropertyChanged += OnPresenterPropertyChanged;
            //    Items.Add(presenter);
            //}

            // And notify the user
            Notify(Resources.ExecutedThisManyItems.EasyFormat(executedItems.Count()), Resources.ExecutionSummary);
        }


        private static bool TryGetPlannedItems(IList arg, out IList<IPlannedActivity> plannedItems)
        {
            IList<IPlannedActivity> plannedItemsArray =
                (from presenter in (arg ?? new object[0]).OfType<IPresenter>()
                 select presenter.Object)
                    .OfType<IPlannedActivity>()
                    .ToArray();

            plannedItems = new List<IPlannedActivity>();

            foreach (var plannedBudgetComponentItem in plannedItemsArray)
            {
                plannedItems.Add(plannedBudgetComponentItem);
            }

            return plannedItems.Any() && arg != null && plannedItems.Count() == arg.Count;
        }


        protected override IPlannedActivityPresenter CreatePresenterFor(IPlannedActivity plannedActivity)
        {
            //var shit = investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlow;
            IPlannedActivityPresenter presenter = base.CreatePresenterFor(plannedActivity);

            //var g = investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlow;

            //presenter.Component = Component;
           // presenter.Object.Component = Component.Object;
            presenter.SubSpecialityHolder = SubSpecialityHolder;
            presenter.Object.SubSpecialityHolder = SubSpecialityHolder.Object;
           

            return presenter;
        }

        /// <summary>
        /// Loads the items from the data source.
        /// </summary>
        public override void Load()
        {
            foreach (var presenter in Items)
                presenter.PropertyChanged -= OnPresenterPropertyChanged;

            Items.Clear();

            ExecuteUsingServices(services =>
            {
                foreach (IPlannedActivity item in GetItems(services))
                {
                    var presenter = CreatePresenterFor(item);

                    presenter.PropertyChanged += OnPresenterPropertyChanged;

                    Items.Add(presenter);
                }
            });
        }

        /// <summary>
        /// Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
        /// with the operation.
        /// </summary>
        /// <param name="plannedActivity">The planned activity that is about to be deleted.</param>
        /// <returns>
        /// A <see cref="string"/> representing the message confirming with the user whether he/she wants to continue with the deletion
        /// of planned activity or not.
        /// </returns>
        protected override string GetDeleteConfirmationMessage(IPlannedActivityPresenter plannedActivity)
        {
            return Resources.SureToDeletePlannedActivity.EasyFormat(plannedActivity);
        }

        /// <summary>
        /// Gets the confirmation message that is show when there is added a new planned activity.
        /// </summary>
        /// <param name="plannedActivity">
        /// The planned activity containing the added planned activity.</param>
        /// <returns>
        /// A <see cref="string"/> representing the message notifying that a new planned activity has been added.
        /// </returns>
        protected override string GetSuccessfullyAddedElementMessage(IPlannedActivityPresenter plannedActivity)
        {
            return Resources.SuccessfullyAddedPlannedActivity.EasyFormat(plannedActivity);
        }

        /// <summary>
        /// Gets the confirmation message that is show when there is deleted a new planned activity.
        /// </summary>
        /// <param name="plannedActivity">
        /// The planned activity containing the deleted planned activity.</param>
        /// <returns>
        /// A <see cref="string"/> representing the message notifying that a new planned activity has been deleted.
        /// </returns>
        protected override string GetSuccessfullyDeletedElementMessage(IPlannedActivityPresenter plannedActivity)
        {
            return Resources.SuccessfullyDeletedPlannedActivity.EasyFormat(plannedActivity);
        }

        protected override IPlannedActivityManagerApplicationServices CreateServices()
        {
            IPlannedActivityManagerApplicationServices service = base.CreateServices();
            //service.Component = Component?.Object;
            service.SubSpecialityHolder = SubSpecialityHolder?.Object;
            return service;
        }

        public ISubSpecialityHolderPresenter<IPlannedSubSpecialityHolder> SubSpecialityHolder { get; set; }

        public override bool CanAdd(IPlannedActivityPresenter presenter)
        {
            //if (!Equals(SubSpecialityHolder, null) && !Equals(SubSpecialityHolder.SubSpeciality, null))
            //    return true;

                return Equals(SubSpecialityHolder, null) || ( !Equals(SubSpecialityHolder.SubSpeciality, null) || !Equals(SubSpecialityHolder.SubExpenseConcept, null) || !Equals(SubSpecialityHolder.Category, null));
        }

        protected override INavigable Parent { get { return SubSpecialityHolder; } }
    }

    /// <summary>
    /// Implementation of the base contract <see cref="IPlannedActivityViewModel{TComponent, TPresenter}"/> representing the crud view
    /// model used to manage the planned activities of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the planned activities managed here.</typeparam>
    /// <typeparam name="TPresenter">The type of the presenter view models decorating the planned activities.</typeparam>
    /// <typeparam name="TServices">
    /// The type of the application services used the send to the data operations generated in the current crud view model.
    /// </typeparam>
    //public class PlannedActivityViewModelBase2<TComponent, TPresenter, TServices> :
    //    BudgetComponentItemViewModelBase<IPlannedActivity, TPresenter, TComponent, TServices>,
    //    IPlannedActivityViewModel<TComponent, TPresenter>
    //    where TComponent : class, ISection
    //    where TPresenter : class, IBudgetComponentItemPresenter<IPlannedActivity, TComponent>
    //    where TServices : class, IActivityManagerApplicationServices<IPlannedActivity, TComponent>
    //{
    //    private IBudgetComponentPresenter<TComponent> _component;
    //    public IBudgetComponentPresenter<TComponent> Component
    //    {
    //        get
    //        {
    //            if (_component == null)
    //                throw new InvalidOperationException(Resources.InitializeComponentReferenceBeforeUsingIt);

    //            return _component;
    //        }
    //        set
    //        {
    //            if (value == null)
    //                throw new ArgumentNullException("value");

    //            _component = value;
    //        }
    //    }
    //    protected override TPresenter CreatePresenterFor(IPlannedActivity plannedActivity)
    //    {
    //        //var shit = investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlow;
    //        IPlannedActivityPresenter<TComponent> presenter = base.CreatePresenterFor(plannedActivity) as IPlannedActivityPresenter<TComponent>;

    //        //var g = investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlow;

    //        var period = ServiceLocator.Current.GetInstance<IPeriodPresenter>();
    //        period.Object = plannedActivity.Period;
    //        period.Holder = presenter;
    //        presenter.Period = period;

    //        return presenter as TPresenter;
    //    }

    //    /// <summary>
    //    /// Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
    //    /// with the operation.
    //    /// </summary>
    //    /// <param name="plannedActivity">The planned activity that is about to be deleted.</param>
    //    /// <returns>
    //    /// A <see cref="string"/> representing the message confirming with the user whether he/she wants to continue with the deletion
    //    /// of planned activity or not.
    //    /// </returns>
    //    protected override string GetDeleteConfirmationMessage(TPresenter plannedActivity)
    //    {
    //        return Resources.SureToDeletePlannedActivity.EasyFormat(plannedActivity);
    //    }

    //    /// <summary>
    //    /// Gets the confirmation message that is show when there is added a new planned activity.
    //    /// </summary>
    //    /// <param name="plannedActivity">
    //    /// The planned activity containing the added planned activity.</param>
    //    /// <returns>
    //    /// A <see cref="string"/> representing the message notifying that a new planned activity has been added.
    //    /// </returns>
    //    protected override string GetSuccessfullyAddedElementMessage(TPresenter plannedActivity)
    //    {
    //        return Resources.SuccessfullyAddedPlannedActivity.EasyFormat(plannedActivity);
    //    }

    //    /// <summary>
    //    /// Gets the confirmation message that is show when there is deleted a new planned activity.
    //    /// </summary>
    //    /// <param name="plannedActivity">
    //    /// The planned activity containing the deleted planned activity.</param>
    //    /// <returns>
    //    /// A <see cref="string"/> representing the message notifying that a new planned activity has been deleted.
    //    /// </returns>
    //    protected override string GetSuccessfullyDeletedElementMessage(TPresenter plannedActivity)
    //    {
    //        return Resources.SuccessfullyDeletedPlannedActivity.EasyFormat(plannedActivity);
    //    }

    //    public override bool CanAdd(TPresenter presenter)
    //    {
    //        return true;
    //    }

    //    public override bool CanDelete(TPresenter presenter)
    //    {
    //        return true;
    //    }

    //    protected override TServices CreateServices()
    //    {
    //        TServices service = base.CreateServices();
    //        service.Component = Component.Object;
    //        return service;
    //    }
    //}

    ///// <summary>
    //    /// Implementation of the base contract <see cref="IPlannedActivityViewModel{TComponent, TPresenter}"/> representing the crud view
    //    /// model used to manage the planned activities of a certain budget component.
    //    /// </summary>
    //    /// <typeparam name="TComponent">The type of the budget component to which belong the planned activities managed here.</typeparam>
    //    /// <typeparam name="TPresenter">The type of the presenter view models decorating the planned activities.</typeparam>
    //    /// <typeparam name="TServices">
    //    /// The type of the application services used the send to the data operations generated in the current crud view model.
    //    /// </typeparam>
    //    public class PlannedActivityViewModelBase3<TComponent, TPresenter, TServices> :
    //        BudgetComponentItemViewModelBase<IPlannedActivity, TPresenter, TComponent, TServices>,
    //        IPlannedActivityViewModel<TComponent, TPresenter>
    //        where TComponent : class, IVariantLinesHolder
    //        where TPresenter : class, IBudgetComponentItemPresenter<IPlannedActivity, TComponent>
    //    where TServices : class, IActivityFullManagerApplicationServices<IPlannedActivity, TComponent>
    //    {
    //        private IBudgetComponentPresenter<TComponent> _component;
    //        public IBudgetComponentPresenter<TComponent> Component
    //        {
    //            get
    //            {
    //                if (_component == null)
    //                    throw new InvalidOperationException(Resources.InitializeComponentReferenceBeforeUsingIt);

    //                return _component;
    //            }
    //            set
    //            {
    //                if (value == null)
    //                    throw new ArgumentNullException("value");

    //                _component = value;
    //            }
    //        }
    //        /// <summary>
    //        /// Gets the message that is prompted to the user previous to a deletion to confirm whether or not to continue
    //        /// with the operation.
    //        /// </summary>
    //        /// <param name="plannedActivity">The planned activity that is about to be deleted.</param>
    //        /// <returns>
    //        /// A <see cref="string"/> representing the message confirming with the user whether he/she wants to continue with the deletion
    //        /// of planned activity or not.
    //        /// </returns>
    //        protected override string GetDeleteConfirmationMessage(TPresenter plannedActivity)
    //        {
    //            return Resources.SureToDeletePlannedActivity.EasyFormat(plannedActivity);
    //        }

    //        /// <summary>
    //        /// Gets the confirmation message that is show when there is added a new planned activity.
    //        /// </summary>
    //        /// <param name="plannedActivity">
    //        /// The planned activity containing the added planned activity.</param>
    //        /// <returns>
    //        /// A <see cref="string"/> representing the message notifying that a new planned activity has been added.
    //        /// </returns>
    //        protected override string GetSuccessfullyAddedElementMessage(TPresenter plannedActivity)
    //        {
    //            return Resources.SuccessfullyAddedPlannedActivity.EasyFormat(plannedActivity);
    //        }

    //        /// <summary>
    //        /// Gets the confirmation message that is show when there is deleted a new planned activity.
    //        /// </summary>
    //        /// <param name="plannedActivity">
    //        /// The planned activity containing the deleted planned activity.</param>
    //        /// <returns>
    //        /// A <see cref="string"/> representing the message notifying that a new planned activity has been deleted.
    //        /// </returns>
    //        protected override string GetSuccessfullyDeletedElementMessage(TPresenter plannedActivity)
    //        {
    //            return Resources.SuccessfullyDeletedPlannedActivity.EasyFormat(plannedActivity);
    //        }

    //        public override bool CanAdd(TPresenter presenter)
    //        {
    //            return true;
    //        }

    //        public override bool CanDelete(TPresenter presenter)
    //        {
    //            return true;
    //        }

    //        protected override TServices CreateServices()
    //        {
    //            TServices service = base.CreateServices();
    //            service.Component = Component.Object;
    //            return service;
    //        }
    //    }
    
}
