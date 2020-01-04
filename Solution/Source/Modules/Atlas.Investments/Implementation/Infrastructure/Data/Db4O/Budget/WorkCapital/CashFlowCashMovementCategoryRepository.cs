using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget.WorkCapital
{
    public class CashFlowCashMovementCategoryRepository<TItem> : Db4ORepositoryBase<TItem>, ICashFlowCashMovementCategoryRepository<TItem>
        where TItem : class ,ICashMovementCategory
    {
        public CashFlowCashMovementCategoryRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
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

            this.Relate(budgetComponentItem, WorkCapitalCashFlow, DatabaseContext);

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

            this.Relate(budgetComponentItem, WorkCapitalCashFlow, DatabaseContext);

        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<TItem> Entities
        {
            get
            {


                ISpecification<TItem> specification = new CashFlowCashMovementCategoryOfSpecification<TItem>(WorkCapitalCashFlow);

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
            foreach (ICashMovement resourceComponentItem in dbbudgetComponentItemt.Movements.ToArray())
                Delete(resourceComponentItem);
            foreach (ICashMovementCategory resourceComponentItem in dbbudgetComponentItemt.SubCategories.ToArray())
                Delete(resourceComponentItem);

            base.Delete(budgetComponentItem);
        }
        public void Relate(TItem current, IWorkCapitalCashFlow other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.SuperiorCategory = other;
            //  IList<TItem> itemCollection = other.SubCategories;
            //if (other.SubCategories.All(x => !Equals(x.Id, current.Id)))
            //    other.SubCategories.Add(current);
        }

        public void Unrelate(TItem current, IWorkCapitalCashFlow other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.SuperiorCategory = null;
            //  IList<TItem> itemCollection = other.SubCategories;
            //if (other.SubCategories.All(x => !Equals(x.Id, current.Id)))
            //    other.SubCategories.Remove(current);
        }

        public void SaveReference(TItem current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Store(current);
        }

        public void SaveReference(IWorkCapitalCashFlow other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            //IList<ICashMovementCategory> itemCollection = other.SubCategories;
            //DatabaseContext.Store(itemCollection);

            //IList<ICashMovement> itemCollection2 = other.Movements;
            //DatabaseContext.Store(itemCollection2);

            DatabaseContext.Store(other);
        }

        public IWorkCapitalCashFlow WorkCapitalCashFlow { get; set; }
    }
}
