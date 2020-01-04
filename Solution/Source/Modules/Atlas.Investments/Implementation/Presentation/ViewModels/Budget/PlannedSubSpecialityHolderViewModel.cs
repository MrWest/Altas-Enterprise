using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget
{
    public class PlannedSubSpecialityHolderViewModel<TComponent,TSubPresenter> : SubSpecialityHolderViewModel<IPlannedSubSpecialityHolder,TComponent, TSubPresenter,IPlannedSubSpecialityHolderManagerApplicationServices>, IPlannedSubSpecialityHolderViewModel<TComponent,TSubPresenter>
        where TComponent : class, IBudgetComponent
        where TSubPresenter : class, IPlannedSubSpecialityHolderPresenter<TComponent>
        
    {
        
    }
}