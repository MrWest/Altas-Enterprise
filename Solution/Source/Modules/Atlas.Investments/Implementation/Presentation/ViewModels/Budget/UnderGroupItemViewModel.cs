using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    public class UnderGroupItemViewModel<TItem, TPresenter,TService> : NavigableViewModel<TItem, TPresenter,TService>, IUnderGroupItemViewModel<TItem,TPresenter>
        where TItem : class, IUnderGroupItem
        where TPresenter : class, IUnderGroupItemPresenter<TItem>
        where TService:class ,IUnderGroupItemManagerApplicationServices<TItem>
    {
        public IUnderGroupPresenter UnderGroup { get; set; }

        protected override INavigable Parent { get { return UnderGroup; } }
    }
}