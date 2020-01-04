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
using CompanyName.Atlas.UIControls;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for VariantLinesViewer.xaml
    /// </summary>
    public partial class VariantLinesViewer 
    {
        public VariantLinesViewer()
        {
            InitializeComponent();
        }
        private void BindDataGrid(AtlasDataGrid dataGrid, ICrudViewModel viewModel)
        {
            dataGrid.AddButtonCommand = viewModel.AddCommand;
            dataGrid.DeleteButtonCommand = viewModel.DeleteCommand;
            // dataGrid.PageCommand = ((IBudgetComponentItemViewModel)viewModel).FilterCommand;

            dataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Items") { Source = viewModel });
            dataGrid.SetBinding(Selector.SelectedItemProperty, new Binding("SelectedItem") { Source = viewModel });
        }


      


        private void VariantLinesViewer_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (EquipmentPlannedActivitiesDataGrid != null)//&&(e.NewValue as Visibility) ==Visibility.Visible)
            {
                BindDataGrid(EquipmentPlannedActivitiesDataGrid, EquipmentPlannedActivitiesDataGrid.DataContext as ICrudViewModel);
                BindDataGrid(EquipmentPlannedResourcesDataGrid, EquipmentPlannedResourcesDataGrid.DataContext as ICrudViewModel);
            }
                
        }
    }


}
