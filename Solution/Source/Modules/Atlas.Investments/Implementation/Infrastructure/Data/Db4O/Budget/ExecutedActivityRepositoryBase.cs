using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget
{
    /// <summary>
    ///     Base class of the repository managing the data operations related to the executed resources of a certain budget
    ///     component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the executed resources.</typeparam>
    public   class ExecutedActivityRepositoryBase :
       ActivityRepositoryBase<IExecutedActivity>, IRelatedRepository<IExecutedActivity, ISubSpecialityHolder> ,IExecutedActivityRepository
        
        //where TComponent : class, IBudgetComponent
    {
        //private TComponent _component;
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
        /// Gets the properties which changes must be saved.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Planification)
                }).ToArray();
                //,GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.ExpenseConcept)
            }
        }
        /// <summary>
        ///     Initializes a new instance of <see cref="ExecutedActivityRepositoryBase{TComponent}" /> with a
        ///     <see cref="IDb4ODatabaseContext" />.
        /// </summary>
        /// <param name="databaseContext">
        ///     The instance of <see cref="IDb4ODatabaseContext" /> that carries on the actual raw data operations the
        ///     initializing repository performs.
        /// </param>
        public ExecutedActivityRepositoryBase(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

       
        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<IExecutedActivity> Entities
        {
            get
            {
                ISpecification<IExecutedActivity> specification = new BudgetComponentExecutedActivityOfSpecification<IExecutedActivity>(SubSpecialityHolder);
                if (!Equals(SubSpecialityHolder, null))
                {
                    specification = new SubSpecialityActivityOfSpecification<IExecutedActivity>(SubSpecialityHolder);
                    // return base.Entities;
                }


                return Where(specification);
            }
        }

       
        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override IExecutedActivity Add(IExecutedActivity budgetComponentItem)
        {
            IExecutedActivity addedBudgetComponentITem = base.Add(budgetComponentItem);

            this.Relate(budgetComponentItem, SubSpecialityHolder, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(IExecutedActivity budgetComponentItem)
        {
            base.Update(budgetComponentItem);

            this.Relate(budgetComponentItem, SubSpecialityHolder, DatabaseContext);
        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Delete(IExecutedActivity budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");

            this.Unrelate(budgetComponentItem, SubSpecialityHolder, DatabaseContext);

            var budgetComponentItemRepository = ServiceLocator.Current.GetInstance<IExecutionRepository>();
            budgetComponentItemRepository.ExecutedActivity = budgetComponentItem;
            budgetComponentItemRepository.DeleteAll();

            base.Delete(budgetComponentItem);
        }

        /// <summary>
        /// Relates a item with its budget component.
        /// </summary>
        /// <param name="budgetComponentItem">The item to relate to <paramref name="budgetComponent"/>.</param>
        /// <param name="budgetComponent">The budget component to relate to <paramref name="budgetComponentItem"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="budgetComponentItem"/> or <paramref name="budgetComponent"/> is null.
        /// </exception>
        public  void Relate(IExecutedActivity budgetComponentItem, ISubSpecialityHolder budgetComponent)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (budgetComponent == null)
                throw new ArgumentNullException("budgetComponent");

            budgetComponentItem.SubSpecialityHolder = budgetComponent;
            //IList<IExecutedActivity> itemCollection = GetItemCollection(budgetComponent);
            //if (itemCollection.All(x => !Equals(x.Id, budgetComponentItem.Id)))
            //    itemCollection.Add(budgetComponentItem);
        }

        /// <summary>
        /// Breaks the relation of the given item with its budget component.
        /// </summary>
        /// <param name="budgetComponentItem">The item to break its relation to <paramref name="budgetComponent"/>.</param>
        /// <param name="budgetComponent">The budget component to break its relation to <paramref name="budgetComponentItem"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="budgetComponentItem"/> or <paramref name="budgetComponent"/> is null.
        /// </exception>
        public  void Unrelate(IExecutedActivity budgetComponentItem, ISubSpecialityHolder budgetComponent)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (budgetComponent == null)
                throw new ArgumentNullException("budgetComponent");

             budgetComponentItem.SubSpecialityHolder = null;
            //IList<IExecutedActivity> itemCollection = GetItemCollection(budgetComponent);
            //if (itemCollection.Any(x => Equals(x.Id, budgetComponentItem.Id)))
            //    itemCollection.Remove(budgetComponentItem);
        }

        public void Relate(IExecutedActivity current, IPeriod other)
        {
            throw new NotImplementedException();
        }

        public void Unrelate(IExecutedActivity current, IPeriod other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the changes made to the references of the given budget component item.
        /// </summary>
        /// <param name="budgetComponentItem">The budget component item which references will be saved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void SaveReference(IExecutedActivity budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");

            DatabaseContext.Store(budgetComponentItem);
        }

        public void SaveReference(IPeriod other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the changes made to the references of the given budget component.
        /// </summary>
        /// <param name="budgetComponent">The budget component which references will be saved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponent"/> is null.</exception>
        public  void SaveReference(ISubSpecialityHolder budgetComponent)
        {
            if (budgetComponent == null)
                throw new ArgumentNullException("budgetComponent");

            //IList<IExecutedActivity> itemCollection = GetItemCollection(budgetComponent);
            //DatabaseContext.Store(itemCollection);

            DatabaseContext.Store(budgetComponent);
        }


        public override IEnumerable<IExecutedActivity> FilterByName(string nameSpecification)
        {
            var itemsOfComponent = new BudgetComponentExecutedActivityOfSpecification<IExecutedActivity>(SubSpecialityHolder);
            var itemsWithName = new NomenclatorHavingInItsNameSpecification<IExecutedActivity>(nameSpecification);
            var query = itemsOfComponent & itemsWithName;

            return DatabaseContext.Where(query).ToArray();
        }
        /// <summary>
        ///     Gets the collection of the executed resources in the budget component in which it will be contained the items
        ///     managed in the current <see cref="ExecutedActivityRepositoryBase{TComponent}" />.
        /// </summary>
        //protected override Func<TComponent, IList<IExecutedActivity>> GetItemCollection
        //{
        //    get { return x => x.ExecutedActivities; }
        //}
    }

    /// <summary>
    ///     Base class of the repository managing the data operations related to the executed resources of a certain budget
    ///     component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the executed resources.</typeparam>
    public class ExecutedActivityRepositoryBaseEF :
            ActivityRepositoryBaseEF<IExecutedActivity, ExecutedActivity>, IRelatedRepository<IExecutedActivity, ISubSpecialityHolder>,
            IExecutedActivityRepository

        //where TComponent : class, IBudgetComponent
    {
        //private TComponent _component;
        
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Planification)
                }).ToArray();
                //,GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.ExpenseConcept)
            }
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="ExecutedActivityRepositoryBase{TComponent}" /> with a
        ///     <see cref="IDb4ODatabaseContext" />.
        /// </summary>
        /// <param name="databaseContext">
        ///     The instance of <see cref="IDb4ODatabaseContext" /> that carries on the actual raw data operations the
        ///     initializing repository performs.
        /// </param>
        public ExecutedActivityRepositoryBaseEF(IEntityFrameworkDbContext<ExecutedActivity> databaseContext)
            : base(databaseContext)
        {
        }

    }
}