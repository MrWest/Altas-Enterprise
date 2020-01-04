using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;


namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget
{
    /// <summary>
    /// Implementation of the base contract <see cref="BudgetComponentItemRepositoryBase{T, TComponent}"/> representing the base
    /// class of the repositories managing the data operations for budget component items.
    /// </summary>
    /// <typeparam name="T">The type of the budget component items managed here.</typeparam>
    /// <typeparam name="TComponent">The type of the budget component to which the items belong.</typeparam>
    public abstract  class BudgetComponentItemRepositoryBase<T> : Db4ORepositoryBase<T>,
       IRelatedRepository<T, IPeriod>, IBudgetComponentItemRepository<T>
        where T : class, IBudgetComponentItem
        //where TComponent : class, IEntity
        
    {

        //private TComponent _component;

        /// <summary>
        /// Initializes a new instance of <see cref="BudgetComponentItemRepositoryBase{T, TComponent}"/> given the database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext"/> used to carry on with the raw data operations.</param>
        /// <exception cref="ArgumentNullException"><paramref name="databaseContext"/> is null.</exception>
        public BudgetComponentItemRepositoryBase(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }


        private IPeriod Clone(IPeriod period)
        {
            var periodClone = ServiceLocator.Current.GetInstance<IPeriod>();

            periodClone.Starts = period.Starts;
            periodClone.Ends = period.Ends;
            periodClone.PeriodKind = period.PeriodKind;

            periodClone.Id = period.Id ?? DatabaseContext.GenerateKey();

            return periodClone;
        }
        /// Relates the given investment element with a period, following the domain specification that every investment element must have a
        /// time period.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to relate to <paramref name="period"/>.</param>
        /// <param name="period">The <see cref="IPeriod"/> to relate to <paramref name="investmentElement"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="investmentElement"/> or <paramref name="period"/> is null.
        /// </exception>
        public void Relate(T plannedActivity, IPeriod period)
        {
            if (plannedActivity == null)
                throw new ArgumentNullException("plannedActivity");
            if (period == null)
                throw new ArgumentNullException("period");

            plannedActivity.Period = period;
            period.Holder = plannedActivity;
        }

        /// <summary>
        ///     Disrelates the given investment element with a budget.
        /// </summary>
        /// <param name="investmentElement">
        ///     The <see cref="IInvestmentElement" /> to break its relation with
        ///     <paramref name="period" />.
        /// </param>
        /// <param name="period">The <see cref="IPeriod" /> to break its relation with <paramref name="investmentElement" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="investmentElement" /> or <paramref name="period" /> is null.
        /// </exception>
        public void Unrelate(T plannedActivity, IPeriod period)
        {
            if (plannedActivity == null)
                throw new ArgumentNullException("plannedActivity");
            if (period == null)
                throw new ArgumentNullException("budget");

            //plannedActivity.Period = null;
            //period.Holder = null;
            var periodRepository = ServiceLocator.Current.GetInstance<IPeriodRepository>();
            periodRepository.Holder = plannedActivity;

            periodRepository.DeleteAll();

        }

        public void SaveReference(IPeriod other)
        {
           // DatabaseContext.Store(other);
        }


        /// <summary>
        /// Gets the properties which changes must be saved.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Code), GetName(x => x.Name), GetName(x => x.Quantity), GetName(x => x.Description),
                    GetName(x => x.UnitaryCost),  GetName(x => x.isUnitaryPriceCalculated),  GetName(x => x.CalculatedUnitaryPrice),GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.SubExpenseConcept),GetName(x=>x.PriceSystem)
                }).ToArray();
                //,GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.ExpenseConcept)
            }
        }

        ///// <summary>
        ///// When overridden in a deriver it returns the list of items of the budget component that the items managed here will added
        ///// or removed from.
        ///// </summary>
        //protected abstract Func<TComponent, IList<T>> GetItemCollection { get; }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override T Add(T budgetComponentItem)
        {
            T addedBudgetComponentITem = base.Add(budgetComponentItem);

            
           // this.Relate(budgetComponentItem, Component, DatabaseContext);
            
            return addedBudgetComponentITem;
        }

        
        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(T budgetComponentItem)
        {
            base.Update(budgetComponentItem);

          // this.Relate(budgetComponentItem, Component, DatabaseContext);
          
        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Delete(T budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");


            //var dbbudgetComponentItemt = DatabaseContext.Find<IBudgetComponentItem>(budgetComponentItem.Id);
           // this.Unrelate(budgetComponentItem, Component, DatabaseContext);

            var budgetComponentItemRepository = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository< T >>();
            budgetComponentItemRepository.Component = budgetComponentItem;
            budgetComponentItemRepository.DeleteAll();

            //foreach (IPlannedResource resourceComponentItem in dbbudgetComponentItemt.PlannedResources.ToArray())
            //    Delete(resourceComponentItem);

            base.Delete(budgetComponentItem);
        }
        ///// <summary>
        ///// Deletes the given buget component item from the current repository.
        ///// </summary>
        ///// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to delete.</param>
        ///// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        //public void Delete(IEntity budgetComponentItem)
        //{
        //    if (budgetComponentItem == null)
        //        throw new ArgumentNullException("budgetComponentItem");

        //    base.Delete( budgetComponentItem);
        //}
        /// <summary>
        /// Relates a item with its budget component.
        /// </summary>
        /// <param name="budgetComponentItem">The item to relate to <paramref name="budgetComponent"/>.</param>
        /// <param name="budgetComponent">The budget component to relate to <paramref name="budgetComponentItem"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="budgetComponentItem"/> or <paramref name="budgetComponent"/> is null.
        /// </exception>
        //public virtual void Relate(T budgetComponentItem, TComponent budgetComponent)
        //{
        //    if (budgetComponentItem == null)
        //        throw new ArgumentNullException("budgetComponentItem");
        //    if (budgetComponent == null)
        //        throw new ArgumentNullException("budgetComponent");

        //    //budgetComponentItem.Component = budgetComponent;
        //    IList<T> itemCollection = GetItemCollection(budgetComponent);
        //    if (itemCollection.All(x => !Equals(x.Id, budgetComponentItem.Id)))
        //        itemCollection.Add(budgetComponentItem);
        //}

        /// <summary>
        /// Relates a item with its budget component.
        /// </summary>
        /// <param name="budgetComponentItem">The item to relate to <paramref name="budgetComponent"/>.</param>
        /// <param name="budgetComponent">The budget component to relate to <paramref name="budgetComponentItem"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="budgetComponentItem"/> or <paramref name="budgetComponent"/> is null.
        /// </exception>
        //public virtual void Relate(T budgetComponentItem, IMeasurementUnit budgetComponent)
        //{
        //    if (budgetComponentItem == null)
        //        throw new ArgumentNullException("budgetComponentItem");
        //    if (budgetComponent == null)
        //        throw new ArgumentNullException("budgetComponent");

        //    budgetComponentItem.MeasurementUnit = budgetComponent;
        //    budgetComponent.OwnerEntity = budgetComponentItem;
        //}

        //public virtual void Relate(T budgetComponentItem, ICurrency budgetComponent)
        //{
        //    if (budgetComponentItem == null)
        //        throw new ArgumentNullException("budgetComponentItem");
        //    if (budgetComponent == null)
        //        throw new ArgumentNullException("budgetComponent");

        //    budgetComponentItem.Currency = budgetComponent;
        //    budgetComponent.OwnerEntity = budgetComponentItem;
        //}
        //public virtual void Relate(T budgetComponentItem, ICategory budgetComponent)
        //{
        //    if (budgetComponentItem == null)
        //        throw new ArgumentNullException("budgetComponentItem");
        //    if (budgetComponent == null)
        //        throw new ArgumentNullException("budgetComponent");

        //    budgetComponentItem.Category = budgetComponent;
        //    budgetComponent.OwnerEntity = budgetComponentItem;
        //}
        //public virtual void Relate(T budgetComponentItem, ISubExpenseConcept budgetComponent)
        //{
        //    if (budgetComponentItem == null)
        //        throw new ArgumentNullException("budgetComponentItem");
        //    if (budgetComponent == null)
        //        throw new ArgumentNullException("budgetComponent");

        //    budgetComponentItem.SubExpenseConcept = budgetComponent;
        //    budgetComponent.OwnerEntity = budgetComponentItem;
        //}
        /// <summary>
        /// Breaks the relation of the given item with its budget component.
        /// </summary>
        /// <param name="budgetComponentItem">The item to break its relation to <paramref name="budgetComponent"/>.</param>
        /// <param name="budgetComponent">The budget component to break its relation to <paramref name="budgetComponentItem"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="budgetComponentItem"/> or <paramref name="budgetComponent"/> is null.
        /// </exception>
        //public virtual void Unrelate(T budgetComponentItem, TComponent budgetComponent)
        //{
        //    if (budgetComponentItem == null)
        //        throw new ArgumentNullException("budgetComponentItem");
        //    if (budgetComponent == null)
        //        throw new ArgumentNullException("budgetComponent");

        //   // budgetComponentItem.Component = null;
        //    IList<T> itemCollection = GetItemCollection(budgetComponent);
        //    if (itemCollection.Any(x => Equals(x.Id, budgetComponentItem.Id)))
        //        itemCollection.Remove(budgetComponentItem);
        //}

        /// <summary>
        /// Saves the changes made to the references of the given budget component item.
        /// </summary>
        /// <param name="budgetComponentItem">The budget component item which references will be saved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public virtual void SaveReference(T budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");

            DatabaseContext.Store(budgetComponentItem);
        }

        /// <summary>
        /// Saves the changes made to the references of the given budget component.
        /// </summary>
        /// <param name="budgetComponent">The budget component which references will be saved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponent"/> is null.</exception>
        //public virtual void SaveReference(TComponent budgetComponent)
        //{
        //    if (budgetComponent == null)
        //        throw new ArgumentNullException("budgetComponent");

        //    IList<T> itemCollection = GetItemCollection(budgetComponent);
        //    DatabaseContext.Store(itemCollection);

        //    DatabaseContext.Store(budgetComponent);
        //}

       
        public virtual IEnumerable<T> FilterByName(string nameSpecification)
        {
            var itemsOfComponent = new BudgetComponentItemsOfSpecification<T>();
            var itemsWithName = new NomenclatorHavingInItsNameSpecification<T>(nameSpecification);
            var query = itemsOfComponent & itemsWithName;

            return DatabaseContext.Where(query).ToArray();
        }
    }


    /// <summary>
    /// Implementation of the base contract <see cref="BudgetComponentItemRepositoryBase{T, TComponent}"/> representing the base
    /// class of the repositories managing the data operations for budget component items.
    /// </summary>
    /// <typeparam name="T">The type of the budget component items managed here.</typeparam>
    /// <typeparam name="TComponent">The type of the budget component to which the items belong.</typeparam>
    public abstract class BudgetComponentItemRepositoryBaseEF<T, TClass> : EntityFrameworkRepositoryBase<T, TClass>,
       IRelatedRepository<T, IPeriod>, IBudgetComponentItemRepository<T>
        where T : class, IBudgetComponentItem
        where TClass : BudgetComponentItemBase
    {

        //private TComponent _component;

        /// <summary>
        /// Initializes a new instance of <see cref="BudgetComponentItemRepositoryBase{T, TComponent}"/> given the database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext"/> used to carry on with the raw data operations.</param>
        /// <exception cref="ArgumentNullException"><paramref name="databaseContext"/> is null.</exception>
        public BudgetComponentItemRepositoryBaseEF(IEntityFrameworkDbContext<TClass> databaseContext)
            : base(databaseContext)
        {
        }


      
        /// Relates the given investment element with a period, following the domain specification that every investment element must have a
        /// time period.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to relate to <paramref name="period"/>.</param>
        /// <param name="period">The <see cref="IPeriod"/> to relate to <paramref name="investmentElement"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="investmentElement"/> or <paramref name="period"/> is null.
        /// </exception>
        public void Relate(T plannedActivity, IPeriod period)
        {
            if (plannedActivity == null)
                throw new ArgumentNullException("plannedActivity");
            if (period == null)
                throw new ArgumentNullException("period");

            plannedActivity.Period = period;
            period.Holder = plannedActivity;
        }

        /// <summary>
        ///     Disrelates the given investment element with a budget.
        /// </summary>
        /// <param name="investmentElement">
        ///     The <see cref="IInvestmentElement" /> to break its relation with
        ///     <paramref name="period" />.
        /// </param>
        /// <param name="period">The <see cref="IPeriod" /> to break its relation with <paramref name="investmentElement" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="investmentElement" /> or <paramref name="period" /> is null.
        /// </exception>
        public void Unrelate(T plannedActivity, IPeriod period)
        {
            if (plannedActivity == null)
                throw new ArgumentNullException("plannedActivity");
            if (period == null)
                throw new ArgumentNullException("budget");

            //plannedActivity.Period = null;
            //period.Holder = null;
            var periodRepository = ServiceLocator.Current.GetInstance<IPeriodRepository>();
            periodRepository.Holder = plannedActivity;

            periodRepository.Delete(period);

        }

        public void SaveReference(IPeriod other)
        {
            // DatabaseContext.Store(other);
        }


        /// <summary>
        /// Gets the properties which changes must be saved.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Code), GetName(x => x.Name), GetName(x => x.Quantity), GetName(x => x.Description),
                    GetName(x => x.UnitaryCost),  GetName(x => x.isUnitaryPriceCalculated),  GetName(x => x.CalculatedUnitaryPrice)
                    ,GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.SubExpenseConcept),
                    GetName(x=>x.PriceSystem), GetName(x=>x.PeriodId), GetName(x => x.LastCalculatedStartDate), GetName(x => x.LastCalculatedFinishDate), GetName(x => x.StartCalculated), GetName(x => x.EndCalculated)
                }).ToArray();
                //,GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.ExpenseConcept)
            }
        }

        protected override T Clone(T budgetcomponentitem)
        {
            if (budgetcomponentitem == null)
                throw new ArgumentNullException("budgetcomponentitem");

            T budgetcomponentitemClone = base.Clone(budgetcomponentitem);

           

            budgetcomponentitemClone.Period = Clone(budgetcomponentitemClone.Period);
            budgetcomponentitemClone.Period.Holder = budgetcomponentitemClone;
            budgetcomponentitemClone.PeriodId = budgetcomponentitemClone.Period.Id;

            return budgetcomponentitemClone;
        }

        private IPeriod Clone(IPeriod period)
        {
            var periodClone = ServiceLocator.Current.GetInstance<IPeriod>();

            periodClone.Starts = period.OriStart();
            periodClone.Ends = period.OriEnd();
            periodClone.PeriodKind = period.PeriodKind;

            periodClone.Id = period.Id ?? DatabaseContext.GenerateKey();

            return periodClone;
        }
        ///// <summary>
        ///// When overridden in a deriver it returns the list of items of the budget component that the items managed here will added
        ///// or removed from.
        ///// </summary>
        //protected abstract Func<TComponent, IList<T>> GetItemCollection { get; }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override T Add(T budgetComponentItem)
        {
            T addedBudgetComponentITem = base.Add(budgetComponentItem);


            // this.Relate(budgetComponentItem, Component, DatabaseContext);

            return addedBudgetComponentITem;
        }


        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(T budgetComponentItem)
        {
            base.Update(budgetComponentItem);

            // this.Relate(budgetComponentItem, Component, DatabaseContext);

        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Delete(T budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");


            //var dbbudgetComponentItemt = DatabaseContext.Find<IBudgetComponentItem>(budgetComponentItem.Id);
            // this.Unrelate(budgetComponentItem, Component, DatabaseContext);

            var budgetComponentItemRepository = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<T>>();
            budgetComponentItemRepository.Component = budgetComponentItem;
            budgetComponentItemRepository.DeleteAll();

            Unrelate(budgetComponentItem, budgetComponentItem.Period);
            //foreach (IPlannedResource resourceComponentItem in dbbudgetComponentItemt.PlannedResources.ToArray())
            //    Delete(resourceComponentItem);

            base.Delete(budgetComponentItem);
        }
        ///// <summary>
        ///// Deletes the given buget component item from the current repository.
        ///// </summary>
        ///// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to delete.</param>
        ///// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        //public void Delete(IEntity budgetComponentItem)
        //{
        //    if (budgetComponentItem == null)
        //        throw new ArgumentNullException("budgetComponentItem");

        //    base.Delete( budgetComponentItem);
        //}
        /// <summary>
        /// Relates a item with its budget component.
        /// </summary>
        /// <param name="budgetComponentItem">The item to relate to <paramref name="budgetComponent"/>.</param>
        /// <param name="budgetComponent">The budget component to relate to <paramref name="budgetComponentItem"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="budgetComponentItem"/> or <paramref name="budgetComponent"/> is null.
        /// </exception>
        //public virtual void Relate(T budgetComponentItem, TComponent budgetComponent)
        //{
        //    if (budgetComponentItem == null)
        //        throw new ArgumentNullException("budgetComponentItem");
        //    if (budgetComponent == null)
        //        throw new ArgumentNullException("budgetComponent");

        //    //budgetComponentItem.Component = budgetComponent;
        //    IList<T> itemCollection = GetItemCollection(budgetComponent);
        //    if (itemCollection.All(x => !Equals(x.Id, budgetComponentItem.Id)))
        //        itemCollection.Add(budgetComponentItem);
        //}

       
        /// <summary>
        /// Saves the changes made to the references of the given budget component item.
        /// </summary>
        /// <param name="budgetComponentItem">The budget component item which references will be saved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public virtual void SaveReference(T budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");

            DatabaseContext.Save();
        }

        /// <summary>
        /// Saves the changes made to the references of the given budget component.
        /// </summary>
        /// <param name="budgetComponent">The budget component which references will be saved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponent"/> is null.</exception>
        //public virtual void SaveReference(TComponent budgetComponent)
        //{
        //    if (budgetComponent == null)
        //        throw new ArgumentNullException("budgetComponent");

        //    IList<T> itemCollection = GetItemCollection(budgetComponent);
        //    DatabaseContext.Store(itemCollection);

        //    DatabaseContext.Store(budgetComponent);
        //}


        public virtual IEnumerable<T> FilterByName(string nameSpecification)
        {
            var itemsOfComponent = new BudgetComponentItemsOfSpecification<T>();
            var itemsWithName = new NomenclatorHavingInItsNameSpecification<T>(nameSpecification);
            var query = itemsOfComponent & itemsWithName;

            return DatabaseContext.Where(query).ToArray();
        }
    }

}
