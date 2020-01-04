using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Reporting
{
    public interface ICustomReportSettingsViewModel<TSettings,TPresenter>: ICrudViewModel<TSettings,TPresenter>
           where TSettings : class, ICustomReportSettings
           where TPresenter:class , ICustomReportSettingsPresenter<TSettings>
    {
        
    }

    public interface IChildCustomReportSettingsViewModel<TSettings, TPresenter, TMainSettings> : ICustomReportSettingsViewModel<TSettings, TPresenter>, IChildCustomReportSettingsViewModel
           where TSettings : class, IChildCustomReportSettings
         where TPresenter : class, IChildCustomReportSettingsPresenter<TSettings, TMainSettings>
         where TMainSettings : class, ICustomReportSettings
    {
        ICustomReportSettingsPresenter<TMainSettings> ParentReport { get; set; }
    }

    public interface IChildCustomReportSettingsViewModel : ICrudViewModel
          
    {
      
    }
    //public interface IChildMainCustomReportSettingsViewModel<TSettings, TPresenter,TMainSettings> : ICustomReportSettingsViewModel<TSettings, TPresenter>
    //      where TSettings : class, IChildCustomReportSettings
    //    where TPresenter : class, IChildCustomReportSettingsPresenter<TSettings>
    //     where TMainSettings : class, ICustomReportSettings
    //{
    //    ICustomReportSettingsPresenter<TMainSettings> Parent { get; set; }
    //}
}