using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    /// <summary>
    ///     Implementation of the contract <see cref="IConvertibleEntityRepository" /> used to manage the data operations in Convertibles domain
    ///     entities.
    /// </summary>
    public class ConvertibleEntityRepository<TEntity> : Db4ORepositoryBase<TEntity>, IConvertibleEntityRepository<TEntity>
     where TEntity : class,  IConvertibleEntity
    {
         /// <summary>
        ///     Initializes a new instance of <see cref="CategoryRepository" /> given a database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext" /> to perform the raw data operations.</param>
        public ConvertibleEntityRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        
        /// <summary>
        ///     Gets the properties of the investment elements to save its values.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Name), GetName(x => x.Description), GetName(x => x.Letters)
                }).ToArray();
            }
        }

        /// <summary>
        ///     Gets all the independent investment elements (<see cref="IInvestmentComponent" />) if the there is no parent
        ///     defined, otherwise returns the investment element being direct childs of the one defined in the InvestmentElement property.
        /// </summary>
        public override IEnumerable<TEntity> Entities
        {
            get
            {


                var specification= new Specification<TEntity>();
                
               
             //   Predicate.GroupBy<TEntity, object>(x => x.Id);
              //  var crap = Where(new Specification<TEntity>()).Distinct()<TEntity, object>(x => x.Id); 
                return Where(new Specification<TEntity>()).Distinct();
            }
        }

    }


    /// <summary>
    ///     Implementation of the contract <see cref="IConvertibleEntityRepository" /> used to manage the data operations in Convertibles domain
    ///     entities.
    /// </summary>
    public class ConvertibleEntityRepositoryEF<TEntity, TClass> : EntityFrameworkRepositoryBase<TEntity,TClass>, IConvertibleEntityRepository<TEntity>
     where TEntity : class, IConvertibleEntity
        where TClass: ConvertibleEntity
        {
        /// <summary>
        ///     Initializes a new instance of <see cref="CategoryRepository" /> given a database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext" /> to perform the raw data operations.</param>
        public ConvertibleEntityRepositoryEF(IEntityFrameworkDbContext<TClass> databaseContext)
            : base(databaseContext)
        {
        }


        /// <summary>
        ///     Gets the properties of the investment elements to save its values.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Name), GetName(x => x.Description), GetName(x => x.Letters), GetName(x => x.OwnerEntityId)
                }).ToArray();
            }
        }

        /// <summary>
        ///     Gets all the independent investment elements (<see cref="IInvestmentComponent" />) if the there is no parent
        ///     defined, otherwise returns the investment element being direct childs of the one defined in the InvestmentElement property.
        /// </summary>
        public override IEnumerable<TEntity> Entities
        {
            get
            {


                var queryable = new EntityFrameworkQueryable<TClass>(DatabaseContext);


                //   Predicate.GroupBy<TEntity, object>(x => x.Id);
                //  var crap = Where(new Specification<TEntity>()).Distinct()<TEntity, object>(x => x.Id); 
                return Where(queryable).Distinct();
            }
        }

    }
}
