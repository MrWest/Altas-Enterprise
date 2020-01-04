using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    ///     Implementation of the base contract of the <see cref="IBudgetComponentItemDomainServices{TItem,TComponent}" />,
    ///     representing the base class of the domain services used to ensure the business rules for the items of a budget
    ///     component.
    /// </summary>
    /// <typeparam name="T">The type of the budget component items which domain rules and enforced here.</typeparam>
    /// <typeparam name="TComponent">The budget component to which belong the items.</typeparam>
    public abstract  class BudgetComponentItemDomainServicesBase<T> : CodedNomenclatorDomainServicesBase<T>,
        IBudgetComponentItemDomainServices<T>
        where T : class, IBudgetComponentItem
        //where TComponent : class, IEntity
    {


        //private TComponent _component;


        ///// <summary>
        /////     Gets or sets the <see cref="IEquipmentComponent" /> to which belong the <see cref="IPlannedResource" /> which
        /////     business rules are managed here.
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
        ///     Creates a new budget component item with default data.
        /// </summary>
        /// <returns>A new <typeparamref cref="T" />.</returns>
        public override T Create()
        {
            T item = base.Create();
            SetDataToNew(item);
           // item.Component = Component;
            return item;
        }

        /// <summary>
        ///     When overridden in a deriver it sets the data for the given item.
        /// </summary>
        /// <param name="item">The just created item that is in need of its creation data.</param>
        protected virtual void SetDataToNew(T item)
        {
            if (item == null)
                throw new ArgumentNullException("budgetcomponentitem");
        }

        /// <summary>
        ///     Determines whether there can be executed any of the given planned items.
        /// </summary>
        /// <typeparam name="TPlanned">The type of the planned items to determine whether there can executed or not.</typeparam>
        /// <param name="plannedItems">
        ///     A <see cref="IEnumerable{T}" /> of planned items to determine whether there can be executed at least one of them.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="plannedItems"/> is null.</exception>
        /// <returns>True if there is at least one planned item unexecuted; false otherwise.</returns>
        public virtual bool CanExecute(IEnumerable<T> plannedItems)
        {
            if (plannedItems == null)
                throw new ArgumentNullException("plannedItems");

            return true; // plannedItems.Any(x => x.Execution == null);
        }

        /// <summary>
        ///     Executes all the planned items still not executed.
        /// </summary>
        /// <typeparam name="TPlanned">The type of the planned items to execute.</typeparam>
        /// <param name="plannedItems">
        ///     The <see cref="IEnumerable{T}" /> of planned items to executed the unexecuted ones among them.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="plannedItems"/> is null.</exception>
        /// <returns>An <see cref="int" /> representing how many planned items have been actually executed.</returns>
        public IEnumerable<T> Execute(IEnumerable<T> plannedItems)
        {
            if (plannedItems == null)
                throw new ArgumentNullException("plannedItems");

            // Get the unexecuted planned items
            //  var unexecutedItems = from plannedItem in plannedItems where plannedItem.Execution == null select plannedItem;
         //   var items = (from plannedItem in plannedItems select plannedItem);
            var rstl = new List<T>();
            foreach (T planned in plannedItems)
            {
                T addedItem = Create();
                //addedItem.Code = planned.Code;
                addedItem.Name = planned.Name;
                addedItem.Description = planned.Description;
                addedItem.MeasurementUnit = planned.MeasurementUnit;
                addedItem.Currency = planned.Currency;
                addedItem.Category = planned.Category;
                addedItem.SubExpenseConcept = planned.SubExpenseConcept;
                addedItem.UnitaryCost = planned.UnitaryCost;

                //fill resources
                ////CatchResourses(addedItem, planned);

                //addedItem = SetComponent(addedItem, Component);


                rstl.Add(addedItem);
            }
            return rstl;
            //// And create the execution for them, by relating them with their corresponding new execution item
            //return unexecutedItems.Aggregate(new List<TItem>(), (list, plannedItem) =>
            //{
            //    TItem itemExecution = Create();
            //    itemExecution.Planification = plannedItem;
            //    plannedItem.Execution = itemExecution;

            //    list.Add(itemExecution);

            //    return list;
            //});
        }

        //protected virtual T SetComponent(T addedItem , TComponent component)
        //{
        //    return addedItem;
        //}
       
        /// <summary>
        /// Fill the new Activity with the resourses of the sample one
        /// </summary>
        //protected void CatchResourses(IBudgetComponentItem newPlannedItem, IBudgetComponentItem existentActivity)
        //{

        //    if (newPlannedItem == null)
        //        throw new ArgumentNullException("plannedResources");
        //    if (existentActivity == null)
        //        throw new ArgumentNullException("existentActivity");

        //    var resourceSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceDomainServices<IPlannedResource, IBudgetComponentItem>>();
           
        //     var weightService = ServiceLocator.Current.GetInstance<IWeightDomainService>();
        //     var volumeService = ServiceLocator.Current.GetInstance<IVolumeDomainService>();
        //   // resourceService.Component =  existentActivity ;
        //    foreach (IPlannedResource resource in existentActivity.PlannedResources)
        //    {

        //        var newResource = resourceSevice.Create();

        //        newResource.Code = resource.Code;
        //        newResource.Name = resource.Name;
        //        newResource.Description = resource.Description;
        //        newResource.MeasurementUnit = resource.MeasurementUnit;
        //        newResource.Currency = resource.Currency;
        //        newResource.Category = resource.Category;
        //        newResource.SubExpenseConcept = resource.SubExpenseConcept;
        //        newResource.UnitaryCost = resource.UnitaryCost;
        //        newResource.WageScale = resource.WageScale;
        //        newResource.WasteCoefficient = resource.WasteCoefficient;
        //        newResource.ResourceKind = resource.ResourceKind;
        //        newResource.MenNumber = resource.MenNumber;
        //        //weight

        //        newResource.Weight = weightService.Create();
        //        newResource.Weight.MeasurementUnit = resource.Weight.MeasurementUnit;
        //        newResource.Weight.Amount = resource.Weight.Amount;
        //        newResource.Weight.Holder = newResource;
        //        newResource.Weight.Id = null;

        //        //volume
        //        newResource.Volume = volumeService.Create();
        //        newResource.Volume.MeasurementUnit = resource.Volume.MeasurementUnit;
        //        newResource.Volume.Amount = resource.Volume.Amount;
        //        newResource.Volume.Holder = newResource;
        //        newResource.Volume.Id = null;

        //        newResource.Component = newPlannedItem;
        //        // recursive fill
        //        CatchResourses(newResource,  resource);
        //        newPlannedItem.PlannedResources.Add(newResource);
        //    }
        //}
    }
}