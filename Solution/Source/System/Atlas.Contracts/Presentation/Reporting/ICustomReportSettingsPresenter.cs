using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Reporting
{
    public interface ICustomReportSettingsPresenter<TSettings>:IPresenter<TSettings>, ICustomReportSettingsPresenter
           where TSettings : class, ICustomReportSettings
    {
        IChildCustomReportSettingsViewModel ChildReports { get; }
    }

    public interface ICustomReportSettingsPresenter : INavigable
    {

    }
    public interface IChildCustomReportSettingsPresenter<TSettings,TMainSettings> : ICustomReportSettingsPresenter<TSettings>
              where TSettings : class, IChildCustomReportSettings
              where TMainSettings : class, ICustomReportSettings
    {
        ICustomReportSettingsPresenter<TMainSettings> ParentReport { get; set; }
    }
}