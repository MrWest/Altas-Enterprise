using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    ///     Contract for the presenter view models used to decorate and impersonate instances of the domain entity: "Category" in
    ///     the UI.
    /// </summary>
    public interface ICategoryPresenter : IPresenter<ICategory>
    {
        string Id { get; set; }

        string Name { get; }
        string Code { get; }
    }
}