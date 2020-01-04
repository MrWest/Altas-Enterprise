using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    ///     Implementation of the contract <see cref="IInvestmentTypeDomainServices" /> used to enforce the business rules in
    ///     Investment Type domain entities.
    /// </summary>
    public class InvestmentTypeDomainServices : CodedNomenclatorDomainServicesBase<IInvestmentType>, IInvestmentTypeDomainServices
    {
        /// <summary>
        ///     Creates a new instance of an Investment Type.
        /// </summary>
        /// <returns>A new instance of type <see cref="IInvestmentType" />.</returns>
        public override IInvestmentType Create()
        {
            IInvestmentType oace = base.Create();
            oace.Name = Resources.NewInvestmentType_Name;
            oace.Description = Resources.NewInvestmentType_Description;

            return oace;
        }
    }
}