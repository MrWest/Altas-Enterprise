using System;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Specifications
{
   /// <summary>
    ///     This is the specification containing the predicate to obtain all those investment elements being child of a certain
    ///     one.
    /// </summary>
    public class PeriodOfSpecification : Specification<IPeriod>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public PeriodOfSpecification(IEntity holder)
        {
            if (holder == null)
                throw new ArgumentNullException("holder");

            Predicate = period => period.Holder != null && Equals(period.Holder.Id, holder.Id);
        }
    }

    /// <summary>
    ///     This is the specification containing the predicate to obtain all those investment elements being child of a certain
    ///     one.
    /// </summary>
    public class PeriodOfQueryable : EntityFrameworkQueryable<Period>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public PeriodOfQueryable(IEntity holder, IEntityFrameworkDbContext<Period> context ): base(context)
        {
            if (holder == null)
                throw new ArgumentNullException("holder");

            Query = from period in context.Entities where period.HolderId == holder.Id select   period;
                //period => period.Holder != null && Equals(period.Holder.Id, holder.Id);
        }
    }

}
