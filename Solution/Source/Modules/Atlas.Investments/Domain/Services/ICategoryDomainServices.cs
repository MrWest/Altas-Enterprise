using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services
{
    /// <summary>
    ///     Contract to be implemented by the domain services used to enforce the business operations for Category domain entities.
    /// </summary>
    public interface ICategoryDomainServices : IDomainServices<ICategory>
    {
    }
}