using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    ///     Contract to be implemented by the Wage Scale crud view models, these view models will handle the CRUD operations for
    ///     Wage Scale domain entities in the UI (presentation layer).
    /// </summary>
    public interface IWageScaleViewModel : ICrudViewModel<IWageScale, IWageScalePresenter>
    {
    }
}