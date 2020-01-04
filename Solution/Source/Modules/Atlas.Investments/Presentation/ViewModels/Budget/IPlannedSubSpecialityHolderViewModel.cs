using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    public interface IPlannedSubSpecialityHolderViewModel<TComponent, TSubPresenter> : ISubSpecialityHolderViewModel<IPlannedSubSpecialityHolder,TComponent, TSubPresenter>
        where TComponent : class, IBudgetComponent
        where TSubPresenter : class, IPlannedSubSpecialityHolderPresenter<TComponent>
        //where TPlanned : class, IPlannedActivityViewModel
        //where TPresenter : class, IPlannedActivityPresenter
    {
        
    }
}