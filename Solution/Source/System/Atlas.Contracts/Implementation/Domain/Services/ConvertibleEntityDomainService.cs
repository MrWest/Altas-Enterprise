using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
    /// <summary>
    ///     Implementation of the contract <see cref="IConvertibleEntityDomainService" /> used to enforce the business rules in Convertibles domain
    ///     entities.
    /// </summary>
    public class ConvertibleEntityDomainService<TEntity> : DomainServicesBase<TEntity>, IConvertibleEntityDomainService<TEntity>
         where TEntity : class, IConvertibleEntity
    {
        /// <summary>
        ///     Creates a new instance of an WageScale.
        /// </summary>
        /// <returns>A new instance of type <see cref="IConvertibleEntity" />.</returns>
        public override TEntity Create()
        {
            TEntity convertibleEntity = base.Create();

            return convertibleEntity;
        }
    }
}
