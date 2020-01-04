using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application.Budget.OtherExpenses
{
    /// <summary>
    /// Contract of the application services handling the CRUD-operation requests coming from upper layers, regarding
    /// to the OtherExpenses executed activities of an investment element's budget.
    /// </summary>
    public interface IOtherExpensesExecutedActivityManagerApplicationServices :
        IExecutedActivityItemManagerApplicationServices
    {
    }
}
