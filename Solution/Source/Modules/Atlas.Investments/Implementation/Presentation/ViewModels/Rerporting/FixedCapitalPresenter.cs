using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Rerporting
{
    public class FixedCapitalPresenter:IBudgetSummary, IFixedCapitalPresenter
    {
        private IBudgetPresenter _budgetPresenter;
        public FixedCapitalPresenter(IBudgetPresenter budgetPresenter )
        {
            _budgetPresenter = budgetPresenter;
        }
        public decimal PlannedCost { get; private set; }
        public decimal ExecutedCost { get; private set; }
        public decimal ExecutionPercent { get; private set; }
        public decimal BudgetByCurrencyAndPeriod(ICurrency currency, IPeriod period)
        {
            decimal rslt = 0;
            if (_budgetPresenter != null)
            {
                rslt = _budgetPresenter.EquipmentComponent.BudgetByCurrencyAndPeriod(currency, period)
                       + _budgetPresenter.ConstructionComponent.BudgetByCurrencyAndPeriod(currency, period)
                       + _budgetPresenter.OtherExpensesComponent.BudgetByCurrencyAndPeriod(currency, period);
            }
            return rslt;
        }

        public IBudgetPresenter Budget { get { return _budgetPresenter; } }
    }

    public interface IFixedCapitalPresenter
    {
        IBudgetPresenter Budget { get; }
    }
}
