using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data
{
    /// <summary>
    ///     Contract defined for the repositories handling data operations of investment components of a certain investment
    ///     element.
    /// </summary>
    public interface IInvestmentComponentRepository : 
        IInvestmentElementRepository2<IInvestmentComponent>,
        IRelatedRepository<IInvestmentComponent, IInvestmentElement>
    {
        /// <summary>
        ///     Gets the reference to the <see cref="IInvestmentElement" /> containing the investment components handled in the
        ///     current repository.
        /// </summary>
        IInvestmentElement InvestmentElement { get; set; }
    }
}