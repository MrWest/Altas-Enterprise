using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    public class ActivityExecutorDomainServicesBase:CodedNomenclatorDomainServicesBase<IActivityExecutor>, IActivityExecutorDomainServicesBase
    {
        public override IActivityExecutor Create()
        {
            var executor = base.Create();
            executor.Name = Resources.NewActivityExecutor;
            executor.Description = Resources.NewActivityExecutorDescription;
            return executor;
        }
    }
}