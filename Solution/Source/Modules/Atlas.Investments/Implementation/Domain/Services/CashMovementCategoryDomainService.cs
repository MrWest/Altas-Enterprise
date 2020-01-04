using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    public class CashMovementCategoryDomainService<TItem>: DomainServicesBase<TItem>,ICashMovementCategoryDomainService<TItem>
        where TItem:class ,ICashMovementCategory
    {
        /// <summary>
        /// Superior Category
        /// </summary>
        public ICashMovementCategory SuperiorCategory { get; set; }

        public override TItem Create()
        {
            
            TItem cmcItem = base.Create();
            cmcItem.Name = cmcItem.GetType().Implements<ICashEntry>()? Resources.NewCashEntry:Resources.NewCashOutGoing;

            

           // cmcItem.Description = Resources.NewPhase_Description;
            return cmcItem;
        }
    }
}
