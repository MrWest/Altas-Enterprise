using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    /// <summary>
    ///     Implementation of the contract <see cref="IPhaseRepository" /> used to manage the data operations in Phase domain
    ///     entities.
    /// </summary>
    public class PhaseRepository : CodedNomenclatorRepositoryBase<IPhase>, IPhaseRepository
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="PhaseRepository" /> given a database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext" /> to perform the raw data operations.</param>
        public PhaseRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }

    /// <summary>
    ///     Implementation of the contract <see cref="IPhaseRepository" /> used to manage the data operations in Phase domain
    ///     entities.
    /// </summary>
    public class PhaseRepositoryEF : CodedNomenclatorRepositoryBaseEF<IPhase, Phase>, IPhaseRepository
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="PhaseRepository" /> given a database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext" /> to perform the raw data operations.</param>
        public PhaseRepositoryEF(IEntityFrameworkDbContext<Phase> databaseContext)
            : base(databaseContext)
        {
        }
    }
}