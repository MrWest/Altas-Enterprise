using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    public class UnderGroupManagerApplicationServices : PriceSystemGroupManagerApplicationServices<IUnderGroup,IUnderGroupRepository,IUnderGroupDomainService>, IUnderGroupManagerApplicationServices
    {
        public IRegularGroup RegularGroup { get; set; }

        protected override IUnderGroupRepository Repository
        {
            get
            {
                var repo = base.Repository;
                repo.RegularGroup = RegularGroup;
                return repo;
            }
        }

        protected override IUnderGroupDomainService DomainServices
        {
            get
            {
                var domain = base.DomainServices;
                domain.RegularGroup = RegularGroup;
                domain.PriceSystem = RegularGroup.PriceSystem;
                return domain;
            }
        }

        public bool ExistActivity(string code, IUnderGroup underGroup)
        {
            var activityService = ServiceLocator.Current.GetInstance<IUnderGroupActivityManagerApplicationServices>();
            activityService.UnderGroup = underGroup;
            return activityService.Items.Any(x =>
                string.Equals(x.Code.ToString(), code,
                    StringComparison.Ordinal));
        }

        public IUnderGroupActivity GetActivity(string code, IUnderGroup underGroup)
        {
            var activityService = ServiceLocator.Current.GetInstance<IUnderGroupActivityManagerApplicationServices>();
            activityService.UnderGroup = underGroup;
            return activityService.Items.SingleOrDefault(x =>
                            string.Equals(x.Code.ToString(), code,
                                StringComparison.Ordinal));
        }
    }
}
