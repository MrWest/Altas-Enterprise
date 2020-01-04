using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    public interface IExecutionPresenter : IPresenter<IExecution>, ICosttable
         //where TComponent :class , IBudgetComponent
    {
        IExecutedActivityPresenter ExecutedActivity { get; set; }
        DateTime Date { get; set; }
        decimal Amount { get; set; }
        String Description { get; set; }
    }
}
