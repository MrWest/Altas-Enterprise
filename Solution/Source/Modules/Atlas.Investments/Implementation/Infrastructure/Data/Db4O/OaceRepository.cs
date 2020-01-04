using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    /// <summary>
    ///     Implementation of the contract <see cref="IOaceRepository" /> used to manage the data operations in OACE domain
    ///     entities.
    /// </summary>
    public class OaceRepository : CodedNomenclatorRepositoryBase<IOace>, IOaceRepository
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="OaceRepository" /> given a database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext" /> to perform the raw data operations.</param>
        public OaceRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }

    /// <summary>
    ///     Implementation of the contract <see cref="IOaceRepository" /> used to manage the data operations in OACE domain
    ///     entities.
    /// </summary>
    public class OaceRepositoryEF : CodedNomenclatorRepositoryBaseEF<IOace, Oace>, IOaceRepository
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="OaceRepository" /> given a database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext" /> to perform the raw data operations.</param>
        public OaceRepositoryEF(IEntityFrameworkDbContext<Oace> databaseContext)
            : base(databaseContext)
        {
        }
    }
}