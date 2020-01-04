using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    public class WorkCapitalCashFlowDomainServices: DomainServicesBase<IWorkCapitalCashFlow>, IWorkCapitalCashFlowDomainServices
    {
        public IWorkCapitalComponent WorkCapitalComponent { get; set; }
        public override IWorkCapitalCashFlow Create()
        {
            var wcf = base.Create();
            wcf.WorkCapital = WorkCapitalComponent;
            return wcf;
        }
    }
}