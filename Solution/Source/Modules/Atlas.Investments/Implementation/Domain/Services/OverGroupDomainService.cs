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
    public class OverGroupDomainService : PriceSystemGroupDomainService<IOverGroup>, IOverGroupDomainService
    {
        public override IOverGroup Create()
        {
            var group = base.Create();
            group.Name = Resources.NewOverGroup;
            group.Description = Resources.NewOverGroupDescription;
            return group;
        }

      
        public IPriceSystem AbovePriceSystem { get; set; }
    }
}
