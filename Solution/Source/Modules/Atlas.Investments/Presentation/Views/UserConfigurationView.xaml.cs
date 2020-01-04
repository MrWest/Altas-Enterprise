﻿using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Presentation.Views.ViewTypes;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;


namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// This is the control where the items of a certain budget component are managed.
    /// </summary>
    public partial class UserConfigurationView
    {
        private readonly CompositeCommand _filterCommand = new CompositeCommand();
        private readonly INavigationServices _navigationServices = ServiceLocator.Current.GetInstance<INavigationServices>();
        private readonly string _assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        /// <summary>
        /// Dependency property containing the current view where there is currently in the budget component items tab controls of a
        /// <see cref="UserConfigurationView"/>.
        /// </summary>
        public static readonly DependencyProperty ViewProperty = DependencyProperty.Register("View", typeof(BudgetComponentItemViewType), typeof(UserConfigurationView), new PropertyMetadata(BudgetComponentItemViewType.PlannedItems));

        /// <summary>
        /// Dependency property containing the criteria to use in filtering the budget component items displayed in
        /// <see cref="UserConfigurationView"/> instances.
        /// </summary>
        public static readonly DependencyProperty FilterCriteriaProperty = DependencyProperty.Register("FilterCriteria", typeof(string), typeof(UserConfigurationView), new PropertyMetadata(null));

        private string optionalNavControlUri;
        /// <summary>
        /// Initializes a new instance of <see cref="UserConfigurationView"/>.
        /// </summary>
        public UserConfigurationView()
        {
            InitializeComponent();

            IsVisibleChanged += InvestmentVariablesEditor_IsVisibleChanged;
        }


        private void InvestmentVariablesEditor_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
         //   var isVisible = e.NewValue as bool?;

            if (IsVisible)
                _navigationServices.ShowOptionalNavigationContent();
            //else
            //    _navigationServices.HideOptionalNavigationContent();
        }
    
      
        /// <summary>
        /// Gets or sets the view where there is currently in the budget component items tab controls.
        /// </summary>
        public BudgetComponentItemViewType View
        {
            get { return (BudgetComponentItemViewType)GetValue(ViewProperty); }
            set { SetValue(ViewProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets the filtering criteria to use when filtering the budget component items by name.
        /// </summary>
        public string FilterCriteria
        {
            get { return (string)GetValue(FilterCriteriaProperty); }
            set { SetValue(FilterCriteriaProperty, value); }
        }

        
        /// <summary>
        /// Invoked when the data context of the current planning execution view changed.
        /// </summary>
        /// <param name="sender">The object who raised the event invoking the current method.</param>
        /// <param name="e">Arguments containing the details of the data context change event.</param>
        protected override void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            base.OnDataContextChanged(sender, e);

            var oldInvestmentElement = e.OldValue as IInvestmentElementPresenter;
            if (oldInvestmentElement != null)
            {
                BreakInteractions(oldInvestmentElement);
                EmptyFilterCommand(oldInvestmentElement);
            }

            var investmentElement = e.NewValue as IInvestmentElementPresenter;
            if (investmentElement == null)
                return;

            SetupInteractions(investmentElement);
            RegisterInFilterCommand(investmentElement);

            // TODO: Refactor this, to make this disapear, find the way around using Data Bindings in XAML
            //BindDataGrid(EquipmentPlannedResourcesDataGrid, investmentElement.Budget.EquipmentComponent.PlannedActivities);
            //BindDataGrid(EquipmentPlannedActivitiesDataGrid, investmentElement.Budget.EquipmentComponent.PlannedActivities);
            //BindDataGrid(EquipmentExecutedResourcesDataGrid, investmentElement.Budget.EquipmentComponent.ExecutedActivities);
            //BindDataGrid(EquipmentExecutedActivitiesDataGrid, investmentElement.Budget.EquipmentComponent.ExecutedActivities);

            //BindDataGrid(EquipmentBothPlannedResourcesDataGrid, investmentElement.Budget.EquipmentComponent.PlannedActivities);
            //BindDataGrid(EquipmentBothExecutedResourcesDataGrid, investmentElement.Budget.EquipmentComponent.ExecutedActivities);
            //BindDataGrid(EquipmentBothPlannedActivitiesDataGrid, investmentElement.Budget.EquipmentComponent.PlannedActivities);
            //BindDataGrid(EquipmentBothExecutedActivitiesDataGrid, investmentElement.Budget.EquipmentComponent.ExecutedActivities);


            //BindDataGrid(ConstructionPlannedResourcesDataGrid, investmentElement.Budget.ConstructionComponent.PlannedActivities);
            //BindDataGrid(ConstructionPlannedActivitiesDataGrid, investmentElement.Budget.ConstructionComponent.PlannedActivities);
            //BindDataGrid(ConstructionExecutedResourcesDataGrid, investmentElement.Budget.ConstructionComponent.ExecutedActivities);
            //BindDataGrid(ConstructionExecutedActivitiesDataGrid, investmentElement.Budget.ConstructionComponent.ExecutedActivities);

            //BindDataGrid(ConstructionBothPlannedResourcesDataGrid, investmentElement.Budget.ConstructionComponent.PlannedActivities);
            //BindDataGrid(ConstructionBothExecutedResourcesDataGrid, investmentElement.Budget.ConstructionComponent.ExecutedActivities);
            //BindDataGrid(ConstructionBothPlannedActivitiesDataGrid, investmentElement.Budget.ConstructionComponent.PlannedActivities);
            //BindDataGrid(ConstructionBothExecutedActivitiesDataGrid, investmentElement.Budget.ConstructionComponent.ExecutedActivities);


            //BindDataGrid(OtherExpensesPlannedResourcesDataGrid, investmentElement.Budget.OtherExpensesComponent.PlannedActivities);
            //BindDataGrid(OtherExpensesPlannedActivitiesDataGrid, investmentElement.Budget.OtherExpensesComponent.PlannedActivities);
            //BindDataGrid(OtherExpensesExecutedResourcesDataGrid, investmentElement.Budget.OtherExpensesComponent.ExecutedActivities);
            //BindDataGrid(OtherExpensesExecutedActivitiesDataGrid, investmentElement.Budget.OtherExpensesComponent.ExecutedActivities);

            //BindDataGrid(OtherExpensesBothPlannedResourcesDataGrid, investmentElement.Budget.OtherExpensesComponent.PlannedActivities);
            //BindDataGrid(OtherExpensesBothExecutedResourcesDataGrid, investmentElement.Budget.OtherExpensesComponent.ExecutedActivities);
            //BindDataGrid(OtherExpensesBothPlannedActivitiesDataGrid, investmentElement.Budget.OtherExpensesComponent.PlannedActivities);
            //BindDataGrid(OtherExpensesBothExecutedActivitiesDataGrid, investmentElement.Budget.OtherExpensesComponent.ExecutedActivities);


            //BindDataGrid(WorkCapitalPlannedResourcesDataGrid, investmentElement.Budget.WorkCapitalComponent.PlannedActivities);
            //BindDataGrid(WorkCapitalPlannedActivitiesDataGrid, investmentElement.Budget.WorkCapitalComponent.PlannedActivities);
            //BindDataGrid(WorkCapitalExecutedResourcesDataGrid, investmentElement.Budget.WorkCapitalComponent.ExecutedActivities);
            //BindDataGrid(WorkCapitalExecutedActivitiesDataGrid, investmentElement.Budget.WorkCapitalComponent.ExecutedActivities);
            //BindDataGrid(WorkCapitalBothPlannedResourcesDataGrid, investmentElement.Budget.WorkCapitalComponent.PlannedActivities);
            //BindDataGrid(WorkCapitalBothExecutedResourcesDataGrid, investmentElement.Budget.WorkCapitalComponent.ExecutedActivities);
            //BindDataGrid(WorkCapitalBothPlannedActivitiesDataGrid, investmentElement.Budget.WorkCapitalComponent.PlannedActivities);
            //BindDataGrid(WorkCapitalBothExecutedActivitiesDataGrid, investmentElement.Budget.WorkCapitalComponent.ExecutedActivities);

            //BindDataGrid(WorkCapitalCashFlowViewer, investmentElement.Budget.WorkCapitalComponent);

            if (_filterCommand != null && _filterCommand.CanExecute(FilterCriteria))
                _filterCommand.Execute(FilterCriteria);
        }

        private void BindDataGrid(WorkCapitalCashFlowView workCapitalCashFlowViewer, IWorkCapitalComponentPresenter viewModel)
        {
            workCapitalCashFlowViewer.SetBinding(DataContextProperty, new Binding("WorkCapitalCashFlow") { Source = viewModel });
        }


        private void RegisterInFilterCommand(IInvestmentElementPresenter investmentElement)
        {
           //  _filterCommand.RegisterCommand(investmentElement.Budget.EquipmentComponent.PlannedActivities.FilterCommand);
           // _filterCommand.RegisterCommand(investmentElement.Budget.EquipmentComponent.ExecutedResources.FilterCommand);
            _filterCommand.RegisterCommand(investmentElement.Budget.EquipmentComponent.PlannedActivities.FilterCommand);
            _filterCommand.RegisterCommand(investmentElement.Budget.EquipmentComponent.ExecutedActivities.FilterCommand);

           // _filterCommand.RegisterCommand(investmentElement.Budget.ConstructionComponent.PlannedResources.FilterCommand);
          //  _filterCommand.RegisterCommand(investmentElement.Budget.ConstructionComponent.ExecutedResources.FilterCommand);
            _filterCommand.RegisterCommand(investmentElement.Budget.ConstructionComponent.PlannedActivities.FilterCommand);
            _filterCommand.RegisterCommand(investmentElement.Budget.ConstructionComponent.ExecutedActivities.FilterCommand);

          //  _filterCommand.RegisterCommand(investmentElement.Budget.OtherExpensesComponent.PlannedResources.FilterCommand);
           // _filterCommand.RegisterCommand(investmentElement.Budget.OtherExpensesComponent.ExecutedResources.FilterCommand);
            _filterCommand.RegisterCommand(investmentElement.Budget.OtherExpensesComponent.PlannedActivities.FilterCommand);
            _filterCommand.RegisterCommand(investmentElement.Budget.OtherExpensesComponent.ExecutedActivities.FilterCommand);

           // _filterCommand.RegisterCommand(investmentElement.Budget.WorkCapitalComponent.PlannedResources.FilterCommand);
          //  _filterCommand.RegisterCommand(investmentElement.Budget.WorkCapitalComponent.ExecutedResources.FilterCommand);
            _filterCommand.RegisterCommand(investmentElement.Budget.WorkCapitalComponent.PlannedActivities.FilterCommand);
            _filterCommand.RegisterCommand(investmentElement.Budget.WorkCapitalComponent.ExecutedActivities.FilterCommand);
        }

        private void EmptyFilterCommand(IInvestmentElementPresenter investmentElement)
        {
            // _filterCommand.UnregisterCommand(investmentElement.Budget.EquipmentComponent.PlannedResources.FilterCommand);
           // _filterCommand.UnregisterCommand(investmentElement.Budget.EquipmentComponent.ExecutedResources.FilterCommand);
            _filterCommand.UnregisterCommand(investmentElement.Budget.EquipmentComponent.PlannedActivities.FilterCommand);
            _filterCommand.UnregisterCommand(investmentElement.Budget.EquipmentComponent.ExecutedActivities.FilterCommand);

            //_filterCommand.UnregisterCommand(investmentElement.Budget.ConstructionComponent.PlannedResources.FilterCommand);
            //_filterCommand.UnregisterCommand(investmentElement.Budget.ConstructionComponent.ExecutedResources.FilterCommand);
            _filterCommand.UnregisterCommand(investmentElement.Budget.ConstructionComponent.PlannedActivities.FilterCommand);
            _filterCommand.UnregisterCommand(investmentElement.Budget.ConstructionComponent.ExecutedActivities.FilterCommand);

           // _filterCommand.UnregisterCommand(investmentElement.Budget.OtherExpensesComponent.PlannedResources.FilterCommand);
           // _filterCommand.UnregisterCommand(investmentElement.Budget.OtherExpensesComponent.ExecutedResources.FilterCommand);
            _filterCommand.UnregisterCommand(investmentElement.Budget.OtherExpensesComponent.PlannedActivities.FilterCommand);
            _filterCommand.UnregisterCommand(investmentElement.Budget.OtherExpensesComponent.ExecutedActivities.FilterCommand);

           // _filterCommand.UnregisterCommand(investmentElement.Budget.WorkCapitalComponent.PlannedResources.FilterCommand);
          //  _filterCommand.UnregisterCommand(investmentElement.Budget.WorkCapitalComponent.ExecutedResources.FilterCommand);
            _filterCommand.UnregisterCommand(investmentElement.Budget.WorkCapitalComponent.PlannedActivities.FilterCommand);
            _filterCommand.UnregisterCommand(investmentElement.Budget.WorkCapitalComponent.ExecutedActivities.FilterCommand);
        }

        private void BreakInteractions(IInvestmentElementPresenter investmentElement)
        {

        }

        private void SetupInteractions(IInvestmentElementPresenter investmentElement)
        {
        
        }

        private void BindDataGrid(AtlasDataGrid dataGrid, ICrudViewModel viewModel)
        {
            dataGrid.AddButtonCommand = viewModel.AddCommand;
            dataGrid.DeleteButtonCommand = viewModel.DeleteCommand;
            dataGrid.PageCommand = ((IBudgetComponentItemViewModel) viewModel).FilterCommand;

            dataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Items") { Source = viewModel });
            dataGrid.SetBinding(Selector.SelectedItemProperty, new Binding("SelectedItem") { Source = viewModel });
        }

        private void BindDataGrid(BudgetComponentResourceDataGrid dataGrid, ICrudViewModel viewModel)
        {
            dataGrid.AddButtonCommand = viewModel.AddCommand;
            dataGrid.DeleteResourceCommand = viewModel.DeleteCommand;
           // dataGrid.PageCommand = ((IBudgetComponentItemViewModel)viewModel).FilterCommand;
            
            dataGrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Items") { Source = viewModel });
            dataGrid.SetBinding(Selector.SelectedItemProperty, new Binding("SelectedItem") { Source = viewModel });
        }

        private void BindDataGrid(PrismUserControlBase userControlBase, IPresenter viewModel)
        {
          //  userControlBase.AddButtonCommand = viewModel.AddCommand;
           // userControlBase.DeleteResourceCommand = viewModel.DeleteCommand;
            // dataGrid.PageCommand = ((IBudgetComponentItemViewModel)viewModel).FilterCommand;

            userControlBase.SetBinding(DataContextProperty, new Binding("WorkCapitalComponent") { Source = viewModel });
          //  userControlBase.SetBinding(Selector.SelectedItemProperty, new Binding("SelectedItem") { Source = viewModel });
        }
        private void FilterCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _filterCommand != null && _filterCommand.CanExecute(e.Parameter);

            e.Handled = true;
        }

        private void FilterCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //TODO this isnt working
            if (_filterCommand != null)
                _filterCommand.Execute(e.Parameter);

            e.Handled = true;
        }
    }
}
