using System;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    public class UnderGroupResourceDomainServices: BudgetComponentItemDomainServicesBase<IUnderGroupResource>, IUnderGroupResourceDomainService
    {
        public IUnderGroup UnderGroup { get; set; }

        public override IUnderGroupResource Create()
        {
            var item = base.Create();
            item.UnderGroup = UnderGroup;
            item.Name = Resources.NewVariantLineName;
            item.Description = Resources.NewVariantLineDescription;
            item.Code = Guid.NewGuid().ToString();
            var weight = ServiceLocator.Current.GetInstance<IWeight>();
           // weight.Holder = item;
            item.Weight = weight;
            var volume = ServiceLocator.Current.GetInstance<IVolume>();
           // volume.Holder = item;
            item.Volume = volume;
            return item;
        }

    }
}