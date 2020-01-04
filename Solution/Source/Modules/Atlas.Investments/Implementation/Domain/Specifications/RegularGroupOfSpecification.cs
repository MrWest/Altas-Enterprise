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

namespace CompanyName.Atlas.Investments.Implementation.Domain.Specifications
{
    public class RegularGroupOfSpecification : Specification<IRegularGroup>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public RegularGroupOfSpecification(IOverGroup overGroup)
        {
            if (overGroup == null)
                throw new ArgumentNullException("aboveCategory");

            Predicate = regular => regular.OverGroup != null && Equals(regular.OverGroup.Id, overGroup.Id);
        }
    }

    public class RegularGroupOfQueryable : EntityFrameworkQueryable<RegularGroup>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public RegularGroupOfQueryable(IOverGroup overGroup, IEntityFrameworkDbContext<RegularGroup> context) : base(context)
        {
            if (overGroup == null)
                throw new ArgumentNullException("overGroup");

            Query = (from e in context.Entities orderby e.Id ascending where e.OverGroupId == overGroup.Id select e);
            Parameter = overGroup.Id;
        }
    }
}
