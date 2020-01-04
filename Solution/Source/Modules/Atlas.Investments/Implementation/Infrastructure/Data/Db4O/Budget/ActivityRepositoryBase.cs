using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
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
    /// Initializes a new instance of <see cref="BudgetComponentItemRepositoryBase{T, TComponent}"/> given the database context.
    /// </summary>
    /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext"/> used to carry on with the raw data operations.</param>
    /// <exception cref="ArgumentNullException"><paramref name="databaseContext"/> is null.</exception>

    public abstract class ActivityRepositoryBase<TActivity> : BudgetComponentItemRepositoryBase<TActivity>,
          IActivityRepository<TActivity>

         // IBudgetComponentItemRepository<IPlannedActivity,TComponent>
         where TActivity : class, IActivity
        //where TComponent : class, IBudgetComponent
    {

      

        /// <summary>
        /// Initializes a new instance of <see cref="BudgetComponentItemRepositoryBase{T, TComponent}"/> given the database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext"/> used to carry on with the raw data operations.</param>
        /// <exception cref="ArgumentNullException"><paramref name="databaseContext"/> is null.</exception>
         public ActivityRepositoryBase(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
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
                    GetName(x=>x.SubSpeciality), GetName(x=>x.Executor)
                }).ToArray();
                 //,GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.ExpenseConcept)
             }
         }
       
        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<TActivity> Entities
        {
            get
            {
                

                ISpecification<TActivity> specification = new BudgetComponentActivityOfSpecification<TActivity>(SubSpecialityHolder);
                if (!Equals(SubSpecialityHolder, null))
                {
                    specification = new SubSpecialityActivityOfSpecification<TActivity>(SubSpecialityHolder);
                   // return base.Entities;
                }
                return Where(specification);
            }
        }

       
       
        /// <summary>
        /// Relates a item with its budget component.
        /// </summary>
        /// <param name="budgetComponentItem">The item to relate to <paramref name="budgetComponent"/>.</param>
        /// <param name="budgetComponent">The budget component to relate to <paramref name="budgetComponentItem"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="budgetComponentItem"/> or <paramref name="budgetComponent"/> is null.
        /// </exception>
        public  void Relate(TActivity budgetComponentItem, ISubSpecialityHolder budgetComponent)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (budgetComponent == null)
                throw new ArgumentNullException("budgetComponent");

            budgetComponentItem.SubSpecialityHolder = budgetComponent;
            //IList<IPlannedActivity> itemCollection = GetItemCollection(budgetComponent);
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
        public  void Unrelate(TActivity budgetComponentItem, ISubSpecialityHolder budgetComponent)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (budgetComponent == null)
                throw new ArgumentNullException("budgetComponent");

             budgetComponentItem.SubSpecialityHolder = null;
            //IList<IPlannedActivity> itemCollection = GetItemCollection(budgetComponent);
            //if (itemCollection.Any(x => Equals(x.Id, budgetComponentItem.Id)))
            //    itemCollection.Remove(budgetComponentItem);
        }

        /// <summary>
        /// Saves the changes made to the references of the given budget component item.
        /// </summary>
        /// <param name="budgetComponentItem">The budget component item which references will be saved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void SaveReference(TActivity budgetComponentItem)
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
        public void SaveReference(ISubSpecialityHolder budgetComponent)
        {
            if (budgetComponent == null)
                throw new ArgumentNullException("budgetComponent");

            //IList<IPlannedActivity> itemCollection = GetItemCollection(budgetComponent);
            //DatabaseContext.Store(itemCollection);

            DatabaseContext.Store(budgetComponent);
        }


        public override IEnumerable<TActivity> FilterByName(string nameSpecification)
        {
            var itemsOfComponent = new BudgetComponentActivityOfSpecification<TActivity>(SubSpecialityHolder);
            var itemsWithName = new NomenclatorHavingInItsNameSpecification<TActivity>(nameSpecification);
            var query = itemsOfComponent & itemsWithName;

            return DatabaseContext.Where(query).ToArray();
        }

        protected override TActivity Clone(TActivity plannedActivity)
        {
            if (plannedActivity == null)
                throw new ArgumentNullException("plannedActivity");

            TActivity plannedActivityClone = base.Clone(plannedActivity);

            bool trouble = false;
            if (plannedActivity.Period == null)
            {
                plannedActivity.Period = new Period();
                trouble = true;
            }
             
              // throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "plannedActivityPeriod");

           

            plannedActivityClone.Period = Clone(plannedActivity.Period);
            plannedActivityClone.Period.Holder = plannedActivityClone;

            if (trouble)
            {
                var periodRepo = ServiceLocator.Current.GetInstance<IPeriodRepository>();
                periodRepo.Holder = plannedActivityClone;

                periodRepo.Add(plannedActivityClone.Period);
                SaveReference(plannedActivityClone);
            }

            return plannedActivityClone;
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
        public void Relate(TActivity plannedActivity, IPeriod period)
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
        public void Unrelate(TActivity plannedActivity, IPeriod period)
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

        }

        public ISubSpecialityHolder SubSpecialityHolder { get; set; }
    }

    /// <summary>
    /// Initializes a new instance of <see cref="BudgetComponentItemRepositoryBase{T, TComponent}"/> given the database context.
    /// </summary>
    /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext"/> used to carry on with the raw data operations.</param>
    /// <exception cref="ArgumentNullException"><paramref name="databaseContext"/> is null.</exception>

    public abstract class ActivityRepositoryBaseEF<TActivity, TClass> : BudgetComponentItemRepositoryBaseEF<TActivity,TClass>,
          IActivityRepository<TActivity>

         // IBudgetComponentItemRepository<IPlannedActivity,TComponent>
         where TActivity : class, IActivity
        where TClass : Activity
    {



        /// <summary>
        /// Initializes a new instance of <see cref="BudgetComponentItemRepositoryBase{T, TComponent}"/> given the database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext"/> used to carry on with the raw data operations.</param>
        /// <exception cref="ArgumentNullException"><paramref name="databaseContext"/> is null.</exception>
        public ActivityRepositoryBaseEF(IEntityFrameworkDbContext<TClass> databaseContext)
           : base(databaseContext)
        {
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
                    GetName(x=>x.SubSpeciality), GetName(x=>x.Executor), GetName(x=>x.SubSpecialityHolderId)
                }).ToArray();
                //,GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.ExpenseConcept)
            }
        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<TActivity> Entities
        {
            get
            {


                var specification = new ActivityBaseOfQueryable<TClass>(SubSpecialityHolder, DatabaseContext);
               
                return WhereSQL(specification);
            }
        }


        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override TActivity Add(TActivity budgetComponentItem)
        {
            this.Relate(budgetComponentItem, SubSpecialityHolder);
            TActivity addedBudgetComponentITem = base.Add(budgetComponentItem);

            Relate(budgetComponentItem, addedBudgetComponentITem.Period);
           // budgetComponentItem = addedBudgetComponentITem;

            // this.Relate(budgetComponentItem, Component, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Relates a item with its budget component.
        /// </summary>
        /// <param name="budgetComponentItem">The item to relate to <paramref name="budgetComponent"/>.</param>
        /// <param name="budgetComponent">The budget component to relate to <paramref name="budgetComponentItem"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="budgetComponentItem"/> or <paramref name="budgetComponent"/> is null.
        /// </exception>
        public void Relate(TActivity budgetComponentItem, ISubSpecialityHolder budgetComponent)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (budgetComponent == null)
                throw new ArgumentNullException("budgetComponent");

            budgetComponentItem.SubSpecialityHolder = budgetComponent;
            budgetComponentItem.SubSpecialityHolderId = budgetComponent.Id;
            //IList<IPlannedActivity> itemCollection = GetItemCollection(budgetComponent);
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
        public void Unrelate(TActivity budgetComponentItem, ISubSpecialityHolder budgetComponent)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (budgetComponent == null)
                throw new ArgumentNullException("budgetComponent");

            budgetComponentItem.SubSpecialityHolder = null;
            budgetComponentItem.SubSpecialityHolderId = null;
            //IList<IPlannedActivity> itemCollection = GetItemCollection(budgetComponent);
            //if (itemCollection.Any(x => Equals(x.Id, budgetComponentItem.Id)))
            //    itemCollection.Remove(budgetComponentItem);
        }

        /// <summary>
        /// Saves the changes made to the references of the given budget component item.
        /// </summary>
        /// <param name="budgetComponentItem">The budget component item which references will be saved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void SaveReference(TActivity budgetComponentItem)
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
        public void SaveReference(ISubSpecialityHolder budgetComponent)
        {
            if (budgetComponent == null)
                throw new ArgumentNullException("budgetComponent");

            //IList<IPlannedActivity> itemCollection = GetItemCollection(budgetComponent);
            //DatabaseContext.Store(itemCollection);

            DatabaseContext.Save();
        }


        public override IEnumerable<TActivity> FilterByName(string nameSpecification)
        {
            var itemsOfComponent = new BudgetComponentActivityOfSpecification<TActivity>(SubSpecialityHolder);
            var itemsWithName = new NomenclatorHavingInItsNameSpecification<TActivity>(nameSpecification);
            var query = itemsOfComponent & itemsWithName;

            return DatabaseContext.Where(query).ToArray();
        }

        protected override TClass Clone(TClass plannedActivity)
        {
            if (plannedActivity == null)
                throw new ArgumentNullException("plannedActivity");

            TClass plannedActivityClone = base.Clone(plannedActivity);

            //bool trouble = false;
            //if (plannedActivity.Period == null)
            //{
            //    plannedActivity.Period = new Period();
            //    trouble = true;
            //}

            // throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "plannedActivityPeriod");
            if (!isAdding)
            {
                var perDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Period>>();
                var realPeriod = perDBC.Find(plannedActivity.PeriodId);
                if (realPeriod != null)
                    plannedActivity.Period = realPeriod;


            }



            plannedActivityClone.Period = Clone(plannedActivity.Period);
            plannedActivityClone.Period.Holder = plannedActivityClone;
            plannedActivityClone.Period.HolderId = plannedActivityClone.Id;
            plannedActivityClone.PeriodId = plannedActivityClone.Period.Id;


            if (isAdding)
            {
                var perDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Period>>();

                perDBC.Add(plannedActivityClone.Period);
                perDBC.Save();
                plannedActivityClone.Period = Clone(plannedActivityClone.Period);
                plannedActivityClone.Period.Holder = plannedActivityClone;
                plannedActivityClone.Period.HolderId = plannedActivityClone.Id;
                plannedActivityClone.PeriodId = plannedActivityClone.Period.Id;
            }
            //if (trouble)
            //{
            //    var periodRepo = ServiceLocator.Current.GetInstance<IPeriodRepository>();
            //    periodRepo.Holder = plannedActivityClone;

            //    periodRepo.Add(plannedActivityClone.Period);
            //    SaveReference(plannedActivityClone);
            //}
            plannedActivityClone.SubSpecialityHolder = SubSpecialityHolder;
            plannedActivityClone.SubSpecialityHolderId = SubSpecialityHolder.Id;
            return plannedActivityClone;
        }

        protected override TActivity Clone(TActivity plannedActivity)
        {
            if (plannedActivity == null)
                throw new ArgumentNullException("plannedActivity");

            TActivity plannedActivityClone = base.Clone(plannedActivity);

            //bool trouble = false;
            //if (plannedActivity.Period == null)
            //{
            //    plannedActivity.Period = new Period();
            //    trouble = true;
            //}

            // throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "plannedActivityPeriod");
            if (!isAdding)
            {
                var perDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Period>>();
                var realPeriod = perDBC.Find(plannedActivity.PeriodId);
                if (realPeriod != null)
                    plannedActivity.Period = realPeriod;


            }



            plannedActivityClone.Period = Clone(plannedActivity.Period);
            plannedActivityClone.Period.Holder = plannedActivityClone;
            plannedActivityClone.Period.HolderId = plannedActivityClone.Id;
            plannedActivityClone.PeriodId = plannedActivityClone.Period.Id;


            if (isAdding)
            {
                var perDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Period>>();

                perDBC.Add(plannedActivityClone.Period);
                perDBC.Save();
                plannedActivityClone.Period = Clone(plannedActivityClone.Period);
                plannedActivityClone.Period.Holder = plannedActivityClone;
                plannedActivityClone.Period.HolderId = plannedActivityClone.Id;
                plannedActivityClone.PeriodId = plannedActivityClone.Period.Id;
            }
            //if (trouble)
            //{
            //    var periodRepo = ServiceLocator.Current.GetInstance<IPeriodRepository>();
            //    periodRepo.Holder = plannedActivityClone;

            //    periodRepo.Add(plannedActivityClone.Period);
            //    SaveReference(plannedActivityClone);
            //}
            plannedActivityClone.SubSpecialityHolder = SubSpecialityHolder;
            plannedActivityClone.SubSpecialityHolderId = SubSpecialityHolder.Id;
            return plannedActivityClone;
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
        public void Relate(TActivity plannedActivity, IPeriod period)
        {
            if (plannedActivity == null)
                throw new ArgumentNullException("plannedActivity");
            if (period == null)
                throw new ArgumentNullException("period");

            plannedActivity.Period = period;
            plannedActivity.PeriodId = period.Id;
            period.Holder = plannedActivity;
            period.HolderId = plannedActivity.Id;
            
        }

      
      
        public ISubSpecialityHolder SubSpecialityHolder { get; set; }
    }
}
