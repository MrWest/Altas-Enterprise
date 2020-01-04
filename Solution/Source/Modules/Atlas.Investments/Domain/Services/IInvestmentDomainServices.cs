using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services
{
    /// <summary>
    ///     Contract of the domain services enforcing the business rules for the domain entities: investment.
    /// </summary>
    public interface IInvestmentDomainServices : IDomainServices<IInvestment>
    {
    }
}