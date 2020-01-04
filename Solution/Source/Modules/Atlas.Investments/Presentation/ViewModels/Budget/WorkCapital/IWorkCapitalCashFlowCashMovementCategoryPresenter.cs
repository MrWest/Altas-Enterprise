using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital
{
    public interface IWorkCapitalCashFlowCashMovementCategoryPresenter<TCategory> : ICashMovementCategoryPresenter<TCategory>
        where TCategory:class ,ICashMovementCategory
    {
        IWorkCapitalCashFlowPresenter WorkCapitalCashFlowPresenter { get; set; }

       

    }
}
