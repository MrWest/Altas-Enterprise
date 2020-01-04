using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities.Reporting;
using CompanyName.Atlas.Investments.Infrastructure.Data.Reporting;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Reporting
{
    
    public class InvestmentChildCustomReportSettingsRepository: ChildCustomReportSettingsRepository<IInvestmentChildCustomReportSettings>, IInvestmentChildCustomReportSettingsRepository
    {
        public InvestmentChildCustomReportSettingsRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                     GetName(x => x.ShowInvestmentElements), GetName(x => x.ShowBudgetComponents),
                     GetName(x => x.ShowEquipment), GetName(x => x.ShowConstruction),
                     GetName(x => x.ShowOthers), GetName(x => x.ShowWorkCapital),
                     GetName(x => x.ShowSubSpecialities), GetName(x => x.ShowActivities), GetName(x => x.ShowResources),
                     GetName(x => x.ShowMU), GetName(x => x.ShowQuantity), GetName(x => x.ShowCurrency),
                     GetName(x => x.ShowUC), GetName(x => x.ShowCost),  GetName(x => x.ShowUC), GetName(x => x.ShowSubExpeseConcepts), GetName(x => x.ShowUC), GetName(x => x.ShowCategories)
                }).ToArray();
            }
        }

        public override void Delete(IInvestmentChildCustomReportSettings entity)
        {
            var repo = ServiceLocator.Current.GetInstance<IInvestmentChildCustomReportSettingsRepository>();
            repo.Parent = entity;
            repo.DeleteAll();
            base.Delete(entity);
        }
    }

    public class InvestmentMainCustomReportSettingsRepository : MainCustomReportSettingsRepository<IInvestmentMainCustomReportSettings>, IInvestmentMainCustomReportSettingsRepository
    {
        public InvestmentMainCustomReportSettingsRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }


    public class InvestmentChildCustomReportSettingsRepositoryEF : ChildCustomReportSettingsRepositoryEF<IInvestmentChildCustomReportSettings, InvestmentChildCustomReportSettings>, IInvestmentChildCustomReportSettingsRepository
    {
        public InvestmentChildCustomReportSettingsRepositoryEF(IEntityFrameworkDbContext<InvestmentChildCustomReportSettings> databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                     GetName(x => x.ShowInvestmentElements), GetName(x => x.ShowBudgetComponents),
                     GetName(x => x.ShowEquipment), GetName(x => x.ShowConstruction),
                     GetName(x => x.ShowOthers), GetName(x => x.ShowWorkCapital),
                     GetName(x => x.ShowSubSpecialities), GetName(x => x.ShowActivities), GetName(x => x.ShowResources),
                     GetName(x => x.ShowMU), GetName(x => x.ShowQuantity), GetName(x => x.ShowCurrency),
                     GetName(x => x.ShowUC), GetName(x => x.ShowCost),  GetName(x => x.ShowUC), GetName(x => x.ShowSubExpeseConcepts), GetName(x => x.ShowUC), GetName(x => x.ShowCategories)
                }).ToArray();
            }
        }

        public override void Delete(IInvestmentChildCustomReportSettings entity)
        {
            var repo = ServiceLocator.Current.GetInstance<IInvestmentChildCustomReportSettingsRepository>();
            repo.Parent = entity;
            repo.DeleteAll();
            base.Delete(entity);
        }
    }

    public class InvestmentMainCustomReportSettingsRepositoryEF : MainCustomReportSettingsRepositoryEF<IInvestmentMainCustomReportSettings, InvestmentMainCustomReportSettings>, IInvestmentMainCustomReportSettingsRepository
    {
        public InvestmentMainCustomReportSettingsRepositoryEF(IEntityFrameworkDbContext<InvestmentMainCustomReportSettings> databaseContext) : base(databaseContext)
        {
        }
    }
}