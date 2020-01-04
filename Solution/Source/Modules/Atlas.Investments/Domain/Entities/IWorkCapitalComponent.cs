namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Contract of the domain entity: "Work capital" budget component.
    /// </summary>
    public interface IWorkCapitalComponent : IBudgetComponent
    {
        //IPlannedCashFlow PlannedCashFlow { get; set; }

        //IExecutedCashFlow ExecutedCashFlow { get; set; }

        IWorkCapitalCashFlow WorkCapitalCashFlow { get; set; }
        IWorkCapitalCashFlow ExecutedWorkCapitalCashFlow { get; set; }
        string WorkCapitalCashFlowId { get; set; }
        string ExecutedWorkCapitalCashFlowId { get; set; }
    }
}
