using System.Collections.Generic;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Base contract of the domain entities being executed budget component items.
    /// </summary>
    public interface IExecutedBudgetComponentItem : IBudgetComponentItem
    {
        /// <summary>
        /// Gets or sets the planification (<see cref="IPlannedBudgetComponentItem"/>) of the current
        /// <see cref="IExecutedBudgetComponentItem"/>.
        /// </summary>
        //IPlannedBudgetComponentItem Planification { get; set; }

        decimal ExecutedQuantity { get; }

        IList<IExecution> ExecutionLog { get; set; }
        /// <summary>
        /// Gets or sets the planification (<see cref="IPlannedBudgetComponentItem"/>) of the current
        /// <see cref="IExecutedBudgetComponentItem"/>.
        /// </summary>
        object Planification { get; set; }
    }
}
