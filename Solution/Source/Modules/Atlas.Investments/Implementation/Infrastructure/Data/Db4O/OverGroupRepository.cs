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
    public class OverGroupRepository : PriceSystemGroupRepository<IOverGroup>, IOverGroupRepository, IRelatedRepository<IOverGroup, IPriceSystem>
    {
        public OverGroupRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }


        public IPriceSystem PriceSystem { get; set; }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<IOverGroup> Entities
        {
            get
            {


                ISpecification<IOverGroup> specification = new OverGroupOfSpecification(PriceSystem);

                return Where(specification);
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override IOverGroup Add(IOverGroup budgetComponenIOverGroup)
        {
            IOverGroup addedBudgetComponenIOverGroup = base.Add(budgetComponenIOverGroup);

            this.Relate(budgetComponenIOverGroup, PriceSystem, DatabaseContext);

            return addedBudgetComponenIOverGroup;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Update(IOverGroup budgetComponenIOverGroup)
        {
            base.Update(budgetComponenIOverGroup);

            this.Relate(budgetComponenIOverGroup, PriceSystem, DatabaseContext);

        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Delete(IOverGroup budgetComponenIOverGroup)
        {
            if (budgetComponenIOverGroup == null)
                throw new ArgumentNullException("ICashMovementCategory");

            // this.Unrelate(budgetComponenIOverGroup, Component, DatabaseContext);

            var dbbudgetComponenIOverGroupt = DatabaseContext.Find<IOverGroup>(budgetComponenIOverGroup.Id);
           
            //TODO do it right for every repository
            if (dbbudgetComponenIOverGroupt != null)
            {
                var regularRepo = ServiceLocator.Current.GetInstance<IRegularGroupRepository>();
                regularRepo.OverGroup = dbbudgetComponenIOverGroupt;
                regularRepo.DeleteAll();
               
               

            }

            base.Delete(budgetComponenIOverGroup);
        }

        public void Relate(IOverGroup current, IPriceSystem other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponenIOverGroup");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.AbovePriceSystem = other;
            //  IList<IOverGroup> itemCollection = other.SubCategories;
            //if (other.OverGroups.All(x => !Equals(x.Id, current.Id)))
            //    other.OverGroups.Add(current);
        }

        public void Unrelate(IOverGroup current, IPriceSystem other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponenIOverGroup");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.AbovePriceSystem = null;
            //  IList<IOverGroup> itemCollection = other.SubCategories;
            //if (other.OverGroups.All(x => !Equals(x.Id, current.Id)))
            //    other.OverGroups.Remove(current);
        }

        public void SaveReference(IOverGroup current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Store(current);
        }

        public void SaveReference(IPriceSystem other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            IList<IOverGroup> itemCollection = other.OverGroups;
            DatabaseContext.Store(itemCollection);
           
        }
    }

    public class OverGroupRepositoryEF : PriceSystemGroupRepositoryEF<IOverGroup, OverGroup>, IOverGroupRepository, IRelatedRepository<IOverGroup, IPriceSystem>
    {
        public OverGroupRepositoryEF(IEntityFrameworkDbContext<OverGroup> databaseContext) : base(databaseContext)
        {
        }


        public IPriceSystem PriceSystem { get; set; }

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
                    GetName(x => x.AbovePriceSystemId)
                }).ToArray();
            }
        }
        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<IOverGroup> Entities
        {
            get
            {


                var specification = new OverGroupOfQueryable(PriceSystem, DatabaseContext);

                return WhereSQL(specification);
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override IOverGroup Add(IOverGroup budgetComponenIOverGroup)
        {
            this.Relate(budgetComponenIOverGroup, PriceSystem);
            IOverGroup addedBudgetComponenIOverGroup = base.Add(budgetComponenIOverGroup);

           

            return budgetComponenIOverGroup;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Update(IOverGroup budgetComponenIOverGroup)
        {
            this.Relate(budgetComponenIOverGroup, PriceSystem);
            base.Update(budgetComponenIOverGroup);

           

        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Delete(IOverGroup budgetComponenIOverGroup)
        {
            if (budgetComponenIOverGroup == null)
                throw new ArgumentNullException("ICashMovementCategory");

            // this.Unrelate(budgetComponenIOverGroup, Component, DatabaseContext);

         //   var dbbudgetComponenIOverGroupt = DatabaseContext.Find<IOverGroup>(budgetComponenIOverGroup.Id);

            //TODO do it right for every repository
            //if (budgetComponenIOverGroup != null)
            //{
                var regularRepo = ServiceLocator.Current.GetInstance<IRegularGroupRepository>();
                regularRepo.OverGroup = budgetComponenIOverGroup;
                regularRepo.DeleteAll();



            //}

            base.Delete(budgetComponenIOverGroup);
        }

        public void Relate(IOverGroup current, IPriceSystem other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponenIOverGroup");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.AbovePriceSystem = other;
            current.AbovePriceSystemId = other.Id;
            //  IList<IOverGroup> itemCollection = other.SubCategories;
            //if (other.OverGroups.All(x => !Equals(x.Id, current.Id)))
            //    other.OverGroups.Add(current);
        }

        public void Unrelate(IOverGroup current, IPriceSystem other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponenIOverGroup");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.AbovePriceSystem = null;
            current.AbovePriceSystemId = null;
            //  IList<IOverGroup> itemCollection = other.SubCategories;
            //if (other.OverGroups.All(x => !Equals(x.Id, current.Id)))
            //    other.OverGroups.Remove(current);
        }

        public void SaveReference(IOverGroup current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Save();
        }

        public void SaveReference(IPriceSystem other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            IList<IOverGroup> itemCollection = other.OverGroups;
            DatabaseContext.Save();

        }
    }

}
