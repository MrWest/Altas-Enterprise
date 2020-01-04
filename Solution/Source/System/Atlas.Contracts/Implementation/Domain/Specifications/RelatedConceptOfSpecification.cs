using System;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Specifications
{
    public class RelatedConceptOfSpecification : Specification<IRelatedConcept>
         
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public RelatedConceptOfSpecification(ISubjectConcept SubjectConcept)
        {
            if (SubjectConcept == null)
                throw new ArgumentNullException("SubjectConcept");

            Predicate = acces => acces.OwnerSubjectConcept != null && Equals(acces.OwnerSubjectConcept.Id, SubjectConcept.Id);
        }
    }

    public class RelatedConceptOfQueryable : EntityFrameworkQueryable<RelatedConcept>
    {
        //     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public RelatedConceptOfQueryable(ISubjectConcept SubjectConcept, IEntityFrameworkDbContext<RelatedConcept> context) : base(context)
        {
            if (SubjectConcept == null)
                throw new ArgumentNullException("SubjectConcept");

            Query = (from e in context.Entities orderby e.Id ascending where e.OwnerSubjectConceptId == SubjectConcept.Id select e);

            // Predicate = convertible => convertible.ConversionForEntity != null && Equals(convertible.ConversionForEntity.Id, convertibleEntity.Id);
        }
    }
}
