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
    class AtlasModuleSubjectOfSpecifcation: Specification<IAtlasModuleSubject>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public AtlasModuleSubjectOfSpecifcation(IAtlasModuleGenericSubject OwnerSubject)
        {
            if (OwnerSubject == null)
                throw new ArgumentNullException("OwnerSubject");

            Predicate = acces => acces.OwnerSubject != null && Equals(acces.OwnerSubject.Id, OwnerSubject.Id);
        }
    }

    class AtlasModuleSubjectOfQueryable : EntityFrameworkQueryable<AtlasModuleSubject>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public AtlasModuleSubjectOfQueryable(IAtlasModuleGenericSubject OwnerSubject, IEntityFrameworkDbContext<AtlasModuleSubject> context) : base(context)
        {
            if (OwnerSubject == null)
                throw new ArgumentNullException("OwnerSubject");

            Query = (from e in context.Entities orderby e.Id ascending where e.OwnerSubjectId == OwnerSubject.Id select e);
        }
    }
}
