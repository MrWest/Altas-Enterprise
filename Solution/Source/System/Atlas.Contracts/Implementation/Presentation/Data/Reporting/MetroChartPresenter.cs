using CompanyName.Atlas.Contracts.Presentation.Reporting;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Reporting
{
    public class MetroChartPresenter:IMetroChartPresenter
    {
    //    private IInvestmentElementPresenter _investmentElementPresenter;
    //    public MetroChartPresenter(IInvestmentElementPresenter investmentElementPresenter)
    //    {
    //        _investmentElementPresenter = investmentElementPresenter;
    //    }
        public ISeriesData GeneratePlanByBudgetComponent( IBudgetPresenter budget)
        {
            var seriesData = ServiceLocator.Current.GetInstance<ISeriesData>();
            seriesData.SeriesDisplayName = budget.GetType().Name;
            seriesData.SeriesDescription = budget.GetType().FullName;

            seriesData.Items.Add(new DataClass() { Category = budget.EquipmentComponent.GetType().Name, Number = budget.EquipmentComponent.PlannedCost });
            seriesData.Items.Add(new DataClass() { Category = budget.ConstructionComponent.GetType().Name, Number = budget.ConstructionComponent.PlannedCost });
            seriesData.Items.Add(new DataClass() { Category = budget.OtherExpensesComponent.GetType().Name, Number = budget.OtherExpensesComponent.PlannedCost });
            seriesData.Items.Add(new DataClass() { Category = budget.WorkCapitalComponent.GetType().Name, Number = budget.WorkCapitalComponent.PlannedCost });
            
            return seriesData;
        }

        public object ObjectToShow { get; set; }
        public ISeriesData PlanByBudgetComponent { get { return GeneratePlanByBudgetComponent(ObjectToShow as IBudgetPresenter); }}
    }

   

   
}
