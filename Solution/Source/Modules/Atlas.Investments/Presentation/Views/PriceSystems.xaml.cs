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
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.UIControls;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for PriceSystems.xaml
    /// </summary>
    public partial class PriceSystems 
    {
        /// <summary>
        /// Dependency property containing the Filter command for instances of <see cref="FilterBox"/>.
        /// </summary>
        public static readonly DependencyProperty FilterCommandProperty = DependencyProperty.Register("FilterCommand", typeof(ICommand), typeof(PriceSystems));

        /// <summary>
        /// contructor area
        /// </summary>
        public PriceSystems()
        {
            InitializeComponent();
         
        }


        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public ICommand FilterCommand
        {
            get { return (ICommand)GetValue(FilterCommandProperty); }
            set { SetValue(FilterCommandProperty, value); }
        }

        private void UIElement_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ////var value = (bool)e.NewValue;
            if (((FrameworkElement)sender).IsVisible && !string.IsNullOrEmpty(AtlasTabControl.FilterCriteria))
            {
                if (AtlasTabControl.FilterCommand != null)
                    AtlasTabControl.FilterCommand.Execute("");
                if(!Equals((((FrameworkElement)sender).DataContext as ICrudViewModel),null))
                AtlasTabControl.FilterCommand = (((FrameworkElement)sender).DataContext as ICrudViewModel).SimpleFilterCommand;
            }
            else
            {
                if ((IPriceSystemPresenter) PsComboBox.SelectedItem != null && !string.IsNullOrEmpty(AtlasTabControl.FilterCriteria))
                {
                    AtlasTabControl.FilterCommand?.Execute("");
                    AtlasTabControl.FilterCommand = ((IPriceSystemPresenter)PsComboBox.SelectedItem).FilterCommand;

                }
            }
        }

        private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var navigableViewModel = e.NewValue as INavigableViewModel;
            if (!Equals(navigableViewModel,null))
            {
                AtlasTabControl.FilterCommand?.Execute("");
                AtlasTabControl.FilterCommand = navigableViewModel.FilterCommand;
            }
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            AtlasTabControl.Variables = ((ToggleButton) sender).IsChecked.Value;
        }
    }
}
