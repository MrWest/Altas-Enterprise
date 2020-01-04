using CompanyName.Atlas.Contracts.Implementation.Application.Common;
using CompanyName.Atlas.Investments.Application.Reporting;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data.Reporting;

namespace CompanyName.Atlas.Investments.Implementation.Application.Reporting
{
    public class InvestmentChildCustomReportSettingsApplicationServices: ChildCustomReportSettingsApplicationServices<IInvestmentChildCustomReportSettings,IInvestmentChildCustomReportSettingsRepository>, IInvestmentChildCustomReportSettingsApplicationServices
    {
        
    }

    public class InvestmentMainCustomReportSettingsApplicationServices : CustomReportSettingsApplicationServices<IInvestmentMainCustomReportSettings, IInvestmentMainCustomReportSettingsRepository>, IInvestmentMainCustomReportSettingsApplicationServices
    {

    }
}