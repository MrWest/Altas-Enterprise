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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;

namespace CompanyName.Atlas.Investments.Presentation.Views.Arrangement
{
    /// <summary>
    /// Interaction logic for ExecutionView.xaml
    /// </summary>
    public partial class ExecutionView : UserControl
    {
        public ExecutionView()
        {
            InitializeComponent();
        }

        private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                AtlasDataGrid.AddButtonCommand = ((ICrudViewModel)e.NewValue).AddCommand;
                AtlasDataGrid.DeleteButtonCommand = ((ICrudViewModel)e.NewValue).DeleteCommand;
            }
           
        }

        private void ExecutionView_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //ExecutionComponentItemDataGrid.ExecutePlannedItemsCommand = ((IExecutedBudgetComponentItemViewModel)e.NewValue).ExecutePlannedItemsCommand;
           
        }

        private void AtlasDataGrid_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                AtlasDataGrid.AddButtonCommand = ((ICrudViewModel)e.NewValue).AddCommand;
                AtlasDataGrid.DeleteButtonCommand = ((ICrudViewModel)e.NewValue).DeleteCommand;
            }
        }

        private void ExecutedSubSpecialityHolderDataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ExecutionDockPanel.DataContext != null)
            {
                var story = ((Storyboard)this.FindResource("CollapseStoryboard"));

                story.Begin(ExecutionDockPanel);
            }
           
           //ExecutionDockPanel.BeginAnimation(WidthProperty,story.an);
        }

        private void ExecutionDockPanel_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (((FrameworkElement) sender).DataContext != null)
            {
                var story = ((Storyboard) this.FindResource("ExpandStoryboard"));

                story.Begin(ExecutionDockPanel);
            }
            else
            {
                var story = ((Storyboard)this.FindResource("CollapseStoryboard"));

                story.Begin(ExecutionDockPanel);
            }

        }
    }
}
