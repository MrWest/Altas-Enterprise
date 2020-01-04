using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data.Reporting
{
    public interface IInvestmentChildCustomReportSettingsRepository : IChildCustomReportSettingsRepository<IInvestmentChildCustomReportSettings>
    {
        
    }

    public interface IInvestmentMainCustomReportSettingsRepository : IMainCustomReportSettingsRepository<IInvestmentMainCustomReportSettings>
    {

    }
}