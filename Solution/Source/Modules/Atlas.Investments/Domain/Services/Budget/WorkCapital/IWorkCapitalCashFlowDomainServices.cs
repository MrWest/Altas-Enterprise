using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services.Budget.WorkCapital
{
    public interface  IWorkCapitalCashFlowDomainServices: IDomainServices<IWorkCapitalCashFlow>
    {
        IWorkCapitalComponent WorkCapitalComponent { get; set; }
    }
}