using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Construction
{
    /// <summary>
    /// Contract to be implemented by the repository handling the Data operations for the set of <see cref="IExecutedResource"/>
    /// of an <see cref="IConstructionComponent"/>.
    /// </summary>
    public interface IConstructionExecutedResourceRepository : IBudgetComponentItemRepository<IExecutedResource>
    {
    }
}
