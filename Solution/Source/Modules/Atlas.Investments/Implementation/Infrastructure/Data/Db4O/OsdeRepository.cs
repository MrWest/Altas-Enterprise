using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    /// <summary>
    ///     Implementation of the contract <see cref="IOsdeRepository" /> used to manage the data operations in OSDE domain
    ///     entities.
    /// </summary>
    public class OsdeRepository : CodedNomenclatorRepositoryBase<IOsde>, IOsdeRepository
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="OsdeRepository" /> given a database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext" /> to perform the raw data operations.</param>
        public OsdeRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }

    /// <summary>
    ///     Implementation of the contract <see cref="IOsdeRepository" /> used to manage the data operations in OSDE domain
    ///     entities.
    /// </summary>
    public class OsdeRepositoryEF : CodedNomenclatorRepositoryBaseEF<IOsde, Osde>, IOsdeRepository
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="OsdeRepository" /> given a database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext" /> to perform the raw data operations.</param>
        public OsdeRepositoryEF(IEntityFrameworkDbContext<Osde> databaseContext)
            : base(databaseContext)
        {
        }
    }
}