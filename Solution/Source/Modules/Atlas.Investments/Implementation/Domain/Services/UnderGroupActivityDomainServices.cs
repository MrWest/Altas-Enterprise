using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    public class UnderGroupActivityDomainServices : BudgetComponentItemDomainServicesBase<IUnderGroupActivity>,IUnderGroupActivityDomainServices
    {
        public override IUnderGroupActivity Create()
        {
            var item = base.Create();
            item.UnderGroup = UnderGroup;
            item.Name = Resources.NewVariantLineName;
            item.Description = Resources.NewVariantLineDescription;
            item.Quantity = 1;
          //  item.Code = Guid.NewGuid().ToString();
            var period = ServiceLocator.Current.GetInstance<IPeriod>();
            period.Holder = item;
            item.Period = period;
            item.PriceSystem = UnderGroup.PriceSystem;
            return item;
        }

        public IUnderGroup UnderGroup { get; set; }

        ///// <summary>
        /////     When overridden in a deriver it sets the data for the given item.
        ///// </summary>
        ///// <param name="item">The just created item that is in need of its creation data.</param>
        //protected override void SetDataToNew(IUnderGroupActivity item)
        //{
        //    if (item == null)
        //        throw new ArgumentNullException("plannedActivity");

        //     base.SetDataToNew(item);
        //     item.Name = Resources.NewVariantLineName;
        //     item.Description = Resources.NewVariantLineDescription;
            
        //}
    }
}
