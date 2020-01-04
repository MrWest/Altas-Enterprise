using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.Construction;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Construction
{
    /// <summary>
    /// Implementation of the contract <see cref="IConstructionExecutedResourceViewModel"/> representing the crud view model used to manage
    /// the executed resources of an equipment budget component.
    /// </summary>
    public class ConstructionExecutedResourceViewModel :
        ExecutedResourceViewModelBase<IConstructionComponent, IConstructionExecutedResourcePresenter, IConstructionExecutedResourceManagerApplicationServices>,
        IConstructionExecutedResourceViewModel
    {
    }
}
