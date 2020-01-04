using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Specifications
{
    public class SubjectConceptContentOfSpecification<TEntity> : Specification<TEntity>
         where TEntity : class, ISubjectConceptContent
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public SubjectConceptContentOfSpecification(ISubjectConcept SubjectConcept)
        {
            if (SubjectConcept == null)
                throw new ArgumentNullException("SubjectConcept");

            Predicate = acces => acces.SubjectConcept != null && Equals(acces.SubjectConcept.Id, SubjectConcept.Id);
        }
    }

    public class SubjectConceptContentOfQueryable<TClass> :  EntityFrameworkQueryable<TClass>
         where TClass : SubjectConceptContent
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public SubjectConceptContentOfQueryable(ISubjectConcept SubjectConcept, IEntityFrameworkDbContext<TClass> context) : base(context)
        {
            if (SubjectConcept == null)
                throw new ArgumentNullException("SubjectConcept");

            Query = (from e in context.Entities orderby e.Id ascending where e.SubjectConceptId == SubjectConcept.Id select e);
        }
    }
}
