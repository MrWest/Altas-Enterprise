using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    ///     Contract of the crud view model handling the crud operations for the investment elements there are in the system in
    ///     the presentation layer.
    /// </summary>
    public interface IInvestmentViewModel : ICrudViewModel<IInvestment, IInvestmentPresenter>
    {
    }
}