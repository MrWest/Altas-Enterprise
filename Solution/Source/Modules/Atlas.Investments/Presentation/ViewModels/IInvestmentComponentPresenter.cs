using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Features;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    ///     Contract of the presenter view model decorating and impersonating instances of the domain entity:
    ///     "investment component" (<see cref="IInvestmentComponent"/> instances).
    /// </summary>
    public interface IInvestmentComponentPresenter<TInvestmentElement> : IInvestmentElementPresenter<IInvestmentComponent>
        where TInvestmentElement:class ,IInvestmentElement
    {
        /// <summary>
        /// Gets or sets the parent investment element of the investment component presenters in the current crud view model.
        /// </summary>
        IInvestmentElementPresenter<TInvestmentElement> InvestmentElement { get; set; }

        //void DoPaste(IInvestmentComponentPresenter<TInvestmentElement> investmentComponent);
    }
}