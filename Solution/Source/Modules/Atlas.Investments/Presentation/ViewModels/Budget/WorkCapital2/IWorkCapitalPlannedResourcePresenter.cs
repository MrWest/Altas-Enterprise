using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital
{
    /// <summary>
    ///     Contract of the presenter view models decorating and impersonating in the UI planned resources of the work capital
    ///     budget component.
    /// </summary>
    public interface IWorkCapitalPlannedResourcePresenter : IPlannedResourcePresenter<IBudgetComponentItem>
    {
    }
}