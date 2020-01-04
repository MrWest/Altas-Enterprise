using System.ComponentModel.DataAnnotations.Schema;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
  

    /// <summary>
        /// Defines the contract for a budget domain entity.
        /// </summary>
        public interface IBudget : IEntity,IPeriodCalculator
    {
        /// <summary>
        /// Gets the equipment component of the current <see cref="IBudget"/>.
        /// </summary>
        [ForeignKey("EquipmentComponentId")]
        IEquipmentComponent EquipmentComponent { get; set; }

        string EquipmentComponentId { get; set; }

        /// <summary>
        /// Gets the buildings component of the current <see cref="IBudget"/>.
        /// </summary>
        /// 
        [ForeignKey("ConstructionComponentId")]
        IConstructionComponent ConstructionComponent { get; set; }

        /// <summary>
        /// Gets the buildings component of the current <see cref="IBudget"/>.
        /// </summary>
        string ConstructionComponentId { get; set; }

        /// <summary>
        /// Gets the other expenses component of the current <see cref="IBudget"/>.
        /// </summary>
        [ForeignKey("ConstructionComponentId")]
        IOtherExpensesComponent OtherExpensesComponent { get; set; }

        /// <summary>
        /// Gets the other expenses component of the current <see cref="IBudget"/>.
        /// </summary>
        string OtherExpensesComponentId { get; set; }

        /// <summary>
        /// Gets the other work capital component of the current <see cref="IBudget"/>.
        /// </summary>
        [ForeignKey("WorkCapitalComponentId")]
        IWorkCapitalComponent WorkCapitalComponent { get; set; }

        /// <summary>
        /// Gets the other work capital component of the current <see cref="IBudget"/>.
        /// </summary>
        string WorkCapitalComponentId { get; set; }

        /// <summary>
        /// Gets the investment element to which belong the current <see cref="IBudget"/>.
        /// </summary>
        IInvestmentElement InvestmentElement { get; set; }

        // TODO: Finish comments
        /// <summary>
        /// Gets the planned cost of all resources and activities of the current <see cref="IBudget"/>.
        /// </summary>
        //decimal PlannedCost { get; }

        /// <summary>
        /// Gets the planned cost of all 
        /// </summary>
        //decimal FixedCapital { get; }

        //decimal ExecutedCost { get; }

        //decimal FinancialExecutedCost { get; }

        //int GeneralProgress { get; }

        //int FinancialProgress { get; }

        //decimal EquipmentProgress { get; }

        //decimal BuildingProgress { get; }

        //decimal OtherProgress { get; }

        //decimal WorkCapitalProgress { get; }

        //decimal GetPlannedByCurrency(ICurrency currency);

        // IPeriod PeriodInherited();
    }
}
