using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Transfer;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    /// <summary>
    ///     This interface describes the contract for a presenter of investments.
    /// </summary>
    public interface IInvestmentPresenter : IInvestmentElementPresenter<IInvestment>,IExportable
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
        ///     Gets or sets the author or emitter of the underlying investment element.
        /// </summary>
        string AuthorOrEmitter { get; set; }

        /// <summary>
        ///     Gets or sets the entity of the underlying investment element.
        /// </summary>
        string Entity { get; set; }

        /// <summary>
        ///     Gets or sets the related programs of the underlying investment element.
        /// </summary>
        string RelatedPrograms { get; set; }

        /// <summary>
        /// Get or sets the Osde of the current <see cref="IInvestment"/>.
        /// </summary>
        IOsdePresenter Osde { get; set; }

        /// <summary>
        /// Get or sets the Oace of the current <see cref="IInvestment"/>.
        /// </summary>
        IOacePresenter Oace { get; set; }
        /// <summary>
        /// Get or sets the Phase of the current <see cref="IInvestment"/>.
        /// </summary>
        IPhasePresenter Phase { get; set; }
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
        IInvestmentDocumentViewModel Documents { get; }

        IEnumerable<string> FindInvestmentTypeByContains(string text);
        IEnumerable<string> FindNatureByContains(string text);
        IEnumerable<string> FindImpactByContains(string text);
    }
}