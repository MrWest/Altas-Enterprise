using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    public interface IExecutedSubSpecialityHolderPresenter<TComponent> : ISubSpecialityHolderPresenter<IExecutedSubSpecialityHolder,TComponent>
         where TComponent : class, IBudgetComponent
        //where TExecuted : class, IExecutedActivityViewModel
        //where TPresenter : class, IExecutedActivityPresenter
    {
        IExecutedActivityViewModel ExecutedActivities { get; }

    }
}