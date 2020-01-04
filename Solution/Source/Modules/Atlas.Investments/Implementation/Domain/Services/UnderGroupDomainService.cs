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
    public class UnderGroupDomainService : PriceSystemGroupDomainService<IUnderGroup>, IUnderGroupDomainService
    {
        public override IUnderGroup Create()
        {
            var group = base.Create();
            group.Name = Resources.NewUnderGroup;
            group.Description = Resources.NewUnderGroupDescription;
            return group;
        }

        public IRegularGroup RegularGroup { get; set; }
    }
}
