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
    public class CashMovementOfSpecification : Specification<ICashMovement>
    {
          /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public CashMovementOfSpecification(ICashMovementCategory aboveCategory)
        {
            if (aboveCategory == null)
                throw new ArgumentNullException("aboveCategory");

            Predicate = section => section.CashMovementCategory != null && Equals(section.CashMovementCategory.Id.ToString(), aboveCategory.Id.ToString());
        }
    }
    public class CashMovementOfQueryable : EntityFrameworkQueryable<CashMovement>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public CashMovementOfQueryable(ICashMovementCategory aboveCategory, IEntityFrameworkDbContext<CashMovement> context ):base(context)
        {
            if (aboveCategory == null)
                throw new ArgumentNullException("aboveCategory");
            Query = from movement in context.Entities
                where movement.CashMovementCategoryId == aboveCategory.Id
                select movement;
          
        }
    }
}
