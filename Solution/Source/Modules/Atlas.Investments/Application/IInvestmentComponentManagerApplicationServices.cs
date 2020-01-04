using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Presentation.Features;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{
    /// <summary>
    ///     Contract for the application services provider for the CRUD operations regarding to the domain entities: investment
    ///     components.
    /// </summary>
    public interface IInvestmentComponentManagerApplicationServices : IInvestmentElementManagerApplicationServices<IInvestmentComponent>, ICopyPasteable
    {
        /// <summary>
        ///     Gets or sets the parent investment element of those managed in the current application services provider.
        /// </summary>
        IInvestmentElement InvestmentElement { get; set; }
    }
}