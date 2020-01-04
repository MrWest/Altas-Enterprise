using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services.Budget.WorkCapital
{
    public interface ICashFlowCashMovementCategoryDomainService<TItem>: IDomainServices<TItem>
        where TItem:class ,ICashMovementCategory
    {
        IWorkCapitalCashFlow WorkCapitalCashFlow { get; set; }
    }
}
