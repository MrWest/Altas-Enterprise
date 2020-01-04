using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    /// <summary>
    /// Base contract for the presenter view models decorating and impersonating in the UI the executed resource of a certain
    /// budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the decorated executed resource.</typeparam>
    public interface IExecutedResourcePresenter<TComponent> : 
        IBudgetComponentItemPresenter<IExecutedResource>
        where TComponent : IBudgetComponent
    {
    }
}
