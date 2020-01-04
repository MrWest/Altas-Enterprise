using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    /// Represents the customized data grid for the Atlas suite.
    /// </summary>
    public class AtlasDataGrid : DataGrid, INotifyPropertyChanged
    {
        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty AddButtonTextProperty = DependencyProperty.Register("AddButtonText", typeof(string), typeof(AtlasDataGrid), new PropertyMetadata(Properties.Resources.Add));
        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the total summary.
        /// </summary>
        public static readonly DependencyProperty TotalTextProperty = DependencyProperty.Register("TotalText", typeof(string), typeof(AtlasDataGrid), new PropertyMetadata(Properties.Resources.Total));

        /// <summary>
        /// Dependency property used to contain the width of the Commands Column for the <see cref="AtlasDataGrid"/>
        /// </summary>
        public static readonly DependencyProperty CommandsColumnWidthProperty = DependencyProperty.Register("CommandsColumnWidth", typeof(DataGridLength), typeof(AtlasDataGrid), new PropertyMetadata(new DataGridLength(40)));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty AddButtonCommandProperty = DependencyProperty.Register("AddButtonCommand", typeof(ICommand), typeof(AtlasDataGrid), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Delete Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty DeleteButtonCommandProperty = DependencyProperty.Register("DeleteButtonCommand", typeof(ICommand), typeof(AtlasDataGrid), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property to contain the total of som datagrig data to sumarize <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty TotalDependencyProperty = DependencyProperty.Register("Total", typeof(decimal), typeof(AtlasDataGrid), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the Filter command for instances of <see cref="FilterBox"/>.
        /// </summary>
        public static readonly DependencyProperty PageCommandProperty = DependencyProperty.Register("PageCommand", typeof(ICommand), typeof(AtlasDataGrid));

        /// <summary>
        /// Dependency property containing the Filter command for instances of <see cref="FilterBox"/>.
        /// </summary>
        public static readonly DependencyProperty ShowTotalProperty = DependencyProperty.Register("ShowTotal", typeof(Visibility), typeof(AtlasDataGrid));


         /// <summary>
        /// Dependency property containing the Filter command for instances of <see cref="FilterBox"/>.
        /// </summary>
        public static readonly DependencyProperty ShowBottomProperty = DependencyProperty.Register("ShowBottom", typeof(Visibility), typeof(AtlasDataGrid));

        /// <summary>
        /// Dependency property containing the Filter command for instances of <see cref="FilterBox"/>.
        /// </summary>
        public static readonly DependencyProperty CanAddProperty = DependencyProperty.Register("CanAdd", typeof(bool), typeof(AtlasDataGrid), new PropertyMetadata(true));



        /// <summary>
        /// Initializes a new instance of <see cref="AtlasDataGrid"/>
        /// </summary>
        public AtlasDataGrid()
        {
            DefaultStyleKey = typeof(DataGrid);
            DataContextChanged+=OnDataContextChanged;
            CurrentCellChanged+=OnCurrentCellChanged;
            PropertyChanged = OnPropertyHasChanged;
            BeginningEdit+=OnBeginningEdit;
            RowEditEnding+=OnRowEditEnding;

            Loaded+=OnLoaded;
           MouseDoubleClick +=OnMouseDoubleClick;
            KeyDown += OnKeyDown;
            
        }

        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if(keyEventArgs.KeyboardDevice.Modifiers == ModifierKeys.Control)
                if(keyEventArgs.Key == Key.N)
                    if(AddButtonCommand != null && AddButtonCommand.CanExecute(null))
                        AddButtonCommand.Execute(null);
        }

        private void OnRowEditEnding(object sender, DataGridRowEditEndingEventArgs dataGridRowEditEndingEventArgs)
        {
            CanAdd = true;
            OnPropertyChanged("CanAdd");
        }

        private void OnBeginningEdit(object sender, DataGridBeginningEditEventArgs dataGridBeginningEditEventArgs)
        {
            CanAdd = false;
            OnPropertyChanged("CanAdd");
        }

        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (RowDetailsVisibilityMode != DataGridRowDetailsVisibilityMode.Collapsed)
                RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.Collapsed;
            else
            {
               RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
            }

            if (ShowTotal == Visibility.Collapsed&& RowDetailsTemplate == null)
                SelectedItem = null;

        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var crap = Total;
            OnPropertyChanged("Total");
        }

        private void OnCurrentCellChanged(object sender, EventArgs eventArgs)
        {
            CommitEdit();
            //OnPropertyChanged("Tag");

            var crap = Total;
            OnPropertyChanged("Total");
          
          
        }

        /// <summary>
        /// Invoked when a property in the current entity presenter view model has been notified as changed.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="e">Arguments containing the details of the event.</param>
        protected virtual void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            var crap = Total;
            OnPropertyChanged("Total");

        }

        /// <summary>
        /// Invoked when a property in the current entity presenter view model has been notified as changed.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="e">Arguments containing the details of the event.</param>
        protected virtual void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged( oldValue,  newValue);
            var crap = Total;
            OnPropertyChanged("Total");

        }
        /// <summary>
        /// Invoked when a property in the current entity presenter view model has been notified as changed.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="e">Arguments containing the details of the event.</param>
        protected virtual void OnPropertyHasChanged(object sender, PropertyChangedEventArgs e)
        {
            
          
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {

            var crap = Total;
            OnPropertyChanged("Total");
          
        }

        /// <summary>
        /// Gets or sets wheher the total summary is shown.
        /// </summary>
        public bool CanAdd
        {
            get
            {
                return (bool)GetValue(CanAddProperty);
            }
            set { SetValue(CanAddProperty, value); }
        }

        /// <summary>
        /// Gets or sets wheher the total summary is shown.
        /// </summary>
        public Visibility ShowTotal
        {
            get { return (Visibility)GetValue(ShowTotalProperty); }
            set { SetValue(ShowTotalProperty, value); }
        }
        /// <summary>
        /// Gets or sets wheher the total summary is shown.
        /// </summary>
        public Visibility ShowBottom
        {
            get { return (Visibility)GetValue(ShowBottomProperty); }
            set { SetValue(ShowBottomProperty, value); }
        }
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public ICommand PageCommand
        {
            get { return (ICommand)GetValue(PageCommandProperty); }
            set { SetValue(PageCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        public  decimal Total
        {
            get
            {
               // Total = Items.OfType<ICosttable>().Sum(x => x.Cost);
                Total = ItemsSource!=null?ItemsSource.OfType<ICosttable>().Sum(x => x.Cost):0;
                return (decimal)GetValue(TotalDependencyProperty) ;
            }
            set
            {
                SetValue(TotalDependencyProperty,value);
            }
          
        }

        /// <summary>
        /// Gets or sets the text to be displayed by the Add button.
        /// </summary>
        public string AddButtonText
        {
            get { return (string)GetValue(AddButtonTextProperty); }
            set { SetValue(AddButtonTextProperty, value); }
        }
        
            
        /// <summary>
        /// Gets or sets the text to be displayed by the Add button.
        /// </summary>
        public string TotalText
        {
            get { return (string)GetValue(TotalTextProperty); }
            set { SetValue(TotalTextProperty, value); }
        }
        /// <summary>
        /// Gets or sets the width of the Commands Column of the <see cref="AtlasDataGrid"/>.
        /// </summary>
        public DataGridLength CommandsColumnWidth
        {
            get { return (DataGridLength)GetValue(CommandsColumnWidthProperty); }
            set { SetValue(CommandsColumnWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public ICommand AddButtonCommand
        {
            get { return (ICommand)GetValue(AddButtonCommandProperty); }
            set { SetValue(AddButtonCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command that is executed by the Delete Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public ICommand DeleteButtonCommand
        {
            get { return (ICommand)GetValue(DeleteButtonCommandProperty); }
            set { SetValue(DeleteButtonCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the item that is currently hovered by the mouse.
        /// </summary>
        private object HoveredItem { get; set; }


        /// <summary>
        /// Executed when the template was applied to the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            AddButtonCommand = AddButtonCommand ?? GetDefaultCommand("Add command not specified");
            DeleteButtonCommand = DeleteButtonCommand ?? GetDefaultCommand("Delete command not specified");
            var commandBinding = new CommandBinding(DeleteCommand, DeleteButtonCommand_Executed, DeleteButtonCommand_CanExecute);
            CommandBindings.Add(commandBinding);
           
        }

        /// <summary>
        /// Invoked when the current grid has been initialized.
        /// </summary>
        /// <param name="e">Not used.</param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            IncludeCommandsColumn();
         
        }

        protected override void OnLoadingRow(DataGridRowEventArgs e)
        {
            base.OnLoadingRow(e);

            e.Row.MouseEnter += OnMouseOverRow;
        }

        protected override void OnUnloadingRow(DataGridRowEventArgs e)
        {
            base.OnUnloadingRow(e);

            e.Row.MouseEnter -= OnMouseOverRow;
           
        }

        private void OnMouseOverRow(object sender, MouseEventArgs e)
        {
            var row = sender as DataGridRow;
            if (row == null)
                return;

            HoveredItem = row.DataContext;
            CommandManager.InvalidateRequerySuggested();
        }

        private void IncludeCommandsColumn()
        {
            var commandsColumnTemplate = FindResource("DataGridCommandsColumnTemplate") as DataTemplate;
            if (commandsColumnTemplate == null)
                return;

            var column = new DataGridTemplateColumn
            {
                CellTemplate = commandsColumnTemplate,
                Width = CommandsColumnWidth.Value, 
                MinWidth = CommandsColumnWidth.Value-10
               

            };

            Columns.Add(column);
        }

        private ICommand GetDefaultCommand(string traceClue)
        {
            // Apply the command to the Add Button if missing
            ICommand command = new RoutedCommand();
            ExecutedRoutedEventHandler executedHandler = (sender, e) => Trace.WriteLine(traceClue);
            CanExecuteRoutedEventHandler canExecuteHandler = (sender, e) => e.CanExecute = true;
            CommandBindings.Add(new CommandBinding(command, executedHandler, canExecuteHandler));

            return command;
        }


        private void DeleteButtonCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true; // HoveredItem != null && DeleteButtonCommand.CanExecute(e.Parameter ?? HoveredItem);
        }

        private void DeleteButtonCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (DeleteButtonCommand != null)
                DeleteButtonCommand.Execute(e.Parameter ?? HoveredItem);

            OnPropertyChanged("Total");
        }
        private void AddButtonCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           
            

        }
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
