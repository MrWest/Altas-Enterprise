using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    public class AtlasModuleAccessRepository: AtlasGenericModuleAccessRepository<IAtlasModuleAccess>,IRelatedRepository<IAtlasModuleAccess, IAtlasGenericModuleAccess>, IAtlasModuleAccessRepository
    {
        public AtlasModuleAccessRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

       
        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override IAtlasModuleAccess Add(IAtlasModuleAccess budgetComponentItem)
        {
            IAtlasModuleAccess addedBudgetComponentITem = base.Add(budgetComponentItem);

            this.Relate(budgetComponentItem, OwnerModuleAccess, DatabaseContext);
            //if (budgetComponentItem.ConversionUnit!=null)
            //this.Relate(budgetComponentItem, budgetComponentItem.ConversionUnit, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(IAtlasModuleAccess budgetComponentItem)
        {
            base.Update(budgetComponentItem);

            this.Relate(budgetComponentItem, OwnerModuleAccess, DatabaseContext);
         
        }

        public override void Delete(IAtlasModuleAccess entity)
        {
            var repo = ServiceLocator.Current.GetInstance<IAtlasModuleAccessRepository>();
            repo.OwnerModuleAccess = entity;
            repo.DeleteAll();
            base.Delete(entity);
        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<IAtlasModuleAccess> Entities
        {
            get
            {

                ISpecification<IAtlasModuleAccess> specification = new AtlasModuleAccessOfSpecifcation(OwnerModuleAccess);

                return Where(specification);
            }
        }

        public void Relate(IAtlasModuleAccess current, IAtlasGenericModuleAccess other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.OwnerModuleAccess = other;
            //  IList<TItem> itemCollection = other.SubCategories;
            if (other.OwnedAccesses.All(x => !Equals(x.Id, current.Id)))
                other.OwnedAccesses.Add(current);

        }

        public void Unrelate(IAtlasModuleAccess current, IAtlasGenericModuleAccess other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.OwnerModuleAccess = null;
            //  IList<TItem> itemCollection = other.SubCategories;
            if (other.OwnedAccesses.All(x => !Equals(x.Id, current.Id)))
                other.OwnedAccesses.Remove(current);
        }

        public void SaveReference(IAtlasModuleAccess current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Store(current);
        }

        void IRelatedRepository<IAtlasModuleAccess, IAtlasGenericModuleAccess>.SaveReference(IAtlasGenericModuleAccess other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            IList<IAtlasModuleAccess> itemCollection = other.OwnedAccesses;
            DatabaseContext.Store(itemCollection);


            DatabaseContext.Store(other);
        }

        public IAtlasGenericModuleAccess OwnerModuleAccess { get; set; }
    }
}
