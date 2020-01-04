using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    ///     Interaction logic for BudgetComponentItemTabControl.xaml.
    /// </summary>
    public partial class BudgetComponentResourceDataGrid : INotifyPropertyChanged
    {
        /// <summary>
        ///     Dependency property used to contain the flag that allows drag and drop operations in the instances of
        ///     <see cref="BudgetComponentItemDataGrid" />.
        /// </summary>
        public static readonly DependencyProperty AllowDragDropProperty = DependencyProperty.Register("AllowDragDrop", typeof(bool), typeof(BudgetComponentResourceDataGrid), new PropertyMetadata(false));

        /// <summary>
        ///     Dependency property containing the command that allows to execute selected planned items in instance of
        ///     <see cref="BudgetComponentItemDataGrid" />.
        /// </summary>
        public static readonly DependencyProperty ExecutePlannedItemsCommandProperty = DependencyProperty.Register("ExecutePlannedItemsCommand", typeof(ICommand), typeof(BudgetComponentResourceDataGrid), new PropertyMetadata(new DisabledCommand()));

        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the total summary.
        /// </summary>
        public static readonly DependencyProperty TotalTextProperty = DependencyProperty.Register("TotalText", typeof(string), typeof(BudgetComponentResourceDataGrid), new PropertyMetadata(Properties.Resources.Total));

        /// <summary>
        /// Dependency property to contain the total of som datagrig data to sumarize <see cref="BudgetComponentResourceDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty TotaldDependencyProperty = DependencyProperty.Register("Total", typeof(decimal), typeof(BudgetComponentResourceDataGrid), new PropertyMetadata(null));

        private DataGrid _dragSource;


        public static readonly DependencyProperty AddResourceCommandProperty = DependencyProperty.Register("AddResourceCommand", typeof(ICommand), typeof(BudgetComponentResourceDataGrid), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the command that allows to delete a child investment element to the one currently focused 
        /// in an instance of <see cref="InvestmentElementTreeView"/>.
        /// </summary>
        public static readonly DependencyProperty DeleteResourceCommandProperty = DependencyProperty.Register("DeleteResourceCommand", typeof(ICommand), typeof(BudgetComponentResourceDataGrid), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty AddButtonCommandProperty = DependencyProperty.Register("AddButtonCommand", typeof(ICommand), typeof(BudgetComponentResourceDataGrid), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty AddButtonTextProperty = DependencyProperty.Register("AddButtonText", typeof(string), typeof(BudgetComponentResourceDataGrid), new PropertyMetadata(Properties.Resources.Add));
        
        /// <summary>
        ///     Dependency property containing the command that allows to execute selected planned items in instance of
        ///     <see cref="BudgetComponentItemDataGrid" />.
        /// </summary>
        public static readonly DependencyProperty AddPlannedItemsCommandProperty = DependencyProperty.Register("AddPlannedItemsCommand", typeof(ICommand), typeof(BudgetComponentResourceDataGrid), new PropertyMetadata(new DisabledCommand()));


        /// <summary>
        ///     Initializes a new instance of <see cref="BudgetComponentItemDataGrid" />.
        /// </summary>
        public BudgetComponentResourceDataGrid ()
        {
            DataContextChanged += OnDataContextChanged;
            InitializeComponent();
            Loaded+=OnLoaded;
            MouseUp += OnMouseUp;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var shit = Total;
            OnPropertyChanged("Total");
        }
        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
             var crap = Total;
        }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        public decimal Total
        {
            get
            {
                Total = Items.OfType<ICosttable>().Sum(x => x.Cost);
                return (decimal)GetValue(TotaldDependencyProperty);
            }
            set
            {
                SetValue(TotaldDependencyProperty, value);
            }

        }
        /// <summary>
        /// Gets or sets the command that is executed by the Add Button of the current <see cref="BudgetComponentItemDataGrid"/>.
        /// </summary>
        public ICommand AddButtonCommand
        {
            get { return (ICommand)GetValue(AddButtonCommandProperty); }
            set { SetValue(AddButtonCommandProperty, value); }
        }


        /// <summary>
        /// Gets or sets the command that allows to add a child investment element to the one currently focused.
        /// </summary>
        public ICommand AddResourceCommand
        {
            get { return (ICommand)GetValue(AddResourceCommandProperty); }
            set { SetValue(AddResourceCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command that allows to delete a child investment element to the one currently focused.
        /// </summary>
        public ICommand DeleteResourceCommand
        {
            get { return (ICommand)GetValue(DeleteResourceCommandProperty); }
            set { SetValue(DeleteResourceCommandProperty, value); }
        }

        ///     Gets or sets the command used to execute selected planned items.
        /// </summary>
        public ICommand ExecutePlannedItemsCommand
        {
            get { return (ICommand)GetValue(ExecutePlannedItemsCommandProperty); }
            set { SetValue(ExecutePlannedItemsCommandProperty, value); }
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
        ///     Gets or sets a value saying whether the drag drop is allows into and out from the current
        ///     <see cref="BudgetComponentItemDataGrid" />.
        /// </summary>
        public bool AllowDragDrop
        {
            get { return (bool)GetValue(AllowDragDropProperty); }
            set { SetValue(AllowDragDropProperty, value); }
        }


        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            OnPropertyChanged("Total");
            var crap = Total;
            //this.SetupInteractionWithDataContext(e, OnInteractionRequested);
        }

        //private void OnInteractionRequested(object sender, InteractionRequestedEventArgs e)
        //{
        //    this.Execute(e);
        //}

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var column = ((DataGridTextColumn)this.Template.FindName("CodeColumn", this));
            if (!Equals(column, null) && column.Width != CodeColumnWidth)
                column.Width = CodeColumnWidth;

            if (!AllowDragDrop)
                return;
            

            try
            {
                if(sender as DataGrid ==null)
                    return;
                _dragSource = (DataGrid)sender;
                IList plannedItems = _dragSource.SelectedItems;

                DragDrop.DoDragDrop((DependencyObject)sender, plannedItems, DragDropEffects.Link);
            }
            finally
            {
                _dragSource = null;
            }

        
        }
        /// <summary>
        /// Gets or sets the command that is executed by the Add Button of the current <see cref="BudgetComponentItemDataGrid"/>.
        /// </summary>
        public double CodeColumnWidth
        {
            get
            {
                CodeColumnWidth = GetCodeColumnWidth();

                return (double)GetValue(CodeColumnWidthProperty);
            }
            set { SetValue(CodeColumnWidthProperty, value); }
        }
        private double GetCodeColumnWidth()
        {
            int deep = 1;
            foreach (INavigable navigable in Items.Cast<INavigable>())
            {
                if (navigable.IsExpanded)
                {
                    int nDeep = navigable.Depth;
                    if (nDeep > deep)
                        deep = nDeep;
                    deep = GetRecursiveDeep(navigable, deep);

                }


            }

            return deep * 16 + 100;
        }
        /// <summary>
        ///     Dependency property containing the command that allows to execute selected planned items in instance of
        ///     <see cref="BudgetComponentItemDataGrid" />.
        /// </summary>
        public static readonly DependencyProperty CodeColumnWidthProperty = DependencyProperty.Register("CodeColumnWidth", typeof(double), typeof(BudgetComponentResourceDataGrid), new PropertyMetadata((double)100));

        private int GetRecursiveDeep(INavigable aboveNavigable, int aboveDeep)
        {
            int deep = aboveDeep;
            foreach (INavigable navigable in aboveNavigable.Items.Cast<INavigable>())
            {
                if (navigable.IsExpanded)
                {
                    int nDeep = navigable.Depth;
                    if (nDeep > deep)
                        deep = nDeep;
                    deep = GetRecursiveDeep(navigable, deep);
                }


            }
            return deep;
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

        private void ComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem != null)
            {
                var sample = ((ComboBox)sender).SelectedItem;

                var current = this.SelectedItem;
                if (current != null)
                {
                    foreach (PropertyInfo prop in current.GetType().GetProperties())
                    {
                        if (sample.GetType().Implements(prop.PropertyType))
                        {
                            var aux = ((IEntity)current.GetType().GetProperty(prop.Name).GetValue(current));
                            if (aux == null)
                                current.GetType().GetProperty(prop.Name).SetValue(current, sample);
                            if (aux != null && ((IEntity)sample).Id != aux.Id)
                                current.GetType().GetProperty(prop.Name).SetValue(current, sample);

                        }

                    }
                }

            }
        }
    }
}