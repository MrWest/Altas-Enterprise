using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    /// <summary>
    ///     Implements some handling data operations for
    ///     Price system domain entities.
    /// </summary>
    public class PriceSystemRepository : Db4ORepositoryBase<IPriceSystem>, IPriceSystemRepository
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="IPriceSystem" /> given a database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext" /> to perform the raw data operations.</param>
        public PriceSystemRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        /// <summary>
        ///     Gets all the public properties non-readonly properties that are relevant to the current repository when making its
        ///     operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Name), GetName(x => x.Description),GetName(x=>x.IsActive)
                }).ToArray();
            }
        }

        public void Delete(IPriceSystem priceSystem)
        {
            if (priceSystem == null)
                throw new ArgumentNullException("priceSystem");

            var dbPriceSystem = DatabaseContext.Find<IPriceSystem>(priceSystem.Id);
            var overRepo = ServiceLocator.Current.GetInstance<IOverGroupRepository>();
            overRepo.PriceSystem = dbPriceSystem;
            overRepo.DeleteAll();

            DatabaseContext.Delete((object)dbPriceSystem.OverGroups);
            DatabaseContext.Delete(dbPriceSystem);
        }
        /// <summary>
        /// Gets all the independent investment elements (<see cref="IInvestmentElement"/>) if the there is no parent defined, otherwise
        /// returns the investment element being direct childs of the one defined in the Parent property.
        /// </summary>
        public override IEnumerable<IPriceSystem> Entities
        {
            get
            {
              
                return Where( new Specification<IPriceSystem>((x=> x.GetType()==typeof(PriceSystem)) ));
            }
        }
    }

    /// <summary>
    ///     Implements some handling data operations for
    ///     Price system domain entities.
    /// </summary>
    public class PriceSystemRepositoryEF : EntityFrameworkRepositoryBase<IPriceSystem, PriceSystem>, IPriceSystemRepository
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="IPriceSystem" /> given a database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext" /> to perform the raw data operations.</param>
        public PriceSystemRepositoryEF(IEntityFrameworkDbContext<PriceSystem> databaseContext)
            : base(databaseContext)
        {
        }

        /// <summary>
        ///     Gets all the public properties non-readonly properties that are relevant to the current repository when making its
        ///     operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Name), GetName(x => x.Description),GetName(x=>x.IsActive)
                }).ToArray();
            }
        }

        public void Delete(IPriceSystem priceSystem)
        {
            if (priceSystem == null)
                throw new ArgumentNullException("priceSystem");

            var dbPriceSystem = DatabaseContext.Find<IPriceSystem>(priceSystem.Id);
            var overRepo = ServiceLocator.Current.GetInstance<IOverGroupRepository>();
            overRepo.PriceSystem = dbPriceSystem;
            overRepo.DeleteAll();

           // DatabaseContext.Delete((object)dbPriceSystem.OverGroups);
            DatabaseContext.Delete(dbPriceSystem);
        }
        /// <summary>
        /// Gets all the independent investment elements (<see cref="IInvestmentElement"/>) if the there is no parent defined, otherwise
        /// returns the investment element being direct childs of the one defined in the Parent property.
        /// </summary>
        public override IEnumerable<IPriceSystem> Entities
        {
            get
            {

                return Where(new EntityFrameworkQueryable<PriceSystem>( DatabaseContext));
            }
        }
    }

}
