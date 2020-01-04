using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data.Budget
{
    public interface IBudgetComponentItemForNomenclatorRepository<TItem>:INomenclatorRepository<TItem>
        where TItem: class , IBudgetComponentItem
    {
        
    }
}