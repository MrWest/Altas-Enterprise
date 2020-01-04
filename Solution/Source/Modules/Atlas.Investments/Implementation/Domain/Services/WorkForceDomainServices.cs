using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    ///     Implementation of the contract <see cref="IWorkForceDomainServices" /> used to enforce the business rules in Work
    ///     Force domain entities.
    /// </summary>
    public class WorkForceDomainServices : DomainServicesBase<IWorkForce>, IWorkForceDomainServices
    {
        /// <summary>
        ///     Creates a new instance of an Work Force.
        /// </summary>
        /// <returns>A new instance of type <see cref="IWorkForce" />.</returns>
        public override IWorkForce Create()
        {
            IWorkForce phase = base.Create();
            phase.Name = Resources.NewWorkForce_Name;

            return phase;
        }
    }
}