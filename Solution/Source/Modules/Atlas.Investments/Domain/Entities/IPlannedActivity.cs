using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Contract representing the specification of the domain entity: "Planned Activity".
    /// </summary>
    public interface IPlannedActivity : IActivity, IPeriodCalculator
    {
        /// <summary>
        /// Gets or sets the execution (<see cref="IExecutedBudgetComponentItem"/>) of the current
        /// <see cref="IPlannedBudgetComponentItem"/>.
        /// </summary>
        object Execution { get; set; }
        /// <summary>
        /// Gets or sets the <see cref="IBudgetComponent"/> to which belong the current <see cref="IBudgetComponentItem"/>.
        /// </summary>
        //IBudgetComponent Component { get; set; }

    }
}
