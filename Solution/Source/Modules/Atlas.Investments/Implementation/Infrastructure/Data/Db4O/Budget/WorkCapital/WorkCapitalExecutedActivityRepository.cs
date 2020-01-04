using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget.WorkCapital
{
    /// <summary>
    /// Implementation of the çontract <see cref="IWorkCapitalExecutedActivityRepository"/>, representing the repository that
    /// is used to handle the Data operations regarding the <see cref="IExecutedActivity"/> of a certain
    /// <see cref="IWorkCapitalComponent"/>.
    /// </summary>
    public class WorkCapitalExecutedActivityRepository :
        ExecutedActivityRepositoryBase,IWorkCapitalExecutedActivityRepository

    {
        /// <summary>
        /// Initializes a new instance of <see cref="WorkCapitalExecutedActivityRepository"/> with a
        /// <see cref="IDb4ODatabaseContext"/>.
        /// </summary>
        /// <param name="databaseContext">
        /// The instance of <see cref="IDb4ODatabaseContext"/> that carries on the actual raw data operations the
        /// initializing repository performs.
        /// </param>
        public WorkCapitalExecutedActivityRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
