

using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;

namespace CompanyName.Atlas.Investments.Infrastructure.Data.Budget
{
    /// <summary>
    /// Base contract of the repositories used to performs the data operations over resources of a certain budget component item to ensure that
    /// their managed safely.
    /// </summary>
    /// <typeparam name="TItem">The budget component items which data operations will be handled here.</typeparam>
    /// <typeparam name="TComponent">The budget component item to which they belong.</typeparam>

    public interface IBudgetComponentResourceRepository<TItem, TComponentItem> : IBudgetComponentItemRepository<TItem,TComponentItem>
        where TItem : class, IBudgetComponentItem
        where TComponentItem : class, IBudgetComponentItem
    {
       
    }
}
