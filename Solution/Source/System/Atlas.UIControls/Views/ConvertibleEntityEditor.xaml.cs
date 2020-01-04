using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.UIControls.Views
{
    /// <summary>
    /// Interaction logic for ConvertibleEntityEditor.xaml
    /// </summary>
    public partial class ConvertibleEntityEditor : UserControl
    {
        public ConvertibleEntityEditor()
        {
            InitializeComponent();
        }
        private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                ((AtlasDataGrid)sender).AddButtonCommand = ((ICrudViewModel)e.NewValue).AddCommand;
                ((AtlasDataGrid)sender).DeleteButtonCommand = ((ICrudViewModel)e.NewValue).DeleteCommand;
                ((AtlasDataGrid)sender).SelectedItem = ((ICrudViewModel)e.NewValue).SelectedItem;
            }
        }
    }
}
