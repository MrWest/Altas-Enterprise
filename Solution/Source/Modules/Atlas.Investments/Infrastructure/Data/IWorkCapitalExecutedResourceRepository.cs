using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;

namespace CompanyName.Atlas.Investments.Infrastructure.Data
{
    /// <summary>
    /// Contract to be implemented by the repository handling the Data operations for the set of <see cref="IExecutedResource"/>
    /// of an <see cref="IWorkCapitalComponent"/>.
    /// </summary>
    public interface IWorkCapitalExecutedResourceRepository : IBudgetComponentItemRepository<IExecutedResource, IWorkCapitalComponent>
    {
    }
}
