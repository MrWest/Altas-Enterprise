using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Reporting
{
    public interface IBudgetSummaryPresenter
    {
        object ObjectToShow { get; set; }
        IList<IBudgetSummaryConcept> BudgetSummary { get; }
        IList<IBudgetSummaryPresenter> BudgetSummaryOverall { get; }
        IList<DataGridColumn> SummaryColumns { get; }
        IPeriod Period { get; set; }
        
    }
}
