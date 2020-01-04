using CompanyName.Atlas.Investments.Application.Budget;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    public interface IPlannedSubSpecialityHolderPresenter<TComponent>: ISubSpecialityHolderPresenter<IPlannedSubSpecialityHolder,TComponent>, IBudgetComponentItemChangesSpreadder
         where TComponent : class, IBudgetComponent
        //where TPlanned: class , IPlannedActivityViewModel
        
    {
        IPlannedActivityViewModel PlannedActivities { get; }
    }
}