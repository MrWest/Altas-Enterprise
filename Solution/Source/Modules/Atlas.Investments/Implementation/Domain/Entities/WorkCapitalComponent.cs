using System.ComponentModel.DataAnnotations.Schema;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    /// This is the implementation of the domain entity representing a: "Work capital", being one of a budget's component.
    /// </summary>
    public class WorkCapitalComponent: BudgetComponentBase, IWorkCapitalComponent
    {
        /// <summary>
        /// Work capital work flow
        /// </summary>
        [ForeignKey("WorkCapitalCashFlowId")]
        public IWorkCapitalCashFlow WorkCapitalCashFlow { get;  set; }

        [ForeignKey("ExecutedWorkCapitalCashFlowId")]
        public IWorkCapitalCashFlow ExecutedWorkCapitalCashFlow { get; set; }

        public string WorkCapitalCashFlowId { get; set; }
        public string ExecutedWorkCapitalCashFlowId { get; set; }
    }
}
