using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
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
   public class NavigableStateToBooleanConverter : IValueConverter
    {
       public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
       {
               var state = ((NavigableState) value);
              


               return state == NavigableState.Marked||state== NavigableState.Selected;

          
       }

       
       public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
       {
           var choose =( bool) value;
           if (choose)
               return NavigableState.Selected;
           return NavigableState.UnSelected;
       }
    }
}
