using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    /// <summary>
    /// Implementation of the çontract <see cref="IOtherExpensesExecutedResourceRepository"/>, representing the repository that
    /// is used to handle the Data operations regarding the <see cref="IExecutedResource"/> of a certain
    /// <see cref="IOtherExpensesComponent"/>.
    /// </summary>
    public class OtherExpensesExecutedResourceRepository :
        ExecutedResourceRepositoryBase<IOtherExpensesComponent>,
        IOtherExpensesExecutedResourceRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="OtherExpensesExecutedResourceRepository"/> with a
        /// <see cref="IDb4ODatabaseContext"/>.
        /// </summary>
        /// <param name="databaseContext">
        /// The instance of <see cref="IDb4ODatabaseContext"/> that carries on the actual raw data operations the
        /// initializing repository performs.
        /// </param>
        public OtherExpensesExecutedResourceRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
