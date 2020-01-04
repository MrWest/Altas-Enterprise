using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    ///     Implementation of the contract <see cref="IOsdeDomainServices" /> used to enforce the business rules in OSDE domain
    ///     entities.
    /// </summary>
    public class OsdeDomainServices : CodedNomenclatorDomainServicesBase<IOsde>, IOsdeDomainServices
    {
        /// <summary>
        ///     Creates a new instance of an OSDE.
        /// </summary>
        /// <returns>A new instance of type <see cref="IOsde" />.</returns>
        public override IOsde Create()
        {
            IOsde osde = base.Create();
            osde.Name = Resources.NewOsde_Name;
            osde.Description = Resources.NewOsde_Description;

            return osde;
        }
    }
}