using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{
    public interface IUnderGroupItemManagerApplicationServices<TItem>: IBudgetComponentItemManagerApplicationServices<TItem>
     where TItem : class, IUnderGroupItem
    {
        IUnderGroup UnderGroup { get; set; }

    }
}