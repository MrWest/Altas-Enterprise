using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital
{
    public interface IWorkCapitalCashFlowCashMovementCategoryViewModel<TItem> : ICrudViewModel<TItem, IWorkCapitalCashFlowCashMovementCategoryPresenter<TItem>>
        where TItem : class ,ICashMovementCategory
    {
        IWorkCapitalCashFlowPresenter WorkCapitalCashFlow { get; set; }
    }
}
