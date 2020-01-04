using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Construction;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget.Construction
{
    /// <summary>
    /// Implementation of the çontract <see cref="IConstructionExecutedResourceRepository"/>, representing the repository that
    /// is used to handle the Data operations regarding the <see cref="IExecutedResource"/> of a certain
    /// <see cref="IConstructionComponent"/>.
    /// </summary>
    public class ConstructionExecutedResourceRepository : 
        ExecutedResourceRepositoryBase<IConstructionComponent>,
        IConstructionExecutedResourceRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConstructionExecutedResourceRepository"/> with a
        /// <see cref="IDb4ODatabaseContext"/>.
        /// </summary>
        /// <param name="databaseContext">
        /// The instance of <see cref="IDb4ODatabaseContext"/> that carries on the actual raw data operations the
        /// initializing repository performs.
        /// </param>
        public ConstructionExecutedResourceRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
