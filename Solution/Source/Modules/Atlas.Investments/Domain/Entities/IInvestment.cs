using System.Collections.Generic;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    ///     Contract defining what is an investment.
    /// </summary>
    public interface IInvestment : IInvestmentElement
    {
        /// <summary>
        ///     Gets or sets the capacity of the underlying investment.
        /// </summary>
        string Capacity { get; set; }

        /// <summary>
        ///     Gets or sets the induced doings of the underlying investment.
        /// </summary>
        string InducedDoings { get; set; }

        /// <summary>
        ///     Gets or sets the author or emitter of the investment.
        /// </summary>
        string AuthorOrEmitter { get; set; }

        /// <summary>
        ///     Gets or sets the entity of the investment.
        /// </summary>
        string Entity { get; set; }

        /// <summary>
        ///     Gets or sets the related programs of the investment.
        /// </summary>
        string RelatedPrograms { get; set; }

        /// <summary>
        /// Get or sets the Osde of the current <see cref="IInvestment"/>.
        /// </summary>
        string Osde { get; set; }

        /// <summary>
        /// Get or sets the Oace of the current <see cref="IInvestment"/>.
        /// </summary>
        string Oace { get; set; }

        /// <summary>
        /// Get or sets the Phase of the current <see cref="IInvestment"/>.
        /// </summary>
        string Phase { get; set; }
        /// <summary>
        /// Get or sets the Investment Type of the current <see cref="IInvestment"/>.
        /// </summary>
        string InvestmentType { get; set; }

        /// <summary>
        ///     Gets or sets the nature of the investment element.
        /// </summary>
        string Nature { get; set; }

        /// <summary>
        ///     Gets or sets the impact of the investment element.
        /// </summary>
        string Impact { get; set; }


        /// <summary>
        /// Get or sets a list of Documents associated to this investment <see cref="IInvestment"/>.
        /// </summary>
        IList<IInvestmentDocument> Documents { get; set; }

    }
}