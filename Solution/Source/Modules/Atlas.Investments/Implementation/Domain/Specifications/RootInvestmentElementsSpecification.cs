using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Specifications
{
    /// <summary>
    /// This is the specification containing the predicate to obtain all those independent investment elements, those having no parent.
    /// </summary>
    public class RootInvestmentElementsSpecification : Specification<IInvestmentElement>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RootInvestmentElementsSpecification"/>.
        /// </summary>
        public RootInvestmentElementsSpecification()
            : base(null)
        {
        }
    }
}
