using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data
{
    /// <summary>
    ///     Contract to be implemented by the repositories to be implemented by the repositories handling data operations for
    ///     Category domain entities.
    /// </summary>
    public interface ICategoryRepository : IRepository<ICategory>
    {
    }
}