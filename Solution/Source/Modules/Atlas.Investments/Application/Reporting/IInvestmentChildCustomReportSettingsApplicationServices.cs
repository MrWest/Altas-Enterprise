using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application.Reporting
{
    public interface IInvestmentChildCustomReportSettingsApplicationServices: IChildCustomReportSettingsApplicationServices<IInvestmentChildCustomReportSettings>
    {
        
    }

    public interface IInvestmentMainCustomReportSettingsApplicationServices : ICustomReportSettingsApplicationServices<IInvestmentMainCustomReportSettings>
    {

    }
}