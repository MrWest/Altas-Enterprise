using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Logging;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    /// <summary>
    ///     Implementation of the base contract <see cref="IBudgetComponentItemViewModel{TItem,TPresenter,TComponent}" />
    ///     representing the crud view model managing the items of a certain budget component.
    /// </summary>
    /// <typeparam name="TItem">The type of the budget component items.</typeparam>
    /// <typeparam name="TPresenter">The type of presenter view model decorating the budget component items.</typeparam>
    /// <typeparam name="TComponent">The type of the budget component of the items managed in the current crud view model.</typeparam>
    /// <typeparam name="TServices">The application services used to receive the data operation requests originated here.</typeparam>
    public abstract class BudgetComponentItemViewModelBase<TItem, TPresenter, TServices> :
        NavigableViewModel<TItem, TPresenter, TServices>, IBudgetComponentItemViewModel<TItem,TPresenter>
        where TItem : class, IBudgetComponentItem
        where TPresenter : class, IBudgetComponentItemPresenter<TItem>
        //where TComponent : class, IEntity
        where TServices : class, IBudgetComponentItemManagerApplicationServices<TItem>
    {
        //private TComponent _component;
        private bool _itemChanged;
        private string _nameSpecification;

        private IEntity _budgetComponentItemParent;

        /// <summary>
        ///     Initializes a new instance of
        ///     <see cref="BudgetComponentItemViewModelBase{TItem,TPresenter,TComponent,TServices}" />.
        /// </summary>
        protected BudgetComponentItemViewModelBase()
        {
          //  FilterCommand = new DelegateCommand<string>(FilterCommand_Executed, FilterCommand_CanExecute);
        }

        //public IEntity BudgetComponentItemParent
        //{
        //    get
        //    {
        //        if (_budgetComponentItemParent == null)
        //            throw new InvalidOperationException(Resources.InitializeComponentReferenceBeforeUsingIt);



        //        return _budgetComponentItemParent;
        //    }
        //    set
        //    {
        //        //if (value == null)
        //        //    throw new ArgumentNullException("value");

        //        _budgetComponentItemParent = value;
        //    }

        //}
        ///// <summary>
        /////     Gets the budget component containing the items managed in the current
        /////     <see cref="BudgetComponentItemViewModelBase{TItem, TPresenter, TComponent, TServices}" />.
        ///// </summary>
        //public TComponent Component
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

        /// <summary>
        ///     Gets the command that allows to filter the set of budget component items managed in the current crud view model.
        /// </summary>
     //   public ICommand FilterCommand { get; private set; }

  

       

        /// <summary>
        ///     Gets the application services used to send the data operations originated in the current
        ///     <see cref="BudgetComponentItemViewModelBase{TItem, TPresenter, TComponent, TServices}" />.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="TServices" />.</returns>
        protected override TServices CreateServices()
        {
            TServices services = base.CreateServices();
          //  services.Component =  Component;

            return services;
        }

        /// <summary>
        ///     Creates a presenter view model for the given budget component item.
        /// </summary>
        /// <param name="budgetComponentItem">The budget component item to get decorated in a new presenter view model.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem" /> is null.</exception>
        /// <returns>A new instance of <typeparamref name="TPresenter" /> containing <paramref name="budgetComponentItem" />.</returns>
        protected override TPresenter CreatePresenterFor(TItem budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");

            TPresenter presenter = base.CreatePresenterFor(budgetComponentItem);
            //presenter.Component = Component;
            //presenter.Object.Component = Component;

            var period = ServiceLocator.Current.GetInstance<IPeriodPresenter>();
            period.Object = budgetComponentItem.Period;
            period.Holder = presenter;
            presenter.Period = period;


            return presenter;
        }

        /// <summary>
        /// When overridden in a deriver it allows it handle the AddedItem event.
        /// </summary>
        /// <param name="sender">The object raising the event.</param>
        /// <param name="e">The information generated in the event with details about the addition.</param>
        //protected override void OnAddedItem(object sender, EventArgs e)
        //{
        //    base.OnAddedItem(sender,e);
        //   // UpdateItemsFather();
        //    ItemEventArgs<TPresenter> arguments;
        //    if (!CheckIsItemEventArgs(e, out arguments))
        //        return;

           
        //}

        /// <summary>
        /// When overridden in a deriver it allows it handle the DeletedItem event.
        /// </summary>
        /// <param name="sender">The object raising the event.</param>
        /// <param name="e">The information generated in the event with details about the deletion.</param>
        //protected override void OnDeletedItem(object sender, EventArgs e)
        //{
        //    base.OnDeletedItem(sender, e);
        //    ItemEventArgs<TPresenter> arguments;
        //    if (!CheckIsItemEventArgs(e, out arguments))
        //        return;

       
          
        //}

        //public void UpdateItemsFather()
        //{
        //    foreach (TPresenter presenter in Items)
        //    {
        //        presenter.BudgetComponentItemParent = BudgetComponentItemParent;
        //    }
        //}
        //private static void NotifyParent(object sender)
        //{
        //    try
        //    {
                

        //        var budgetComponentItemParent =
        //            sender.GetType().GetProperty("BudgetComponentItemParent").GetValue(sender);
               
        //        budgetComponentItemParent.GetType().GetMethod("Notify").Invoke(budgetComponentItemParent, null); 
        //    }
        //    catch (Exception exception)
        //    {
        //        Logger.Instance.Log(exception.Message.ToString(), Category.Exception, Priority.Low);
        //    }
        //}

        /// <summary>
        ///     Gets the budget component items requesting them to the given service.
        /// </summary>
        /// <param name="service">The instance of the service to used in the retrieving of the items.</param>
        /// <returns>
        ///     An <see cref="IEnumerable{T}" /> allowing iteration over items gotten from the service. The enumerable contains
        ///     all items if there is no name specification to narrow the search; otherwise returns all the items which names
        ///     match the defined name specification.
        /// </returns>
        protected override IEnumerable<TItem> GetItems(TServices service)
        {
            return string.IsNullOrEmpty(_nameSpecification) ? base.GetItems(service) : service.Filter(_nameSpecification);
        }

     
        /// <summary>
        ///     Invoked when an item in the current crud view model has changed any of its properties. Also this method marks the
        ///     current view
        ///     model in order to make it trigger a re-filtering as soon as possible, because if a filter is on maybe the changed
        ///     item may no
        ///     longer be part of the filtering results because it does not match anymore the criteria.
        /// </summary>
        /// <param name="sender">The budget component item raising the event invoking the current method.</param>
        /// <param name="e">The details of the budget component item change.</param>
        protected override void OnPresenterPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPresenterPropertyChanged(sender, e);

            _itemChanged = true;
        }


        //private bool FilterCommand_CanExecute(string nameSpecification)
        //{
        //    bool canFilter = ExecuteUsingServices(services => services.CanFilter(nameSpecification));

        //    //if (canFilter && _itemChanged)
        //    //{
        //    //    FilterCommand_Executed(nameSpecification);
        //    //    _itemChanged = false;
        //    //}

        //    return canFilter;
        //}

        
        //private void FilterCommand_Executed(string nameSpecification)
        //{
        //    //TODO this isnt working
        //    _nameSpecification = nameSpecification;
        //    Load();
        //}

     
      


       


    }
}