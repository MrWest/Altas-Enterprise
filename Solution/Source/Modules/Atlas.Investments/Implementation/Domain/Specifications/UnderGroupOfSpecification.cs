using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using Db4objects.Db4o.Query;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Specifications
{
    public class UnderGroupOfSpecification : Specification<IUnderGroup>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public UnderGroupOfSpecification(IRegularGroup regularGroup)
        {
            if (regularGroup == null)
                throw new ArgumentNullException("aboveCategory");

            Predicate = under => under.RegularGroup != null && Equals(under.RegularGroup.Id, regularGroup.Id);
        }
    }

    public class UnderGroupOfQueryable : EntityFrameworkQueryable<UnderGroup>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public UnderGroupOfQueryable(IRegularGroup regularGroup, IEntityFrameworkDbContext<UnderGroup> context) : base(context)
        {
            if (regularGroup == null)
                throw new ArgumentNullException("regularGroup");

            Query = (from e in context.Entities orderby e.Id ascending where e.RegularGroupId == regularGroup.Id select e);
            Parameter = regularGroup.Id;
        }
    }
}
