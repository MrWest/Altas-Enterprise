using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Reporting;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CompanyName.Atlas.Contracts.Presentation.Features;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ReportSetcion.xaml
    /// </summary>
    public partial class ReportSetcion : UserControl, IPrinttableContainer, IExporttableContainer
    {
        public ReportSetcion()
        {
            InitializeComponent();
            IsVisibleChanged += InvestmentVariablesEditor_IsVisibleChanged;
        }

        private readonly INavigationServices _navigationServices = ServiceLocator.Current.GetInstance<INavigationServices>();
        private void InvestmentVariablesEditor_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //   var isVisible = e.NewValue as bool?;

            if (IsVisible)
                _navigationServices.ShowOptionalNavigationContent();
            //else
            //    _navigationServices.HideOptionalNavigationContent();
        }
        private void FrameworkContentElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var budgetSummaryPresenter = e.NewValue as IBudgetSummaryPresenter;
                ((DataGrid)sender).Columns.Clear();
                foreach (DataGridTextColumn dataGridColumn in budgetSummaryPresenter.SummaryColumns)
                {
                    ((DataGrid)sender).Columns.Add(new DataGridTextColumn() { Header = dataGridColumn.Header, Width = dataGridColumn.Width, Binding = dataGridColumn.Binding });
                }

            }
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            var budgetSummaryPresenter = ((DataGrid)sender).DataContext as IBudgetSummaryPresenter;

            if (budgetSummaryPresenter != null)
            {

                ((DataGrid)sender).Columns.Clear();
                bool noneFirst = false;
                foreach (DataGridTextColumn dataGridColumn in budgetSummaryPresenter.SummaryColumns)
                {
                    if (noneFirst)
                        ((DataGrid)sender).Columns.Add(new DataGridTextColumn() { Header = dataGridColumn.Header, Width = dataGridColumn.Width, Binding = dataGridColumn.Binding });
                    noneFirst = true;
                }

            }
        }

         public void Print()
        {

            ReportViews.Print();

        }

        public void Export()
        {
            ReportViews.Export();
        }
    }
}
