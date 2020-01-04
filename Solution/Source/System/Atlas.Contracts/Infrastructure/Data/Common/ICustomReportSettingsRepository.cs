using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data.Common
{
    public interface ICustomReportSettingsRepository<TSettings> : IRepository<TSettings>
        where TSettings: class , ICustomReportSettings
    {
        
    }
    public interface IMainCustomReportSettingsRepository<TSettings> : ICustomReportSettingsRepository<TSettings>
           where TSettings : class, IMainCustomReportSettings
    {

    }
    public interface IChildCustomReportSettingsRepository<TSettings> : ICustomReportSettingsRepository<TSettings>
           where TSettings : class, IChildCustomReportSettings
    {
        ICustomReportSettings Parent { get; set; }
    }
}