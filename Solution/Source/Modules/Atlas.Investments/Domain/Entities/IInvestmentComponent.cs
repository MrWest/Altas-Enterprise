namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    ///     Contract defining a domain entity called: "investment component". A set of this entities conform another component
    ///     or investment.
    /// </summary>
    public interface IInvestmentComponent : IInvestmentElement
    {
        /// <summary>
        ///     Gets or sets the parent investment element (<see cref="IInvestmentElement" />) of the current one.
        /// </summary>
        IInvestmentElement Parent { get; set; }
        /// <summary>
        ///     Gets or sets the parent investment element (<see cref="IInvestmentElement" />) of the current one.
        /// </summary>
        string ParentId { get; set; }
    }
}