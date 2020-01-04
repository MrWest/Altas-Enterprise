using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Application.Common
{
    /// <summary>
    /// Deescribes an applicationservice for  measurement unit entities
    /// </summary>
    public interface IMeasurementUnitManagerApplicationServices : IConvertibleEntityManagerApplicationServices<IMeasurementUnit>
    {
    }
}
