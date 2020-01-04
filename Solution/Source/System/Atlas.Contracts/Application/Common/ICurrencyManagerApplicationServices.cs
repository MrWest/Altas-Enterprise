using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Application.Common
{
    /// <summary>
    /// Deescribes an applicationservice for  currency entities
    /// </summary>
    public interface ICurrencyManagerApplicationServices : IConvertibleEntityManagerApplicationServices<ICurrency>
    {
    }
}
