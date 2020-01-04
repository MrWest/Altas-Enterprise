using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    ///     Contract for the crud view model handling the crud operations in the presentation layer for the investment
    ///     components in the presentation layer.
    /// </summary>
    public interface IInvestmentComponentViewModel<TInvestmentElement> : INavigableViewModel<IInvestmentComponent, IInvestmentComponentPresenter<TInvestmentElement>>
      where TInvestmentElement : class, IInvestmentElement
    {
        /// <summary>
        /// Gets or sets the parent investment element of the investment component presenters in the current crud view model.
        /// </summary>
        IInvestmentElementPresenter<TInvestmentElement> InvestmentElement { get; set; }
    }
}