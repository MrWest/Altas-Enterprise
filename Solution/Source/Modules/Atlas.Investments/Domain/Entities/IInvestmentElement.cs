using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Represents the domain concept of an investment element.
    /// </summary>
    public interface IInvestmentElement : ICodedNomenclator, IPeriodCalculator
    {
        

        /// <summary>
        /// Gets the collection of investment elements (<see cref="IInvestmentElement"/>) composing the current one.
        /// </summary>
        IList<IInvestmentElement> Elements { get; }

        /// <summary>
        /// Gets or sets the budget (<see cref="IBudget"/>) of the current <see cref="IInvestmentElement"/>.
        /// </summary>
        [ForeignKey("BudgetId")]
        IBudget Budget { get; set; }

        /// <summary>
        /// Gets or sets the budget (<see cref="IBudget"/>) of the current <see cref="IInvestmentElement"/>.
        /// </summary>
        string BudgetId { get; set; }

      
        /// <summary>
        /// Gets or sets the time interval (<see cref="IInvestmentElementPeriod"/> for  the current <see cref="IInvestmentElement"/>.
        /// </summary>
        [ForeignKey("PeriodId")]
        IPeriod Period { get; set; }

        /// <summary>
        /// Gets or sets the time interval (<see cref="IInvestmentElementPeriod"/> for  the current <see cref="IInvestmentElement"/>.
        /// </summary>
        string PeriodId { get; set; }

        /// <summary>
        ///     Gets or sets the code of the investment element.
        /// </summary>
        string Code { get; set; }

        /// <summary>
        ///     Gets or sets the location of the investment element.
        /// </summary>
        string Location { get; set; }

        /// <summary>
        ///     Gets or sets the constructor of the investment element.
        /// </summary>
        string Constructor { get; set; }

        /// <summary>
        ///     Gets or sets the objective of the investmentelement.
        /// </summary>
        string Objective { get; set; }

        /// <summary>
        ///     Gets or sets the scope of the investment element.
        /// </summary>
        string Scope { get; set; }
    }
}
