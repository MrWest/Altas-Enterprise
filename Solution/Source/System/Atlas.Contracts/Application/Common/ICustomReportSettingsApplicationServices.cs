using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Application.Common
{
    public interface ICustomReportSettingsApplicationServices<TSettings>: IItemManagerApplicationServices<TSettings>
        where TSettings: class , ICustomReportSettings
    {
        
    }

    public interface IChildCustomReportSettingsApplicationServices<TSettings> : ICustomReportSettingsApplicationServices<TSettings>
           where TSettings : class, IChildCustomReportSettings
    {
        ICustomReportSettings Parent { get; set; }
    }

}