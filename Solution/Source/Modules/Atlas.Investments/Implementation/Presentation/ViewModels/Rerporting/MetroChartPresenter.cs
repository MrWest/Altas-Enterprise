using System.Collections.ObjectModel;
using System.Windows;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Reporting;
using CompanyName.Atlas.Contracts.Presentation.Reporting;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Reporting;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Rerporting
{
    public class MetroChartPresenter: BudgetSummaryPresenter, IMetroChartPresenter
    {
        private object _selectedItem;
        //    private IInvestmentElementPresenter _investmentElementPresenter;
        //    public MetroChartPresenter(IInvestmentElementPresenter investmentElementPresenter)
        //    {
        //        _investmentElementPresenter = investmentElementPresenter;
        //    }
      
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(()=> SelectedItem);
            }
        }
        private ObservableCollection<ISeriesData>  GetPlannedBudget(IBudgetPresenter budget)
        {

            var plannedseriesData = ServiceLocator.Current.GetInstance<ISeriesData>();
            plannedseriesData.SeriesDisplayName = Resources.Planification;
            plannedseriesData.SeriesDescription = Resources.Planification + " " + Resources.ByBudgetComponent;

            plannedseriesData.Items.Add(new DataClass() { Category = budget.EquipmentComponent.GetType().Name, Number = budget.EquipmentComponent.PlannedCost });
            plannedseriesData.Items.Add(new DataClass() { Category = budget.ConstructionComponent.GetType().Name, Number = budget.ConstructionComponent.PlannedCost });
            plannedseriesData.Items.Add(new DataClass() { Category = budget.OtherExpensesComponent.GetType().Name, Number = budget.OtherExpensesComponent.PlannedCost });
            plannedseriesData.Items.Add(new DataClass() { Category = budget.WorkCapitalComponent.GetType().Name, Number = budget.WorkCapitalComponent.PlannedCost });

            var seriesdataCollection = new ObservableCollection<ISeriesData>();
            seriesdataCollection.Add(plannedseriesData);
            
            return seriesdataCollection;
        }
        public ObservableCollection<ISeriesData> GeneratePlanByBudgetComponent(IBudgetPresenter budget)
        {
            var plannedseriesData = ServiceLocator.Current.GetInstance<ISeriesData>();
            plannedseriesData.SeriesDisplayName = Resources.Planification;
            plannedseriesData.SeriesDescription = Resources.Planification+" "+Resources.ByBudgetComponent;

            plannedseriesData.Items.Add(new DataClass() { Category = Resources.EquipmentComponent, Number = GetPlannedBudgetComponet(budget, 0) });
            plannedseriesData.Items.Add(new DataClass() { Category = Resources.ConstructionComponent, Number = GetPlannedBudgetComponet(budget, 1) });
            plannedseriesData.Items.Add(new DataClass() { Category = Resources.OtherExpensesComponent, Number = GetPlannedBudgetComponet(budget, 2) });
            plannedseriesData.Items.Add(new DataClass() { Category = Resources.WorkCapitalComponent, Number = GetPlannedBudgetComponet(budget, 3) });

            var executedseriesData = ServiceLocator.Current.GetInstance<ISeriesData>();
            executedseriesData.SeriesDisplayName = Resources.Execution;
            executedseriesData.SeriesDescription = Resources.Execution+" "+Resources.ByBudgetComponent;
            
            executedseriesData.Items.Add(new DataClass() { Category = Resources.EquipmentComponent, Number = GetExecutedBudgetComponet(budget, 0) });
            executedseriesData.Items.Add(new DataClass() { Category = Resources.ConstructionComponent, Number = GetExecutedBudgetComponet(budget, 1) });
            executedseriesData.Items.Add(new DataClass() { Category = Resources.OtherExpensesComponent, Number = GetExecutedBudgetComponet(budget, 2) });
            executedseriesData.Items.Add(new DataClass() { Category = Resources.WorkCapitalComponent, Number = GetExecutedBudgetComponet(budget, 3) });

            var seriesdataCollection = new ObservableCollection<ISeriesData>();
            seriesdataCollection.Add(plannedseriesData);
            seriesdataCollection.Add(executedseriesData);
            return seriesdataCollection;
        }

        private decimal GetPlannedBudgetComponet(IBudgetPresenter budget, int option)
        {

            var investmentElement = budget.InvestmentElement;

            decimal component = 0;

            if(option == 0)
              component = investmentElement.Budget.EquipmentComponent.PlannedCost;
            if (option == 1)
                component = investmentElement.Budget.ConstructionComponent.PlannedCost;
            if (option == 2)
                component = investmentElement.Budget.OtherExpensesComponent.PlannedCost;
            if (option == 3)
                component = investmentElement.Budget.WorkCapitalComponent.PlannedCost;
            if (investmentElement.HasItems)
            {
                foreach (IInvestmentElementPresenter element in investmentElement.MyElements)
                {
                    component += GetPlannedBudgetComponet(element.Budget, option);
                }
            }

            return component;
        }

        private decimal GetExecutedBudgetComponet(IBudgetPresenter budget, int option)
        {

            var investmentElement = budget.InvestmentElement;

            decimal component = 0;

            if (option == 0)
                component = investmentElement.Budget.EquipmentComponent.ExecutedCost;
            if (option == 1)
                component = investmentElement.Budget.ConstructionComponent.ExecutedCost;
            if (option == 2)
                component = investmentElement.Budget.OtherExpensesComponent.ExecutedCost;
            if (option == 3)
                component = investmentElement.Budget.WorkCapitalComponent.ExecutedCost;
            if (investmentElement.HasItems)
            {
                foreach (IInvestmentElementPresenter element in investmentElement.MyElements)
                {
                    component += GetExecutedBudgetComponet(element.Budget, option);
                }
            }

            return component;
        }

        public ObservableCollection<ISeriesData> GenerateExecutionByBudgetComponent(IBudgetPresenter budget)
        {
            var plannedseriesData = ServiceLocator.Current.GetInstance<ISeriesData>();
            plannedseriesData.SeriesDisplayName = Resources.Execution;
            plannedseriesData.SeriesDescription = Resources.Execution + " " + Resources.ByBudgetComponent;

            plannedseriesData.Items.Add(new DataClass() { Category = Resources.EquipmentComponent, Number = GetRealExecutionPercent(GetPlannedBudgetComponet(budget, 0), GetExecutedBudgetComponet(budget, 0)) });
            plannedseriesData.Items.Add(new DataClass() { Category = Resources.ConstructionComponent, Number = GetRealExecutionPercent(GetPlannedBudgetComponet(budget, 1), GetExecutedBudgetComponet(budget, 1)) });
            plannedseriesData.Items.Add(new DataClass() { Category = Resources.OtherExpensesComponent, Number = GetRealExecutionPercent(GetPlannedBudgetComponet(budget, 2), GetExecutedBudgetComponet(budget, 2)) });
            plannedseriesData.Items.Add(new DataClass() { Category = Resources.WorkCapitalComponent, Number = GetRealExecutionPercent(GetPlannedBudgetComponet(budget, 3), GetExecutedBudgetComponet(budget, 3)) });

           
            var seriesdataCollection = new ObservableCollection<ISeriesData>();
            seriesdataCollection.Add(plannedseriesData);
           
            return seriesdataCollection;
        }

        private decimal GetRealExecutionPercent(decimal planned, decimal executed)
        {
            if (planned > 0)
                return (executed * 100) / planned;
            return 0;
        }

        //public object ObjectToShow { get; set; }

        private ObservableCollection<ISeriesData> _planByBudgetComponent = new ObservableCollection<ISeriesData>();
        private ObservableCollection<ISeriesData> _executionPercentByBudgetComponent = new ObservableCollection<ISeriesData>();

        public ObservableCollection<ISeriesData> PlanByBudgetComponent
        {
            get
            {
                if(_planByBudgetComponent.Count==0)
                  _planByBudgetComponent =  GeneratePlanByBudgetComponent(ObjectToShow as IBudgetPresenter);
                return _planByBudgetComponent;
            }
        }

        public ObservableCollection<ISeriesData> ExecutionPercentByBudgetComponent
        {
            get
            {
                if (_executionPercentByBudgetComponent.Count == 0)
                    _executionPercentByBudgetComponent = GenerateExecutionByBudgetComponent(ObjectToShow as IBudgetPresenter);
                return _executionPercentByBudgetComponent;
            }
        }

        public ObservableCollection<ISeriesData> PlannedBudget { get { return GetPlannedBudget(ObjectToShow as IBudgetPresenter); } }
    }

   

   
}
