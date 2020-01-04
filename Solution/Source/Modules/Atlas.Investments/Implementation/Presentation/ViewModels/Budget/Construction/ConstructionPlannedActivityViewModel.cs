using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.Construction;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.Construction
{
    /// <summary>
    /// Implementation of the contract <see cref="IConstructionPlannedActivityViewModel"/> representing the crud view model used to manage
    /// the planned activities of an equipment budget component.
    /// </summary>
    public class ConstructionPlannedActivityViewModel :
        PlannedActivityViewModelBase,
        IConstructionPlannedActivityViewModel
    {
    }
}
