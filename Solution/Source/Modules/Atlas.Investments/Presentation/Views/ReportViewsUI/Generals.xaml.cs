using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using CompanyName.Atlas.Contracts.Presentation.Features;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Reporting;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Presentation.Views.ReportViewsUI
{
    /// <summary>
    /// Interaction logic for Generals.xaml
    /// </summary>
    public partial class Generals : UserControl, IPrinttableContainer
    {
        public Generals()
        {
            InitializeComponent();
           DataContextChanged+=OnDataContextChanged;
        }

      


        private void FrameworkContentElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                StatusBarServices.SignalText2(Atlas.UIControls.Properties.Resources.Loading);

                this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
        (ThreadStart)delegate () {

           
            // DocumentPageViewer.Print();
            Style rightStyle = (Style)this.FindResource("DataGridCellRighted");

            Style rightStyleHeader = (Style)this.FindResource("DataGridColumnHeaderRighted");

            Style rightStyleHeader2 = (Style)this.FindResource("DataGridColumnHeaderLeft");

            var budgetSummaryPresenter = e.NewValue as IBudgetSummaryPresenter;
            ((DataGrid)sender).Columns.Clear();

            bool isFirst = true;
            foreach (DataGridTextColumn dataGridColumn in budgetSummaryPresenter.SummaryColumns)
            {
                if (isFirst)
                {
                    ((DataGrid)sender).Columns.Add(new DataGridTextColumn() { Header = dataGridColumn.Header, Width = dataGridColumn.Width, Binding = dataGridColumn.Binding, HeaderStyle = rightStyleHeader2 });
                    isFirst = false;
                }
                else
                {
                    ((DataGrid)sender).Columns.Add(new DataGridTextColumn() { Header = dataGridColumn.Header, Width = dataGridColumn.Width, Binding = dataGridColumn.Binding, CellStyle = rightStyle, HeaderStyle = rightStyleHeader });
                }

            }

            int count = 0;
            PeriodGrid.Children.Clear();
            PeriodGrid.ColumnDefinitions.Clear();

            foreach (IBudgetSummaryPresenter budgetSummaryOverall in budgetSummaryPresenter.BudgetSummaryOverall)
            {

                PeriodGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(35, GridUnitType.Auto) });
                var periodDatagrid =
                new DataGrid()
                {
                    Background = Brushes.Transparent,
                    AutoGenerateColumns = false,
                    ColumnWidth = new DataGridLength(35, DataGridLengthUnitType.Auto),
                    IsReadOnly = true,
                    Margin = new Thickness(0, 0, 0, 0),
                    CanUserSortColumns = false,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    CanUserAddRows = false,
                    CanUserResizeRows = false,
                    CanUserReorderColumns = false,
                    CanUserResizeColumns = false
                };
                periodDatagrid.Resources = SummaryDataGrid.Resources;

                var contentBinding = new Binding { Source = budgetSummaryOverall.BudgetSummary };
                 //ContentPlace.SetBinding(ContentProperty, contentBinding);
                 periodDatagrid.SetBinding(ItemsControl.ItemsSourceProperty, contentBinding);

                periodDatagrid.CellStyle = (Style)FindResource("DataGridCell");

                Grid.SetColumn(periodDatagrid, count);
                Grid.SetRow(periodDatagrid, 1);

                var tittle = new TextBlock() { Text = budgetSummaryPresenter.Period.Periods[count].Name, VerticalAlignment = VerticalAlignment.Center };

                var namebottombody = new Border
                {
                    Padding = new Thickness { Bottom = 0, Top = 0, Left = 10, Right = 0 },
                    BorderThickness = new Thickness { Bottom = 0, Top = 0, Left = 0, Right = 1 },
                    Background = new SolidColorBrush(Color.FromRgb(211, 211, 211)),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(243, 242, 233)),
                    Height = 35,


                };

                namebottombody.Child = tittle;

                Grid.SetColumn(namebottombody, count);

                var dataGridbody = new Border
                {
                    DataContext = budgetSummaryOverall,
                    Margin = new Thickness { Bottom = 0, Top = 0, Left = 0, Right = 0 },
                    BorderThickness = new Thickness { Bottom = 0, Top = 0, Left = 0, Right = 1 },
                    BorderBrush = new SolidColorBrush(Color.FromRgb(243, 242, 233))

                };

                if (count == 0)
                {
                    namebottombody.BorderThickness = new Thickness { Bottom = 0, Top = 0, Left = 1, Right = 1 };
                    dataGridbody.BorderThickness = new Thickness { Bottom = 0, Top = 0, Left = 1, Right = 1 };
                }


                isFirst = true;
                foreach (DataGridTextColumn dataGridColumn in budgetSummaryOverall.SummaryColumns)
                {
                    if (isFirst)
                    {
                         //  periodDatagrid.Columns.Add(new DataGridTextColumn() { Header = dataGridColumn.Header, Width = dataGridColumn.Width, Binding = dataGridColumn.Binding, HeaderStyle = rightStyleHeader2 });
                         isFirst = false;
                    }
                    else
                    {
                        periodDatagrid.Columns.Add(new DataGridTextColumn() { Header = dataGridColumn.Header, Width = new DataGridLength(35, DataGridLengthUnitType.Auto), Binding = dataGridColumn.Binding, CellStyle = rightStyle, HeaderStyle = rightStyleHeader });
                    }

                }


                 //periodDatagrid.DataContext = dataGridbody.DataContext;
                 //periodDatagrid.ItemsSource = budgetSummaryOverall.BudgetSummary;
                 dataGridbody.Child = periodDatagrid;

                Grid.SetColumn(dataGridbody, count);
                Grid.SetRow(dataGridbody, 1);

                PeriodGrid.Children.Add(namebottombody);
                PeriodGrid.Children.Add(dataGridbody);
                count++;

            }

            StatusBarServices.SignalText2(Atlas.UIControls.Properties.Resources.Ready);
        }


        );

            }

          
        }
        private  void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
           (ThreadStart)delegate () {
               bool makeContentBinding = true;
               object dataContext = e.NewValue;

               if (dataContext is IInvestmentPresenter)
                   ContentPlace.ContentTemplate = (DataTemplate)FindResource("InvestmentDataTemplate");
               else if (dataContext is IInvestmentComponentPresenter<IInvestmentComponent> || dataContext is IInvestmentComponentPresenter<IInvestment>)
                   ContentPlace.ContentTemplate = (DataTemplate)FindResource("InvestmentComponentDataTemplate");
               else
                   makeContentBinding = false;

               if (!makeContentBinding) return;

               var contentBinding = new Binding { Source = dataContext };
               ContentPlace.SetBinding(ContentProperty, contentBinding);
           }
           );

            
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
                    if(noneFirst)
                    ((DataGrid)sender).Columns.Add(new DataGridTextColumn() { Header = dataGridColumn.Header, Width = dataGridColumn.Width, Binding = dataGridColumn.Binding });
                    noneFirst = true;
                }

                
               
            }
        }
        private IStatusBarServices _statusBarServices;
        private IStatusBarServices StatusBarServices
        {
            get
            {
                return _statusBarServices ?? (_statusBarServices = ServiceLocator.Current.GetInstance<IStatusBarServices>());
            }
        }
        public void Print()
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
            (ThreadStart)delegate () {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    //Notify status bar
                    StatusBarServices.SignalText2(Properties.Resources.PrintingWait);

                    //printDialog.PrintableAreaHeight = PrintGrid.ActualHeight;
                    //printDialog.PrintableAreaWidth = PrintGrid.ActualWidth;
                    
                    printDialog.PrintVisual(
                    PrintGrid,
                    Properties.Resources.Generals);

                    StatusBarServices.SignalText2(UIControls.Properties.Resources.Ready);
                  
                }
            }
            );
        }
    }
}
