using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget
{
    public interface IBudgetSummary
    {
        //<summary>
        //Gets the total of planned activities composing the current <see cref="IBudgetComponentPresenter"/>.
        //</summary>
        decimal PlannedCost { get; }
        //<summary>
        //Gets the total of planned resource composing the current <see cref="IBudgetComponentPresenter"/>.
        //</summary>
        decimal ExecutedCost { get; }
        /// <summary>
        /// gets the execution percent
        /// </summary>
        decimal ExecutionPercent { get; }

        decimal BudgetByCurrencyAndPeriod(ICurrency currency, IPeriod period);
    }
}
