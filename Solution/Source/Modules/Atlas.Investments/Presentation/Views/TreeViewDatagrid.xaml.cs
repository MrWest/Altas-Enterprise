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
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.UIControls;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for TreeViewDatagrid.xaml
    /// </summary>
    public partial class TreeViewDatagrid : UserControl
    {
        public TreeViewDatagrid()
        {
            InitializeComponent();
        }

        private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (((AtlasDataGrid)(sender)).DataContext != null)
            {
                ((AtlasDataGrid)(sender)).AddButtonCommand = (((AtlasDataGrid)(sender)).DataContext as ICrudViewModel).AddCommand;
                ((AtlasDataGrid)(sender)).DeleteButtonCommand = (((AtlasDataGrid)(sender)).DataContext as ICrudViewModel).DeleteCommand;
                // dataGrid.PageCommand = ((IBudgetComponentItemViewModel)viewModel).FilterCommand;

                ((AtlasDataGrid)(sender)).SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Items") { Source = ((AtlasDataGrid)(sender)).DataContext });
                ((AtlasDataGrid)(sender)).SetBinding(Selector.SelectedItemProperty, new Binding("SelectedItem") { Source = ((AtlasDataGrid)(sender)).DataContext });

            }

        }
    }
}
