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
    public class OverGroupOfSpecification : Specification<IOverGroup>
    {
         /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public OverGroupOfSpecification(IPriceSystem priceSystem)
        {
            if (priceSystem == null)
                throw new ArgumentNullException("priceSystem");

            Predicate = over => over.AbovePriceSystem != null && Equals(over.AbovePriceSystem.Id, priceSystem.Id);
        }
    }

    public class OverGroupOfQueryable : EntityFrameworkQueryable<OverGroup>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public OverGroupOfQueryable(IPriceSystem priceSystem, IEntityFrameworkDbContext<OverGroup> context) : base(context)
        {
            if (priceSystem == null)
                throw new ArgumentNullException("priceSystem");

            Query = (from e in context.Entities orderby e.Id ascending where e.AbovePriceSystemId == priceSystem.Id select e);
            Parameter = priceSystem.Id;
        }
    }

   
}
