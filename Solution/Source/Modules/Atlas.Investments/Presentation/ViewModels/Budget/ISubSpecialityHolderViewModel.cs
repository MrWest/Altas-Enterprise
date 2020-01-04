using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    public interface ISubSpecialityHolderViewModel<THolder,TComponent, TPresenter> : INavigableViewModel<THolder, TPresenter>
        where TComponent : class, IBudgetComponent
        where TPresenter: class, ISubSpecialityHolderPresenter<THolder,TComponent>
        where THolder:class ,ISubSpecialityHolder
    {
        IBudgetComponentPresenter<TComponent> BudgetComponent { get; set; }
    }
}