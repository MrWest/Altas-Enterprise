using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    ///     Contract to be implemented by the Work Force crud view models, these view models will handle the CRUD operations
    ///     for Work Force domain entities in the UI (presentation layer).
    /// </summary>
    public interface IWorkForceViewModel : ICrudViewModel<IWorkForce, IWorkForcePresenter>
    {
    }
}