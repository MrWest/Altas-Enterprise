using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.OtherExpenses;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget.OtherExpenses
{
    /// <summary>
    /// Implementation of the çontract <see cref="IOtherExpensesPlannedActivityRepository"/>, representing the repository that
    /// is used to handle the Data operations regarding the <see cref="IPlannedActivity"/> of a certain
    /// <see cref="IOtherExpensesComponent"/>.
    /// </summary>
    public class OtherExpensesPlannedActivityRepository : 
        PlannedActivityRepositoryBase,
        IOtherExpensesPlannedActivityRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="OtherExpensesPlannedActivityRepository"/> with a
        /// <see cref="IDb4ODatabaseContext"/>.
        /// </summary>
        /// <param name="databaseContext">
        /// The instance of <see cref="IDb4ODatabaseContext"/> that carries on the actual raw data operations the
        /// initializing repository performs.
        /// </param>
        public OtherExpensesPlannedActivityRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
