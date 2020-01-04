using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Application.Common
{
    /// <summary>
    /// defines an application service for a currency entity
    /// </summary>
    public class CurrencyManagerApplicationServices : ConvertibleEntityManagerApplicationServices<ICurrency,ICurrencyDomainService>, ICurrencyManagerApplicationServices
    {
    }
}
