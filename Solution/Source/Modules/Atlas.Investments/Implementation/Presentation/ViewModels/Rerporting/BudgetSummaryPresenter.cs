using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Reporting;
using CompanyName.Atlas.Investments.Presentation.Views.Converters;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Rerporting
{
    public class BudgetSummaryPresenter : BindableBase, IBudgetSummaryPresenter
    {
        private IList<IBudgetSummaryConcept> _budgetSummaryConcepts;
        private IList<DataGridColumn> _summaryColumns;
        public object ObjectToShow { get; set; }

        public IList<IBudgetSummaryConcept> BudgetSummary
        {
            get
            {
                if (_budgetSummaryConcepts == null&&ObjectToShow!=null)
                {
                    _budgetSummaryConcepts = new List<IBudgetSummaryConcept>(); //(ObjectToShow as IBudgetPresenter).InvestmentElement.Period


                    _budgetSummaryConcepts.Add(new BudgetSummaryConcept() { ConceptName = Resources.EquipmentComponent, ThousandsOfMoney = (ObjectToShow as IBudgetPresenter).EquipmentComponent});
                    _budgetSummaryConcepts.Add(new BudgetSummaryConcept() { ConceptName = Resources.ConstructionComponent,  ThousandsOfMoney = (ObjectToShow as IBudgetPresenter).ConstructionComponent });
                    _budgetSummaryConcepts.Add(new BudgetSummaryConcept() { ConceptName = Resources.OtherExpensesComponent, ThousandsOfMoney = (ObjectToShow as IBudgetPresenter).OtherExpensesComponent });
                    _budgetSummaryConcepts.Add(new BudgetSummaryConcept() { ConceptName = Resources.FixedCapital, ThousandsOfMoney = new FixedCapitalPresenter(ObjectToShow as IBudgetPresenter) ,Background = true});
                    _budgetSummaryConcepts.Add(new BudgetSummaryConcept() { ConceptName = Resources.WorkCapitalComponent, ThousandsOfMoney = (ObjectToShow as IBudgetPresenter).WorkCapitalComponent });
                    _budgetSummaryConcepts.Add(new BudgetSummaryConcept() { ConceptName = Resources.JustTotal, ThousandsOfMoney = (ObjectToShow as IBudgetPresenter) ,Background = true});
                }
                return _budgetSummaryConcepts;
            }
        }

        public IList<DataGridColumn> SummaryColumns
        {
            get
            {
                if (_summaryColumns == null&&ObjectToShow!=null)
                {
                    _summaryColumns = new List<DataGridColumn>();


                    _summaryColumns.Add(new DataGridTextColumn()
                    {
                        Header = Resources.Concept,
                        Width = new DataGridLength(25, DataGridLengthUnitType.Auto),
                        Binding = new Binding("ConceptName") { Mode = BindingMode.OneWay }

                    });

                   
                    _summaryColumns.Add(new DataGridTextColumn()
                    {
                        Header = Resources.JustTotal,
                        Width = new DataGridLength(25, DataGridLengthUnitType.Auto),
                        Binding = new Binding("ThousandsOfMoney") { Converter = new BudgetSummaryConverter(), ConverterParameter = new CurrencyPeriodContainer() { Currency = null, Period = Period } }

                    });

                    var currencies = ServiceLocator.Current.GetInstance<ICurrencyViewModel>();
                    currencies.Load();

                    foreach (ICurrencyPresenter currency in currencies.Items)
                    {


                        var container = new CurrencyPeriodContainer(){Currency = currency.Object,Period = Period};
                        _summaryColumns.Add(new DataGridTextColumn()
                            {
                                Header = currency.Name,
                                Width = new DataGridLength(25, DataGridLengthUnitType.Auto),
                                Binding = new Binding("ThousandsOfMoney") { Converter = new BudgetSummaryConverter(), ConverterParameter = container }


                            });
                       



                    }






                    return _summaryColumns;

                }
                return _summaryColumns;
            }
        }

        private IPeriod _period;

        public IPeriod Period
        {
            get
            {
                if (_period == null)
                {
                    _period = ServiceLocator.Current.GetInstance<IPeriod>();
                    _period.Starts = (ObjectToShow as IBudgetPresenter).InvestmentElement.Period.Starts;
                    _period.Ends = (ObjectToShow as IBudgetPresenter).InvestmentElement.Period.Ends;
                    _period.PeriodKind = (ObjectToShow as IBudgetPresenter).InvestmentElement.Budget.WorkCapitalComponent.PlannedWorkCapitalCashFlow.PeriodKind;
                }
                   
                return _period;
            }
            set { _period = value; }
        }

        public IList<IBudgetSummaryPresenter> BudgetSummaryOverall
        {
            get
            {
                var budgetSummaryOverall = new List<IBudgetSummaryPresenter>();

                //var mainbudgetsummary = new BudgetSummaryPresenter() {ObjectToShow = ObjectToShow};
                //mainbudgetsummary.Period = Period;
                //mainbudgetsummary.Period.Name = Resources.Summary;
                //mainbudgetsummary.Period.PeriodKind = DateTimeScale.Year;
                //budgetSummaryOverall.Add(mainbudgetsummary);

                foreach ( IPeriod period in Period.Periods)
                {
                    var budgetsummary = new BudgetSummaryPresenter() { ObjectToShow = ObjectToShow };
                    budgetsummary.Period = period;
                  //  budgetsummary.Period.Name = Resources.Summary;
                   // budgetsummary.Period.PeriodKind = DateTimeScale.Year;
                    budgetSummaryOverall.Add(budgetsummary);
                }

                return budgetSummaryOverall;
            }
        }

        private IList<DataGridColumn> CreateSummary()
        {
            throw new NotImplementedException();
        }
    }
}
