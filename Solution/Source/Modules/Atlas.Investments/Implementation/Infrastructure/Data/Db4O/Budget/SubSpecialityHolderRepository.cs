using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget
{
    public abstract class SubSpecialityHolderRepository<THolder> : Db4ORepositoryBase<THolder>, IRelatedRepository<THolder, IBudgetComponent>, ISubSpecialityHolderRepository<THolder>
        where THolder:class,ISubSpecialityHolder
    {
        public SubSpecialityHolderRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public IBudgetComponent BudgetComponent { get; set; }

        /// <summary>
        /// Gets the properties which changes must be saved.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
               {
                    GetName(x=>x.SubSpeciality), GetName(x=>x.Category), GetName(x=>x.SubExpenseConcept)
                }).ToArray();
                //,GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.ExpenseConcept)
            }
        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<THolder> Entities
        {
            get
            {
                //if (typeof(IVariantLinesHolder) == Component.GetType())
                //{
                //    return base.Entities;
                //}

                ISpecification<THolder> specification = new SubSpecialityHolderOfSpecification<THolder>(BudgetComponent);

                return Where(specification);
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override THolder Add(THolder budgetComponentItem)
        {
            THolder addedBudgetComponentITem = base.Add(budgetComponentItem);

            
            this.Relate(budgetComponentItem, BudgetComponent, DatabaseContext);

            return budgetComponentItem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(THolder budgetComponentItem)
        {
            base.Update(budgetComponentItem);

            this.Relate(budgetComponentItem, BudgetComponent, DatabaseContext);
           
        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Delete(THolder budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");

            this.Unrelate(budgetComponentItem, BudgetComponent, DatabaseContext);

            var plannedRepo = ServiceLocator.Current.GetInstance<IPlannedActivityRepository>();
            plannedRepo.SubSpecialityHolder = budgetComponentItem;
            plannedRepo.DeleteAll();

            var executedRepo = ServiceLocator.Current.GetInstance<IExecutedActivityRepository>();
            executedRepo.SubSpecialityHolder = budgetComponentItem;
            executedRepo.DeleteAll();

            base.Delete(budgetComponentItem);
        }

        public void Relate(THolder current, IBudgetComponent other)
        {
            if (current == null)
                throw new ArgumentNullException("current");
            if (other == null)
                throw new ArgumentNullException("other");

            current.BudgetComponent = other;
        }

        public void Unrelate(THolder current, IBudgetComponent other)
        {
            if (current == null)
                throw new ArgumentNullException("current");
            if (other == null)
                throw new ArgumentNullException("other");

            current.BudgetComponent = null;
        }

        public void SaveReference(THolder current)
        {
            if (current == null)
                throw new ArgumentNullException("current");

            DatabaseContext.Store(current);
        }

        public void SaveReference(IBudgetComponent other)
        {

            if (other == null)
                throw new ArgumentNullException("other");

            DatabaseContext.Store(other);
        }
    }

    public abstract class SubSpecialityHolderRepositoryEF<THolder, TClass> : EntityFrameworkRepositoryBase<THolder, TClass>, IRelatedRepository<THolder, IBudgetComponent>, ISubSpecialityHolderRepository<THolder>
           where THolder : class, ISubSpecialityHolder
          where TClass :  SubSpecialityHolder
    {
        public SubSpecialityHolderRepositoryEF(IEntityFrameworkDbContext<TClass> databaseContext) : base(databaseContext)
        {
        }

        public IBudgetComponent BudgetComponent { get; set; }

        /// <summary>
        /// Gets the properties which changes must be saved.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
               {
                    GetName(x=>x.SubSpeciality), GetName(x=>x.Category), GetName(x=>x.SubExpenseConcept), GetName(x=>x.BudgetComponentId)
                }).ToArray();
                //,GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.ExpenseConcept)
            }
        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<THolder> Entities
        {
            get
            {
                //if (typeof(IVariantLinesHolder) == Component.GetType())
                //{
                //    return base.Entities;
                //}

                var specification = new SubSpecialityHolderOfQueryable<TClass>(BudgetComponent,DatabaseContext);
                var list = DatabaseContext.Entities.ToList();
                return Where(specification);
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override THolder Add(THolder budgetComponentItem)
        {

            this.Relate(budgetComponentItem, BudgetComponent);

            THolder addedBudgetComponentITem = base.Add(budgetComponentItem);


           
            return budgetComponentItem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(THolder budgetComponentItem)
        {
            this.Relate(budgetComponentItem, BudgetComponent);
            base.Update(budgetComponentItem);

            var list = Entities.ToList();

        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Delete(THolder budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");

            this.Unrelate(budgetComponentItem, BudgetComponent);

            var plannedRepo = ServiceLocator.Current.GetInstance<IPlannedActivityRepository>();
            plannedRepo.SubSpecialityHolder = budgetComponentItem;
            plannedRepo.DeleteAll();

            var executedRepo = ServiceLocator.Current.GetInstance<IExecutedActivityRepository>();
            executedRepo.SubSpecialityHolder = budgetComponentItem;
            executedRepo.DeleteAll();

            base.Delete(budgetComponentItem);
        }

        public void Relate(THolder current, IBudgetComponent other)
        {
            if (current == null)
                throw new ArgumentNullException("current");
            if (other == null)
                throw new ArgumentNullException("other");

            current.BudgetComponent = other;
            current.BudgetComponentId = other.Id;
        }

        public void Unrelate(THolder current, IBudgetComponent other)
        {
            if (current == null)
                throw new ArgumentNullException("current");
            if (other == null)
                throw new ArgumentNullException("other");

            current.BudgetComponent = null;
            current.BudgetComponentId = null;
        }

        public void SaveReference(THolder current)
        {
            if (current == null)
                throw new ArgumentNullException("current");

            DatabaseContext.Save();
        }

        public void SaveReference(IBudgetComponent other)
        {

            if (other == null)
                throw new ArgumentNullException("other");

            DatabaseContext.Save();
        }
    }
}