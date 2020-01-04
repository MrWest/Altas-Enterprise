using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    ///     Implementation of the contract <see cref="IInvestmentDomainServices" /> representing the domain services used to
    ///     enforce the business rules for the investments.
    /// </summary>
    public class InvestmentDomainServices : InvestmentElementDomainServicesBase<IInvestment>, IInvestmentDomainServices
    {
        public override IInvestment Create()
        {
            var investment = base.Create();
            investment.Name =  Resources.NewInvestment;
            return investment;
        }
    }
}