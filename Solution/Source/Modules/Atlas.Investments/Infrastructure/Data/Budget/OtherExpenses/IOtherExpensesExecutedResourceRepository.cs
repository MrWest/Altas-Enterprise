using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data.Budget.OtherExpenses
{
    /// <summary>
    /// Contract to be implemented by the repository handling the Data operations for the set of <see cref="IExecutedResource"/>
    /// of an <see cref="IOtherExpensesComponent"/>.
    /// </summary>
    public interface IOtherExpensesExecutedResourceRepository : IBudgetComponentItemRepository<IExecutedResource>
    {
    }
}
