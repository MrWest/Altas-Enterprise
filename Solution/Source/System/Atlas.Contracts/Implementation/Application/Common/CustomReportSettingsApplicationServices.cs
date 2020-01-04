using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Application.Common
{
    public abstract class CustomReportSettingsApplicationServices<TSettings, TRepository>: ItemManagerApplicationServicesBase<TSettings, TRepository, ICustomReportSettingsDomainServices<TSettings>>, ICustomReportSettingsApplicationServices<TSettings>
        where TSettings:class , ICustomReportSettings
        where TRepository : class, ICustomReportSettingsRepository<TSettings>
    {
        
    }


    public abstract class ChildCustomReportSettingsApplicationServices<TSettings, TRepository> : CustomReportSettingsApplicationServices<TSettings, TRepository>, IChildCustomReportSettingsApplicationServices<TSettings>
           where TSettings : class, IChildCustomReportSettings
        where TRepository : class, IChildCustomReportSettingsRepository<TSettings>
    {
        public ICustomReportSettings Parent { get; set; }

        protected override TRepository Repository
        {
            get
            {
                var repo = base.Repository;
                repo.Parent = Parent;
                return repo;
            }
        }
    }

}