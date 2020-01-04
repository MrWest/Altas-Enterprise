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

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    public class AtlasModuleRoleRepository : Db4ORepositoryBase<IAtlasModuleRole>, IRelatedRepository<IAtlasModuleRole, IAtlasGenericModuleAccess>, IAtlasModuleRoleRepository
    {
        public AtlasModuleRoleRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.AllowedEntity), GetName(x => x.ModulePermission)
                }).ToArray();
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override IAtlasModuleRole Add(IAtlasModuleRole budgetComponentItem)
        {
            IAtlasModuleRole addedBudgetComponentITem = base.Add(budgetComponentItem);

            this.Relate(budgetComponentItem, ModuleAccess, DatabaseContext);
            //if (budgetComponentItem.ConversionUnit!=null)
            //this.Relate(budgetComponentItem, budgetComponentItem.ConversionUnit, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(IAtlasModuleRole budgetComponentItem)
        {
            base.Update(budgetComponentItem);

            this.Relate(budgetComponentItem, ModuleAccess, DatabaseContext);

        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<IAtlasModuleRole> Entities
        {
            get
            {

                ISpecification<IAtlasModuleRole> specification = new AtlasModuleRoleOfSpecification(ModuleAccess);

                return Where(specification);
            }
        }
        public void Relate(IAtlasModuleRole current, IAtlasGenericModuleAccess other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.ModuleAccess = other;
            //  IList<TItem> itemCollection = other.SubCategories;
            if (other.Rols.All(x => !Equals(x.Id, current.Id)))
                other.Rols.Add(current);
        }

        public void Unrelate(IAtlasModuleRole current, IAtlasGenericModuleAccess other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.ModuleAccess = null;
            //  IList<TItem> itemCollection = other.SubCategories;
            if (other.Rols.All(x => !Equals(x.Id, current.Id)))
                other.Rols.Remove(current);
        }

        public void SaveReference(IAtlasModuleRole current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Store(current);
        }

        public void SaveReference(IAtlasGenericModuleAccess other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            IList<IAtlasModuleRole> itemCollection = other.Rols;
            DatabaseContext.Store(itemCollection);

            

            DatabaseContext.Store(other);
        }

        public IAtlasGenericModuleAccess ModuleAccess { get; set; }
    }
}
