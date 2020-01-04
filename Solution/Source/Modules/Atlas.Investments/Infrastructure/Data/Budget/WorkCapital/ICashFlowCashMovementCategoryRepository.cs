using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data.Budget.WorkCapital
{
    public interface ICashFlowCashMovementCategoryRepository<TItem> : IRelatedRepository<TItem, IWorkCapitalCashFlow>
        where TItem : class ,ICashMovementCategory
    {
        /// <summary>
        /// Superior Category
        /// </summary>
        IWorkCapitalCashFlow WorkCapitalCashFlow { get; set; }
    }
}
