using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    public class RegularGroupManagerApplicationServices : PriceSystemGroupManagerApplicationServices<IRegularGroup,IRegularGroupRepository,IRegularGroupDomainService>, IRegularGroupManagerApplicationServices
    {
        public IOverGroup OverGroup { get; set; }
        protected override IRegularGroupRepository Repository
        {
            get
            {
                var repo = base.Repository;
                repo.OverGroup = OverGroup;
                return repo;
            }
        }

        protected override IRegularGroupDomainService DomainServices
        {
            get
            {
                var domain = base.DomainServices;
                domain.OverGroup = OverGroup;
                domain.PriceSystem = OverGroup.PriceSystem;
                return domain;
            }
        }

        public bool ExistUnderGroup(string code, IRegularGroup regularGroup)
        {
            var underrGroupService = ServiceLocator.Current.GetInstance<IUnderGroupManagerApplicationServices>();
            underrGroupService.RegularGroup = regularGroup;
            return underrGroupService.Items.Any(x =>
                string.Equals(x.Code.ToString(), code,
                    StringComparison.Ordinal));
        }

        public IUnderGroup GetUnderGroup(string code, IRegularGroup regularGroup)
        {
            var underGroupService = ServiceLocator.Current.GetInstance<IUnderGroupManagerApplicationServices>();
            underGroupService.RegularGroup = regularGroup;
            return underGroupService.Items.SingleOrDefault(x =>
                            string.Equals(x.Code.ToString(), code,
                                StringComparison.Ordinal));
        }
    }
}
