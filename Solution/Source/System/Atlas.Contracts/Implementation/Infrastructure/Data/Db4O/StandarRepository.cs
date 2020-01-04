using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O
{
    public class StandarDb4ORepository<TEntity> : Db4ORepositoryBase<TEntity>,IRepository<TEntity>
        where TEntity:class ,IEntity
    {
        public StandarDb4ORepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        /// <summary>
        /// Clones the given object.
        /// </summary>
        /// <param name="entity">The object of type <typeparamref name="T"/> to clone.</param>
        /// <returns>A clone of <paramref name="entity"/> with the same value of it in the relevant properties.</returns>
        protected override TEntity Clone(TEntity entity)
        {
            if (Equals(entity, null))
                return default(TEntity);

            var copy = ServiceLocator.Current.GetInstance<TEntity>();
            string[] properties = new string[0];

            foreach (var prop in entity.GetType().GetProperties())
            {
                if(prop.Name!="FullName")
                properties = properties.Concat(new[] { prop.Name }).ToArray();
            }

            entity.UpdateProperties(copy, properties);

            return copy;
        }
    }

    
}
