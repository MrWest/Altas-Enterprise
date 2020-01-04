using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget
{
    public class ExecutedSubSpecialityHolderRepository:SubSpecialityHolderRepository<IExecutedSubSpecialityHolder>,IExecutedSubSpecialityHolderRepository
    {
        public ExecutedSubSpecialityHolderRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }

    public class ExecutedSubSpecialityHolderRepositoryEF : SubSpecialityHolderRepositoryEF<IExecutedSubSpecialityHolder, ExecutedSubSpecialityHolder>, IExecutedSubSpecialityHolderRepository
    {
        public ExecutedSubSpecialityHolderRepositoryEF(IEntityFrameworkDbContext<ExecutedSubSpecialityHolder> databaseContext) : base(databaseContext)
        {
        }
    }
}