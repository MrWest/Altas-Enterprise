using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Base contract of the crud view model used to manage the executed resources of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the executed resources managed here.</typeparam>
    /// <typeparam name="TPresenter">The type of the presenter view models decorating the executed resources.</typeparam>
    public interface IExecutedResourceViewModel<TComponent, TPresenter> :
        IExecutedBudgetComponentItemViewModel<IExecutedResource, TPresenter, TComponent>
        where TComponent : IBudgetComponent
        where TPresenter : class, IBudgetComponentItemPresenter<IExecutedResource>
    {
    }
}
