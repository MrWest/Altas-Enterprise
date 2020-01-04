using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    public class RegularGroupDomainService : PriceSystemGroupDomainService<IRegularGroup>, IRegularGroupDomainService
    {
        public override IRegularGroup Create()
        {
            var group = base.Create();
            group.Name = Resources.NewRegularGroup;
            group.Description = Resources.NewRegularGroupDescription;
            return group;
        }

        public IOverGroup OverGroup { get; set; }
    }
}
