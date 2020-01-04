using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data.Budget
{
    public interface IUnderGroupItemRepository<TItem>: IBudgetComponentItemRepository<TItem>
        where TItem : class ,IUnderGroupItem
    {
        IUnderGroup UnderGroup { get; set; }
    }
}