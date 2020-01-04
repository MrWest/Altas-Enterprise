using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    ///     Implementation of the contract <see cref="IOaceDomainServices" /> used to enforce the business rules in OACE domain
    ///     entities.
    /// </summary>
    public class OaceDomainServices : CodedNomenclatorDomainServicesBase<IOace>, IOaceDomainServices
    {
        /// <summary>
        ///     Creates a new instance of an OACE.
        /// </summary>
        /// <returns>A new instance of type <see cref="IOace" />.</returns>
        public override IOace Create()
        {
            IOace oace = base.Create();
            oace.Name = Resources.NewOace_Name;
            oace.Description = Resources.NewOace_Description;

            return oace;
        }
    }
}