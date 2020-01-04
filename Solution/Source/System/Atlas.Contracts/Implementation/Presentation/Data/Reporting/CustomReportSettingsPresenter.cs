using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Reporting;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Reporting
{
    public abstract class CustomReportSettingsPresenter<TSettings,TService>: NavigableNomenclator<TSettings,TService>, ICustomReportSettingsPresenter<TSettings>
        where TSettings: class, ICustomReportSettings
        where TService: class , ICustomReportSettingsApplicationServices<TSettings>
    {
        public abstract IChildCustomReportSettingsViewModel ChildReports { get; }

       

    }

    public abstract class ChildCustomReportSettingsPresenter<TSettings, TService, TMainSettings> : CustomReportSettingsPresenter<TSettings, TService>, IChildCustomReportSettingsPresenter<TSettings, TMainSettings>
        where TSettings : class, IChildCustomReportSettings
        where TService : class, IChildCustomReportSettingsApplicationServices<TSettings>
        where TMainSettings:class, ICustomReportSettings
    {
        public ICustomReportSettingsPresenter<TMainSettings> ParentReport { get; set; }

        protected override TService CreateServices()
        {
            var service = base.CreateServices();
            service.Parent = ParentReport.Object;
            return service;
        }

    }
}