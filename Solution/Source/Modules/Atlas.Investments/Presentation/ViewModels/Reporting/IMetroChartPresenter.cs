using System.Collections.ObjectModel;
using CompanyName.Atlas.Contracts.Presentation.Reporting;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Reporting
{
    public interface IMetroChartPresenter : IBudgetSummaryPresenter
    {
      //  object ObjectToShow { get; set; }
        ObservableCollection<ISeriesData> PlanByBudgetComponent { get; }
        ObservableCollection<ISeriesData> ExecutionPercentByBudgetComponent { get; }

        object SelectedItem { get; set; }
    }

}
