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
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using CompanyName.Atlas.UIControls;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for WorkCapitalCashFlow.xaml
    /// </summary>
    public partial class WorkCapitalCashFlowView 
    {
        public WorkCapitalCashFlowView()
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

        /// <summary>
        /// Invoked when the datacontext for the current view has been changed. Makes sure that the interactions channels
        /// between this view and the new investment element view model are wired up.
        /// </summary>
        /// <param name="sender">The object sending the event invoking this method.</param>
        /// <param name="e">The event arguments containing the details about the datacontext change.</param>
        protected override void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            base.OnDataContextChanged(sender, e);
            if (e.NewValue != null)
            {
                var presenter = (e.NewValue as IWorkCapitalCashFlowPresenter);
                if (presenter == null)
                    return;
              presenter.CellStyle =  this.FindResource(typeof(DataGridCell)) as Style;
            }
            //    var viewModel2 = (e.NewValue as IWorkCapitalCashFlowPresenter).CashOutgoings;

                //    if (viewModel2 == null)
                //        return;

                //    RecursivelySetupInteractions(viewModel2);
                //}


                //if (e.NewValue != null)
                //{
                //    var presenter = e.NewValue as IWorkCapitalCashFlowPresenter;
                //    if (presenter != null)
                //    {
                //       // AtlasCashFlowDataGridEntries.DataGridColumns = presenter.DataGridColumns;
                //        AtlasCashFlowDataGridOutGoings.DataGridColumns = presenter.DataGridColumns;
                //    }



                //}
            }

        private void RecursivelySetupInteractions<TCategory>(
            IWorkCapitalCashFlowCashMovementCategoryViewModel<TCategory> cashEntryviewModel)
            where TCategory : class,ICashMovementCategory
        {
            cashEntryviewModel.AddedItem += SetupInteraction;
            cashEntryviewModel.DeletedItem += BreakInteraction;
          

            foreach (var invElemPresenter in cashEntryviewModel.Items)
            {
             //   invElemPresenter.SubCategories.Raised += OnInteractionRequested;
                RecursivelySetupInteractions(invElemPresenter.SubCategories);
            }

            
        }

        private void RecursivelySetupInteractions<TCategory>(ICashMovementCategoryViewModel<TCategory> cashOutgoingviewModel)
            where TCategory : class,ICashMovementCategory
        {

            cashOutgoingviewModel.AddedItem += SetupInteraction;
            cashOutgoingviewModel.DeletedItem += BreakInteraction;


            foreach (var invElemPresenter in cashOutgoingviewModel.Items)
            {
                // invElemPresenter.SubCategories.Raised += OnInteractionRequested;
                RecursivelySetupInteractions(invElemPresenter.SubCategories);
            }
        }
        private void RecursivelyBreakInteractions(ICashMovementCategoryViewModel<ICashOutgoing> viewModel)
        {
            viewModel.AddedItem -= SetupInteraction;
            viewModel.DeletedItem -= BreakInteraction;
            viewModel.Raised -= OnInteractionRequested;

           
            foreach (ICashMovementCategoryPresenter<ICashOutgoing> invElemPresenter in viewModel.Items)
                RecursivelyBreakInteractions(invElemPresenter.SubCategories);
        }

        private void RecursivelyBreakInteractions(ICashMovementCategoryViewModel<ICashEntry> viewModel)
        {
            viewModel.AddedItem -= SetupInteraction;
            viewModel.DeletedItem -= BreakInteraction;
            viewModel.Raised -= OnInteractionRequested;

            foreach (ICashMovementCategoryPresenter<ICashEntry> invElemPresenter in viewModel.Items)
                RecursivelyBreakInteractions(invElemPresenter.SubCategories);
           
        }

        private void SetupInteraction(object sender, ItemEventArgs e)
        {
            var invElemPresenter = (e.Item as ICashMovementCategoryPresenter<ICashEntry>);
            var invElemPresenter2 = (e.Item as ICashMovementCategoryPresenter<ICashOutgoing>);

            if (invElemPresenter != null)
            {
             //   invElemPresenter.SubCategories.Raised += OnInteractionRequested;
                RecursivelySetupInteractions(invElemPresenter.SubCategories);
            }

            if (invElemPresenter2 != null)
            {
             //   invElemPresenter2.SubCategories.Raised += OnInteractionRequested;
                RecursivelySetupInteractions(invElemPresenter2.SubCategories);
            }
           
        }

        private void BreakInteraction(object sender, ItemEventArgs e)
        {
         
            var invElemPresenter = (e.Item as ICashMovementCategoryPresenter<ICashEntry>);
            var invElemPresenter2 = (e.Item as ICashMovementCategoryPresenter<ICashOutgoing>);

            if (invElemPresenter != null) 
            {
                RecursivelyBreakInteractions(invElemPresenter.SubCategories);
            }

            if (invElemPresenter2 != null)
            {
                RecursivelyBreakInteractions(invElemPresenter2.SubCategories);
            }
           
        }
        #region Command event handlers

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var invElemPresenter = e.Parameter as ICashMovementCategoryPresenter<ICashEntry>;

            e.CanExecute = invElemPresenter != null && invElemPresenter.SubCategories.AddCommand.CanExecute(null);

            if (invElemPresenter == null)
            {
                var invElemPresenter2 = e.Parameter as ICashMovementCategoryPresenter<ICashOutgoing>;

                e.CanExecute = invElemPresenter2 != null && invElemPresenter2.SubCategories.AddCommand.CanExecute(null);

            }
        
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var invElemPresenter = e.Parameter as ICashMovementCategoryPresenter<ICashEntry>;

            if (invElemPresenter == null)
            {
                if (ViewModel != null)
                    ViewModel.AddCommand.Execute(e.Parameter);
            }
            else
            {
                invElemPresenter.SubCategories.AddCommand.Execute(null);
                invElemPresenter.IsExpanded = true;
            }

            if (invElemPresenter == null)
            {
                var invElemPresenter2 = e.Parameter as ICashMovementCategoryPresenter<ICashOutgoing>;

                if (invElemPresenter2 == null)
                {
                    if (ViewModel != null)
                        ViewModel.AddCommand.Execute(e.Parameter);
                }
                else
                {
                    invElemPresenter2.SubCategories.AddCommand.Execute(null);
                    invElemPresenter2.IsExpanded = true;
                }
            }
        }

        private void DeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            //var invElemPresenter = e.Parameter as ICashMovementCategoryPresenter<ICashEntry>;

            //if (invElemPresenter == null || invElemPresenter.SuperiorCategory == null)
            //{
            //    var type = e.Parameter.GetType();
            //    e.CanExecute = ViewModel != null && ViewModel.DeleteCommand.CanExecute(e.Parameter);
            //}
            //else
            //{
            //    var parent = invElemPresenter.SuperiorCategory;
            //    if(parent.GetType().Implements<IWorkCapitalCashFlowPresenter>())
            //    e.CanExecute = parent != null && (parent as IWorkCapitalCashFlowPresenter).CashEntries.DeleteCommand.CanExecute(e.Parameter);
            //    else
            //        e.CanExecute = parent != null && parent.SubCategories.DeleteCommand.CanExecute(e.Parameter);
            //}

            //if (invElemPresenter == null)
            //{
            //    var invElemPresenter2 = e.Parameter as ICashMovementCategoryPresenter<ICashOutgoing>;

            //    if (invElemPresenter2 == null || invElemPresenter2.SuperiorCategory == null)
            //    {
            //        var type = e.Parameter.GetType();
            //        e.CanExecute = ViewModel != null && ViewModel.DeleteCommand.CanExecute(e.Parameter);
            //    }
            //    else
            //    {
            //        var parent = invElemPresenter2.SuperiorCategory;
            //        if (parent.GetType().Implements<IWorkCapitalCashFlowPresenter>())
            //            e.CanExecute = parent != null && (parent as IWorkCapitalCashFlowPresenter).CashOutgoings.DeleteCommand.CanExecute(e.Parameter);
            //        else
            //            e.CanExecute = parent != null && parent.SubCategories.DeleteCommand.CanExecute(e.Parameter);

            //    }
            //}
        }

        private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var invElemPresenter = e.Parameter as ICashMovementCategoryPresenter<ICashEntry>;

            if (invElemPresenter == null || invElemPresenter.SuperiorCategory == null)
            {
                if (ViewModel != null)
                    ViewModel.DeleteCommand.Execute(e.Parameter);
            }
            else
            {



                var parent = invElemPresenter.SuperiorCategory;
                if (parent != null)
                {
                    //if (parent.GetType().Implements<IWorkCapitalCashFlowPresenter>())
                    //    (parent as IWorkCapitalCashFlowPresenter).CashEntries.DeleteCommand.Execute(e.Parameter);
                    //else
                        parent.SubCategories.DeleteCommand.Execute(e.Parameter);

                }

            }

            if (invElemPresenter == null)
            {
                var invElemPresenter2 = e.Parameter as ICashMovementCategoryPresenter<ICashOutgoing>;

                if (invElemPresenter2 == null || invElemPresenter2.SuperiorCategory == null)
                {
                    if (ViewModel != null)
                        ViewModel.DeleteCommand.Execute(e.Parameter);
                }
                else
                {


                    var parent = invElemPresenter2.SuperiorCategory;
                    if (parent != null)
                    {
                        //if (parent.GetType().Implements<ICashMovementCategoryPresenter>())
                        //    (parent as ICashMovementCategoryPresenter).CashOutgoings.DeleteCommand.Execute(e.Parameter);
                        //else
                            parent.SubCategories.DeleteCommand.Execute(e.Parameter);

                    }
                }
            }
        }

        #endregion

      
        private void DateTimePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (DataContext != null)
            //{
            //    var presenter = DataContext as IWorkCapitalCashFlowPresenter;
            //    if (presenter != null)
            //    {
            //     //   AtlasCashFlowDataGridEntries.DataGridColumns = presenter.DataGridColumns;
            //     //   AtlasCashFlowDataGridOutGoings.DataGridColumns = presenter.DataGridColumns;
            //    }
               


            //}
           
        }

        private bool stop;
        private void AtlasCashFlowDataGridAllEntries_OnCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //if (e.EditAction == DataGridEditAction.Commit)
            //{
            //   if (!stop)
            //    {
            //        var value = Convert.ToDecimal((e.EditingElement as TextBox).Text);

            //        var presenter = e.Row.Item as ICashMovementCategoryPresenter;
            //        var period = ((e.Column as DataGridTextColumn).Binding as Binding).ConverterParameter as IPeriod;
            //        presenter.SetCashMovement(value, period);
                   
                    
            //        stop = true;
            //       //(sender as AtlasCashFlowDataGrid).CommitEdit();
            //       //(sender as AtlasCashFlowDataGrid).CancelEdit();
            //      // presenter.TellYourFather();
                    
            //    }

               
            //    stop = false;



            //}
          
        }


        private void CashEntriesTreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
           var path = CashEntriesTreeView.SelectedValuePath;
           var paht =  AtlasCashFlowDataGridEntries.SelectedValuePath;

            AtlasCashFlowDataGridEntries.SelectedItem = CashEntriesTreeView.SelectedItem;

        }

        private void AtlasCashFlowDataGridEntries_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var path = CashEntriesTreeView.SelectedValuePath;
            var paht = AtlasCashFlowDataGridEntries.SelectedValuePath;

             

        }

        //private void AtlasCashFlowDataGridEntries_OnCurrentCellChanged(object sender, EventArgs e)
        //{
        //    ((DataGrid) sender).CommitEdit(DataGridEditingUnit.Cell, false);
        //}
        private void AtlasCashFlowDataGridEntries_OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Tab)
                ((DataGrid)sender).CommitEdit(DataGridEditingUnit.Cell, false);
        }

        private void CashEntriesTreeView_OnExpanded(object sender, RoutedEventArgs e)
        {
            //CashEntriesTreeView
            //    CashEntriesTreeView
            Int32Animation angleAnimation = new Int32Animation(90, -90, new Duration(new TimeSpan(0, 0, 0, 0,200)));

            CashEntriesTreeView.BeginAnimation(CashMovementTreeView.ButtonAngleProperty, angleAnimation);


            DoubleAnimation heightAnimation = new DoubleAnimation(EntriesGrid.ActualHeight, (MainDockPanel.ActualHeight - 70) / 2, new Duration(new TimeSpan(0, 0, 0, 0, 200)));
            EntriesGrid.BeginAnimation(Grid.HeightProperty, heightAnimation);
            DoubleAnimation heightAnimation2 = new DoubleAnimation(OutingsGrid.ActualHeight, (MainDockPanel.ActualHeight - 70) / 2, new Duration(new TimeSpan(0, 0, 0, 0, 200)));
            OutingsGrid.BeginAnimation(Grid.HeightProperty, heightAnimation2);
            //   OutGoingGrid.BeginAnimation(Grid.HeightProperty, heightAnimation);
            // CashOutGoingsTreeView.IsCollapsed = true;



        }

        private void CashOutGoingsTreeView_OnExpanded(object sender, RoutedEventArgs e)
        {
            Int32Animation angleAnimation = new Int32Animation(-90, 90, new Duration(new TimeSpan(0, 0, 0, 0,200)));

            CashOutGoingsTreeView.BeginAnimation(CashMovementTreeView.ButtonAngleProperty, angleAnimation);


            DoubleAnimation heightAnimation = new DoubleAnimation(EntriesGrid.ActualHeight, (MainDockPanel.ActualHeight - 70) / 2, new Duration(new TimeSpan(0, 0, 0, 0,200)));
            EntriesGrid.BeginAnimation(Grid.HeightProperty, heightAnimation);
            DoubleAnimation heightAnimation2 = new DoubleAnimation(OutingsGrid.ActualHeight, (MainDockPanel.ActualHeight - 70) / 2, new Duration(new TimeSpan(0, 0, 0, 0, 200)));
            OutingsGrid.BeginAnimation(Grid.HeightProperty, heightAnimation2);
            //  AtlasCashFlowDataGridEntries.BeginAnimation(Grid.HeightProperty, heightAnimation);
            //  AtlasCashFlowDataGridEntries.HeadersVisibility = DataGridHeadersVisibility.Column;
            //   CashEntriesTreeView.IsCollapsed = true;
        }

        private void CashEntriesTreeView_OnCollapsed(object sender, RoutedEventArgs e)
        {
            Int32Animation angleAnimation = new Int32Animation(-90, 90, new Duration(new TimeSpan(0, 0, 0, 0,200)));

            CashEntriesTreeView.BeginAnimation(CashMovementTreeView.ButtonAngleProperty, angleAnimation);


            DoubleAnimation heightAnimation = new DoubleAnimation(OutingsGrid.ActualHeight, 0, new Duration(new TimeSpan(0, 0, 0, 0,200)));
            OutingsGrid.BeginAnimation(Grid.HeightProperty, heightAnimation);

            DoubleAnimation heightAnimation2 = new DoubleAnimation(EntriesGrid.ActualHeight, (MainDockPanel.ActualHeight - 70), new Duration(new TimeSpan(0, 0, 0, 0, 200)));
            EntriesGrid.BeginAnimation(Grid.HeightProperty, heightAnimation2);
            //OutGoingGrid.BeginAnimation(Grid.HeightProperty, heightAnimation);
            //CashOutGoingsTreeView.IsCollapsed = false;
        }

        private void CashOutGoingsTreeView_OnCollapsed(object sender, RoutedEventArgs e)
        {

            Int32Animation angleAnimation = new Int32Animation(90, -90, new Duration(new TimeSpan(0, 0, 0, 0,200)));

            CashOutGoingsTreeView.BeginAnimation(CashMovementTreeView.ButtonAngleProperty, angleAnimation);


            DoubleAnimation heightAnimation = new DoubleAnimation(EntriesGrid.ActualHeight, 0, new Duration(new TimeSpan(0, 0, 0, 0,200)));
            EntriesGrid.BeginAnimation(Grid.HeightProperty, heightAnimation);
            DoubleAnimation heightAnimation2 = new DoubleAnimation(OutingsGrid.ActualHeight, (MainDockPanel.ActualHeight - 70), new Duration(new TimeSpan(0, 0, 0, 0, 200)));
            OutingsGrid.BeginAnimation(Grid.HeightProperty, heightAnimation2);
            //AtlasCashFlowDataGridEntries.BeginAnimation(Grid.HeightProperty, heightAnimation);
            //AtlasCashFlowDataGridEntries.HeadersVisibility = DataGridHeadersVisibility.None;
            //CashEntriesTreeView.IsCollapsed = false;
        }

        

        private void MainDockPanel_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (MainDockPanel.ActualHeight > 0)
            {
                var heigth = (MainDockPanel.ActualHeight - 70) / 2;
                EntriesGrid.Height = heigth;
                OutingsGrid.Height = heigth;
            }
            
        }

        private void MainDockPanel_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (((FrameworkElement) sender).IsVisible && MainDockPanel.ActualHeight > 0 &&
                Math.Abs(EntriesGrid.ActualHeight - OutingsGrid.ActualHeight) < 0.5)
            {
                var heigth = (MainDockPanel.ActualHeight - 70) / 2;
                EntriesGrid.Height = heigth;
                OutingsGrid.Height = heigth;
            }
        }

        private void MainDockPanel_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (MainDockPanel.ActualHeight > 0)
            {
                var heigth = (MainDockPanel.ActualHeight - 70) / 2;
                EntriesGrid.Height = heigth;
                OutingsGrid.Height = heigth;
                //TopScrollViewer.Height = heigth;
                //BottomScrollViewer.Height = heigth;
            }
        }

        private void TopScrollViewer_OnLoaded(object sender, RoutedEventArgs e)
        {
           /// ((FrameworkElement) sender).Height = (MainDockPanel.ActualHeight - 70) / 2;
        }

        private void AtlasCashFlowDataGridEntries_OnCurrentCellChanged(object sender, EventArgs e)
        {
            
            (sender as AtlasCashFlowDataGrid).CommitEdit();

            if (DataContext != null && DataContext as IWorkCapitalCashFlowPresenter != null)
            {
                var presenter = (DataContext as IWorkCapitalCashFlowPresenter);
                presenter.isWorkCapitalCalculated = false;
            }
            
        }
    }
}
