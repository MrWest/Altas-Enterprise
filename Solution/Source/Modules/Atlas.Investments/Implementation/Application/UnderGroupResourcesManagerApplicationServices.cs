using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    public class UnderGroupResourcesManagerApplicationServices: UnderGroupItemManagerApplicationServices<IUnderGroupResource,IUnderGroupResourceRepository,IUnderGroupResourceDomainService>, IUnderGroupResourcesManagerApplicationServices
    {
        protected override IUnderGroupResourceDomainService DomainServices
        {
            get
            {
                var domain = base.DomainServices;
                domain.UnderGroup = UnderGroup;
                return domain;
            }
        }

        public override IUnderGroupResource Export(IDatabaseContext exportDatabaseContext, IUnderGroupResource item)
        {
            throw new System.NotImplementedException();
        }
    }
}