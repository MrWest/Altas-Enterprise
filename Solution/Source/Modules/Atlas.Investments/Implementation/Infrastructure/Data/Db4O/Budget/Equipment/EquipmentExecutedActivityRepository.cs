using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Equipment;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget.Equipment
{
    /// <summary>
    /// Implementation of the çontract <see cref="IEquipmentExecutedActivityRepository"/>, representing the repository that
    /// is used to handle the Data operations regarding the <see cref="IExecutedActivity"/> of a certain
    /// <see cref="IEquipmentComponent"/>.
    /// </summary>
    public class EquipmentExecutedActivityRepository : 
        ExecutedActivityRepositoryBase,
        IEquipmentExecutedActivityRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EquipmentExecutedActivityRepository"/> with a
        /// <see cref="IDb4ODatabaseContext"/>.
        /// </summary>
        /// <param name="databaseContext">
        /// The instance of <see cref="IDb4ODatabaseContext"/> that carries on the actual raw data operations the
        /// initializing repository performs.
        /// </param>
        public EquipmentExecutedActivityRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
