using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Domain
{
    /// <summary>
    ///     Contract to be implemented by the domain services used to enforce the business operations for Currency domain entities.
    /// </summary>
    public interface ICurrencyDomainService: IConvertibleEntityDomainService<ICurrency>
    {
    }
}
