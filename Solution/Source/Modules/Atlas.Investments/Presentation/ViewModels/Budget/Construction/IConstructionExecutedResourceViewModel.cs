using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction
{
    /// <summary>
    /// Contract of the crud view model managing the executed resources of an construction budget component.
    /// </summary>
    public interface IConstructionExecutedResourceViewModel : IExecutedResourceViewModel<IConstructionComponent, IConstructionExecutedResourcePresenter>
    {
    }
}
