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
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.Views.ViewTypes;
using CompanyName.Atlas.UIControls;

namespace CompanyName.Atlas.Investments.Presentation.Views.Arrangement
{
    /// <summary>
    /// Interaction logic for BudgetView.xaml
    /// </summary>
    public partial class BudgetView : UserControl
    {
        public BudgetView()
        {
            InitializeComponent();
                IsVisibleChanged+=OnIsVisibleChanged;
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (IsVisible && (IBudgetPresenter)DataContext != null)
                ((IBudgetPresenter)DataContext).SecondView = BudgetViewType.All;
        }

        private void UIElement_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ////var value = (bool)e.NewValue;
            if (((FrameworkElement)sender).IsVisible)
            {
               
                  
                if (AtlasTabControl.FilterCommand != null&&!Equals((((FrameworkElement) sender).DataContext as ICrudViewModel), null)
                    &&!Equals(AtlasTabControl.FilterCommand , (((FrameworkElement)sender).DataContext as ICrudViewModel)?.SimpleFilterCommand)
                    &&!string.IsNullOrEmpty(AtlasTabControl.FilterCriteria))
                {
                        AtlasTabControl.FilterCommand.Execute("");
                        AtlasTabControl.FilterCommand = (((FrameworkElement)sender).DataContext as ICrudViewModel)?.SimpleFilterCommand;
                }
                     (((FrameworkElement)sender).DataContext as ICrudViewModel)?.Change();
            }
           
        }

        private void AtlasTabControl_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((UIControls.AtlasTabControl) sender).SelectedIndex > -1)
            {
                if ((IBudgetPresenter) DataContext != null)
                {
                    var tab =
                        ((UIControls.AtlasTabControl) sender).Items[((UIControls.AtlasTabControl) sender).SelectedIndex]
                        as AtlasTabItem;
                    if(!Equals(((IBudgetPresenter)DataContext).SecondView , tab?.View))
                    ((IBudgetPresenter) DataContext).SecondView = tab?.View;
                    if (!Equals(AtlasTabControl.FilterCommand, ((IBudgetPresenter) DataContext).FilterCommand)
                        && String.IsNullOrEmpty(AtlasTabControl.FilterCriteria))
                    {
                        AtlasTabControl.FilterCommand?.Execute("");
                        AtlasTabControl.FilterCommand = ((IBudgetPresenter)DataContext).FilterCommand;

                    }
                   
                }
                  
            }
        }
    }
}
