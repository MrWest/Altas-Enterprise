using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    public class OverGroupManagerApplicationServices : PriceSystemGroupManagerApplicationServices<IOverGroup,IOverGroupRepository,IOverGroupDomainService>, IOverGroupManagerApplicationServices
    {
        public IPriceSystem PriceSystem { get; set; }

        protected override IOverGroupRepository Repository
        {
            get
            {
                var repo = base.Repository;
                repo.PriceSystem = PriceSystem;
                return repo;
            }
        }

        protected override IOverGroupDomainService DomainServices
        {
            get
            {
                var domain = base.DomainServices;
                domain.AbovePriceSystem = PriceSystem;
                domain.PriceSystem = PriceSystem.Id;
                return domain;
            }
        }

        public bool ExistGroup(string code, IOverGroup overGroup)
        {
            var regularGroupService = ServiceLocator.Current.GetInstance<IRegularGroupManagerApplicationServices>();
            regularGroupService.OverGroup = overGroup;
            return regularGroupService.Items.Any(x =>
                string.Equals(x.Code.ToString(), code,
                    StringComparison.Ordinal));
        }

        public IRegularGroup GetRegularGroup(string code, IOverGroup overGroup)
        {
            var regularGroupService = ServiceLocator.Current.GetInstance<IRegularGroupManagerApplicationServices>();
            regularGroupService.OverGroup = overGroup;
            return regularGroupService.Items.SingleOrDefault(x =>
                            string.Equals(x.Code.ToString(), code,
                                StringComparison.Ordinal));
        }
    }
}
