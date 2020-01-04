using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
    public class CustomReportSettingsDomainServices<TSetttings>: DomainServicesBase<TSetttings>, ICustomReportSettingsDomainServices<TSetttings>
        where TSetttings: class , ICustomReportSettings
    {
        public override TSetttings Create()
        {
            var  customreport = base.Create();

            customreport.Name = Resources.NewCustomReportSettings;
            customreport.Description = Resources.NewCustomReportSettingsDescription;

            return customreport;
        }
    }
}