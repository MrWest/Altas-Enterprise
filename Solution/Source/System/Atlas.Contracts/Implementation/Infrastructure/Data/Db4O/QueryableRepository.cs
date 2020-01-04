using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O
{
    public class QueryableRepository<TEntity>: CommonRepository<TEntity>, IQueryableRepository<TEntity>
         where TEntity : class, IEntity
    {

        public QueryableRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
            //_databaseContext.Query<TEntity>(new Predicate<TEntity>( x=> x.))
        }

        /// <summary>
        /// Gets all the entities from the data source.
        /// </summary>
        public override IEnumerable<TEntity> Entities
        {
            get
            {
                return Query!=null? Clone(_databaseContext.Query<TEntity>(Query)).ToArray():null;
            }


        }

        public Predicate<TEntity> Query { get; set; }
    }
}