using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Domain.Specification.EntityFramework;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    /// <summary>
    /// Implements fuctionalities for a <see cref="IUnitConverter"/>
    /// </summary>
    public class UnitConverterRepository : Db4ORepositoryBase<IUnitConverter>, IUnitConverterRepository
    {

        private IConvertibleEntity _conversionForEntity;

        public IConvertibleEntity ConversionForEntity
        {
            get { return _conversionForEntity; }
            set { _conversionForEntity = value; }
        }
         public UnitConverterRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
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
                    GetName(x => x.Factor),GetName(x => x.ConversionUnit)
                }).ToArray();
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override IUnitConverter Add(IUnitConverter budgetComponentItem)
        {
            IUnitConverter addedBudgetComponentITem = base.Add(budgetComponentItem);

            this.Relate(budgetComponentItem, ConversionForEntity, DatabaseContext);
            //if (budgetComponentItem.ConversionUnit!=null)
            //this.Relate(budgetComponentItem, budgetComponentItem.ConversionUnit, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(IUnitConverter budgetComponentItem)
        {
            base.Update(budgetComponentItem);

            this.Relate(budgetComponentItem, ConversionForEntity, DatabaseContext);
            //if (budgetComponentItem.ConversionUnit != null)
            //this.Relate(budgetComponentItem, budgetComponentItem.ConversionUnit, DatabaseContext);

        }

       
        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<IUnitConverter> Entities
        {
            get
            {
                
                ISpecification<IUnitConverter> specification = new UnitConverterOfSpecification(ConversionForEntity);

                return Where(specification);
            }
        }

        public void Relate(IUnitConverter current, IConvertibleEntity other)
        {
            if (current == null)
                throw new ArgumentNullException("IUnitConverter");
            if (other == null)
                throw new ArgumentNullException("IConvertibleEntity");

            if (current.ConversionForEntity != null && current.ConversionForEntity.Id != other.Id)
                current.ConversionUnit = other.Id;
            else
            {
                current.ConversionForEntity = other;
                //  IList<TItem> itemCollection = other.SubCategories;
                if (other.Convertions.All(x => !Equals(x.Id, current.Id)))
                    other.Convertions.Add(current);
            }

        }

        public void Unrelate(IUnitConverter current, IConvertibleEntity other)
        {
            if (current == null)
                throw new ArgumentNullException("IUnitConverter");
            if (other == null)
                throw new ArgumentNullException("IConvertibleEntity");

            if (current.ConversionForEntity != null && current.ConversionForEntity.Id != other.Id)
                current.ConversionUnit = null;
            else
            {
                current.ConversionForEntity = null;
                
            }
        }

        public void SaveReference(IUnitConverter current)
        {
            if (current == null)
                throw new ArgumentNullException("IUnitConverter");

            DatabaseContext.Store(current);
        }

        public void SaveReference(IConvertibleEntity other)
        {
            if (other == null)
                throw new ArgumentNullException("IConvertibleEntity");

            DatabaseContext.Store(other);
        }
    }



    /// <summary>
    /// Implements fuctionalities for a <see cref="IUnitConverter"/>
    /// </summary>
    public class UnitConverterRepositoryEF : EntityFrameworkRepositoryBase<IUnitConverter, UnitConverter>, IUnitConverterRepository
    {

        private IConvertibleEntity _conversionForEntity;

        public IConvertibleEntity ConversionForEntity
        {
            get { return _conversionForEntity; }
            set { _conversionForEntity = value; }
        }
        public UnitConverterRepositoryEF(IEntityFrameworkDbContext<UnitConverter> databaseContext) : base(databaseContext)
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
                    GetName(x => x.Factor),GetName(x => x.ConversionUnit),GetName(x => x.ConversionForEntityId)
                }).ToArray();
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override IUnitConverter Add(IUnitConverter budgetComponentItem)
        {
            this.Relate(budgetComponentItem, ConversionForEntity);
            IUnitConverter addedBudgetComponentITem = base.Add(budgetComponentItem);

            
            //if (budgetComponentItem.ConversionUnit!=null)
            //this.Relate(budgetComponentItem, budgetComponentItem.ConversionUnit, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(IUnitConverter budgetComponentItem)
        {
            this.Relate(budgetComponentItem, ConversionForEntity);
            base.Update(budgetComponentItem);

           
            //if (budgetComponentItem.ConversionUnit != null)
            //this.Relate(budgetComponentItem, budgetComponentItem.ConversionUnit, DatabaseContext);

        }


        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<IUnitConverter> Entities
        {
            get
            {

                IEntityFrameworkQuerable<UnitConverter> queryable = new UnitConverterOfEntityFrameworkQueryable(ConversionForEntity, DatabaseContext);

                return Where(queryable);
            }
        }

        public void Relate(IUnitConverter current, IConvertibleEntity other)
        {
            if (current == null)
                throw new ArgumentNullException("IUnitConverter");
            if (other == null)
                throw new ArgumentNullException("IConvertibleEntity");

            current.ConversionForEntity = other;
            current.ConversionForEntityId = other.Id;
        
        }

        public void Unrelate(IUnitConverter current, IConvertibleEntity other)
        {
            if (current == null)
                throw new ArgumentNullException("IUnitConverter");
            if (other == null)
                throw new ArgumentNullException("IConvertibleEntity");

            current.ConversionForEntity = null;
            current.ConversionForEntityId = null;

        }

        public void SaveReference(IUnitConverter current)
        {
            if (current == null)
                throw new ArgumentNullException("IUnitConverter");

            DatabaseContext.Save();
        }

        public void SaveReference(IConvertibleEntity other)
        {
            if (other == null)
                throw new ArgumentNullException("IConvertibleEntity");

            DatabaseContext.Save();
        }
    }

}
