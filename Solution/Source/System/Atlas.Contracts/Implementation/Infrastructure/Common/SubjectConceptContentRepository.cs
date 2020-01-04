using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    public  class SubjectConceptContentRepository<TEntity>: Db4ORepositoryBase<TEntity>, IRelatedRepository<TEntity, ISubjectConcept>, ISubjectConceptContentRepository<TEntity>
         where TEntity : class, ISubjectConceptContent
    {
        public SubjectConceptContentRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public ISubjectConcept SubjectConcept { get; set; }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                     GetName(x => x.Content), GetName(x => x.Source), GetName(x => x.Author),GetName(x => x.LastUpdate), GetName(x => x.WebSite)
                }).ToArray();
            }
        }
        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<TEntity> Entities
        {
            get
            {

                ISpecification<TEntity> specification = new SubjectConceptContentOfSpecification<TEntity>(SubjectConcept);

                return Where(specification);
            }
        }
        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override TEntity Add(TEntity budgetComponentItem)
        {
            TEntity addedBudgetComponentITem = base.Add(budgetComponentItem);

            this.Relate(budgetComponentItem, SubjectConcept, DatabaseContext);
            //if (budgetComponentItem.ConversionUnit!=null)
            //this.Relate(budgetComponentItem, budgetComponentItem.ConversionUnit, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(TEntity budgetComponentItem)
        {
            base.Update(budgetComponentItem);

            this.Relate(budgetComponentItem, SubjectConcept, DatabaseContext);

        }


        public void Relate(TEntity current, ISubjectConcept other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.SubjectConcept = other;
        }

        public void Unrelate(TEntity current, ISubjectConcept other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.SubjectConcept = null;
        }

        public void SaveReference(TEntity current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Store(current);
        }

        public void SaveReference(ISubjectConcept other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            DatabaseContext.Store(other);
        }
    }


    public class SubjectConceptContentRepositoryEF<TEntity, TClass> : EntityFrameworkRepositoryBase<TEntity,TClass>, IRelatedRepository<TEntity, ISubjectConcept>, ISubjectConceptContentRepository<TEntity>
        where TEntity : class, ISubjectConceptContent
        where TClass: SubjectConceptContent
    {
        public SubjectConceptContentRepositoryEF(IEntityFrameworkDbContext<TClass> databaseContext) : base(databaseContext)
        {
        }

        public ISubjectConcept SubjectConcept { get; set; }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                     GetName(x => x.Content), GetName(x => x.Source), GetName(x => x.Author),GetName(x => x.LastUpdate), GetName(x => x.WebSite), GetName(x => x.SubjectConceptId)
                }).ToArray();
            }
        }
        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<TEntity> Entities
        {
            get
            {

                var queryable = new SubjectConceptContentOfQueryable<TClass>(SubjectConcept,DatabaseContext);

                return Where(queryable);
            }
        }
        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override TEntity Add(TEntity budgetComponentItem)
        {
            this.Relate(budgetComponentItem, SubjectConcept);

            TEntity addedBudgetComponentITem = base.Add(budgetComponentItem);

           // this.Relate(budgetComponentItem, SubjectConcept, DatabaseContext);

            //if (budgetComponentItem.ConversionUnit!=null)
            //this.Relate(budgetComponentItem, budgetComponentItem.ConversionUnit, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(TEntity budgetComponentItem)
        {
            this.Relate(budgetComponentItem, SubjectConcept);
            base.Update(budgetComponentItem);

          

        }


        public void Relate(TEntity current, ISubjectConcept other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.SubjectConcept = other;
            current.SubjectConceptId = other.Id;
        }

        public void Unrelate(TEntity current, ISubjectConcept other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.SubjectConcept = null;
            current.SubjectConceptId = null;
        }

        public void SaveReference(TEntity current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Save();
        }

        public void SaveReference(ISubjectConcept other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            DatabaseContext.Save();
        }
    }
}
