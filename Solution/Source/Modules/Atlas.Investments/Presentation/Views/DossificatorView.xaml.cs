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
using CompanyName.Atlas.Investments.Application.Budget.Construction;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.Prism.Commands;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for Dossificator.xaml
    /// </summary>
    public partial class DossificatorView 
    {
       
        public DossificatorView()
        {
            InitializeComponent();
        }

      

        private ICrudViewModel ViewModel
        {
            get
            {

                return DataContext as ICrudViewModel;
            }
        }

        private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            BindDataGrid(EquipmentPlannedActivitiesDataGrid, (((TabControl)(sender)).DataContext as IConstructionPlannedActivityViewModel));
            BindDataGrid(EquipmentPlannedResourcesDataGrid, (((TabControl)(sender)).DataContext as IConstructionPlannedActivityViewModel));

        }
        private void BindDataGrid(AtlasDataGrid dataGrid, ICrudViewModel viewModel)
        {
            dataGrid.AddButtonCommand = viewModel.AddCommand;
            dataGrid.DeleteButtonCommand = viewModel.DeleteCommand;
            dataGrid.PageCommand = ((IBudgetComponentItemViewModel)viewModel).FilterCommand;

            dataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Items") { Source = viewModel });
            dataGrid.SetBinding(Selector.SelectedItemProperty, new Binding("SelectedItem") { Source = viewModel });
        }

        private void BindDataGrid(BudgetComponentResourceDataGrid dataGrid, ICrudViewModel viewModel)
        {
            if (viewModel == null)
                return;

            dataGrid.AddButtonCommand = viewModel.AddCommand;
            dataGrid.DeleteResourceCommand = viewModel.DeleteCommand;
            // dataGrid.PageCommand = ((IBudgetComponentItemViewModel)viewModel).FilterCommand;

            dataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Items") { Source = viewModel });
            dataGrid.SetBinding(Selector.SelectedItemProperty, new Binding("SelectedItem") { Source = viewModel });
        }


        /// <summary>
        /// Invoked when the datacontext for the current view has been changed. Makes sure that the interactions channels
        /// between this view and the new investment element view model are wired up.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The event arguments containing the details about the datacontext change.</param>
        protected override void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            base.OnDataContextChanged(sender, e);

            var viewModel = e.NewValue as IDossificatorViewModel;
            if (viewModel == null)
                return;

            RecursivelySetupInteractions(viewModel);
        }

        private void RecursivelySetupInteractions(ICrudViewModel viewModel)
        {
            viewModel.AddedItem += SetupInteraction;
            viewModel.DeletedItem += BreakInteraction;

            foreach (IInvestmentElementPresenter invElemPresenter in viewModel.Items)
            {
                invElemPresenter.Elements.Raised += OnInteractionRequested;
                RecursivelySetupInteractions(invElemPresenter.Elements);
            }
        }

        private void RecursivelyBreakInteractions(ICrudViewModel viewModel)
        {
            viewModel.AddedItem -= SetupInteraction;
            viewModel.DeletedItem -= BreakInteraction;
            viewModel.Raised -= OnInteractionRequested;

            foreach (IInvestmentElementPresenter invElemPresenter in viewModel.Items)
                RecursivelyBreakInteractions(invElemPresenter.Elements);
        }

        private void SetupInteraction(object sender, ItemEventArgs e)
        {
            var invElemPresenter = (IInvestmentElementPresenter)e.Item;
            invElemPresenter.Elements.Raised += OnInteractionRequested;
            RecursivelySetupInteractions(invElemPresenter.Elements);
        }

        private void BreakInteraction(object sender, ItemEventArgs e)
        {
            var invElemPresenter = (IInvestmentElementPresenter)e.Item;
            RecursivelyBreakInteractions(invElemPresenter.Elements);
        }

        #region Command event handlers

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var invElemPresenter = e.Parameter as IInvestmentElementPresenter;

            e.CanExecute = invElemPresenter != null && invElemPresenter.Elements.AddCommand.CanExecute(null);
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var invElemPresenter = e.Parameter as IInvestmentElementPresenter;

            if (invElemPresenter == null)
            {
                if (ViewModel != null)
                    ViewModel.AddCommand.Execute(e.Parameter);
            }
            else
            {
                invElemPresenter.Elements.AddCommand.Execute(null);
                invElemPresenter.IsExpanded = true;
            }
        }

        private void DeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var invElemPresenter = e.Parameter as IInvestmentElementPresenter;

            if (invElemPresenter == null || invElemPresenter.Parent == null)
            {
                var type = e.Parameter.GetType();
                e.CanExecute = ViewModel != null && ViewModel.DeleteCommand.CanExecute(e.Parameter);
            }
            else
            {
                var parent = invElemPresenter.Parent as IInvestmentElementPresenter;
                e.CanExecute = parent != null && parent.Elements.DeleteCommand.CanExecute(e.Parameter);
            }
        }

        private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var invElemPresenter = e.Parameter as IInvestmentElementPresenter;

            if (invElemPresenter == null || invElemPresenter.Parent == null)
            {
                if (ViewModel != null)
                    ViewModel.DeleteCommand.Execute(e.Parameter);
            }
            else
            {
                var parent = invElemPresenter.Parent as IInvestmentElementPresenter;
                if (parent != null)
                    parent.Elements.DeleteCommand.Execute(e.Parameter);
            }
        }

        #endregion
    }
}
