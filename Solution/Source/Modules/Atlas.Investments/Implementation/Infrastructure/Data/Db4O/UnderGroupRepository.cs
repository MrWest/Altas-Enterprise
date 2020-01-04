using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    
    public class UnderGroupRepository : PriceSystemGroupRepository<IUnderGroup>, IUnderGroupRepository, IRelatedRepository<IUnderGroup, IRegularGroup>
    {
        public UnderGroupRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public IRegularGroup RegularGroup { get; set; }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<IUnderGroup> Entities
        {
            get
            {


                ISpecification<IUnderGroup> specification = new UnderGroupOfSpecification(RegularGroup);

                return Where(specification);
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override IUnderGroup Add(IUnderGroup budgetComponenIOverGroup)
        {
            IUnderGroup addedBudgetComponenIOverGroup = base.Add(budgetComponenIOverGroup);

            this.Relate(budgetComponenIOverGroup, RegularGroup, DatabaseContext);

            return addedBudgetComponenIOverGroup;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Update(IUnderGroup budgetComponenIOverGroup)
        {
            base.Update(budgetComponenIOverGroup);

            this.Relate(budgetComponenIOverGroup, RegularGroup, DatabaseContext);

        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Delete(IUnderGroup budgetComponenIOverGroup)
        {
            if (budgetComponenIOverGroup == null)
                throw new ArgumentNullException("ICashMovementCategory");

            // this.Unrelate(budgetComponenIOverGroup, Component, DatabaseContext);

            var dbbudgetComponenIOverGroupt = DatabaseContext.Find<IUnderGroup>(budgetComponenIOverGroup.Id);
            if (dbbudgetComponenIOverGroupt != null)
            {
                var variantRepo = ServiceLocator.Current.GetInstance<IUnderGroupActivityRepository>();
                variantRepo.UnderGroup = dbbudgetComponenIOverGroupt;
                variantRepo.DeleteAll();
                var variantRepo2 = ServiceLocator.Current.GetInstance<IUnderGroupResourceRepository>();
                variantRepo2.UnderGroup = dbbudgetComponenIOverGroupt;
                variantRepo2.DeleteAll();
            }

            base.Delete(budgetComponenIOverGroup);
        }

        public void Relate(IUnderGroup current, IRegularGroup other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.RegularGroup = other;
            //  IList<TItem> itemCollection = other.SubCategories;
            //if (other.UnderGroups.All(x => !Equals(x.Id, current.Id)))
            //    other.UnderGroups.Add(current);
        }

        public void Unrelate(IUnderGroup current, IRegularGroup other)
        {

            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.RegularGroup = null;
            //  IList<TItem> itemCollection = other.SubCategories;
            //if (other.UnderGroups.All(x => !Equals(x.Id, current.Id)))
                //other.UnderGroups.Remove(current);
            
            
        }

        public void SaveReference(IUnderGroup current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Store(current);
        }

        public void SaveReference(IRegularGroup other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            IList<IUnderGroup> itemCollection = other.UnderGroups;
            DatabaseContext.Store(itemCollection);
           

            DatabaseContext.Store(other);
        }
    }

    public class UnderGroupRepositoryEF : PriceSystemGroupRepositoryEF<IUnderGroup, UnderGroup>, IUnderGroupRepository, IRelatedRepository<IUnderGroup, IRegularGroup>
    {
        public UnderGroupRepositoryEF(IEntityFrameworkDbContext<UnderGroup> databaseContext) : base(databaseContext)
        {
        }

        public IRegularGroup RegularGroup { get; set; }

        /// <summary>
        ///     Gets all the public properties non-readonly properties of the coded nomenclators that are relevant to the current
        ///     repository when making its operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.RegularGroupId)
                }).ToArray();
            }
        }
        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<IUnderGroup> Entities
        {
            get
            {


                var specification = new UnderGroupOfQueryable(RegularGroup, DatabaseContext);

                return WhereSQL(specification);
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override IUnderGroup Add(IUnderGroup budgetComponenIOverGroup)
        {
            this.Relate(budgetComponenIOverGroup, RegularGroup);
            IUnderGroup addedBudgetComponenIOverGroup = base.Add(budgetComponenIOverGroup);

           

            return budgetComponenIOverGroup;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Update(IUnderGroup budgetComponenIOverGroup)
        {
            this.Relate(budgetComponenIOverGroup, RegularGroup);
            base.Update(budgetComponenIOverGroup);

            

        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Delete(IUnderGroup budgetComponenIOverGroup)
        {
            if (budgetComponenIOverGroup == null)
                throw new ArgumentNullException("ICashMovementCategory");

            // this.Unrelate(budgetComponenIOverGroup, Component, DatabaseContext);

            //var dbbudgetComponenIOverGroupt = DatabaseContext.Find<IUnderGroup>(budgetComponenIOverGroup.Id);
          //  if (dbbudgetComponenIOverGroupt != null)
           // {
                var variantRepo = ServiceLocator.Current.GetInstance<IUnderGroupActivityRepository>();
                variantRepo.UnderGroup = budgetComponenIOverGroup;
                variantRepo.DeleteAll();
               // var variantRepo2 = ServiceLocator.Current.GetInstance<IUnderGroupResourceRepository>();
               // variantRepo2.UnderGroup = dbbudgetComponenIOverGroupt;
               // variantRepo2.DeleteAll();
          //  }

            base.Delete(budgetComponenIOverGroup);
        }

        public void Relate(IUnderGroup current, IRegularGroup other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.RegularGroup = other;
            current.RegularGroupId = other.Id;
            //  IList<TItem> itemCollection = other.SubCategories;
            //if (other.UnderGroups.All(x => !Equals(x.Id, current.Id)))
            //    other.UnderGroups.Add(current);
        }

        public void Unrelate(IUnderGroup current, IRegularGroup other)
        {

            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.RegularGroup = null;
            current.RegularGroupId = null;
            //  IList<TItem> itemCollection = other.SubCategories;
            //if (other.UnderGroups.All(x => !Equals(x.Id, current.Id)))
            //other.UnderGroups.Remove(current);


        }

        public void SaveReference(IUnderGroup current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Save();
        }

        public void SaveReference(IRegularGroup other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

          //  IList<IUnderGroup> itemCollection = other.UnderGroups;
            DatabaseContext.Save();


          //  DatabaseContext.Save();
        }
    }
}
