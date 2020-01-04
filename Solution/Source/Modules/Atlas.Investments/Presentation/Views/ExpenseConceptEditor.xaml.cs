using System.Windows;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.UIControls;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    ///     Interaction logic for ExpenseConceptEditor.xaml
    /// </summary>
    public partial class ExpenseConceptEditor
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="ExpenseConceptEditor" />.
        /// </summary>
        public ExpenseConceptEditor()
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