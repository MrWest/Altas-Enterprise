using System;
using System.Collections;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;
using DataGrid = System.Windows.Controls.DataGrid;
using DragDropEffects = System.Windows.DragDropEffects;
using DragEventArgs = System.Windows.DragEventArgs;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using ListBox = System.Windows.Controls.ListBox;
using TextBox = System.Windows.Controls.TextBox;

namespace CompanyName.Atlas.Investments.Presentation.Views.Arrangement
{
    /// <summary>
    ///     Interaction logic for BudgetComponentItemTabControl.xaml.
    /// </summary>
    public partial class UserRolesDataGrid : IView
    {
        /// <summary>
        ///     Dependency property used to contain the flag that allows drag and drop operations in the instances of
        ///     <see cref="UserRolesDataGrid" />.
        /// </summary>
        public static readonly DependencyProperty AllowDragDropProperty = DependencyProperty.Register("AllowDragDrop", typeof(bool), typeof(UserRolesDataGrid), new PropertyMetadata(false));

        /// <summary>
        ///     Dependency property containing the command that allows to execute selected planned items in instance of
        ///     <see cref="UserRolesDataGrid" />.
        /// </summary>
        public static readonly DependencyProperty ExecutePlannedItemsCommandProperty = DependencyProperty.Register("ExecutePlannedItemsCommand", typeof(ICommand), typeof(UserRolesDataGrid), new PropertyMetadata(new DisabledCommand()));

        /// <summary>
        ///     Dependency property containing the command that allows to execute selected planned items in instance of
        ///     <see cref="UserRolesDataGrid" />.
        /// </summary>
        public static readonly DependencyProperty AddPlannedItemsCommandProperty = DependencyProperty.Register("AddPlannedItemsCommand", typeof(ICommand), typeof(UserRolesDataGrid), new PropertyMetadata(new DisabledCommand()));


        private DataGrid _dragSource;


        /// <summary>
        ///     Initializes a new instance of <see cref="UserRolesDataGrid" />.
        /// </summary>
        public UserRolesDataGrid()
        {
            DataContextChanged += OnDataContextChanged;
            InitializeComponent();
            MouseUp+=OnMouseUp;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var shit = Total;
            OnPropertyChanged("Total");
        }

        /// <summary>
        ///     Gets or sets the command used to execute selected planned items.
        /// </summary>
        public ICommand AddPlannedItemsCommand
        {
            get { return (ICommand)GetValue(AddPlannedItemsCommandProperty); }
            set { SetValue(AddPlannedItemsCommandProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the command used to execute selected planned items.
        /// </summary>
        public ICommand ExecutePlannedItemsCommand
        {
            get { return (ICommand)GetValue(ExecutePlannedItemsCommandProperty); }
            set { SetValue(ExecutePlannedItemsCommandProperty, value); }
        }

        /// <summary>
        ///     Gets or sets a value saying whether the drag drop is allows into and out from the current
        ///     <see cref="UserRolesDataGrid" />.
        /// </summary>
        public bool AllowDragDrop
        {
            get { return (bool)GetValue(AllowDragDropProperty); }
            set { SetValue(AllowDragDropProperty, value); }
        }


        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //this.SetupInteractionWithDataContext(e, OnInteractionRequested);
            //if (DataContext != null)
            //{
            //    //measurementUnitColumn.ItemsSource =
            //    //    (DataContext as IInvestmentElementPresenter).MeasurementUnits;
            //    //currencyColumn.ItemsSource =
            //    //   (DataContext as IInvestmentElementPresenter).Currencies;
               
              
            //}
        }

        private void OnInteractionRequested(object sender, InteractionRequestedEventArgs e)
        {
            this.Execute(e);
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!AllowDragDrop)
                return;


            try
            {
                _dragSource = (DataGrid)sender;
                IList plannedItems = _dragSource.SelectedItems;

                DragDrop.DoDragDrop((DependencyObject)sender, plannedItems, DragDropEffects.Link);
            }
            finally
            {
                _dragSource = null;
            }
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            var dataGrid = (DataGrid)sender;

            // Do not allow drag drop from the same data grid
            if (ReferenceEquals(dataGrid, _dragSource))
                return;
      
            // Grab the planned items and command to execute them.
            var plannedItems = (IList)e.Data.GetData("System.Windows.Controls.SelectedItemCollection");
            if (ExecutePlannedItemsCommand.CanExecute(plannedItems))
                ExecutePlannedItemsCommand.Execute(plannedItems);
            
            // Grab the planned items and command to add them.
            if (AddPlannedItemsCommand.CanExecute(plannedItems))
                AddPlannedItemsCommand.Execute(plannedItems);
        }

     

            //foreach (PropertyInfo prop in sample.GetType().GetProperties() )
            //{
            //    if (prop.Name != "Id")
            //    {
            //        foreach (PropertyInfo prop2 in current.GetType().GetProperties())
            //        {
            //            if (prop.Name == prop2.Name&&Evaluate(prop))
            //            {
            //                current.GetType().GetProperty(prop.Name).SetValue(current,prop2.GetValue(sample));
            //            }
            //        }
      


        private void UserRolesDataGrid_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = DataContext as ICrudViewModel;
            if (viewModel != null)
            {
                AddButtonCommand = viewModel.AddCommand;
                DeleteButtonCommand = viewModel.DeleteCommand;
               // PageCommand = (viewModel as IBudgetComponentItemViewModel).FilterCommand;
            }
        }
    }
}