using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    /// This is the base class of the planned items of a budget component.
    /// </summary>
    public abstract class PlannedBudgetComponentItemBase :  BudgetComponentItemBase, IPlannedBudgetComponentItem
    {
        /// <summary>
        /// Gets or sets the execution (<see cref="IExecutedBudgetComponentItem"/>) of the current
        /// <see cref="IPlannedBudgetComponentItem"/>.
        /// </summary>
        public object Execution { get; set; }
    }
}
