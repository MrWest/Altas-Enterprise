using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    public interface IUnderGroupItemViewModel<TItem,TPresenter>:INavigableViewModel<TItem,TPresenter>
        where TItem: class, IUnderGroupItem
        where TPresenter: class, IUnderGroupItemPresenter<TItem>
    {
        IUnderGroupPresenter UnderGroup { get; set; }
    }
}