using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget
{
    public class UnderGroupItemRepository<TItem>: Db4ORepositoryBase<TItem>, IUnderGroupItemRepository<TItem>,IRelatedRepository<TItem,IUnderGroup>
        where TItem : class, IUnderGroupItem
    {
        public UnderGroupItemRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public IUnderGroup UnderGroup { get; set; }

        /// <summary>
        /// Gets the properties which changes must be saved.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Code), GetName(x => x.Name), GetName(x => x.Quantity), GetName(x => x.Description),
                    GetName(x => x.UnitaryCost),GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.SubExpenseConcept),GetName(x=>x.PriceSystem),  GetName(x => x.isUnitaryPriceCalculated),  GetName(x => x.CalculatedUnitaryPrice)
                }).ToArray();
                //,GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.ExpenseConcept)
            }
        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<TItem> Entities
        {
            get
            {
                ISpecification<TItem> specification = new UnderGroupItemOfSpecification<TItem>(UnderGroup);

                return Where(specification);
            }
        }

        public override TItem Add(TItem entity)
        {
           TItem copy = base.Add(entity);
           

            this.Relate(entity, UnderGroup, DatabaseContext);

            return copy;

        }

        public override void Update(TItem entity)
        {
            base.Update(entity);
            this.Relate(entity, UnderGroup, DatabaseContext);
        }

        public override void Delete(TItem entity)
        {

            this.Unrelate(entity, UnderGroup, DatabaseContext);
            base.Delete(entity);

        }

        public void Relate(TItem current, IUnderGroup other)
        {
           
            if (current == null)
                throw new ArgumentNullException("current");
            if (other == null)
                throw new ArgumentNullException("other");

            current.UnderGroup = other;
           
        }

        public void Unrelate(TItem current, IUnderGroup other)
        {

            if (current == null)
                throw new ArgumentNullException("current");
            if (other == null)
                throw new ArgumentNullException("other");

            current.UnderGroup = null;
        }

        public void SaveReference(TItem current)
        {
            if (current == null)
                throw new ArgumentNullException("current");

            DatabaseContext.Store(current);
        }

        public void SaveReference(IUnderGroup other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            DatabaseContext.Store(other);
        }

        public IEnumerable<TItem> FilterByName(string nameSpecification)
        {
            throw new NotImplementedException();
        }
    }

    public class UnderGroupItemRepositoryEF<TItem, TClass> : EntityFrameworkRepositoryBase<TItem, TClass>, IUnderGroupItemRepository<TItem>, IRelatedRepository<TItem, IUnderGroup>
        where TItem : class, IUnderGroupItem
        where TClass : BudgetComponentItemBase, IUnderGroupItem

    {
        public UnderGroupItemRepositoryEF(IEntityFrameworkDbContext<TClass> databaseContext) : base(databaseContext)
        {
        }

        public IUnderGroup UnderGroup { get; set; }

        /// <summary>
        /// Gets the properties which changes must be saved.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Code), GetName(x => x.Name), GetName(x => x.Quantity), GetName(x => x.Description),
                    GetName(x => x.UnitaryCost),GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.SubExpenseConcept),GetName(x=>x.PriceSystem),
                    GetName(x => x.isUnitaryPriceCalculated),  GetName(x => x.CalculatedUnitaryPrice),  GetName(x => x.UnderGroupId)
                }).ToArray();
                //,GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.ExpenseConcept)
            }
        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<TItem> Entities
        {
            get
            {
                var specification = new UnderGroupItemOfQueryable<TClass>(UnderGroup, DatabaseContext);

                return WhereSQL(specification);
            }
        }

        public override TItem Add(TItem entity)
        {
            this.Relate(entity, UnderGroup);
            TItem copy = base.Add(entity);


           

            return copy;

        }

        public override void Update(TItem entity)
        {
            this.Relate(entity, UnderGroup);
            base.Update(entity);
           
        }

        public override void Delete(TItem entity)
        {

            var resourceRepo = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<TItem>>();
            resourceRepo.Component = entity;
            resourceRepo.DeleteAll();

            this.Unrelate(entity, UnderGroup);
            base.Delete(entity);

        }

        public void Relate(TItem current, IUnderGroup other)
        {

            if (current == null)
                throw new ArgumentNullException("current");
            if (other == null)
                throw new ArgumentNullException("other");

            current.UnderGroup = other;
            current.UnderGroupId = other.Id;

        }

        public void Unrelate(TItem current, IUnderGroup other)
        {

            if (current == null)
                throw new ArgumentNullException("current");
            if (other == null)
                throw new ArgumentNullException("other");

            current.UnderGroup = null;
            current.UnderGroupId = null;
        }

        public void SaveReference(TItem current)
        {
            if (current == null)
                throw new ArgumentNullException("current");

            DatabaseContext.Save();
        }

        public void SaveReference(IUnderGroup other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            DatabaseContext.Save();
        }

        public IEnumerable<TItem> FilterByName(string nameSpecification)
        {
            throw new NotImplementedException();
        }
    }
}