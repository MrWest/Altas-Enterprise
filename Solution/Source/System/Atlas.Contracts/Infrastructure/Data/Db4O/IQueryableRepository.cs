using System;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O
{
    public interface IQueryableRepository<TEntity>: IRepository<TEntity>
         where TEntity : class, IEntity
    {
        Predicate<TEntity> Query { get; set; }
    }
}