using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Application.Common
{
    /// <summary>
    /// defines an application service for a measurement unit entity
    /// </summary>
    public class MeasurementUnitManagerApplicationServices: ConvertibleEntityManagerApplicationServices<IMeasurementUnit,IMeasurementUnitDomainService>,IMeasurementUnitManagerApplicationServices
    {
    }
}
