using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    ///     Implementation of the contract for the domain entity: "investment component".
    /// </summary>
    public class InvestmentComponent : InvestmentElement, IInvestmentComponent
    {
        /// <summary>
        ///     Gets or sets the parent element of the current <see cref="InvestmentComponent" />.
        /// </summary>
        public IInvestmentElement Parent { get; set; }

        public string ParentId { get; set; }
    }
}