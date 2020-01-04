using System;
using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Represents the base contract of an item composing a budget component.
    /// </summary>
    public interface IBudgetComponentItem : ICodedNomenclator ,ICurrenciable, IPeriodCalculator
    {
        ///// <summary>
        ///// Gets or sets the list the budget component element associated with this one.
        ///// </summary>
     //   IEntity Component { get; set; }

        /// <summary>
        /// Gets the list of planned resources composing the current <see cref="IBudgetComponent"/>.
        /// </summary>
        IList<IPlannedResource> PlannedResources { get; }

        /// <summary>
        /// Gets or sets an identifier to differentiate the current <see cref="IBudgetComponentItem"/> with respect to others.
        /// </summary>
        decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets an identifier to differentiate the current <see cref="IBudgetComponentItem"/>.
        /// </summary>
        //string Code { get; set; }

        /// <summary>
        /// Get or sets the Unitary Cost of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        decimal UnitaryCost { get; set; }

        /// <summary>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       1   `                                                                                                                                                                                                                                                                   
        /// Get or sets the Measurement id of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        string MeasurementUnit { get; set; }

        /// <summary>
        /// Get or sets the Currency id of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        string Currency { get; set; }


        /// <summary>
        /// Get or sets the Category id of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        string Category { get; set; }

        /// <summary>
        /// Get or sets the Expense Concept id of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        string SubExpenseConcept { get; set; }


        /// <summary>
        /// Get or sets the Expense Concept id of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        string PriceSystem { get; set; }


        /// <summary>
        /// Gets or sets the time interval (<see cref="IInvestmentElementPeriod"/> for  the current <see cref="IInvestmentElement"/>.
        /// </summary>
        IPeriod Period { get; set; }

        string PeriodId { get; set; }

        bool isUnitaryPriceCalculated { get; set; }

        decimal CalculatedUnitaryPrice{ get; set; }

        /// <summary>
        /// Get or sets the Cost of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        /// decimal Cost { get; }
    }
}