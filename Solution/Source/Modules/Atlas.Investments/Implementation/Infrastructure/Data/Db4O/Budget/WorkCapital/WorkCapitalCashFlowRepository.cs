using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget.WorkCapital
{
    public class WorkCapitalCashFlowRepository: EntityFrameworkRepositoryBase<IWorkCapitalCashFlow, WorkCapitalCashFlow>, IWorkCapitalCashFlowRepository
    {
        public WorkCapitalCashFlowRepository(IEntityFrameworkDbContext<WorkCapitalCashFlow> databaseContext) : base(databaseContext)
        {
        }

        public IWorkCapitalComponent WorkCapitalComponent { get; set; }

        /// <summary>
        /// Gets the properties which changes must be saved.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Starts), GetName(x => x.Ends),GetName(x => x.IsWorkCapitalCalculated), GetName(x => x.CalculatedWorkCapital), GetName(x => x.DateTimeScale)
                }).ToArray();
            }
        }

    }
}