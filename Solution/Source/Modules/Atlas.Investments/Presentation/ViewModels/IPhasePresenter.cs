using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    ///     Contract for the presenter view models used to decorate and impersonate instances of the domain entity: "Phase" in
    ///     the UI.
    /// </summary>
    public interface IPhasePresenter : IPresenter<IPhase>, ICodedNomenclator
    {
    }
}