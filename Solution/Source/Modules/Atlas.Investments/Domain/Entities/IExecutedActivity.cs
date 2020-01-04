using System.Collections.Generic;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Contract defining the domain entity called: "Executed Activity".
    /// </summary>
    public interface IExecutedActivity : IActivity
    {
        /// <summary>
        /// Gets or sets the planification (<see cref="IPlannedBudgetComponentItem"/>) of the current
        /// <see cref="IExecutedBudgetComponentItem"/>.
        /// </summary>
        //IPlannedBudgetComponentItem Planification { get; set; }

       // decimal ExecutedQuantity { get; }

        IList<IExecution> ExecutionLog { get; set; }
        /// <summary>
        /// Gets or sets the planification (<see cref="IPlannedBudgetComponentItem"/>) of the current
        /// <see cref="IExecutedBudgetComponentItem"/>.
        /// </summary>
        object Planification { get; set; }
        ///// <summary>
        ///// Gets or sets the <see cref="IBudgetComponent"/> to which belong the current <see cref="IBudgetComponentItem"/>.
        ///// </summary>
        //IBudgetComponent Component { get; set; }


    }
}
