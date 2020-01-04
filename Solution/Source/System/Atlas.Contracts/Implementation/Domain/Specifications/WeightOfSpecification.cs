using System;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Specifications
{
    public class MeasurableOfSpecification<TMeasurable> : Specification<TMeasurable>
    where TMeasurable : class ,IMeasurableUnit
    {
          /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public MeasurableOfSpecification(IEntity holder)
        {
            if (holder == null)
                throw new ArgumentNullException("holder");

           // Predicate = weight => weight.Holder != null && Equals(weight.Holder.Id, holder.Id);
        }
    }
}
