using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    public interface IExecutedSubSpecialityHolderViewModel<TComponent, TSubPresenter> : ISubSpecialityHolderViewModel<IExecutedSubSpecialityHolder, TComponent, TSubPresenter>
        where TComponent : class, IBudgetComponent
        where TSubPresenter : class, IExecutedSubSpecialityHolderPresenter<TComponent>
        //where TExecuted : class, IExecutedActivityViewModel
        //where TPresenter : class, IExecutedActivityPresenter
    {
        
    }
}