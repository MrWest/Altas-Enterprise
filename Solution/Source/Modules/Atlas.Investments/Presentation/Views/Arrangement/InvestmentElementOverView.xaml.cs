using CompanyName.Atlas.Contracts.Presentation.Services;
using Microsoft.Practices.ServiceLocation;
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
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.UIControls;

namespace CompanyName.Atlas.Investments.Presentation.Views.Arrangement
{
    /// <summary>
    /// Interaction logic for InvestmentElementOverView.xaml
    /// </summary>
    public partial class InvestmentElementOverView : UserControl
    {
        /// <summary>
        /// constructor
        /// </summary>
        public InvestmentElementOverView()
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

        private void UIElement_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ////var value = (bool)e.NewValue;
            if (((FrameworkElement)sender).IsVisible && !Equals(AtlasTabControl.FilterCommand , (((FrameworkElement)sender).DataContext as ICrudViewModel)?.SimpleFilterCommand))
            {
                if (AtlasTabControl.FilterCommand != null && !Equals((((FrameworkElement)sender).DataContext as ICrudViewModel), null)
                   && !string.IsNullOrEmpty(AtlasTabControl.FilterCriteria))
                {
                    AtlasTabControl.FilterCommand.Execute("");
                    AtlasTabControl.FilterCommand = (((FrameworkElement)sender).DataContext as ICrudViewModel)?.SimpleFilterCommand;
                }

                (((FrameworkElement)sender).DataContext as ICrudViewModel)?.Change();
            }
          
        }

        private void AtlasTabControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((UIControls.AtlasTabControl)sender).SelectedIndex > -1)
            {
                if ((DataContext as IInvestmentPresenter) != null)
                {
                    //var tab =
                    //    ((UIControls.AtlasTabControl)sender).Items[((UIControls.AtlasTabControl)sender).SelectedIndex]
                    //    as AtlasTabItem;
                    // ((IInvestmentPresenter)DataContext).SecondView = tab.View;

                    if (AtlasTabControl.FilterCommand !=
                        ((IInvestmentPresenter) DataContext).Documents.SimpleFilterCommand)
                    {
                        AtlasTabControl.FilterCommand?.Execute("");
                        AtlasTabControl.FilterCommand = ((IInvestmentPresenter)DataContext).Documents.SimpleFilterCommand;
                    }

                   
                }

            }
        }
    }
}
