using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using CompanyName.Atlas.Contracts.Presentation.Features;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Construction;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Presentation.Views.ReportViewsUI
{
    /// <summary>
    /// Interaction logic for InvestmentFullReport.xaml
    /// </summary>
    public partial class InvestmentFullReport : UserControl, IPrinttableContainer
    {
        public InvestmentFullReport()
        {
            InitializeComponent();
        }

        private void UIElement_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Thread thread = new Thread(LoadDocument);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
          
        }

        private void LoadDocument()
        {
            // Get the dispatcher from the current window, and use it to invoke
            // the update code.
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
            (ThreadStart)delegate () {
                var investmentElement = DataContext as IInvestmentElementPresenter;
                if (IsVisible && investmentElement != null)
                {
                    FlowDocument.Blocks.Clear();
                    //  ShowInvestmentElement(investmentElement);
                    ShowInvestmentElementTable(investmentElement);
                }
            }
            );
        }

        private void ShowInvestmentElement(BlockCollection blockCollection, IInvestmentElementPresenter investmentElement)
        {
            Table table = new Table();
            table.Margin = new Thickness(0,5,0,0);
            table.Columns.Add(new TableColumn() { Name = "NameColumn" });
            table.Columns.Add(new TableColumn() { Name = "CostColumn", Width = new GridLength(80, GridUnitType.Pixel) });



            TableRowGroup tableRowGroup = new TableRowGroup();

            tableRowGroup.FontWeight= FontWeights.Normal;

            TableRow investmentElementRow = new TableRow();


            TableCell nameCell = new TableCell();

            Run nameText = new Run();
            nameText.Text = investmentElement.Name;
            // Add three parts of sentence to a paragraph, in order.
            Paragraph nameParagraph = new Paragraph();
            nameParagraph.Inlines.Add(nameText);
            nameCell.Blocks.Add(nameParagraph);
            investmentElementRow.Cells.Add(nameCell);

          
            TableCell costCell = new TableCell();
            Run costText = new Run();
            costText.Text = investmentElement.Cost.ToString();
            // Add three parts of sentence to a paragraph, in order.
            Paragraph costParagraph = new Paragraph() {TextAlignment = TextAlignment.Right};
            costParagraph.Inlines.Add(costText);
            costCell.Blocks.Add(costParagraph);
            investmentElementRow.Cells.Add(costCell);


            tableRowGroup.Rows.Add(investmentElementRow);

            table.RowGroups.Add(tableRowGroup);

            blockCollection.Add(table);

            ShowBudgetRow(blockCollection, investmentElement);

            foreach (IInvestmentElementPresenter item in investmentElement.Items.Items)
            {
                ShowInvestmentElement(blockCollection, item);
            }


        }

        private void ShowInvestmentElementTable(IInvestmentElementPresenter investmentElement)
        {
            //Table table = new Table(){Margin = new Thickness(0,0,0,0)};

            //table.Columns.Add(new TableColumn() {Name = "CodeColumn", Width = new GridLength(80, GridUnitType.Pixel) });
            //table.Columns.Add(new TableColumn() {Name = "NameColumn" });
            //table.Columns.Add(new TableColumn() {Name = "UMColumn", Width = new GridLength(40, GridUnitType.Pixel) });
            //table.Columns.Add(new TableColumn() {Name = "QuantityColumn", Width = new GridLength(60, GridUnitType.Pixel) });
            //table.Columns.Add(new TableColumn() {Name = "CurrencyColumn", Width = new GridLength(40, GridUnitType.Pixel) });
            //table.Columns.Add(new TableColumn() {Name = "PriceColumn", Width = new GridLength(50, GridUnitType.Pixel) });
            //table.Columns.Add(new TableColumn() {Name = "CostColumn", Width = new GridLength(80, GridUnitType.Pixel) });

            

            //TableRowGroup tableRowGroup = new TableRowGroup();

            ShowInvestmentElement(FlowDocument.Blocks, investmentElement);

            //table.RowGroups.Add(tableRowGroup);

            //FlowDocument.Blocks.Add(table);
        }

        private  void ShowBudgetRow(BlockCollection blockCollection, IInvestmentElementPresenter item)
        {

            decimal equipment = item.Budget.EquipmentComponent.Cost;
            decimal construction = item.Budget.ConstructionComponent.Cost;
            decimal others = item.Budget.OtherExpensesComponent.Cost;
            decimal workcapital = item.Budget.WorkCapitalComponent.Cost;


            if (equipment > 0|| construction > 0|| others > 0|| workcapital > 0)
            {
                Table table = new Table() { Margin = new Thickness(0, 0, 0, 0) };

                table.Columns.Add(new TableColumn() { Name = "NameColumn" });
                table.Columns.Add(new TableColumn() { Name = "UMColumn", Width = new GridLength(40, GridUnitType.Pixel) });
                table.Columns.Add(new TableColumn() { Name = "QuantityColumn", Width = new GridLength(60, GridUnitType.Pixel) });
                table.Columns.Add(new TableColumn() { Name = "CurrencyColumn", Width = new GridLength(40, GridUnitType.Pixel) });
                table.Columns.Add(new TableColumn() { Name = "PriceColumn", Width = new GridLength(50, GridUnitType.Pixel) });
                table.Columns.Add(new TableColumn() { Name = "CostColumn", Width = new GridLength(80, GridUnitType.Pixel) });



                TableRowGroup tableRowGroup = new TableRowGroup();

                tableRowGroup.FontWeight = FontWeights.Light;

                tableRowGroup.FontSize = 10;

                TableRow headerRow = new TableRow();

                TableCell nameCell = new TableCell();

                Run nameText = new Run();
                nameText.Text = "";
                // Add three parts of sentence to a paragraph, in order.
                Paragraph nameParagraph = new Paragraph();
                nameParagraph.Inlines.Add(nameText);
                nameCell.Blocks.Add(nameParagraph);
                headerRow.Cells.Add(nameCell);

                TableCell umCell = new TableCell();
                Run umText = new Run();
                umText.Text = Properties.Resources.U_Slash_M;
                // Add three parts of sentence to a paragraph, in order.
                Paragraph umParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                umParagraph.Inlines.Add(umText);
                umCell.Blocks.Add(umParagraph);
                headerRow.Cells.Add(umCell);

                TableCell quantityCell = new TableCell();

                Run quantityText = new Run();
                quantityText.Text = Properties.Resources.Quantity;
                // Add three parts of sentence to a paragraph, in order.
                Paragraph quantityParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                quantityParagraph.Inlines.Add(quantityText);
                quantityCell.Blocks.Add(quantityParagraph);
                headerRow.Cells.Add(quantityCell);

                TableCell currencyCell = new TableCell();
                Run currencyText = new Run();
                currencyText.Text = Properties.Resources.Currency;
                // Add three parts of sentence to a paragraph, in order.
                Paragraph currencyParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                currencyParagraph.Inlines.Add(currencyText);
                currencyCell.Blocks.Add(currencyParagraph);
                headerRow.Cells.Add(currencyCell);

                TableCell priceCell = new TableCell();
                Run priceText = new Run();
                priceText.Text = Properties.Resources.UnitaryCost;
                // Add three parts of sentence to a paragraph, in order.
                Paragraph priceParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                priceParagraph.Inlines.Add(priceText);
                priceCell.Blocks.Add(priceParagraph);
                headerRow.Cells.Add(priceCell);

                TableCell costCell = new TableCell();
                Run costText = new Run();
                costText.Text = Properties.Resources.Cost;
                // Add three parts of sentence to a paragraph, in order.
                Paragraph costParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
                costParagraph.Inlines.Add(costText);
                costCell.Blocks.Add(costParagraph);
                headerRow.Cells.Add(costCell);

                tableRowGroup.Rows.Add(headerRow);
                table.RowGroups.Add(tableRowGroup);

                blockCollection.Add(table);
            }

            if (equipment > 0)
                ShowBudgetComponetRow(blockCollection, item.Budget.EquipmentComponent,Properties.Resources.EquipmentComponent + ":");
            if (construction > 0)
                ShowBudgetComponetRow(blockCollection, item.Budget.ConstructionComponent, Properties.Resources.ConstructionComponent + ":");
            if (others > 0)
                ShowBudgetComponetRow(blockCollection, item.Budget.OtherExpensesComponent, Properties.Resources.OtherExpensesComponent + ":");
            if (workcapital > 0)
                ShowBudgetComponetRow(blockCollection, item.Budget.WorkCapitalComponent, Properties.Resources.WorkCapitalComponent + ":");

        }

        private  void ShowBudgetComponetRow<TComponent, TPlannedSubSpecialityHolders, TPlannedSubSpecialityHolderPresenter, TExecutedSubSpecialityHolders, TExecutedSubSpecialityHolderPresenter>(BlockCollection blockCollection, IBudgetComponentPresenter<TComponent, TPlannedSubSpecialityHolders, TPlannedSubSpecialityHolderPresenter, TExecutedSubSpecialityHolders, TExecutedSubSpecialityHolderPresenter> item, string label)
             where TComponent : class, IBudgetComponent
        where TPlannedSubSpecialityHolders : class, IPlannedSubSpecialityHolderViewModel<TComponent, TPlannedSubSpecialityHolderPresenter>
        where TPlannedSubSpecialityHolderPresenter : class, IPlannedSubSpecialityHolderPresenter<TComponent>
        where TExecutedSubSpecialityHolders : class, IExecutedSubSpecialityHolderViewModel<TComponent, TExecutedSubSpecialityHolderPresenter>
        where TExecutedSubSpecialityHolderPresenter : class, IExecutedSubSpecialityHolderPresenter<TComponent>
        {
            Table table = new Table(){Margin = new Thickness(0,0,0,0)};

            table.Columns.Add(new TableColumn() { Name = "NameColumn" });
            table.Columns.Add(new TableColumn() { Name = "CostColumn", Width = new GridLength(80, GridUnitType.Pixel) });


          

            TableRowGroup tableRowGroup = new TableRowGroup();
            tableRowGroup.FontSize = 12;
            tableRowGroup.FontWeight = FontWeights.Normal;

            TableRow budgetComponentRow = new TableRow();

            TableCell nameCell = new TableCell();

            Run nameText = new Run();
            nameText.Text = label;
            // Add three parts of sentence to a paragraph, in order.
            Paragraph nameParagraph = new Paragraph();
            nameParagraph.Inlines.Add(nameText);
            nameCell.Blocks.Add(nameParagraph);
            budgetComponentRow.Cells.Add(nameCell);

           

            TableCell costCell = new TableCell();
            Run costText = new Run();
            costText.Text = item.Cost.ToString();
            // Add three parts of sentence to a paragraph, in order.
            Paragraph costParagraph = new Paragraph() {TextAlignment = TextAlignment.Right};
            costParagraph.Inlines.Add(costText);
            costCell.Blocks.Add(costParagraph);
            budgetComponentRow.Cells.Add(costCell);

            tableRowGroup.Rows.Add(budgetComponentRow);

            table.RowGroups.Add(tableRowGroup);

            blockCollection.Add(table);

            foreach (TPlannedSubSpecialityHolderPresenter plannedSubSpecialityHolderPresenter in item.PlannedSubSpecialityHolders.Items)
            {
                ShowSubSpecialityRow <TComponent,TPlannedSubSpecialityHolderPresenter > (blockCollection,plannedSubSpecialityHolderPresenter);
            }

        }

        private  void ShowSubSpecialityRow<TComponent,TPlannedSubSpecialityHolderPresenter>(BlockCollection blockCollection, TPlannedSubSpecialityHolderPresenter item)
            where TComponent : class, IBudgetComponent
            where TPlannedSubSpecialityHolderPresenter : class, IPlannedSubSpecialityHolderPresenter<TComponent>
        {
            Table table = new Table(){Margin = new Thickness(0,0,0,0)};

            table.Columns.Add(new TableColumn() { Name = "CodeColumn", Width = new GridLength(80, GridUnitType.Pixel) });
            table.Columns.Add(new TableColumn() { Name = "NameColumn" });
            table.Columns.Add(new TableColumn() { Name = "UMColumn", Width = new GridLength(40, GridUnitType.Pixel) });
            table.Columns.Add(new TableColumn() { Name = "QuantityColumn", Width = new GridLength(60, GridUnitType.Pixel) });
            table.Columns.Add(new TableColumn() { Name = "CurrencyColumn", Width = new GridLength(40, GridUnitType.Pixel) });
            table.Columns.Add(new TableColumn() { Name = "PriceColumn", Width = new GridLength(50, GridUnitType.Pixel) });
            table.Columns.Add(new TableColumn() { Name = "CostColumn", Width = new GridLength(80, GridUnitType.Pixel) });

            TableRowGroup tableRowGroup = new TableRowGroup();
            tableRowGroup.FontWeight = FontWeights.Light;

            TableRow subspecialityRow = new TableRow();

            TableCell codeCell = new TableCell();
            Run codeText = new Run();
            codeText.Text = item.Code.ToString();
            // Add three parts of sentence to a paragraph, in order.
            Paragraph codeParagraph = new Paragraph();
            codeParagraph.Inlines.Add(codeText);
            codeCell.Blocks.Add(codeParagraph);
            subspecialityRow.Cells.Add(codeCell);

            TableCell nameCell = new TableCell();

            Run nameText = new Run();
            nameText.Text = item.Name;
            // Add three parts of sentence to a paragraph, in order.
            Paragraph nameParagraph = new Paragraph();
            nameParagraph.Inlines.Add(nameText);
            nameCell.Blocks.Add(nameParagraph);
            subspecialityRow.Cells.Add(nameCell);

            TableCell umCell = new TableCell();
            Run umText = new Run();
            umText.Text = "";
            // Add three parts of sentence to a paragraph, in order.
            Paragraph umParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
            umParagraph.Inlines.Add(umText);
            umCell.Blocks.Add(umParagraph);
            subspecialityRow.Cells.Add(umCell);

            TableCell quantityCell = new TableCell();

            Run quantityText = new Run();
            quantityText.Text = "";
            // Add three parts of sentence to a paragraph, in order.
            Paragraph quantityParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
            quantityParagraph.Inlines.Add(quantityText);
            quantityCell.Blocks.Add(quantityParagraph);
            subspecialityRow.Cells.Add(quantityCell);

            TableCell currencyCell = new TableCell();
            Run currencyText = new Run();
            currencyText.Text = "";
            // Add three parts of sentence to a paragraph, in order.
            Paragraph currencyParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
            currencyParagraph.Inlines.Add(currencyText);
            currencyCell.Blocks.Add(currencyParagraph);
            subspecialityRow.Cells.Add(currencyCell);

            TableCell priceCell = new TableCell();
            Run priceText = new Run();
            priceText.Text = "";
            // Add three parts of sentence to a paragraph, in order.
            Paragraph priceParagraph = new Paragraph() { TextAlignment = TextAlignment.Right };
            priceParagraph.Inlines.Add(priceText);
            priceCell.Blocks.Add(priceParagraph);
            subspecialityRow.Cells.Add(priceCell);

            TableCell costCell = new TableCell();
            Run costText = new Run();
            costText.Text = item.Cost.ToString();
            // Add three parts of sentence to a paragraph, in order.
            Paragraph costParagraph = new Paragraph() {TextAlignment = TextAlignment.Right};
            costParagraph.Inlines.Add(costText);
            costCell.Blocks.Add(costParagraph);
            subspecialityRow.Cells.Add(costCell);

            tableRowGroup.Rows.Add(subspecialityRow);

            foreach (IPlannedActivityPresenter plannedActivityPresenter in item.PlannedActivities.Items)
            {
                ShowBudgetComponentItemRow(tableRowGroup,plannedActivityPresenter);
            }

            table.RowGroups.Add(tableRowGroup);
            blockCollection.Add(table);
        }

      

        private void ShowBudgetComponentItemRow<TItem>(TableRowGroup tableRowGroup, IBudgetComponentItemPresenter<TItem> budgetComponentItemPresenter)
            where TItem:class ,IBudgetComponentItem
        {
            TableRow budgetComponentItemRow = new TableRow();

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

            TableCell umCell = new TableCell();
            Run umText = new Run();
            umText.Text = budgetComponentItemPresenter.MeasurementUnit?.Letters;
            // Add three parts of sentence to a paragraph, in order.
            Paragraph umParagraph = new Paragraph() {TextAlignment = TextAlignment.Right};
            umParagraph.Inlines.Add(umText);
            umCell.Blocks.Add(umParagraph);
            budgetComponentItemRow.Cells.Add(umCell);

            TableCell quantityCell = new TableCell();

            Run quantityText = new Run();
            quantityText.Text = budgetComponentItemPresenter.Quantity.ToString();
            // Add three parts of sentence to a paragraph, in order.
            Paragraph quantityParagraph = new Paragraph() {TextAlignment = TextAlignment.Right};
            quantityParagraph.Inlines.Add(quantityText);
            quantityCell.Blocks.Add(quantityParagraph);
            budgetComponentItemRow.Cells.Add(quantityCell);

            TableCell currencyCell = new TableCell();
            Run currencyText = new Run();
            currencyText.Text = budgetComponentItemPresenter.Currency?.Letters;
            // Add three parts of sentence to a paragraph, in order.
            Paragraph currencyParagraph = new Paragraph() {TextAlignment = TextAlignment.Right};
            currencyParagraph.Inlines.Add(currencyText);
            currencyCell.Blocks.Add(currencyParagraph);
            budgetComponentItemRow.Cells.Add(currencyCell);

            TableCell priceCell = new TableCell();
            Run priceText = new Run();
            priceText.Text = budgetComponentItemPresenter.UnitaryCost.ToString();
            // Add three parts of sentence to a paragraph, in order.
            Paragraph priceParagraph = new Paragraph() {TextAlignment = TextAlignment.Right};
            priceParagraph.Inlines.Add(priceText);
            priceCell.Blocks.Add(priceParagraph);
            budgetComponentItemRow.Cells.Add(priceCell);


            TableCell costCell = new TableCell();
            Run costText = new Run();
            costText.Text = budgetComponentItemPresenter.Cost.ToString();
            // Add three parts of sentence to a paragraph, in order.
            Paragraph costParagraph = new Paragraph() {TextAlignment = TextAlignment.Right};
            costParagraph.Inlines.Add(costText);
            costCell.Blocks.Add(costParagraph);
            budgetComponentItemRow.Cells.Add(costCell);

            tableRowGroup.Rows.Add(budgetComponentItemRow);

            foreach (IPlannedResourcePresenter<TItem> plannedResourcePresenter in budgetComponentItemPresenter.PlannedResources.Items)
            {
                ShowBudgetComponentItemRow(tableRowGroup, plannedResourcePresenter);
            }

        }

        private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Thread thread = new Thread(LoadDocument);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }

        private IStatusBarServices _statusBarServices;

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

        private void DoPrint()
        {
            // Get the dispatcher from the current window, and use it to invoke
            // the update code.
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
            (ThreadStart) delegate()
                {
                    PrintDialog printDialog = new PrintDialog();
                    if (printDialog.ShowDialog() == true)
                    {
                        //Notify status bar
                        //StatusBarServices.SignalText(Properties.Resources.PrintingWait);

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
                        ConvinientReportPaginator investmentFullReportPaginator =
                            new ConvinientReportPaginator(FlowDocument.Blocks, new Typeface("Calibri"), 12, 48 * 0.75,
                                new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight),
                                MyDocumentPageViewer.ActualWidth, Properties.Resources.Detailed);

                        // Reapply the old settings.
                        //FlowDocument.PageHeight = pageHeight;
                        //FlowDocument.PageWidth = pageWidth;
                        //FlowDocument.PagePadding = pagePadding;
                        //FlowDocument.ColumnGap = columnGap;
                        //FlowDocument.ColumnWidth = columnWidth;
                    }
                });
           
        }

        private void InvestmentFullReport_OnLoaded(object sender, RoutedEventArgs e)
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
    }
}
