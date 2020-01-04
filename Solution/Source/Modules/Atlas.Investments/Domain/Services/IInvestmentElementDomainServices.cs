using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services
{
    /// <summary>
    /// Provides services allowing to ensure that the business rules defined for investment element entities are 
    /// always applied correctly.
    /// </summary>
    public interface IInvestmentElementDomainServices : IDomainServices<IInvestmentElement>
    {
        /// <summary>
        /// Gets or sets the <see cref="IInvestmentElement"/> being the parent of those handled in the current
        /// <see cref="IInvestmentElementDomainServices"/>.
        /// </summary>
        IInvestmentElement Parent { get; set; }
    }
}
