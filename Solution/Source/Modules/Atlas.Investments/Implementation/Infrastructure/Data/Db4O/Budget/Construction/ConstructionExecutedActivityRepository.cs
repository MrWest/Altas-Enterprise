using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Construction;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget.Construction
{
    /// <summary>
    /// Implementation of the çontract <see cref="IConstructionExecutedActivityRepository"/>, representing the repository that
    /// is used to handle the Data operations regarding the <see cref="IExecutedActivity"/> of a certain
    /// <see cref="IConstructionComponent"/>.
    /// </summary>
    public class ConstructionExecutedActivityRepository : 
        ExecutedActivityRepositoryBase,
        IConstructionExecutedActivityRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConstructionExecutedActivityRepository"/> with a
        /// <see cref="IDb4ODatabaseContext"/>.
        /// </summary>
        /// <param name="databaseContext">
        /// The instance of <see cref="IDb4ODatabaseContext"/> that carries on the actual raw data operations the
        /// initializing repository performs.
        /// </param>
        public ConstructionExecutedActivityRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
