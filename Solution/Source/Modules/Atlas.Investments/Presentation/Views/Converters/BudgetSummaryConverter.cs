using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Rerporting;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Reporting;

namespace CompanyName.Atlas.Investments.Presentation.Views.Converters
{
   public class BudgetSummaryConverter : IValueConverter
    {
       public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
       {
           if(value!=null&&parameter!=null)
           {
               var budgetSummry = (value as IBudgetSummary);
               var container = (CurrencyPeriodContainer)parameter;


               return GoDeepOnBudgetComponent(budgetSummry, container.Currency, container.Period);

               //return budgetSummry?.BudgetByCurrencyAndPeriod(container.Currency, container.Period) +
               //    GoDeepOnBudgetComponent(budgetSummry,container.Currency,container.Period);
           }
           return 0;
       }

        private decimal GoDeepOnBudgetComponent(IBudgetSummary budgetComponent, ICurrency currency, IPeriod period)
        {
            if (budgetComponent.GetType().Implements<IEquipmentComponentPresenter>())
            {
                var equipmentComponentPresenter = budgetComponent as IEquipmentComponentPresenter;
                if (equipmentComponentPresenter != null)
                    return equipmentComponentPresenter.Budget.InvestmentElement.GoDeepOnBudgetComponent(equipmentComponentPresenter.GetType(),currency, period);
            }
            if (budgetComponent.GetType().Implements<IConstructionComponentPresenter>())
            {
                var constructionComponentPresenter = budgetComponent as IConstructionComponentPresenter;
                if (constructionComponentPresenter != null)
                    return constructionComponentPresenter.Budget.InvestmentElement.GoDeepOnBudgetComponent(constructionComponentPresenter.GetType(),currency, period);
            }
            if (budgetComponent.GetType().Implements<IOtherExpensesComponentPresenter>())
            {
                var otherExpensesComponentPresenter = budgetComponent as IOtherExpensesComponentPresenter;
                if (otherExpensesComponentPresenter != null)
                    return otherExpensesComponentPresenter.Budget.InvestmentElement.GoDeepOnBudgetComponent(otherExpensesComponentPresenter.GetType(),currency, period);
            }
            if (budgetComponent.GetType().Implements<IWorkCapitalComponentPresenter>())
            {
                var workCapitalComponentPresenter = budgetComponent as IWorkCapitalComponentPresenter;
                if (workCapitalComponentPresenter != null)
                    return workCapitalComponentPresenter.Budget.InvestmentElement.GoDeepOnBudgetComponent(workCapitalComponentPresenter.GetType(),currency, period);
            }

            if (budgetComponent.GetType().Implements<IFixedCapitalPresenter>())
            {
                var fixedCapitalPresenter = budgetComponent as IFixedCapitalPresenter;
                if (fixedCapitalPresenter != null)
                    return fixedCapitalPresenter.Budget.InvestmentElement.GoDeepOnBudgetComponent(fixedCapitalPresenter.GetType(), currency, period);
            }

            if (budgetComponent.GetType().Implements<IBudgetPresenter>())
            {
                var budgetPresenter = budgetComponent as IBudgetPresenter;
                if (budgetPresenter != null)
                    return budgetPresenter.InvestmentElement.GoDeepOnBudgetComponent(budgetPresenter.GetType(), currency, period);
            }
            return 0;

        }
       public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
       {
           throw new NotImplementedException();
       }
    }
}
