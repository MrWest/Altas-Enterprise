using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{    /// <summary>
    ///     Implementation of the contract <see cref="IMeasurementUnitDomainService" /> used to enforce the business rules in Measurement Units domain
    ///     entities.
    /// </summary>
    public class MeasurementUnitDomainService : ConvertibleEntityDomainService<IMeasurementUnit>, IMeasurementUnitDomainService
    {
        /// <summary>
        ///     Creates a new instance of an WageScale.
        /// </summary>
        /// <returns>A new instance of type <see cref="IConvertibleEntity" />.</returns>
        public override IMeasurementUnit Create()
        {
            IMeasurementUnit measurementUnit = base.Create(); 
            measurementUnit.Name = Resources.NewMeasurementUnit;
            measurementUnit.Description = Resources.NewMeasurementUnitDescription;
            measurementUnit.Letters = Resources.DefaultLettersMeasurementUnit;

            return measurementUnit;
        }
    }
}
