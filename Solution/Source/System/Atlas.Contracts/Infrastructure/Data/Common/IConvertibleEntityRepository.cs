using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data.Common
{
    /// <summary>
    ///     Contract to be implemented by the repositories to be implemented by the repositories handling data operations for
    ///     Convertibles domain entities.
    /// </summary>
    public interface IConvertibleEntityRepository<TEntity> : IRepository<TEntity>
      where TEntity:class,  IConvertibleEntity
    {
    }


}
