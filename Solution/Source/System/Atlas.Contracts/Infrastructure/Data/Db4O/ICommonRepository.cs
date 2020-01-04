using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Domain.Specification.EntityFramework;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O
{
    public interface ICommonRepository<TEntity>: IRepository<TEntity>
        where TEntity: class, IEntity
    {
        int Count(ISpecification<TEntity> specification);
        int Count(IQueryable<TEntity> queryable);
        bool Exist(ISpecification<TEntity> specification);
        bool Exist(IQueryable<TEntity> queryable);
        IDatabaseContext DbContext { get; }
    }
}
