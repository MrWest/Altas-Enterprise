using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services
{
    /// <summary>
    ///     Contract of the domain services used to enforce the business rules of domain entities: investment components.
    /// </summary>
    public interface IInvestmentComponentDomainServices : IDomainServices<IInvestmentComponent>
    {
    }
}