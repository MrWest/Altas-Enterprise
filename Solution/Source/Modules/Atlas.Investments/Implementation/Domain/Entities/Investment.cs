using System.Collections.Generic;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    ///     Implementation of the domain entity called: "investment". This represents an investment.
    /// </summary>
    public class Investment : InvestmentElement, IInvestment
    {

        /// <summary>
        ///     Gets or sets the capacity of the underlying investment.
        /// </summary>
        public string Capacity { get; set; }

        /// <summary>
        ///     Gets or sets the induced doings of the underlying investment.
        /// </summary>
        public string InducedDoings { get; set; }

        /// <summary>
        ///     Gets or sets the author or emitter of the investment element.
        /// </summary>
        public string AuthorOrEmitter { get; set; }

        /// <summary>
        ///     Gets or sets the entity of the investment element.
        /// </summary>
        public string Entity { get; set; }

        /// <summary>
        ///     Gets or sets the related programs of the investment element.
        /// </summary>
        public string RelatedPrograms { get; set; }

        /// <summary>
        /// Get or sets the Osde of the current <see cref="IInvestment"/>.
        /// </summary>
        public string Osde { get; set; }

        /// <summary>
        /// Get or sets the Oace of the current <see cref="IInvestment"/>.
        /// </summary>
        public string Oace { get; set; }
        /// <summary>
        /// Get or sets the Phase of the current <see cref="IInvestment"/>.
        /// </summary>
        public string Phase { get; set; }
        /// <summary>
        /// Get or sets the Investment Type of the current <see cref="IInvestment"/>.
        /// </summary>
        public string InvestmentType { get; set; }

        /// <summary>
        ///     Gets or sets the nature of the investment element.
        /// </summary>
        public string Nature { get; set; }

        /// <summary>
        ///     Gets or sets the impact of the investment element.
        /// </summary>
        public string Impact { get; set; }

     

        private IList<IInvestmentDocument> _documents;

        /// <summary>
        /// Get or sets a list of Documents associated to this investment <see cref="IInvestment"/>.
        /// </summary>
        public IList<IInvestmentDocument> Documents { 
            get { return _documents??new List<IInvestmentDocument>(); } 
            set{ _documents = value; } 
        }
    }
}