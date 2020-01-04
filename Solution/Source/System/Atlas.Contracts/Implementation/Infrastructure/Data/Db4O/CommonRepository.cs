using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Domain.Specification.EntityFramework;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O
{
    public class CommonRepository<TEntity> : Db4ORepositoryBase<TEntity>,ICommonRepository<TEntity> 
        where TEntity : class, IEntity
    {
        public CommonRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
              //  var atribs = this.GetType().GetGenericArguments();
                //if (atribs.Count() > 0)
                //{
                    var rslt = base.RelevantProperties.ToArray();
                    var entity = ServiceLocator.Current.GetInstance<TEntity>();

                    foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties().Where(p => p.SetMethod != null))
                    {
                        if (typeof(IEntity).GetProperties().All(x=>x.Name!=propertyInfo.Name))
                              rslt =  rslt.Concat( new[] { propertyInfo.Name }).ToArray();
                    }

                    return rslt;
                //}
                //return base.RelevantProperties;
            }
        }

        public int Count(ISpecification<TEntity> specification)
        {
            throw new NotImplementedException();
        }

        public int Count(IQueryable<TEntity> queryable)
        {
            throw new NotImplementedException();
        }

        public bool Exist(ISpecification<TEntity> specification)
        {
            throw new NotImplementedException();
        }

        public bool Exist(IQueryable<TEntity> queryable)
        {
            throw new NotImplementedException();
        }

       
        public IDatabaseContext DbContext { get; }
        public IEnumerable<TEntity> WhereSQL(IEntityFrameworkQuerable<TEntity> querable)
        {
            throw new NotImplementedException();
        }
    }

    public class CommonRepositoryEF<TEntity, TClass> : EntityFrameworkRepositoryBase<TEntity, TClass>, ICommonRepository<TEntity>
       where TEntity : class, IEntity
         where TClass : EntityBase
    {
        public CommonRepositoryEF(IEntityFrameworkDbContext<TClass> databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
                //  var atribs = this.GetType().GetGenericArguments();
                //if (atribs.Count() > 0)
                //{
                var rslt = base.RelevantProperties.ToArray();
                var entity = ServiceLocator.Current.GetInstance<TEntity>();

                foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties().Where(p => p.SetMethod != null))
                {
                    if (typeof(IEntity).GetProperties().All(x => x.Name != propertyInfo.Name))
                        rslt = rslt.Concat(new[] { propertyInfo.Name }).ToArray();
                }

                return rslt;
                //}
                //return base.RelevantProperties;
            }
        }

        public int Count(ISpecification<TEntity> specification)
        {
            return DatabaseContext.Entities.Count();
        }

        public int Count(IQueryable<TEntity> queryable)
        {
            return queryable.Count();

        }

        public bool Exist(ISpecification<TEntity> specification)
        {
            return DatabaseContext.Entities.Any();
        }

        public bool Exist(IQueryable<TEntity> queryable)
        {
            return queryable.Any();
        }

      
        public IDatabaseContext DbContext { get { return DatabaseContext; } }
        public IEnumerable<TEntity> WhereSQL(IEntityFrameworkQuerable<TClass> querable)
        {
            return WhereSQL(querable);
        }
    }

    public class CommonRepositoryInheritanceEF<TEntity, TClass, TClass2> : EntityFrameworkRepositoryBase<TEntity, TClass>, ICommonRepository<TEntity>
       where TEntity : class, IEntity
         where TClass : EntityBase
         where TClass2 : TClass
    {
        public CommonRepositoryInheritanceEF(IEntityFrameworkDbContext<TClass> databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
                //  var atribs = this.GetType().GetGenericArguments();
                //if (atribs.Count() > 0)
                //{
                var rslt = base.RelevantProperties.ToArray();
                var entity = ServiceLocator.Current.GetInstance<TEntity>();

                foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties().Where(p => p.SetMethod != null))
                {
                    if (typeof(IEntity).GetProperties().All(x => x.Name != propertyInfo.Name))
                        rslt = rslt.Concat(new[] { propertyInfo.Name }).ToArray();
                }

                return rslt;
                //}
                //return base.RelevantProperties;
            }
        }

        public override TEntity Find(object id)
        {
            var entity =  base.Find(id);
            if (entity != null)
                return entity;
            
            IEntityFrameworkDbContext<TClass2> EntityFrameworkDbContext2 =
           ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<TClass2>>();

            entity = EntityFrameworkDbContext2.Find(id) as TClass as TEntity;

            return entity;
        }
        
        public int Count(ISpecification<TEntity> specification)
        {
            return DatabaseContext.Entities.Count();
        }

        public int Count(IQueryable<TEntity> queryable)
        {
            return queryable.Count();

        }

        public bool Exist(ISpecification<TEntity> specification)
        {
            return DatabaseContext.Entities.Any();
        }

        public bool Exist(IQueryable<TEntity> queryable)
        {
            return queryable.Any();
        }


        public IDatabaseContext DbContext { get { return DatabaseContext; } }
        public IEnumerable<TEntity> WhereSQL(IEntityFrameworkQuerable<TEntity> querable)
        {
            throw new NotImplementedException();
        }
    }
}
