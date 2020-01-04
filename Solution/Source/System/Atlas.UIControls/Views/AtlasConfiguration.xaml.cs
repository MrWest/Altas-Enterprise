using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Infralution.Localization.Wpf;

namespace CompanyName.Atlas.UIControls.Views
{
    /// <summary>
    /// Interaction logic for AtlasConfiguration.xaml
    /// </summary>
    public partial class AtlasConfiguration : UserControl
    {
        public AtlasConfiguration()
        {
            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CultureManager.UICulture = new CultureInfo(e.AddedItems[0].ToString());
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            ((ComboBox) sender).SelectedItem = CultureManager.UICulture.ToString();
        }
    }
}
