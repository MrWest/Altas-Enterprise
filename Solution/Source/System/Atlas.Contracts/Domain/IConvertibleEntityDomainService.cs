using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Domain
{
    /// <summary>
    ///     Contract to be implemented by the domain services used to enforce the business operations for Convertible domain entities.
    /// </summary>
    public interface IConvertibleEntityDomainService<TEntity>: IDomainServices<TEntity>
         where TEntity : class, IConvertibleEntity
    {
    }
}
