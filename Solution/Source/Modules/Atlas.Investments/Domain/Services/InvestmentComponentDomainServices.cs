using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    public class InvestmentComponentDomainServices :
        InvestmentElementDomainServicesBase<IInvestmentComponent>, IInvestmentComponentDomainServices
    {
    }
}