using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    ///     Implementation of the contract <see cref="IWageScaleDomainServices" /> used to enforce the business rules in Wage Scale domain
    ///     entities.
    /// </summary>
    public class WageScaleDomainServices : DomainServicesBase<IWageScale>, IWageScaleDomainServices
    {
        /// <summary>
        ///     Creates a new instance of an WageScale.
        /// </summary>
        /// <returns>A new instance of type <see cref="IWageScale" />.</returns>
        public override IWageScale Create()
        {
            IWageScale phase = base.Create();
            phase.Name = Resources.NewWageScale_Name;

            return phase;
        }
    }
}