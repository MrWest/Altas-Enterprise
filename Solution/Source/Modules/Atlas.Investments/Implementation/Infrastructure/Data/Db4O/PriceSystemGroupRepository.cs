using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    public abstract class PriceSystemGroupRepository<TGroup> : CodedNomenclatorRepositoryBase<TGroup>, IPriceSystemGroupRepository<TGroup>
        where TGroup : class, IPriceSystemGroup
    {
        public PriceSystemGroupRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        /// <summary>
        ///     Gets all the public properties non-readonly properties of the coded nomenclators that are relevant to the current
        ///     repository when making its operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Code), GetName(x => x.Name), GetName(x => x.Description), GetName(x => x.PriceSystem)
                }).ToArray();
            }
        }
        //public abstract void Relate(TGroup current, IPriceSystemGroup other);

        //public abstract void Unrelate(TGroup current, IPriceSystemGroup other);

        //public virtual void SaveReference(TGroup current)
        //{
        //    if (current == null)
        //        throw new ArgumentNullException("TGroup");

        //    if (Equals(current.Id, default(Guid)))
        //        return;

        //    DatabaseContext.Store(current);
           
        //}

        //public void SaveReference(IPriceSystemGroup other)
        //{
        // //   DatabaseContext.Store(other);
        //}
    }

    public abstract class PriceSystemGroupRepositoryEF<TGroup,TClass> : CodedNomenclatorRepositoryBaseEF<TGroup, TClass>, IPriceSystemGroupRepository<TGroup>
       where TGroup : class, IPriceSystemGroup
         where TClass : PriceSystemGroup
    {
        public PriceSystemGroupRepositoryEF(IEntityFrameworkDbContext<TClass> databaseContext) : base(databaseContext)
        {
        }

        /// <summary>
        ///     Gets all the public properties non-readonly properties of the coded nomenclators that are relevant to the current
        ///     repository when making its operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Code), GetName(x => x.Name), GetName(x => x.Description), GetName(x => x.PriceSystem)
                }).ToArray();
            }
        }
        //public abstract void Relate(TGroup current, IPriceSystemGroup other);

        //public abstract void Unrelate(TGroup current, IPriceSystemGroup other);

        //public virtual void SaveReference(TGroup current)
        //{
        //    if (current == null)
        //        throw new ArgumentNullException("TGroup");

        //    if (Equals(current.Id, default(Guid)))
        //        return;

        //    DatabaseContext.Store(current);

        //}

        //public void SaveReference(IPriceSystemGroup other)
        //{
        // //   DatabaseContext.Store(other);
        //}
    }
}
