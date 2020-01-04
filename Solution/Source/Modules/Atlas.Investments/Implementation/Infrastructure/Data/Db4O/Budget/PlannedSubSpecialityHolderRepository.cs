using System.Linq;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget
{
    public class PlannedSubSpecialityHolderRepository:SubSpecialityHolderRepository<IPlannedSubSpecialityHolder>,IPlannedSubSpecialityHolderRepository
    {
        public PlannedSubSpecialityHolderRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }

    public class PlannedSubSpecialityHolderRepositoryEF : SubSpecialityHolderRepositoryEF<IPlannedSubSpecialityHolder, PlannedSubSpecialityHolder>, IPlannedSubSpecialityHolderRepository
    {

        /// <summary>
        /// Gets the properties which changes must be saved.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
               {
                    GetName(x=>x.Execution)
                }).ToArray();
                //,GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.ExpenseConcept)
            }
        }

        public PlannedSubSpecialityHolderRepositoryEF(IEntityFrameworkDbContext<PlannedSubSpecialityHolder> databaseContext) : base(databaseContext)
        {
        }
    }
}