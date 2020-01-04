using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    public class CashMovementCategoryRepository<TItem>: Db4ORepositoryBase<TItem>,ICashMovementCategoryRepository<TItem>
        where TItem:class ,ICashMovementCategory
    {
        private ICashMovementCategory _cashMovementCategory;
        /// <summary>
        /// Superior Category
        /// </summary>
        public ICashMovementCategory SuperiorCategory { 
            get { return _cashMovementCategory; }
            set { _cashMovementCategory = value; } }

        public CashMovementCategoryRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
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
                    GetName(x => x.Name), GetName(x => x.Description)
                }).ToArray();
            }
        }


        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override TItem Add(TItem budgetComponentItem)
        {
            TItem addedBudgetComponentITem = base.Add(budgetComponentItem);

            this.Relate(budgetComponentItem, SuperiorCategory, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(TItem budgetComponentItem)
        {
            base.Update(budgetComponentItem);

            this.Relate(budgetComponentItem, SuperiorCategory, DatabaseContext);

        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<TItem> Entities
        {
            get
            {


                ISpecification<TItem> specification = new CashMovementCategoryOfSpecification<TItem>(SuperiorCategory);

                return Where(specification);
            }
        }


        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Delete(TItem budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("ICashMovementCategory");

            // this.Unrelate(budgetComponentItem, Component, DatabaseContext);

            var dbbudgetComponentItemt = DatabaseContext.Find<TItem>(budgetComponentItem.Id);
            if (dbbudgetComponentItemt != null)
            {
                foreach (ICashMovement resourceComponentItem in dbbudgetComponentItemt.Movements.ToArray())
                    Delete(resourceComponentItem);
                foreach (ICashMovementCategory resourceComponentItem in dbbudgetComponentItemt.SubCategories.ToArray())
                    Delete(resourceComponentItem);

            }
           
            base.Delete(budgetComponentItem);
        }
        public void Relate(TItem current, ICashMovementCategory other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.SuperiorCategory = other;
          //  IList<TItem> itemCollection = other.SubCategories;
            if (other.SubCategories.All(x => !Equals(x.Id, current.Id)))
                other.SubCategories.Add(current);
        }

        public void Unrelate(TItem current, ICashMovementCategory other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.SuperiorCategory = other;
            //  IList<TItem> itemCollection = other.SubCategories;
            if (other.SubCategories.All(x => !Equals(x.Id, current.Id)))
                other.SubCategories.Remove(current);
        }

        public void SaveReference(TItem current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Store(current);
        }

        public void SaveReference(ICashMovementCategory other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            IList<ICashMovementCategory> itemCollection = other.SubCategories;
            DatabaseContext.Store(itemCollection);

            IList<ICashMovement> itemCollection2 = other.Movements;
            DatabaseContext.Store(itemCollection2);

            DatabaseContext.Store(other);
        }
    }

    public class CashMovementCategoryRepositoryEF<TItem, TClass> : EntityFrameworkRepositoryBase<TItem, TClass>, ICashMovementCategoryRepository<TItem>
        where TItem : class, ICashMovementCategory
        where TClass : CashMovementCategory
    {
        private ICashMovementCategory _cashMovementCategory;
        /// <summary>
        /// Superior Category
        /// </summary>
        public ICashMovementCategory SuperiorCategory
        {
            get { return _cashMovementCategory; }
            set { _cashMovementCategory = value; }
        }

        public CashMovementCategoryRepositoryEF(IEntityFrameworkDbContext<TClass> databaseContext) : base(databaseContext)
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
                    GetName(x => x.Name), GetName(x => x.Description), GetName(x => x.SuperiorCategoryId)
                }).ToArray();
            }
        }


        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override TItem Add(TItem budgetComponentItem)
        {
            this.Relate(budgetComponentItem, SuperiorCategory);

            TItem addedBudgetComponentITem = base.Add(budgetComponentItem);

            //this.Relate(budgetComponentItem, SuperiorCategory, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(TItem budgetComponentItem)
        {
            this.Relate(budgetComponentItem, SuperiorCategory);
            base.Update(budgetComponentItem);

            

        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<TItem> Entities
        {
            get
            {


                var specification = new CashMovementCategoryOfQueryable<TClass>(SuperiorCategory, DatabaseContext);

                return Where(specification);
            }
        }


        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Delete(TItem budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("ICashMovementCategory");

            // this.Unrelate(budgetComponentItem, Component, DatabaseContext);

            //var dbbudgetComponentItemt = DatabaseContext.Find<TItem>(budgetComponentItem.Id);

            var movementcatRepo = ServiceLocator.Current.GetInstance<ICashMovementCategoryRepository<TItem>>();
            movementcatRepo.SuperiorCategory = budgetComponentItem;
            movementcatRepo.DeleteAll();


            var movementRepo = ServiceLocator.Current.GetInstance<ICashMovementRepository>();
            movementRepo.Category = budgetComponentItem;
            movementRepo.DeleteAll();


            //if (dbbudgetComponentItemt != null)
            //{
            //    foreach (ICashMovement resourceComponentItem in dbbudgetComponentItemt.Movements.ToArray())
            //        Delete(resourceComponentItem);
            //    foreach (ICashMovementCategory resourceComponentItem in dbbudgetComponentItemt.SubCategories.ToArray())
            //        Delete(resourceComponentItem);

            //}

            base.Delete(budgetComponentItem);
        }
        public void Relate(TItem current, ICashMovementCategory other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.SuperiorCategory = other;
            current.SuperiorCategoryId = other.Id;
            //  IList<TItem> itemCollection = other.SubCategories;
            //if (other.SubCategories.All(x => !Equals(x.Id, current.Id)))
            //    other.SubCategories.Add(current);
        }

        public void Unrelate(TItem current, ICashMovementCategory other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.SuperiorCategory = other;
            current.SuperiorCategoryId = other.Id;
            //  IList<TItem> itemCollection = other.SubCategories;
            //if (other.SubCategories.All(x => !Equals(x.Id, current.Id)))
            //    other.SubCategories.Remove(current);
        }

        public void SaveReference(TItem current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Save();
        }

        public void SaveReference(ICashMovementCategory other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            DatabaseContext.Save();

        }
    }
}
