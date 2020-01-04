namespace CompanyName.Atlas.Contracts.Presentation.Reporting
{
    public interface IMetroChartPresenter
    {
        object ObjectToShow { get; set; }
        ISeriesData PlanByBudgetComponent { get; }
    }
}
