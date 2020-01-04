using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Specifications
{
    /// <summary>
    ///     This is the specification containing the predicate to obtain all those investment elements being child of a certain
    ///     one.
    /// </summary>
    public class SectionOfSpecification : Specification<ISection>
    {
         /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public SectionOfSpecification(IPriceSystem aboveSection)
        {
            if (aboveSection == null)
                throw new ArgumentNullException("aboveSection");

            Predicate = section => section.AboveSection != null && Equals(section.AboveSection.Id, aboveSection.Id);
        }
    }
}
