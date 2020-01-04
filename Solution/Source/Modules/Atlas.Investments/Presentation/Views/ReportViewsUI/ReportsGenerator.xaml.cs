using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using CompanyName.Atlas.Contracts.Presentation.Features;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Rerporting;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Reporting;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.UIControls.Properties;
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
namespace CompanyName.Atlas.Investments.Presentation.Views.ReportViewsUI
{
    /// <summary>
    /// Interaction logic for ReportsGenerator.xaml
    /// </summary>
    public partial class ReportsGenerator : UserControl, IPrinttableContainer, IExporttableContainer
    {
        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty CustomReportSettingsProperty =
            DependencyProperty.Register("CustomReportSettings", typeof(CustomReportSettings), typeof(ReportsGenerator),
                new PropertyMetadata(null));

        //private Thread thread;
        //private Thread prinThread;
        public ReportsGenerator()
        {
            InitializeComponent();

            CustomReportSettings = new CustomReportSettings() {PreViewCommand = new DelegateCommand(ExecuteMethod)};
            Loaded+=OnLoaded;

            CustomReportTreeViewContainer.PrintCommand = new DelegateCommand(ExecuteMethod);
            //var viewmodel = ServiceLocator.Current.GetInstance<IInvestmentMainCustomReportSettingsViewModel>();
            //viewmodel.Load();
            //CustomReportTreeView.ItemsSource = viewmodel.Items;
            //CustomReportTreeView.AddInvestmentElementButtonCommand = viewmodel.AddCommand;

            _backgroundWorker = new BackgroundWorker();

            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;

            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
            _backgroundWorker.ProgressChanged += BackgroundWorkerOnProgressChanged;
           _backgroundWorker.RunWorkerCompleted += BackgroundWorkerOnRunWorkerCompleted;

        }

        private void BackgroundWorkerOnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
            InfoTextBlock.Text = UIControls.Properties.Resources.Loading + " " + e.ProgressPercentage + "%";
            StatusBarServices.SignalText2(InfoTextBlock.Text);
        }

        private void BackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            InfoGrid.Visibility = Visibility.Collapsed;
            
            MemoryStream stream = e.Result as MemoryStream;
            //XamlWriter.Save(result, stream);

            stream.Seek(0, SeekOrigin.Begin);
            Table mytable = (Table)XamlReader.Load(stream);
            //InfoTextBlock.Text = UIControls.Properties.Resources.Loading + " 100%";
            FlowDocument.Blocks.Add(mytable);
            
            InfoTextBlock.Text = UIControls.Properties.Resources.Loading + " 0%";
            StatusBarServices.SignalText2(UIControls.Properties.Resources.Ready);
            // table.Dispatcher.InvokeShutdown();
            //  _timer.Start();
            //  StatusBarServices.SignalText2(UIControls.Properties.Resources.Loading + " " + e.a + "%");


        }



        private async void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
           
            
            e.Result = GenerateReport(); 
            //StatusBarServices.ForceSignalLoading();
            //InfoTextBlock.Text = UIControls.Properties.Resources.Loading + " " + e.Argument + "%";

            // (e.Argument as FlowDocument).Blocks.Add(table);
        }

        private void DoPrint()
        {
           

            // Get the dispatcher from the current window, and use it to invoke
            // the update code.
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
            (ThreadStart)delegate () {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    //Notify status bar
                    StatusBarServices.SignalText2(Properties.Resources.PrintingWait);

                    //// Save all the existing settings.
                    //double pageHeight = FlowDocument.PageHeight;
                    //double pageWidth = FlowDocument.PageWidth;
                    //Thickness pagePadding = FlowDocument.PagePadding;
                    //double columnGap = FlowDocument.ColumnGap;
                    //double columnWidth = FlowDocument.ColumnWidth;
                    //// Make the FlowDocument page match the printed page.

                    //FlowDocument.PageHeight = printDialog.PrintableAreaHeight;
                    //FlowDocument.PageWidth = printDialog.PrintableAreaWidth;
                    //FlowDocument.PagePadding = new Thickness(50);
                    ConvinientReportPaginator investmentFullReportPaginator = new ConvinientReportPaginator(FlowDocument.Blocks, new Typeface("Calibri"), 10, 48 * 0.75,
                    new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight), MyDocumentPageViewer.ActualWidth, _customReportSettingsPresenter?.Name);

                    printDialog.PrintDocument(
                    investmentFullReportPaginator,
                    _customReportSettingsPresenter?.Name);

                    StatusBarServices.SignalText2(UIControls.Properties.Resources.Ready);
                    // Reapply the old settings.
                    //FlowDocument.PageHeight = pageHeight;
                    //FlowDocument.PageWidth = pageWidth;
                    //FlowDocument.PagePadding = pagePadding;
                    //FlowDocument.ColumnGap = columnGap;
                    //FlowDocument.ColumnWidth = columnWidth;
                }
            }
            );
        }

        private async void InternalSignalText()
        {


            await AsyncSignalText().ContinueWith(Continues);



        }
        private async void Continues(Task task)
        {
            //GenerateReport();
            //FlowDocument.Blocks.Add(table);
        }
        private async Task AsyncSignalText()
        {
            
             StatusBarServices.ForceSignalLoading();

          
              
        }

        private System.Timers.Timer _timer;// = new System.Timers.Timer(500);
        private async void LoadDocument()
        {
            _customReportSettingsPresenter = CustomReportTreeViewContainer.SelectedSettings;

            var investmentElement = _datacontext;
            if (!_backgroundWorker.IsBusy && !_isLoaded && IsVisible && investmentElement != null && _customReportSettingsPresenter != null)
            {
                //InfoGrid.Visibility = Visibility.Visible;
                //this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                //    (ThreadStart) delegate()
                //    {

                        InfoGrid.Visibility = Visibility.Visible;
                         FlowDocument.Blocks.Clear();

               
                _backgroundWorker.RunWorkerAsync();
                //_flowDocument = FlowDocument;

                StatusBarServices.ForceSignalLoading();




                // FlowDocument.Blocks.Add(table);


                //InfoGrid.Visibility = Visibility.Collapsed;


                _isLoaded = true;
                    //});

            }
            // await AsyncSignalText().ContinueWith(GenerateReport);
            // Get the dispatcher from the current window, and use it to invoke
            // the update code.
           // GenerateReport();
        }

        private IInvestmentElementPresenter _datacontext;
        private IInvestmentCustomReportSettingsPresenter _customReportSettingsPresenter;
        private MemoryStream GenerateReport()
        {

            //this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
            //    (ThreadStart) delegate()
            //    {
            //  ShowInvestmentElement(investmentElement);
                         //_backgroundWorker.RunWorkerAsync();
                        var investmentElement = _datacontext;
                        totalRecords = 1;
                         count = 0;
                        totalRecords = investmentElement.InDeep;
                     return   ShowInvestmentElementTable(investmentElement);
                        
            //  }
            //    }
            //);
        }

        private void GetTotalItems(IInvestmentElementPresenter investmentElement)
        {
            totalRecords++;

        //    GetBudgetRecords(investmentElement.Budget);

            foreach (IInvestmentElementPresenter investmentElementItem in investmentElement.Items.Items)
            {
               GetTotalItems(investmentElementItem);
            }

           
        }

        private void GetBudgetRecords(IBudgetPresenter budget)
        {

            totalRecords++;

            GetBudgetComponentRecords(budget.EquipmentComponent);
            GetBudgetComponentRecords(budget.ConstructionComponent);
            GetBudgetComponentRecords(budget.OtherExpensesComponent);
            GetBudgetComponentRecords(budget.WorkCapitalComponent);


            
        }

        private void GetBudgetComponentRecords<TComponent, TPlannedSubSpecialityHolders, TPlannedSubSpecialityHolderPresenter, TExecutedSubSpecialityHolders, TExecutedSubSpecialityHolderPresenter>(IBudgetComponentPresenter<TComponent, TPlannedSubSpecialityHolders, TPlannedSubSpecialityHolderPresenter, TExecutedSubSpecialityHolders, TExecutedSubSpecialityHolderPresenter> item)
             where TComponent : class, IBudgetComponent
        where TPlannedSubSpecialityHolders : class, IPlannedSubSpecialityHolderViewModel<TComponent, TPlannedSubSpecialityHolderPresenter>
        where TPlannedSubSpecialityHolderPresenter : class, IPlannedSubSpecialityHolderPresenter<TComponent>
        where TExecutedSubSpecialityHolders : class, IExecutedSubSpecialityHolderViewModel<TComponent, TExecutedSubSpecialityHolderPresenter>
        where TExecutedSubSpecialityHolderPresenter : class, IExecutedSubSpecialityHolderPresenter<TComponent>
        {

            totalRecords++;

            foreach (TPlannedSubSpecialityHolderPresenter plannedSubSpecialityHolderPresenter in item.PlannedSubSpecialityHolders)
            {
                GetSubSpecialityRecords<TComponent, TPlannedSubSpecialityHolderPresenter>(plannedSubSpecialityHolderPresenter);
            }

           
            
        }
        private void  GetSubSpecialityRecords<TComponent, TPlannedSubSpecialityHolderPresenter>( TPlannedSubSpecialityHolderPresenter item)
           where TComponent : class, IBudgetComponent
           where TPlannedSubSpecialityHolderPresenter : class, IPlannedSubSpecialityHolderPresenter<TComponent>
         {
            totalRecords++;

            foreach (var plannedActivity in item.PlannedActivities)
            {
                 GetBudgetComponentItemRow(plannedActivity);
            }


           
        }

        private void  GetBudgetComponentItemRow<TItem>(IBudgetComponentItemPresenter<TItem> budgetComponentItemPresenter)
            where TItem : class, IBudgetComponentItem
        {
            totalRecords++;

            foreach (var planneditem in budgetComponentItemPresenter.PlannedResources)
            {
                GetBudgetComponentItemRow(planneditem);
            }


           
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var defaultReport = ServiceLocator.Current.GetInstance<IInvestmentMainCustomReportSettingsPresenter>();
            defaultReport.Object = ServiceLocator.Current.GetInstance<IInvestmentMainCustomReportSettings>();
            CustomReportTreeViewContainer.SelectedSettings = defaultReport;

           //_timer = new System.Timers.Timer(500);
           // _timer.Elapsed +=TimerOnElapsed;
            //  LoadDocument();
            //var investmentElement = DataContext as IInvestmentElementPresenter;
            //if (((FrameworkElement) sender).IsVisible && investmentElement!=null)
            //{
            //    FlowDocument.Blocks.Clear();
            //  //  ShowInvestmentElement(investmentElement);
            //    ShowInvestmentElementTable(investmentElement);
            //}
        }

        //private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        //{
        //    if (!_backgroundWorker.IsBusy && _isLoaded && IsVisible)
        //    {
        //        FlowDocument.Blocks.Add(table);
        //        _timer.Stop();
        //    }
        //}

        private  void ExecuteMethod()
        {

            _isLoaded = false;
            LoadDocument();
            //Thread thread = new Thread(LoadDocument);
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Priority = ThreadPriority.Highest;
            //thread.Start();
           
        }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public CustomReportSettings CustomReportSettings
        {
            get { return (CustomReportSettings)GetValue(CustomReportSettingsProperty); }
            set { SetValue(CustomReportSettingsProperty, value); }
        }

        private void UIElement_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
            LoadDocument();
            //Thread thread = new Thread(LoadDocument);
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Priority = ThreadPriority.Highest;
            //thread.Start();
            //InfoGrid.Visibility = Visibility.Collapsed;
            //var investmentElement = DataContext as IInvestmentElementPresenter;
            //if (((FrameworkElement) sender).IsVisible && investmentElement!=null)
            //{
            //    FlowDocument.Blocks.Clear();
            //  //  ShowInvestmentElement(investmentElement);
            //    ShowInvestmentElementTable(investmentElement);
            //}
        }

        private TableRow BlankRow()
        {
            TableRow blankRow = new TableRow();
            blankRow.Cells.Add(new TableCell());

            return blankRow;
        }
        private void ShowInvestmentElement(TableRowGroup tableRowGroup, IInvestmentElementPresenter investmentElement)
        {
            if (_customReportSettingsPresenter.ShowInvestmentElements)
            {
                //Table table = new Table();
                //table.Margin = new Thickness(0, 5, 0, 0);
                //table.Columns.Add(new TableColumn() { Name = "NameColumn" });
                //table.Columns.Add(new TableColumn() { Name = "CostColumn", Width = new GridLength(80, GridUnitType.Pixel) });



                //TableRowGroup tableRowGroup = new TableRowGroup();

                tableRowGroup.FontWeight = FontWeights.Normal;

                TableRow investmentElementRow = new TableRow();


                TableCell nameCell = new TableCell();

                nameCell.ColumnSpan = 2;
                Run nameText = new Run();
                nameText.Text = investmentElement.Name;
                nameText.FontWeight = FontWeights.Bold;
                // Add three parts of sentence to a paragraph, in order.
                Paragraph nameParagraph = new Paragraph();
                nameParagraph.Inlines.Add(nameText);
                nameCell.Blocks.Add(nameParagraph);
                investmentElementRow.Cells.Add(nameCell);


                if (_customReportSettingsPresenter.ShowMU)
                {
                    TableCell umCell = new TableCell();
                    Run umText = new Run();
                    umText.Text = "";
                    // Add three parts of sentence to a paragraph, in order.
                    Paragraph umParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                    umParagraph.Inlines.Add(umText);
                    umCell.Blocks.Add(umParagraph);
                    investmentElementRow.Cells.Add(umCell);
                }

                if (_customReportSettingsPresenter.ShowQuantity)
                {
                    TableCell quantityCell = new TableCell();

                    Run quantityText = new Run();
                    quantityText.Text = "";
                    // Add three parts of sentence to a paragraph, in order.
                    Paragraph quantityParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                    quantityParagraph.Inlines.Add(quantityText);
                    quantityCell.Blocks.Add(quantityParagraph);
                    investmentElementRow.Cells.Add(quantityCell);
                }

                if (_customReportSettingsPresenter.ShowCurrency)
                {
                    TableCell currencyCell = new TableCell();
                    Run currencyText = new Run();
                    currencyText.Text = "";
                    // Add three parts of sentence to a paragraph, in order.
                    Paragraph currencyParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                    currencyParagraph.Inlines.Add(currencyText);
                    currencyCell.Blocks.Add(currencyParagraph);
                    investmentElementRow.Cells.Add(currencyCell);
                }

                if (_customReportSettingsPresenter.ShowUC)
                {
                    TableCell priceCell = new TableCell();
                    Run priceText = new Run();
                    priceText.Text = "";
                    // Add three parts of sentence to a paragraph, in order.
                    Paragraph priceParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                    priceParagraph.Inlines.Add(priceText);
                    priceCell.Blocks.Add(priceParagraph);
                    investmentElementRow.Cells.Add(priceCell);
                }

                if (_customReportSettingsPresenter.ShowCost)
                {

                    TableCell costCell = new TableCell();
                    Run costText = new Run();
                    costText.Text = investmentElement.Cost.ToString();
                    costText.FontWeight = FontWeights.Bold;
                    // Add three parts of sentence to a paragraph, in order.
                    Paragraph costParagraph = new Paragraph() {TextAlignment = TextAlignment.Right};
                    costParagraph.Inlines.Add(costText);
                    costCell.Blocks.Add(costParagraph);
                    investmentElementRow.Cells.Add(costCell);
                }


                tableRowGroup.Rows.Add(investmentElementRow);

                //table.RowGroups.Add(tableRowGroup);

                //blockCollection.Add(table);
            }
            

            ShowBudgetRow(tableRowGroup, investmentElement);

            foreach (IInvestmentElementPresenter item in investmentElement.Items.Items)
            {
                ShowInvestmentElement(tableRowGroup, item);
                tableRowGroup.Rows.Add(BlankRow());
                
            }

            count++;
            int percent = Convert.ToInt32((count * 100) / totalRecords);

            //if(!_backgroundWorker.IsBusy)
            //    _backgroundWorker.RunWorkerAsync(percent);
            _backgroundWorker.ReportProgress(percent);

            //Thread thread = new Thread(Notify);
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Priority = ThreadPriority.Highest;
            //thread.Start();

        }

      //  private int percent { get; set; }

        //private void Notify()
        //{
        //  //  InfoTextBlock.Text = UIControls.Properties.Resources.Loading + " " + percent + "%";
        //    StatusBarServices.SignalText2(UIControls.Properties.Resources.Loading + " " + percent + "%");
           
        //}
        private int count = 0;
        private int totalRecords = 1;
        //private Table table;
        private MemoryStream ShowInvestmentElementTable(IInvestmentElementPresenter investmentElement)
        {

            var table = new Table() { Margin = new Thickness(0, 0, 0, 0) };
            table.Columns.Add(new TableColumn() { Name = "CodeColumn", Width = new GridLength(90, GridUnitType.Pixel) });
            table.Columns.Add(new TableColumn() { Name = "NameColumn" });

            if(_customReportSettingsPresenter.ShowMU)
                table.Columns.Add(new TableColumn() { Name = "UMColumn", Width = new GridLength(30, GridUnitType.Pixel) });
            if (_customReportSettingsPresenter.ShowQuantity)
                table.Columns.Add(new TableColumn() { Name = "QuantityColumn", Width = new GridLength(60, GridUnitType.Pixel) });
            if (_customReportSettingsPresenter.ShowCurrency)
                table.Columns.Add(new TableColumn() { Name = "CurrencyColumn", Width = new GridLength(30, GridUnitType.Pixel) });
            if (_customReportSettingsPresenter.ShowUC)
                table.Columns.Add(new TableColumn() { Name = "PriceColumn", Width = new GridLength(60, GridUnitType.Pixel) });
            if (_customReportSettingsPresenter.ShowCost)
                table.Columns.Add(new TableColumn() { Name = "CostColumn", Width = new GridLength(80, GridUnitType.Pixel) });



            TableRowGroup tableRowGroup = new TableRowGroup();
            table.RowGroups.Add(tableRowGroup);

            ShowInvestmentElement(tableRowGroup, investmentElement);

            MemoryStream stream = new MemoryStream();
            XamlWriter.Save(table, stream);
            return stream;
            //table.RowGroups.Add(tableRowGroup);


        }

        private Table  CopyTable(Table sourceTable)
        {
            Table destinationTable = new Table();
            if (sourceTable != null)
            {
                //foreach (TableColumn column in sourceTable.Columns)
                //{
                //    var shit = column.GetValue(WidthProperty);
                //   // var hollycow = shit.Value;
                //    destinationTable.Columns.Add(new TableColumn() { Name = column.Name, Width = new GridLength(column.Width.Value, column.Width.GridUnitType) });
                //}

                destinationTable.Columns.Add(new TableColumn() { Name = "CodeColumn", Width = new GridLength(80, GridUnitType.Pixel) });
                destinationTable.Columns.Add(new TableColumn() { Name = "NameColumn" });

                if (_customReportSettingsPresenter.ShowMU)
                    destinationTable.Columns.Add(new TableColumn() { Name = "UMColumn", Width = new GridLength(40, GridUnitType.Pixel) });
                if (_customReportSettingsPresenter.ShowQuantity)
                    destinationTable.Columns.Add(new TableColumn() { Name = "QuantityColumn", Width = new GridLength(80, GridUnitType.Pixel) });
                if (_customReportSettingsPresenter.ShowCurrency)
                    destinationTable.Columns.Add(new TableColumn() { Name = "CurrencyColumn", Width = new GridLength(40, GridUnitType.Pixel) });
                if (_customReportSettingsPresenter.ShowUC)
                    destinationTable.Columns.Add(new TableColumn() { Name = "PriceColumn", Width = new GridLength(80, GridUnitType.Pixel) });
                if (_customReportSettingsPresenter.ShowCost)
                    destinationTable.Columns.Add(new TableColumn() { Name = "CostColumn", Width = new GridLength(80, GridUnitType.Pixel) });



                foreach (TableRowGroup rowGroup in sourceTable.RowGroups)
                {
                    TableRowGroup newTableRowGroup = new TableRowGroup();
                    destinationTable.RowGroups.Add(newTableRowGroup);

                    foreach (TableRow tableRow in rowGroup.Rows)
                    {
                        TableRow newTableRow = new TableRow();
                        newTableRowGroup.Rows.Add( newTableRow);

                        foreach (TableCell tableCell in tableRow.Cells)
                        {
                            TableCell newtableCell = new TableCell();

                            var sourceParagraph = tableCell.Blocks.FirstBlock as Paragraph;
                            Paragraph newParagraph = new Paragraph();
                            newParagraph.Inlines.Add(sourceParagraph.Inlines.FirstInline);
                            newtableCell.Blocks.Add(newParagraph);
                            newTableRow.Cells.Add(newtableCell);
                        }
                    }
                }
            }

                return destinationTable;
        }
        private bool CanBeShown(BlockCollection blockCollection)
        {
            if (!_customReportSettingsPresenter.ShowInvestmentElements && !_customReportSettingsPresenter.ShowBudgetComponents)
            {
                return blockCollection.Cast<Table>().All(x => x.Name != "BudgetItemHeader");

            }

            return false;
        }
        private  void ShowBudgetRow(TableRowGroup tableRowGroup, IInvestmentElementPresenter item)
        {



            decimal equipment = item.Budget.EquipmentComponent.Cost;
            decimal construction = item.Budget.ConstructionComponent.Cost;
            decimal others = item.Budget.OtherExpensesComponent.Cost;
            decimal workcapital = item.Budget.WorkCapitalComponent.Cost;


          

            if (equipment > 0)
                ShowBudgetComponetRow(tableRowGroup, item.Budget.EquipmentComponent,Properties.Resources.EquipmentComponent + ":", _customReportSettingsPresenter.ShowEquipment);
            if (construction > 0)
                ShowBudgetComponetRow(tableRowGroup, item.Budget.ConstructionComponent, Properties.Resources.ConstructionComponent + ":", _customReportSettingsPresenter.ShowConstruction);
            if (others > 0)
                ShowBudgetComponetRow(tableRowGroup, item.Budget.OtherExpensesComponent, Properties.Resources.OtherExpensesComponent + ":", _customReportSettingsPresenter.ShowOthers);
            if (workcapital > 0)
                ShowBudgetComponetRow(tableRowGroup, item.Budget.WorkCapitalComponent, Properties.Resources.WorkCapitalComponent + ":", _customReportSettingsPresenter.ShowWorkCapital);

            //count++;
            //int percent = Convert.ToInt32((count * 100) / totalRecords);
            //_backgroundWorker.ReportProgress(percent);
        }

        private  void ShowBudgetComponetRow<TComponent, TPlannedSubSpecialityHolders, TPlannedSubSpecialityHolderPresenter, TExecutedSubSpecialityHolders, TExecutedSubSpecialityHolderPresenter>(TableRowGroup ptableRowGroup, IBudgetComponentPresenter<TComponent, TPlannedSubSpecialityHolders, TPlannedSubSpecialityHolderPresenter, TExecutedSubSpecialityHolders, TExecutedSubSpecialityHolderPresenter> item, string label, bool doShow)
             where TComponent : class, IBudgetComponent
        where TPlannedSubSpecialityHolders : class, IPlannedSubSpecialityHolderViewModel<TComponent, TPlannedSubSpecialityHolderPresenter>
        where TPlannedSubSpecialityHolderPresenter : class, IPlannedSubSpecialityHolderPresenter<TComponent>
        where TExecutedSubSpecialityHolders : class, IExecutedSubSpecialityHolderViewModel<TComponent, TExecutedSubSpecialityHolderPresenter>
        where TExecutedSubSpecialityHolderPresenter : class, IExecutedSubSpecialityHolderPresenter<TComponent>
        {
            TableRowGroup tableRowGroup = ptableRowGroup;
            TableRow budgetComponentRow = null;
            bool didnt = true;
            if (!_customReportSettingsPresenter.ShowInvestmentElements)
            {
                Table table = ptableRowGroup.Parent as Table;

                tableRowGroup = table?.RowGroups.SingleOrDefault(trg => trg.Tag != null && trg.Tag.ToString() == label);

                if (Equals(tableRowGroup, null))
                {
                    tableRowGroup = new TableRowGroup() { Tag = label };
                    table.RowGroups.Add(tableRowGroup);
                }
                else
                {
                  

                    if (table != null)
                    {
                        budgetComponentRow = tableRowGroup.Rows.SingleOrDefault(x =>
                        new TextRange(x.Cells[0].ContentStart, x.Cells[0].ContentEnd).Text == label);

                        var column = table.Columns.SingleOrDefault(c => c.Name == "CostColumn");
                        if (column != null && !Equals(budgetComponentRow, null) && _customReportSettingsPresenter.ShowCost)
                        {
                            int index = table.Columns.IndexOf(column);
                            TableCell costCell = budgetComponentRow.Cells[index-1];

                            TextRange currentRowCellTextRange = new TextRange(costCell.ContentStart, costCell.ContentEnd);
                            decimal cost = Convert.ToDecimal(currentRowCellTextRange.Text);
                            cost += item.Cost;

                            Paragraph costParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                            costParagraph.Inlines.Add(cost.ToString());
                            costCell.Blocks.Clear();
                            costCell.Blocks.Add(costParagraph);

                            didnt = false;
                        }

                    }
                  
                }

               

            }

            if (_customReportSettingsPresenter.ShowBudgetComponents && didnt && doShow)
            {
                

                
               

                    budgetComponentRow = new TableRow();
                budgetComponentRow.FontWeight = FontWeights.DemiBold;
                TableCell nameCell = new TableCell();
                    nameCell.ColumnSpan = 2;
                    Run nameText = new Run();
                    nameText.Text = label;
                    // Add three parts of sentence to a paragraph, in order.
                    Paragraph nameParagraph = new Paragraph();
                    nameParagraph.Inlines.Add(nameText);
                    nameCell.Blocks.Add(nameParagraph);
                    budgetComponentRow.Cells.Add(nameCell);

                    if (_customReportSettingsPresenter.ShowMU)
                    {
                        TableCell umCell = new TableCell();
                        Run umText = new Run();
                        umText.Text = "";
                        // Add three parts of sentence to a paragraph, in order.
                        Paragraph umParagraph = new Paragraph() {TextAlignment = TextAlignment.Right};
                        umParagraph.Inlines.Add(umText);
                        umCell.Blocks.Add(umParagraph);
                        budgetComponentRow.Cells.Add(umCell);
                    }

                    if (_customReportSettingsPresenter.ShowQuantity)
                    {
                        TableCell quantityCell = new TableCell();

                        Run quantityText = new Run();
                        quantityText.Text = "";
                        // Add three parts of sentence to a paragraph, in order.
                        Paragraph quantityParagraph = new Paragraph() {TextAlignment = TextAlignment.Right};
                        quantityParagraph.Inlines.Add(quantityText);
                        quantityCell.Blocks.Add(quantityParagraph);
                        budgetComponentRow.Cells.Add(quantityCell);
                    }

                    if (_customReportSettingsPresenter.ShowCurrency)
                    {
                        TableCell currencyCell = new TableCell();
                        Run currencyText = new Run();
                        currencyText.Text = "";
                        // Add three parts of sentence to a paragraph, in order.
                        Paragraph currencyParagraph = new Paragraph() {TextAlignment = TextAlignment.Right};
                        currencyParagraph.Inlines.Add(currencyText);
                        currencyCell.Blocks.Add(currencyParagraph);
                        budgetComponentRow.Cells.Add(currencyCell);
                    }

                    if (_customReportSettingsPresenter.ShowUC)
                    {
                        TableCell priceCell = new TableCell();
                        Run priceText = new Run();
                        priceText.Text = "";
                        // Add three parts of sentence to a paragraph, in order.
                        Paragraph priceParagraph = new Paragraph() {TextAlignment = TextAlignment.Right};
                        priceParagraph.Inlines.Add(priceText);
                        priceCell.Blocks.Add(priceParagraph);
                        budgetComponentRow.Cells.Add(priceCell);
                    }

                    if (_customReportSettingsPresenter.ShowCost)
                    {

                        TableCell costCell = new TableCell();
                        Run costText = new Run();
                        costText.Text = "";  //item.Cost?.ToString();
                        // Add three parts of sentence to a paragraph, in order.
                        Paragraph costParagraph = new Paragraph() {TextAlignment = TextAlignment.Right};
                        costParagraph.Inlines.Add(costText);
                        costCell.Blocks.Add(costParagraph);
                        budgetComponentRow.Cells.Add(costCell);
                    }

                    tableRowGroup.Rows.Add(budgetComponentRow);
              
                //table.RowGroups.Add(tableRowGroup);

                //blockCollection.Add(table);
            }
            if (((_customReportSettingsPresenter.ShowActivities) || 
                (_customReportSettingsPresenter.ShowResources)) && 
                (tableRowGroup.Rows.Count==0 || (tableRowGroup.Rows[tableRowGroup.Rows.Count - 1].Tag == null ||
                (tableRowGroup.Rows[tableRowGroup.Rows.Count-1].Tag!=null
                && tableRowGroup.Rows[tableRowGroup.Rows.Count - 1].Tag.ToString() != "headerRow"))))
            {



                //TableRowGroup tableRowGroup = new TableRowGroup();

                //tableRowGroup.FontWeight = FontWeights.Light;

                //tableRowGroup.FontSize = 10;

                TableRow headerRow = new TableRow();
                headerRow.Tag = "headerRow";
                headerRow.FontWeight = FontWeights.Light;
                headerRow.FontSize = 10;

                TableCell nameCell = new TableCell();
                nameCell.ColumnSpan = 2;
                Run nameText = new Run();
                nameText.Text = "";
                // Add three parts of sentence to a paragraph, in order.
                Paragraph nameParagraph = new Paragraph();
                nameParagraph.Inlines.Add(nameText);
                nameCell.Blocks.Add(nameParagraph);
                headerRow.Cells.Add(nameCell);

                if (_customReportSettingsPresenter.ShowMU)
                {
                    TableCell umCell = new TableCell();
                    Run umText = new Run();
                    umText.Text = Properties.Resources.U_Slash_M;
                    umText.FontWeight = FontWeights.Bold;
                    // Add three parts of sentence to a paragraph, in order.
                    Paragraph umParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                    umParagraph.Inlines.Add(umText);
                    umCell.Blocks.Add(umParagraph);
                    headerRow.Cells.Add(umCell);
                }

                if (_customReportSettingsPresenter.ShowQuantity)
                {
                    TableCell quantityCell = new TableCell();

                    Run quantityText = new Run();
                    quantityText.Text = Properties.Resources.Quantity;
                    quantityText.FontWeight = FontWeights.Bold;
                    // Add three parts of sentence to a paragraph, in order.
                    Paragraph quantityParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                    quantityParagraph.Inlines.Add(quantityText);
                    quantityCell.Blocks.Add(quantityParagraph);
                    headerRow.Cells.Add(quantityCell);
                }

                if (_customReportSettingsPresenter.ShowCurrency)
                {
                    TableCell currencyCell = new TableCell();
                    Run currencyText = new Run();
                    currencyText.Text = Properties.Resources.CurrencyAbv;
                    currencyText.FontWeight = FontWeights.Bold;
                    // Add three parts of sentence to a paragraph, in order.
                    Paragraph currencyParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                    currencyParagraph.Inlines.Add(currencyText);
                    currencyCell.Blocks.Add(currencyParagraph);
                    headerRow.Cells.Add(currencyCell);
                }

                if (_customReportSettingsPresenter.ShowUC)
                {
                    TableCell priceCell = new TableCell();
                    Run priceText = new Run();
                    priceText.Text = Properties.Resources.UnitaryCost;
                    priceText.FontWeight = FontWeights.Bold;
                    // Add three parts of sentence to a paragraph, in order.
                    Paragraph priceParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                    priceParagraph.Inlines.Add(priceText);
                    priceCell.Blocks.Add(priceParagraph);
                    headerRow.Cells.Add(priceCell);
                }

                if (_customReportSettingsPresenter.ShowCost)
                {
                    TableCell costCell = new TableCell();
                    Run costText = new Run();
                    costText.Text = Properties.Resources.Cost;
                    costText.FontWeight = FontWeights.Bold;
                    // Add three parts of sentence to a paragraph, in order.
                    Paragraph costParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                    costParagraph.Inlines.Add(costText);
                    costCell.Blocks.Add(costParagraph);
                    headerRow.Cells.Add(costCell);
                }


                tableRowGroup.Rows.Add(headerRow);
                //table.RowGroups.Add(tableRowGroup);

                //blockCollection.Add(table);
            }
            foreach (TPlannedSubSpecialityHolderPresenter plannedSubSpecialityHolderPresenter in item.PlannedSubSpecialityHolders.Items)
            {
                ShowSubSpecialityRow <TComponent,TPlannedSubSpecialityHolderPresenter > (tableRowGroup,plannedSubSpecialityHolderPresenter);
            }

            if (_customReportSettingsPresenter.ShowBudgetComponents && _customReportSettingsPresenter.ShowCost)
            {

                budgetComponentRow = new TableRow();
                budgetComponentRow.FontWeight = FontWeights.DemiBold;

                TableCell codeCell = new TableCell();
                Run codeText = new Run();
                codeText.Text = "";
                // Add three parts of sentence to a paragraph, in order.
                Paragraph codeParagraph = new Paragraph();
                codeParagraph.Inlines.Add(codeText);
                codeCell.Blocks.Add(codeParagraph);
                budgetComponentRow.Cells.Add(codeCell);

                TableCell nameCell = new TableCell();

                Run nameText = new Run();
                nameText.Text = "";
                // Add three parts of sentence to a paragraph, in order.
                Paragraph nameParagraph = new Paragraph();
                nameParagraph.Inlines.Add(nameText);
                nameCell.Blocks.Add(nameParagraph);
                budgetComponentRow.Cells.Add(nameCell);

                if (_customReportSettingsPresenter.ShowCost)
                {
                    TableCell umCell = new TableCell();
                    umCell.TextAlignment = TextAlignment.Right;
                   
                    Run umText = new Run();
                    umText.Text = "";
                    umText.FontWeight = FontWeights.SemiBold;
                    // Add three parts of sentence to a paragraph, in order.
                    Paragraph umParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                    umParagraph.Inlines.Add(umText);
                    umCell.Blocks.Add(umParagraph);
                    budgetComponentRow.Cells.Add(umCell);
                }

                ////if (CustomReportTreeViewContainer.SelectedSettings.ShowQuantity)
                ////{
                ////    TableCell quantityCell = new TableCell();

                ////    Run quantityText = new Run();
                ////    quantityText.Text = "";
                ////    // Add three parts of sentence to a paragraph, in order.
                ////    Paragraph quantityParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                ////    quantityParagraph.Inlines.Add(quantityText);
                ////    quantityCell.Blocks.Add(quantityParagraph);
                ////    subspecialityRow.Cells.Add(quantityCell);
                ////}

                ////if (CustomReportTreeViewContainer.SelectedSettings.ShowCurrency)
                ////{
                ////    TableCell currencyCell = new TableCell();
                ////    Run currencyText = new Run();
                ////    currencyText.Text = "";
                ////    // Add three parts of sentence to a paragraph, in order.
                ////    Paragraph currencyParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                ////    currencyParagraph.Inlines.Add(currencyText);
                ////    currencyCell.Blocks.Add(currencyParagraph);
                ////    subspecialityRow.Cells.Add(currencyCell);
                ////}

                if (_customReportSettingsPresenter.ShowCost)
                {
                    TableCell priceCell = new TableCell();
                    Run priceText = new Run();
                    priceText.Text = Properties.Resources.BudgetComponent + " " + Properties.Resources.Total;
                    priceText.FontWeight = FontWeights.SemiBold;
                    //TODO fix if a clown want to generate a report with less that 2 data columns
                    priceCell.ColumnSpan = (tableRowGroup.Parent as Table).Columns.Count - 4;
                    // Add three parts of sentence to a paragraph, in order.
                    Paragraph priceParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                    priceParagraph.Inlines.Add(priceText);
                    priceCell.Blocks.Add(priceParagraph);
                    budgetComponentRow.Cells.Add(priceCell);
                }

                if (_customReportSettingsPresenter.ShowCost)
                {
                    TableCell costCell = new TableCell();
                    Run costText = new Run();
                    costText.Text = item.Cost.ToString();
                    costText.FontWeight = FontWeights.SemiBold;
                    //costText.Text = "";
                    // Add three parts of sentence to a paragraph, in order.
                    Paragraph costParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };

                    costParagraph.Inlines.Add(costText);
                    costCell.Blocks.Add(costParagraph);
                    budgetComponentRow.Cells.Add(costCell);
                }


                tableRowGroup.Rows.Add(budgetComponentRow);

            }

            //count++;
            //int percent = Convert.ToInt32((count * 100) / totalRecords);
            //_backgroundWorker.ReportProgress(percent);
        }

        private  void ShowSubSpecialityRow<TComponent,TPlannedSubSpecialityHolderPresenter>(TableRowGroup ptableRowGroup, TPlannedSubSpecialityHolderPresenter item)
            where TComponent : class, IBudgetComponent
            where TPlannedSubSpecialityHolderPresenter : class, IPlannedSubSpecialityHolderPresenter<TComponent>
        {
            TableRowGroup tableRowGroup = ptableRowGroup;

            TableRow subspecialityRow = null;



            if (!_customReportSettingsPresenter.ShowInvestmentElements && !_customReportSettingsPresenter.ShowBudgetComponents)
            {


                Table table = ptableRowGroup.Parent as Table;

                tableRowGroup = table?.RowGroups.SingleOrDefault(trg => trg.Tag != null && trg.Tag.ToString() == item.Code+item.Name);


                

                if (Equals(tableRowGroup, null))
                {
                    tableRowGroup = new TableRowGroup() { Tag = item.Code + item.Name };
                    table.RowGroups.Add(tableRowGroup);
                }

            }


            if (_customReportSettingsPresenter.ShowSubSpecialities|| _customReportSettingsPresenter.ShowSubExpeseConcepts || _customReportSettingsPresenter.ShowCategories)
                {

                if (!_customReportSettingsPresenter.ShowInvestmentElements && !_customReportSettingsPresenter.ShowBudgetComponents)
                 subspecialityRow = tableRowGroup.Rows.SingleOrDefault(x =>
                 new TextRange(x.Cells[0].ContentStart, x.Cells[0].ContentEnd).Text == item.Code.ToString() &&
                 new TextRange(x.Cells[1].ContentStart, x.Cells[1].ContentEnd).Text == item.Name);

                if (!Equals(subspecialityRow, null))
                {

                    if (_customReportSettingsPresenter.ShowCost )
                    {
                        Table table = tableRowGroup.Parent as Table;

                        if (table != null)
                        {
                            var column = table.Columns.SingleOrDefault(c => c.Name == "CostColumn");
                            if (column != null)
                            {
                                int index = table.Columns.IndexOf(column);
                                TableCell costCell = subspecialityRow.Cells[index];

                                TextRange currentRowCellTextRange = new TextRange(costCell.ContentStart, costCell.ContentEnd);
                                decimal cost = Convert.ToDecimal(currentRowCellTextRange.Text);
                                cost += item.Cost;

                                Paragraph costParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                                costParagraph.Inlines.Add(cost.ToString());
                                costCell.Blocks.Clear();
                                costCell.Blocks.Add(costParagraph);
                            }

                        }


                    }


                    tableRowGroup = subspecialityRow.Parent as TableRowGroup;

                }

                else
                    {
                        subspecialityRow = new TableRow();
                        subspecialityRow.FontWeight = FontWeights.Light;

                        TableCell codeCell = new TableCell();
                        Run codeText = new Run();
                        codeText.Text = item.Code?.ToString();
                        codeText.FontWeight = FontWeights.SemiBold;
                        // Add three parts of sentence to a paragraph, in order.
                        Paragraph codeParagraph = new Paragraph();
                        codeParagraph.Inlines.Add(codeText);
                        codeCell.Blocks.Add(codeParagraph);
                        subspecialityRow.Cells.Add(codeCell);

                        TableCell nameCell = new TableCell();

                        Run nameText = new Run();
                        if(_customReportSettingsPresenter.ShowSubSpecialities)
                        nameText.Text = item.Name;
                    if (_customReportSettingsPresenter.ShowSubExpeseConcepts)
                        nameText.Text = item.SubExpenseConcept.Name;
                    if (_customReportSettingsPresenter.ShowCategories)
                        nameText.Text = item.Category.Name;
                    nameText.FontWeight = FontWeights.SemiBold;
                    // Add three parts of sentence to a paragraph, in order.
                         Paragraph nameParagraph = new Paragraph();
                        nameParagraph.Inlines.Add(nameText);
                        nameCell.Blocks.Add(nameParagraph);
                        subspecialityRow.Cells.Add(nameCell);

                        if (_customReportSettingsPresenter.ShowMU)
                        {
                            TableCell umCell = new TableCell();
                            Run umText = new Run();
                            umText.Text = "";
                            // Add three parts of sentence to a paragraph, in order.
                            Paragraph umParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                            umParagraph.Inlines.Add(umText);
                            umCell.Blocks.Add(umParagraph);
                            subspecialityRow.Cells.Add(umCell);
                        }

                        if (_customReportSettingsPresenter.ShowQuantity)
                        {
                            TableCell quantityCell = new TableCell();

                            Run quantityText = new Run();
                            quantityText.Text = "";
                            // Add three parts of sentence to a paragraph, in order.
                            Paragraph quantityParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                            quantityParagraph.Inlines.Add(quantityText);
                            quantityCell.Blocks.Add(quantityParagraph);
                            subspecialityRow.Cells.Add(quantityCell);
                        }

                        if (_customReportSettingsPresenter.ShowCurrency)
                        {
                            TableCell currencyCell = new TableCell();
                            Run currencyText = new Run();
                            currencyText.Text = "";
                            // Add three parts of sentence to a paragraph, in order.
                            Paragraph currencyParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                            currencyParagraph.Inlines.Add(currencyText);
                            currencyCell.Blocks.Add(currencyParagraph);
                            subspecialityRow.Cells.Add(currencyCell);
                        }

                        if (_customReportSettingsPresenter.ShowUC)
                        {
                            TableCell priceCell = new TableCell();
                            Run priceText = new Run();
                            priceText.Text = "";
                            // Add three parts of sentence to a paragraph, in order.
                            Paragraph priceParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                            priceParagraph.Inlines.Add(priceText);
                            priceCell.Blocks.Add(priceParagraph);
                            subspecialityRow.Cells.Add(priceCell);
                        }

                        if (_customReportSettingsPresenter.ShowCost)
                        {
                            TableCell costCell = new TableCell();
                            Run costText = new Run();
                        //  costText.Text = item.Cost?.ToString();
                            costText.Text = "";
                         // Add three parts of sentence to a paragraph, in order.
                        Paragraph costParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                            costParagraph.Inlines.Add(costText);
                            costCell.Blocks.Add(costParagraph);
                            subspecialityRow.Cells.Add(costCell);
                        }


                        tableRowGroup.Rows.Add(subspecialityRow);
                    }



                    

                
            }

            

            foreach (IPlannedActivityPresenter plannedActivityPresenter in item.PlannedActivities.Items)
            {
                ShowBudgetComponentItemRow(tableRowGroup,plannedActivityPresenter);
            }


                    if ((_customReportSettingsPresenter.ShowSubSpecialities || _customReportSettingsPresenter.ShowSubExpeseConcepts || _customReportSettingsPresenter.ShowCategories) && _customReportSettingsPresenter.ShowCost)
                    {

                        subspecialityRow = new TableRow();
                        subspecialityRow.FontWeight = FontWeights.Light;

                        TableCell codeCell = new TableCell();
                        Run codeText = new Run();
                        codeText.Text = "";
                        // Add three parts of sentence to a paragraph, in order.
                        Paragraph codeParagraph = new Paragraph();
                        codeParagraph.Inlines.Add(codeText);
                        codeCell.Blocks.Add(codeParagraph);
                        subspecialityRow.Cells.Add(codeCell);

                        TableCell nameCell = new TableCell();

                        Run nameText = new Run();
                        nameText.Text = "";
                        // Add three parts of sentence to a paragraph, in order.
                        Paragraph nameParagraph = new Paragraph();
                        nameParagraph.Inlines.Add(nameText);
                        nameCell.Blocks.Add(nameParagraph);
                        subspecialityRow.Cells.Add(nameCell);

                if (_customReportSettingsPresenter.ShowCost)
                {
                    TableCell umCell = new TableCell();
                            umCell.TextAlignment = TextAlignment.Right;
                            //umCell.ColumnSpan = 3;
                            Run umText = new Run();
                            umText.Text = "";
                            umText.FontWeight = FontWeights.SemiBold;
                    // Add three parts of sentence to a paragraph, in order.
                            Paragraph umParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                            umParagraph.Inlines.Add(umText);
                            umCell.Blocks.Add(umParagraph);
                            subspecialityRow.Cells.Add(umCell);
                }

                ////if (CustomReportTreeViewContainer.SelectedSettings.ShowQuantity)
                ////{
                ////    TableCell quantityCell = new TableCell();

                ////    Run quantityText = new Run();
                ////    quantityText.Text = "";
                ////    // Add three parts of sentence to a paragraph, in order.
                ////    Paragraph quantityParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                ////    quantityParagraph.Inlines.Add(quantityText);
                ////    quantityCell.Blocks.Add(quantityParagraph);
                ////    subspecialityRow.Cells.Add(quantityCell);
                ////}

                ////if (CustomReportTreeViewContainer.SelectedSettings.ShowCurrency)
                ////{
                ////    TableCell currencyCell = new TableCell();
                ////    Run currencyText = new Run();
                ////    currencyText.Text = "";
                ////    // Add three parts of sentence to a paragraph, in order.
                ////    Paragraph currencyParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                ////    currencyParagraph.Inlines.Add(currencyText);
                ////    currencyCell.Blocks.Add(currencyParagraph);
                ////    subspecialityRow.Cells.Add(currencyCell);
                ////}

                if (_customReportSettingsPresenter.ShowCost)
                {
                    TableCell priceCell = new TableCell();
                            priceCell.ColumnSpan = (tableRowGroup.Parent as Table).Columns.Count - 4;
                            Run priceText = new Run();
                           
                            if(_customReportSettingsPresenter.ShowSubSpecialities)
                                  priceText.Text = Properties.Resources.SubSpeciality + " " + Properties.Resources.Total;
                            if (_customReportSettingsPresenter.ShowSubExpeseConcepts)
                                  priceText.Text = Properties.Resources.ExpenseConcepts + " " + Properties.Resources.Total;
                            if (_customReportSettingsPresenter.ShowCategories)
                                   priceText.Text = Properties.Resources.Category + " " + Properties.Resources.Total;
                    priceText.FontWeight = FontWeights.SemiBold;
                    // Add three parts of sentence to a paragraph, in order.
                            Paragraph priceParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                            priceParagraph.Inlines.Add(priceText);
                            priceCell.Blocks.Add(priceParagraph);
                            subspecialityRow.Cells.Add(priceCell);
                }

                if (_customReportSettingsPresenter.ShowCost)
                        {
                            TableCell costCell = new TableCell();
                            Run costText = new Run();
                            costText.Text = item.Cost.ToString();
                            costText.FontWeight = FontWeights.SemiBold;
                            //costText.Text = "";
                            // Add three parts of sentence to a paragraph, in order.
                            Paragraph costParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                           
                            costParagraph.Inlines.Add(costText);
                            costCell.Blocks.Add(costParagraph);
                            subspecialityRow.Cells.Add(costCell);
                        }


                        tableRowGroup.Rows.Add(subspecialityRow);
            
                }

            //table.RowGroups.Add(tableRowGroup);
            //blockCollection.Add(table);

            //count++;
            //int percent = Convert.ToInt32((count * 100) / totalRecords);
            //_backgroundWorker.ReportProgress(percent);
        }

      

        private void ShowBudgetComponentItemRow<TItem>(TableRowGroup ptableRowGroup, IBudgetComponentItemPresenter<TItem> budgetComponentItemPresenter)
            where TItem:class ,IBudgetComponentItem
        {

            TableRowGroup tableRowGroup = ptableRowGroup;

            if ((_customReportSettingsPresenter.ShowActivities && budgetComponentItemPresenter.Object is IActivity) ||
                (_customReportSettingsPresenter.ShowResources && budgetComponentItemPresenter.Object is IPlannedResource))
            {

               

                TableRow budgetComponentItemRow = null;

                if (((!_customReportSettingsPresenter.ShowInvestmentElements && !_customReportSettingsPresenter.ShowBudgetComponents
                    && !_customReportSettingsPresenter.ShowSubSpecialities) && budgetComponentItemPresenter.Object is IActivity) ||
                    ((!_customReportSettingsPresenter.ShowInvestmentElements && !_customReportSettingsPresenter.ShowBudgetComponents
                    && !_customReportSettingsPresenter.ShowSubSpecialities && !_customReportSettingsPresenter.ShowActivities) && budgetComponentItemPresenter.Object is IPlannedResource))
                {
                    Table table = ptableRowGroup.Parent as Table;

                    tableRowGroup = table?.RowGroups.SingleOrDefault(trg => trg.Tag != null && trg.Tag.ToString() == budgetComponentItemPresenter.Code + budgetComponentItemPresenter.Name);

                    if (Equals(tableRowGroup, null))
                    {
                        tableRowGroup = new TableRowGroup() { Tag = budgetComponentItemPresenter.Code + budgetComponentItemPresenter.Name };
                        table.RowGroups.Add(tableRowGroup);
                    }
                    else
                    {
                        var query = from nrow in tableRowGroup.Rows
                                    where new TextRange(nrow.Cells[0].ContentStart, nrow.Cells[0].ContentEnd).Text ==
                                        budgetComponentItemPresenter.Code &&
                                        new TextRange(nrow.Cells[1].ContentStart, nrow.Cells[1].ContentEnd).Text ==
                                        budgetComponentItemPresenter.Name
                                    select nrow;

                        if (query.Any())
                            budgetComponentItemRow = query.First();
                    }

                    

                }



                if (!Equals(budgetComponentItemRow, null))
                {
                 ////   var tableColumn = table.Columns.SingleOrDefault(x => x.Name == "QuantityColumn");
                 //   /
                 //     /

                    Table table = tableRowGroup.Parent as Table;

                    if (!Equals(table, null))
                    {
                        var column = table.Columns.SingleOrDefault(c => c.Name == "QuantityColumn");

                        if (!Equals(column, null))
                        {
                            int index = table.Columns.IndexOf(column);
                            TableCell quantityCell = budgetComponentItemRow.Cells[index];
                            TextRange quantityRowCellTextRange = new TextRange(quantityCell.ContentStart,
                                quantityCell.ContentEnd);
                            decimal quantity = Convert.ToDecimal(quantityRowCellTextRange.Text);
                            quantity += budgetComponentItemPresenter.Quantity;

                            Paragraph quantityParagraph = new Paragraph() {TextAlignment = TextAlignment.Right};
                            quantityParagraph.Inlines.Add(quantity.ToString());
                            quantityCell.Blocks.Clear();
                            quantityCell.Blocks.Add(quantityParagraph);
                        }

                        column = table.Columns.SingleOrDefault(c => c.Name == "CostColumn");

                        if (!Equals(column, null))
                        {
                            int index = table.Columns.IndexOf(column);
                            TableCell costCell = budgetComponentItemRow.Cells[index];
                            TextRange currentRowCellTextRange = new TextRange(costCell.ContentStart, costCell.ContentEnd);
                            decimal cost = Convert.ToDecimal(currentRowCellTextRange.Text);
                            cost += budgetComponentItemPresenter.Cost;

                            Paragraph costParagraph = new Paragraph() {TextAlignment = TextAlignment.Right};
                            costParagraph.Inlines.Add(cost.ToString());
                            costCell.Blocks.Clear();
                            costCell.Blocks.Add(costParagraph);
                        }

                    }
                    //  }

                    //tableColumn = table.Columns.SingleOrDefault(x => x.Name == "CostColumn");
                    //if (!Equals(tableColumn, null))
                    //{
                        //int index = table.Columns.IndexOf(tableColumn);

                    }

            
                else
                {
                    budgetComponentItemRow = new TableRow();
                    if(budgetComponentItemPresenter.Kind== "Resource")
                     budgetComponentItemRow.FontWeight = FontWeights.Light;
                    TableCell codeCell = new TableCell();

                    Run codeText = new Run();
                    codeText.Text = budgetComponentItemPresenter.Code;
                    // Add three parts of sentence to a paragraph, in order.
                    Paragraph codeParagraph = new Paragraph();
                    codeParagraph.Inlines.Add(codeText);
                    codeCell.Blocks.Add(codeParagraph);
                    budgetComponentItemRow.Cells.Add(codeCell);

                    TableCell nameCell = new TableCell();

                    Run nameText = new Run();
                    nameText.Text = budgetComponentItemPresenter.Name;
                    // Add three parts of sentence to a paragraph, in order.
                    Paragraph nameParagraph = new Paragraph();
                    nameParagraph.Inlines.Add(nameText);
                    nameCell.Blocks.Add(nameParagraph);
                    budgetComponentItemRow.Cells.Add(nameCell);


                    if (_customReportSettingsPresenter.ShowMU)
                    {
                        TableCell umCell = new TableCell();
                        Run umText = new Run();
                        umText.Text = budgetComponentItemPresenter.MeasurementUnit?.Letters;
                        // Add three parts of sentence to a paragraph, in order.
                        Paragraph umParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                        umParagraph.Inlines.Add(umText);
                        umCell.Blocks.Add(umParagraph);
                        budgetComponentItemRow.Cells.Add(umCell);
                    }

                    if (_customReportSettingsPresenter.ShowQuantity)
                    {
                        TableCell quantityCell = new TableCell();

                        Run quantityText = new Run();
                        quantityText.Text = budgetComponentItemPresenter.Quantity.ToString();
                        // Add three parts of sentence to a paragraph, in order.
                        Paragraph quantityParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                        quantityParagraph.Inlines.Add(quantityText);
                        quantityCell.Blocks.Add(quantityParagraph);
                        budgetComponentItemRow.Cells.Add(quantityCell);
                    }

                    if (_customReportSettingsPresenter.ShowCurrency)
                    {
                        TableCell currencyCell = new TableCell();
                        Run currencyText = new Run();
                        currencyText.Text = budgetComponentItemPresenter.Currency?.Letters;
                        // Add three parts of sentence to a paragraph, in order.
                        Paragraph currencyParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                        currencyParagraph.Inlines.Add(currencyText);
                        currencyCell.Blocks.Add(currencyParagraph);
                        budgetComponentItemRow.Cells.Add(currencyCell);
                    }

                    if (_customReportSettingsPresenter.ShowUC)
                    {
                        TableCell priceCell = new TableCell();
                        Run priceText = new Run();
                        priceText.Text = budgetComponentItemPresenter.UnitaryCost.ToString();
                        // Add three parts of sentence to a paragraph, in order.
                        Paragraph priceParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                        priceParagraph.Inlines.Add(priceText);
                        priceCell.Blocks.Add(priceParagraph);
                        budgetComponentItemRow.Cells.Add(priceCell);

                    }

                    if (_customReportSettingsPresenter.ShowCost)
                    {
                        TableCell costCell = new TableCell();
                        Run costText = new Run();
                        costText.Text = budgetComponentItemPresenter.Cost.ToString();
                        // Add three parts of sentence to a paragraph, in order.
                        Paragraph costParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                        costParagraph.Inlines.Add(costText);
                        costCell.Blocks.Add(costParagraph);
                        budgetComponentItemRow.Cells.Add(costCell);
                    }

                    tableRowGroup.Rows.Add(budgetComponentItemRow);
                }


               
            }


            foreach (IPlannedResourcePresenter<TItem> plannedResourcePresenter in budgetComponentItemPresenter.PlannedResources.Items)
            {
                ShowBudgetComponentItemRow(tableRowGroup, plannedResourcePresenter);
            }

            //count++;
            //int percent = Convert.ToInt32((count * 100) / totalRecords);
            //_backgroundWorker.ReportProgress(percent);
        }

        private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _isLoaded = false;
            
            //InfoTextBlock.Text = UIControls.Properties.Resources.Loading;
            _datacontext = e.NewValue as IInvestmentElementPresenter;
            LoadDocument();
            //Thread thread = new Thread(LoadDocument);
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Priority = ThreadPriority.Highest;
            //thread.Start();
            //InfoGrid.Visibility = Visibility.Collapsed;
            //var investmentElement = DataContext as IInvestmentElementPresenter;
            //if (((FrameworkElement)sender).IsVisible && investmentElement != null)
            //{
            //    FlowDocument.Blocks.Clear();
            //    //  ShowInvestmentElement(investmentElement);
            //    ShowInvestmentElementTable(investmentElement);
            //}
        }

        private IStatusBarServices _statusBarServices;
        private BackgroundWorker _backgroundWorker;
        private bool _isLoaded;
        private FlowDocument _flowDocument;

        private IStatusBarServices StatusBarServices
        {
            get
            {
                return _statusBarServices ?? (_statusBarServices = ServiceLocator.Current.GetInstance<IStatusBarServices>());
            }
        }


        public void Print()
        {
            Thread prinThread = new Thread(DoPrint);
            prinThread.SetApartmentState(ApartmentState.STA);
            prinThread.Priority = ThreadPriority.Highest;
            prinThread.Start();
        }

        private void ReportsGenerator_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (Tag is ICommand)
            {
                // Create the binding.
                CommandBinding binding = new CommandBinding((ICommand)Tag);
                // Attach the event handler.
                binding.Executed += FindCommand_Executed;
                // Register the binding.
                FlowDocument.CommandBindings.Add(binding);

            }
          
        }

        private void FindCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        public void Export()
        {
            if (!_backgroundWorker.IsBusy && _isLoaded)
            {
                
           
            // Creating a Excel object. 
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            try
            {

                worksheet = workbook.ActiveSheet;

            if (_customReportSettingsPresenter.Name!=null)
                worksheet.Name = _customReportSettingsPresenter.Name;
            else
                         worksheet.Name = "ExportFromInvestmentFullReport";

                    int cellRowIndex = 1;
                int cellColumnIndex = 1;

                var table = FlowDocument.Blocks.FirstBlock as Table;

                //Loop through each row and read value from each column. 
                for (int i = 0; i < table.RowGroups.Count; i++)
                {

                    for (int e = 0; e < table.RowGroups[i].Rows.Count; e++)
                    {
                        for (int j = 0; j < table.RowGroups[i].Rows[e].Cells.Count; j++)
                        {
                            // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check. 
                            //if (cellRowIndex == 1)
                            //{
                            //    worksheet.Cells[cellRowIndex, cellColumnIndex] = dgvCityDetails.Columns[j].HeaderText;
                            //}
                            //else
                            //{
                            //    worksheet.Cells[cellRowIndex, cellColumnIndex] = dgvCityDetails.Rows[i].Cells[j].Value.ToString();
                            //}
                            var tablecell = table.RowGroups[i].Rows[e].Cells[j];
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = new TextRange(tablecell.ContentStart, tablecell.ContentEnd).Text;
                           
                            cellColumnIndex +=tablecell.ColumnSpan;
                        }
                        cellColumnIndex = 1;
                        cellRowIndex++;
                    }
                }

                //Getting the location and file name of the excel to save from user. 
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FileName = worksheet.Name;
                saveDialog.FilterIndex = 2;

                if (saveDialog.ShowDialog() == true)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    StatusBarServices.SignalText("Export Successful");
                }
            }
            catch (System.Exception ex)
            {
                StatusBarServices.SignalText(ex.Message);
            }
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }

            }
        }

        //private void ResourceCheckBox_OnChecked(object sender, RoutedEventArgs e)
        //{
        //    var investmentElement = DataContext as IInvestmentElementPresenter;
        //    if (((FrameworkElement)sender).IsVisible && investmentElement != null)
        //    {
        //        FlowDocument.Blocks.Clear();
        //        //  ShowInvestmentElement(investmentElement);
        //        ShowInvestmentElementTable(investmentElement);
        //    }
        //}

       
    }

  
}
