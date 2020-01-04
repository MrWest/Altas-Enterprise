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
    public class SubjectConceptOfSpecifcation : Specification<ISubjectConcept>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public SubjectConceptOfSpecifcation(IAtlasModuleGenericSubject genericSubject)
        {
            if (genericSubject == null)
                throw new ArgumentNullException("genericSubject");

            Predicate = acces => acces.ModuleSubject != null && Equals(acces.ModuleSubject.Id, genericSubject.Id);
        }
    }

    public class SubjectConceptOfQueryable : EntityFrameworkQueryable<SubjectConcept>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public SubjectConceptOfQueryable(IAtlasModuleGenericSubject genericSubject, IEntityFrameworkDbContext<SubjectConcept> context) : base(context)
        {
            if (genericSubject == null)
                throw new ArgumentNullException("genericSubject");

            Query = (from e in context.Entities orderby e.Id ascending where e.ModuleSubjectId == genericSubject.Id select e);
        }
    }
}
