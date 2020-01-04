using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    public class CashMovementRepository : Db4ORepositoryBase<ICashMovement>,ICashMovementRepository
    {

        private ICashMovementCategory _category;
        public CashMovementRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
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
                    GetName(x => x.Date), GetName(x => x.Amount)
                }).ToArray();
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override ICashMovement Add(ICashMovement budgetComponentItem)
        {
            ICashMovement addedBudgetComponentITem = base.Add(budgetComponentItem);

            this.Relate(budgetComponentItem, Category, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(ICashMovement budgetComponentItem)
        {
            base.Update(budgetComponentItem);

            this.Relate(budgetComponentItem, Category, DatabaseContext);

        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<ICashMovement> Entities
        {
            get
            {


                ISpecification<ICashMovement> specification = new CashMovementOfSpecification(Category);

                return Where(specification);
            }
        }


        public void Relate(ICashMovement current, ICashMovementCategory other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.CashMovementCategory = other;
            //  IList<TItem> itemCollection = other.SubCategories;
            if (other.Movements.All(x => !Equals(x.Id, current.Id)))
                other.Movements.Add(current);
        }

        public void Unrelate(ICashMovement current, ICashMovementCategory other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.CashMovementCategory = other;
            //  IList<TItem> itemCollection = other.SubCategories;
            if (other.Movements.All(x => !Equals(x.Id, current.Id)))
                other.Movements.Remove(current);
        }

        public void SaveReference(ICashMovement current)
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

        public ICashMovementCategory Category { get { return _category; } set { _category = value; }}
   }

    public class CashMovementRepositoryEF : EntityFrameworkRepositoryBase<ICashMovement, CashMovement>, ICashMovementRepository
    {

        private ICashMovementCategory _category;
        public CashMovementRepositoryEF(IEntityFrameworkDbContext<CashMovement> databaseContext) : base(databaseContext)
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
                    GetName(x => x.Date), GetName(x => x.Amount), GetName(x => x.CashMovementCategoryId)
                }).ToArray();
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override ICashMovement Add(ICashMovement budgetComponentItem)
        {
            this.Relate(budgetComponentItem, Category);
            ICashMovement addedBudgetComponentITem = base.Add(budgetComponentItem);

            //this.Relate(budgetComponentItem, Category, DatabaseContext);

            return budgetComponentItem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(ICashMovement budgetComponentItem)
        {
            this.Relate(budgetComponentItem, Category);
            base.Update(budgetComponentItem);

            

        }


        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<ICashMovement> Entities
        {
            get
            {


                var specification = new CashMovementOfQueryable(Category,DatabaseContext);

                return Where(specification);
            }
        }


        public void Relate(ICashMovement current, ICashMovementCategory other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.CashMovementCategory = other;
            current.CashMovementCategoryId = other.Id;
        }

        public void Unrelate(ICashMovement current, ICashMovementCategory other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.CashMovementCategory = null;
            current.CashMovementCategoryId = null;
        }

        public void SaveReference(ICashMovement current)
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

        public ICashMovementCategory Category { get { return _category; } set { _category = value; } }
    }
}
