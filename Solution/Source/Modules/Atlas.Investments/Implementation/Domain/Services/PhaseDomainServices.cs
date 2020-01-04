using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    ///     Implementation of the contract <see cref="IPhaseDomainServices" /> used to enforce the business rules in Phase domain
    ///     entities.
    /// </summary>
    public class PhaseDomainServices : CodedNomenclatorDomainServicesBase<IPhase>, IPhaseDomainServices
    {
        /// <summary>
        ///     Creates a new instance of an Phase.
        /// </summary>
        /// <returns>A new instance of type <see cref="IPhase" />.</returns>
        public override IPhase Create()
        {
            IPhase phase = base.Create();
            phase.Name = Resources.NewPhase_Name;
            phase.Description = Resources.NewPhase_Description;

            return phase;
        }
    }
}