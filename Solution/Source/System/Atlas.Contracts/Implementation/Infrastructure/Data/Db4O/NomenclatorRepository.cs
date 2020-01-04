using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Domain.Comparer;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O
{
    //public class NomenclatorRepository<T> : Db4ORepositoryBase<T>, INomenclatorRepository<T>
    //    where T :class , ICodedNomenclator
    //{
    //    public NomenclatorRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
    //    {
    //    }
    //    protected override string[] RelevantProperties
    //    {
    //        get
    //        {
    //            return base.RelevantProperties.Concat(new[]
    //            {
    //             GetName(x => x.Name),GetName(x => x.Description),GetName(x => x.Code)
    //            }).ToArray();
    //        }
    //    }

    //    T IRepository<T>.Add(T entity)
    //    {
    //        return entity;
    //    }

    //    void IRepository<T>.Update(T item)
    //    {
    //        ;
    //    }

    //    void IRepository<T>.Delete(T entity)
    //    {
    //        ;
    //    }
    //    public override IEnumerable<T> Entities
    //    {
    //        get
    //        {
    //            ISpecification<T> specification = new NomenclatorByContainsSpecification<T>(Text, AddedExpression);

             
    //            return Where(specification).Distinct(new CodeNomenclatorComaprer<T>()).Take(MaxNumber);
    //        }
    //    }

    //    public string Text { get; set; }

    //    public int MaxNumber { get; set; }
    //    public Expression<Predicate<T>> AddedExpression { get; set; }
    //}

    public class NomenclatorRepositoryEF<T, TClass> : EntityFrameworkRepositoryBase<T, TClass>, INomenclatorRepository<T>
       where T : class, ICodedNomenclator
        where TClass : CodedNomenclatorBase
    {
        public NomenclatorRepositoryEF(IEntityFrameworkDbContext<TClass> databaseContext) : base(databaseContext)
        {
        }
        protected override string[] RelevantProperties
        {
            get
            {
                if (AddedExpression == null)
                    return base.RelevantProperties.Concat(new[]
                    {
                 GetName(x => x.Name),GetName(x => x.Description),GetName(x => x.Code)
                }).ToArray();

                return base.RelevantProperties.Concat(new[]
                {
                 GetName(x => x.Name),GetName(x => x.Description),GetName(x => x.Code)
                }).ToArray().Concat(new[] { AddedExpression.Item2 }).ToArray();
            }
        }

        T IRepository<T>.Add(T entity)
        {
            return entity;
        }

        void IRepository<T>.Update(T item)
        {
            ;
        }

        void IRepository<T>.Delete(T entity)
        {
            ;
        }
        public override IEnumerable<T> Entities
        {
            get
            {
                var specification = new NomenclatorByContainsQueryable<TClass>(Text, DatabaseContext);

                var rstlList = Where(specification).Distinct(new CodeNomenclatorComaprer<T>());

                if (AddedExpression != null)
                    rstlList = rstlList.Where(AddedExpression.Item1);

                return rstlList.Take(MaxNumber);
           
               
            }
        }

        public string Text { get; set; }

        public int MaxNumber { get; set; }
        public Tuple<Func<T, bool>, string> AddedExpression { get; set; }
    }

    public class NomenclatorRepositoryInheritanceEF<T, TClass, TClass2> : EntityFrameworkRepositoryBase<T, TClass>, INomenclatorRepository<T>
      where T : class, ICodedNomenclator
       where TClass : CodedNomenclatorBase
         where TClass2 : TClass
    {
        public NomenclatorRepositoryInheritanceEF(IEntityFrameworkDbContext<TClass> databaseContext) : base(databaseContext)
        {
        }
        protected override string[] RelevantProperties
        {
            get
            {
                if(AddedExpression==null)
                return base.RelevantProperties.Concat(new[]
                {
                 GetName(x => x.Name),GetName(x => x.Description),GetName(x => x.Code)
                }).ToArray();

                return base.RelevantProperties.Concat(new[]
                {
                 GetName(x => x.Name),GetName(x => x.Description),GetName(x => x.Code)
                }).ToArray().Concat(new [] {AddedExpression.Item2}).ToArray();
            }
        }

        T IRepository<T>.Add(T entity)
        {
            return entity;
        }

        void IRepository<T>.Update(T item)
        {
            ;
        }

        void IRepository<T>.Delete(T entity)
        {
            ;
        }
        public override IEnumerable<T> Entities
        {
            get
            {
                var specification = new NomenclatorByContainsQueryable<TClass>(Text, DatabaseContext);


                var firstlist =  Where(specification).Distinct(new CodeNomenclatorComaprer<T>());

                
                var specification2 = new NomenclatorByContainsQueryable<TClass2>(Text, EntityFrameworkDbContext2);

             //   var sqlstring = specification2.Query.ToString();

                var secondlist = Where(specification2, EntityFrameworkDbContext2).Distinct(new CodeNomenclatorComaprer<T>());
                //.Take(MaxNumber);

                var rstlList = secondlist.Concat(firstlist);

                if (AddedExpression != null)
                {
                   
                    rstlList = rstlList.Where(AddedExpression.Item1);
                }
                

                return rstlList. Take(MaxNumber);
            }
        }

        private IEnumerable<T> Where(NomenclatorByContainsQueryable<TClass2> specification2, IEntityFrameworkDbContext<TClass2> entityFrameworkDbContext2)
        {
            if (specification2 == null)
                throw new ArgumentNullException("specification2");

            return Clone(entityFrameworkDbContext2.Where(specification2.Query).ToArray());
        }

        private IEnumerable<T> Clone(TClass2[] entities)
        {
            return (entities ?? new TClass2[0])
               .Aggregate(new List<T>(), (list, entity) =>
               {
                   T clone = Clone(entity as TClass) as T;
                   list.Add(clone);
                   return list;
               })
               .ToArray();
        }

  
        private IEntityFrameworkDbContext<TClass2> EntityFrameworkDbContext2 =
            ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<TClass2>>();
        public string Text { get; set; }

        public int MaxNumber { get; set; }
        public Tuple<Func<T, bool>, string> AddedExpression { get; set; }
    }
}