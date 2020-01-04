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
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    public class RegularGroupRepository : PriceSystemGroupRepository<IRegularGroup>, IRegularGroupRepository,IRelatedRepository<IRegularGroup,IOverGroup>
    {
        public RegularGroupRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public IOverGroup OverGroup { get; set; }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<IRegularGroup> Entities
        {
            get
            {


                ISpecification<IRegularGroup> specification = new RegularGroupOfSpecification(OverGroup);

                return Where(specification);
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override IRegularGroup Add(IRegularGroup budgetComponenIOverGroup)
        {
            IRegularGroup addedBudgetComponenIOverGroup = base.Add(budgetComponenIOverGroup);

            this.Relate(budgetComponenIOverGroup, OverGroup, DatabaseContext);

            return addedBudgetComponenIOverGroup;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Update(IRegularGroup budgetComponenIOverGroup)
        {
            base.Update(budgetComponenIOverGroup);

            this.Relate(budgetComponenIOverGroup, OverGroup, DatabaseContext);

        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Delete(IRegularGroup budgetComponenIOverGroup)
        {
            if (budgetComponenIOverGroup == null)
                throw new ArgumentNullException("ICashMovementCategory");

            // this.Unrelate(budgetComponenIOverGroup, Component, DatabaseContext);

            var dbbudgetComponenIOverGroupt = DatabaseContext.Find<IRegularGroup>(budgetComponenIOverGroup.Id);
            if (dbbudgetComponenIOverGroupt != null)
            {
                var underRepo = ServiceLocator.Current.GetInstance<IUnderGroupRepository>();
                underRepo.RegularGroup = dbbudgetComponenIOverGroupt;
                underRepo.DeleteAll();

            }

            base.Delete(budgetComponenIOverGroup);
        }

        public void Relate(IRegularGroup current, IOverGroup other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.OverGroup = other;
            //  IList<TItem> itemCollection = other.SubCategories;
            //if (other.RegularGroups.All(x => !Equals(x.Id, current.Id)))
            //    other.RegularGroups.Add(current);
        }

        public void Unrelate(IRegularGroup current, IOverGroup other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.OverGroup = null;
            //  IList<TItem> itemCollection = other.SubCategories;
            //if (other.RegularGroups.All(x => !Equals(x.Id, current.Id)))
            //    other.RegularGroups.Remove(current);
        }

        public void SaveReference(IRegularGroup current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Store(current);
           
        }

        public void SaveReference(IOverGroup other)
        {
            
           
        }
    }

    public class RegularGroupRepositoryEF : PriceSystemGroupRepositoryEF<IRegularGroup, RegularGroup>, IRegularGroupRepository, IRelatedRepository<IRegularGroup, IOverGroup>
    {
        public RegularGroupRepositoryEF(IEntityFrameworkDbContext<RegularGroup> databaseContext) : base(databaseContext)
        {
        }

        public IOverGroup OverGroup { get; set; }


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
                    GetName(x => x.OverGroupId)
                }).ToArray();
            }
        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<IRegularGroup> Entities
        {
            get
            {


                var specification = new RegularGroupOfQueryable(OverGroup, DatabaseContext);

                return WhereSQL(specification);
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override IRegularGroup Add(IRegularGroup budgetComponenIOverGroup)
        {

            this.Relate(budgetComponenIOverGroup, OverGroup);
            IRegularGroup addedBudgetComponenIOverGroup = base.Add(budgetComponenIOverGroup);

            

            return budgetComponenIOverGroup;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Update(IRegularGroup budgetComponenIOverGroup)
        {

            this.Relate(budgetComponenIOverGroup, OverGroup);
            base.Update(budgetComponenIOverGroup);

            

        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Delete(IRegularGroup budgetComponenIOverGroup)
        {
            if (budgetComponenIOverGroup == null)
                throw new ArgumentNullException("ICashMovementCategory");

            // this.Unrelate(budgetComponenIOverGroup, Component, DatabaseContext);

            //var dbbudgetComponenIOverGroupt = DatabaseContext.Find<IRegularGroup>(budgetComponenIOverGroup.Id);
           // if (dbbudgetComponenIOverGroupt != null)
           // {
                var underRepo = ServiceLocator.Current.GetInstance<IUnderGroupRepository>();
                underRepo.RegularGroup = budgetComponenIOverGroup;
                underRepo.DeleteAll();

           // }

            base.Delete(budgetComponenIOverGroup);
        }

        public void Relate(IRegularGroup current, IOverGroup other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.OverGroup = other;
            current.OverGroupId = other.Id;
            //  IList<TItem> itemCollection = other.SubCategories;
            //if (other.RegularGroups.All(x => !Equals(x.Id, current.Id)))
            //    other.RegularGroups.Add(current);
        }

        public void Unrelate(IRegularGroup current, IOverGroup other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.OverGroup = null;
            current.OverGroupId = null;
            //  IList<TItem> itemCollection = other.SubCategories;
            //if (other.RegularGroups.All(x => !Equals(x.Id, current.Id)))
            //    other.RegularGroups.Remove(current);
        }

        public void SaveReference(IRegularGroup current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Save();

        }

        public void SaveReference(IOverGroup other)
        {


        }
    }
}
