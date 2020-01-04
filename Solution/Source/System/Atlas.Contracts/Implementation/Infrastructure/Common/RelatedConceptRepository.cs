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
    public class RelatedConceptRepository: Db4ORepositoryBase<IRelatedConcept>,IRelatedRepository<IRelatedConcept,ISubjectConcept>, IRelatedConceptRepository
    {
        public RelatedConceptRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                     GetName(x => x.SubjectConcept)
                }).ToArray();
            }
        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<IRelatedConcept> Entities
        {
            get
            {

                ISpecification<IRelatedConcept> specification = new
                RelatedConceptOfSpecification(OwnerSubjectConcept);
                
                return Where(specification);
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override IRelatedConcept Add(IRelatedConcept budgetComponentItem)
        {
            IRelatedConcept addedBudgetComponentITem = base.Add(budgetComponentItem);

            this.Relate(budgetComponentItem, OwnerSubjectConcept, DatabaseContext);
            //if (budgetComponentItem.ConversionUnit!=null)
            //this.Relate(budgetComponentItem, budgetComponentItem.ConversionUnit, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(IRelatedConcept budgetComponentItem)
        {
            base.Update(budgetComponentItem);

            this.Relate(budgetComponentItem, OwnerSubjectConcept, DatabaseContext);

        }
        public void Relate(IRelatedConcept current, ISubjectConcept other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.OwnerSubjectConcept = other;
        }

        public void Unrelate(IRelatedConcept current, ISubjectConcept other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.OwnerSubjectConcept = null;
        }

        public void SaveReference(IRelatedConcept current)
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

        public ISubjectConcept OwnerSubjectConcept { get; set; }
    }



    public class RelatedConceptRepositoryEF : EntityFrameworkRepositoryBase<IRelatedConcept, RelatedConcept>, IRelatedRepository<IRelatedConcept, ISubjectConcept>, IRelatedConceptRepository
    {
        public RelatedConceptRepositoryEF(IEntityFrameworkDbContext<RelatedConcept> databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                     GetName(x => x.SubjectConcept), GetName(x => x.OwnerSubjectConceptId)
                }).ToArray();
            }
        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<IRelatedConcept> Entities
        {
            get
            {

               var queryable = new
                RelatedConceptOfQueryable(OwnerSubjectConcept, DatabaseContext);

                return Where(queryable);
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override IRelatedConcept Add(IRelatedConcept budgetComponentItem)
        {
            Relate(budgetComponentItem, OwnerSubjectConcept);
            IRelatedConcept addedBudgetComponentITem = base.Add(budgetComponentItem);

           
            //if (budgetComponentItem.ConversionUnit!=null)
            //this.Relate(budgetComponentItem, budgetComponentItem.ConversionUnit, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(IRelatedConcept budgetComponentItem)
        {
            this.Relate(budgetComponentItem, OwnerSubjectConcept);
            base.Update(budgetComponentItem);

            

        }
        public void Relate(IRelatedConcept current, ISubjectConcept other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.OwnerSubjectConcept = other;
            current.OwnerSubjectConceptId = other.Id;
        }

        public void Unrelate(IRelatedConcept current, ISubjectConcept other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.OwnerSubjectConcept = null;
            current.OwnerSubjectConceptId = null;
        }

        public void SaveReference(IRelatedConcept current)
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

        public ISubjectConcept OwnerSubjectConcept { get; set; }
    }
}
