using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;

namespace CompanyName.Atlas.Investments.Presentation.Views.Converters
{
    /// <summary>
    /// selector for row template on resource datagrid xaml
    /// </summary>
    public class InvestmentElementTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate crap = null;
            if ( (container as FrameworkElement).DataContext != null &&  (container as FrameworkElement).DataContext.GetType().Implements<IInvestmentPresenter>())
            {

                return (container as FrameworkElement).FindResource(
                    "InvestmentDataTemplate") as DataTemplate;

            }

            return (container as FrameworkElement).FindResource(
                "InvestmentComponentDataTemplate") as DataTemplate;
        }
    }
}
