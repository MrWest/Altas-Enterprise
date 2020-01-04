using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital
{
    /// <summary>
    ///     Contract of the crud view model managing the planned resources of a work capital budget component.
    /// </summary>
    public interface IWorkCapitalPlannedResourceViewModel :
        IPlannedResourceViewModel<IBudgetComponentItem, IWorkCapitalPlannedResourcePresenter>
    {
    }
}