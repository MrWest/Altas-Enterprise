namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Base contract for the Planned budget component components.
    /// </summary>
    public interface IPlannedBudgetComponentItem : IBudgetComponentItem
    {
        /// <summary>
        /// Gets or sets the execution (<see cref="IExecutedBudgetComponentItem"/>) of the current
        /// <see cref="IPlannedBudgetComponentItem"/>.
        /// </summary>
        object Execution { get; set; }

        //IMenLabor MenLabor { get; set; }

        //decimal MenLaborCost { get; set; }

        //ICategory Category { get; set; }

        //decimal TotalCost { get; set; }

        //IExpenseConcept ExpenseConcept { get; set; }

        //IClassification Classification { get; set; }

        //IMeasurementUnit MeasurementUnit { get; set; }

        //ICurrency Currency { get; set; }

        //decimal WithMaterialsCost { get; set; }

        //decimal WithoutMaterialsCost { get; set; }

        //decimal EquipmentCost { get; set; }

        //decimal Cost { get; }

        // public virtual ICollection<LogEntry> Log { get; set; }

        //IPeriod Period { get; set; }

        //IWeigth Weigth { get; set; }

        //public decimal Weigth { get; set; }

        // void Notify();

    }
}
