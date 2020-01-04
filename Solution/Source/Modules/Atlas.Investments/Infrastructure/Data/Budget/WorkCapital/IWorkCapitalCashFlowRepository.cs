using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data.Budget.WorkCapital
{
    public interface IWorkCapitalCashFlowRepository: IRepository<IWorkCapitalCashFlow>
    {
        IWorkCapitalComponent WorkCapitalComponent { get; set; }
    }
}