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
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.UIControls;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for BudgetConstructionView.xaml
    /// </summary>
    public partial class BudgetConstructionView : UserControl
    {
        public BudgetConstructionView()
        {
            InitializeComponent();
        }

        ////private void BudgetConstructionView_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        ////{
        ////  //  base.OnDataContextChanged(sender, e);

        ////    var oldInvestmentElement = e.OldValue as IInvestmentElementPresenter;
           
        ////    var investmentElement = e.NewValue as IInvestmentElementPresenter;
        ////    if (investmentElement == null)
        ////        return;

        ////    BindDataGrid(EquipmentPlannedResourcesDataGrid, investmentElement.Budget.ConstructionComponent.PlannedActivities);
        ////    BindDataGrid(EquipmentPlannedActivitiesDataGrid, investmentElement.Budget.ConstructionComponent.PlannedActivities);
          
        ////}

        ////private void BindDataGrid(AtlasDataGrid dataGrid, ICrudViewModel viewModel)
        ////{
        ////    dataGrid.AddButtonCommand = viewModel.AddCommand;
        ////    dataGrid.DeleteButtonCommand = viewModel.DeleteCommand;
        ////    dataGrid.PageCommand = ((IBudgetComponentItemViewModel)viewModel).FilterCommand;

        ////    dataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Items") { Source = viewModel });
        ////    dataGrid.SetBinding(Selector.SelectedItemProperty, new Binding("SelectedItem") { Source = viewModel });
        ////}

        ////private void BindDataGrid(BudgetComponentResourceDataGrid dataGrid, ICrudViewModel viewModel)
        ////{
        ////    dataGrid.AddButtonCommand = viewModel.AddCommand;
        ////    dataGrid.DeleteResourceCommand = viewModel.DeleteCommand;
        ////    // dataGrid.PageCommand = ((IBudgetComponentItemViewModel)viewModel).FilterCommand;

        ////    dataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Items") { Source = viewModel });
        ////    dataGrid.SetBinding(Selector.SelectedItemProperty, new Binding("SelectedItem") { Source = viewModel });
        ////}
    }
}
