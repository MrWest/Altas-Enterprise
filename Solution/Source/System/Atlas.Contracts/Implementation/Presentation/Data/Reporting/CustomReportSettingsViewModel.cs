using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Reporting;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Reporting
{
    public abstract class CustomReportSettingsViewModel<TSettings,TPresenter,TService>:NavigableViewModel<TSettings,TPresenter,TService>, ICustomReportSettingsViewModel<TSettings,TPresenter>
        where TSettings: class , ICustomReportSettings
        where TPresenter: class , ICustomReportSettingsPresenter<TSettings>
        where TService:class , ICustomReportSettingsApplicationServices<TSettings>
    {
        
    }
    public abstract class ChildCustomReportSettingsViewModel<TSettings, TPresenter, TService, TMainSettings> : CustomReportSettingsViewModel<TSettings, TPresenter, TService>, IChildCustomReportSettingsViewModel<TSettings, TPresenter, TMainSettings>
           where TSettings : class, IChildCustomReportSettings
           where TPresenter : class, IChildCustomReportSettingsPresenter<TSettings, TMainSettings>
           where TService : class, IChildCustomReportSettingsApplicationServices<TSettings>
           where TMainSettings : class, ICustomReportSettings
    {
        public ICustomReportSettingsPresenter<TMainSettings> ParentReport { get; set; }

        protected override TService CreateServices()
        {
            var service = base.CreateServices();
            service.Parent = ParentReport.Object;
            return service;
        }

        protected override TPresenter CreatePresenterFor(TSettings item)
        {
            var presenter = base.CreatePresenterFor(item);
            presenter.ParentReport = ParentReport;
            return presenter;
        }

        protected override INavigable Parent { get { return ParentReport; } }
    }

    //////public abstract class ChildMainCustomReportSettingsViewModel<TSettings, TPresenter, TService,TMainSettings> : CustomReportSettingsViewModel<TSettings, TPresenter, TService>, IChildMainCustomReportSettingsViewModel<TSettings, TPresenter, TMainSettings>
    //////      where TSettings : class, IChildCustomReportSettings
    //////      where TPresenter : class, IChildCustomReportSettingsPresenter<TSettings, TMainSettings>
    //////      where TService : class, IChildCustomReportSettingsApplicationServices<TSettings>
    //////     where TMainSettings : class, IMainCustomReportSettings
    //////{
    //////    public ICustomReportSettingsPresenter<TMainSettings> Parent { get; set; }

    //////    protected override TService CreateServices()
    //////    {
    //////        var service = base.CreateServices();
    //////        service.Parent = Parent.Object;
    //////        return service;
    //////    }

    //////    protected override TPresenter CreatePresenterFor(TSettings item)
    //////    {
    //////        var presenter = base.CreatePresenterFor(item);
    //////        presenter.Parent = Parent;
    //////        return presenter;
    //////    }
    //////}
}