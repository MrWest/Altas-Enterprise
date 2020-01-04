using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Domain
{
    public interface ICustomReportSettingsDomainServices<TSettings> : IDomainServices<TSettings>
        where TSettings:class ,ICustomReportSettings
    {
        
    }
}