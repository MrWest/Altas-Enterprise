using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services.Budget.WorkCapital
{
    public class CashFlowCashMovementCategoryDomainService<TItem> : DomainServicesBase<TItem>, ICashFlowCashMovementCategoryDomainService<TItem> 
        where TItem : class ,ICashMovementCategory
    {
        /// <summary>
        /// Superior Category
        /// </summary>
        public IWorkCapitalCashFlow WorkCapitalCashFlow { get; set; }

        public override TItem Create()
        {

            TItem cmcItem = base.Create();
            cmcItem.Name = cmcItem.GetType().Implements<ICashEntry>() ? Resources.NewCashEntry : Resources.NewCashOutGoing;



            // cmcItem.Description = Resources.NewPhase_Description;
            return cmcItem;
        }
    }
}
