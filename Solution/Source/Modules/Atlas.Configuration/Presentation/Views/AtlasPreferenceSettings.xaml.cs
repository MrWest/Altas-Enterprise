using System;
using System.Collections.Generic;
using System.Globalization;
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
using CompanyName.Atlas.Contracts.Presentation.Services;
using Infralution.Localization.Wpf;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Configuration.Presentation.Views
{
    /// <summary>
    /// Interaction logic for AtlasPreferenceSettings.xaml
    /// </summary>
    public partial class AtlasPreferenceSettings : UserControl
    {
        public AtlasPreferenceSettings()
        {
            InitializeComponent();
            IsVisibleChanged += InvestmentVariablesEditor_IsVisibleChanged;
        }

        private readonly INavigationServices _navigationServices = ServiceLocator.Current.GetInstance<INavigationServices>();
        private void InvestmentVariablesEditor_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //   var isVisible = e.NewValue as bool?;

            if (IsVisible)
                _navigationServices.HideOptionalNavigationContent();
            //else
            //    _navigationServices.ShowOptionalNavigationContent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CultureManager.UICulture = new CultureInfo(e.AddedItems[0].ToString());
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            ((ComboBox)sender).SelectedItem = CultureManager.UICulture.ToString();
        }

    }
}
