using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    public interface IUnderGroupItemPresenter<TItem> : IBudgetComponentItemPresenter<TItem>
         where TItem : class, IUnderGroupItem
    {
        IUnderGroupPresenter UnderGroup { get; set; }
    }
}