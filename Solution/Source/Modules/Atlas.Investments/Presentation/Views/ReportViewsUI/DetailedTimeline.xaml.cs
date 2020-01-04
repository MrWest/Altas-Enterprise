using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Reporting;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Features;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.Equipment;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Documents;

namespace CompanyName.Atlas.Investments.Presentation.Views.ReportViewsUI
{
    /// <summary>
    /// Interaction logic for DetailedTimeline.xaml
    /// </summary>
    public partial class DetailedTimeline : UserControl, IPrinttableContainer
    {
      
        /// <summary>
        /// Identifies the SelectedSource dependency property.
        /// </summary>
        public static readonly DependencyProperty InDeepNavigableListProperty = DependencyProperty.Register("InDeepNavigableList", typeof(IList<ITreeNode>), typeof(DetailedTimeline), new PropertyMetadata(null));
        protected readonly ResourceDictionary GenericResources = new ResourceDictionary
        {
            Source = new Uri("/Atlas.UIControls;component/Themes/AtlasTimelineGrid.xaml", UriKind.RelativeOrAbsolute)
        };

        protected readonly ResourceDictionary ColorResources = new ResourceDictionary
        {
            Source = new Uri("/Atlas.UIControls;component/Assets/GaussChart.xaml", UriKind.RelativeOrAbsolute)
        };
        private static readonly Random SeedRandom = new Random();

        private readonly Random _random = new Random(SeedRandom.Next(int.MaxValue));
        private IPeriod _innerperiods;
        public DetailedTimeline()
        {
            InitializeComponent();
            IsVisibleChanged += UIElement_OnIsVisibleChanged;
            DataContextChanged += FrameworkElement_OnDataContextChanged;
            InDeepNavigableList = new List<ITreeNode>();
        }
        private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var investmentElement = DataContext as IInvestmentElementPresenter;
            if (((FrameworkElement)sender).IsVisible && investmentElement != null)
            {
                GenerateTimeline(investmentElement);
            }
        }

        private  void GenerateTimeline(IInvestmentElementPresenter investmentElement)
        {
            FlowDocument.Blocks.Clear();

            Table table = new Table() {Margin = new Thickness(0, 0, 0, 0), CellSpacing = 0};

            table.Columns.Add(new TableColumn() {Name = "NameColumn", Width = new GridLength(240, GridUnitType.Pixel)});
            table.Columns.Add(new TableColumn() {Name = "TimeLineColumn"});

            TableRowGroup headerRowGroup = new TableRowGroup();
            //headerRowGroup.Background = Brushes.DimGray;
            
            //EasyTimeNode easyTimeNode = new EasyTimeNode();
            TableRow headerRow = new TableRow();
            
            TableCell nameCell = new TableCell() {Padding = new Thickness(0,0,3,0)};
            //nameCell.Foreground = Brushes.White;
            nameCell.TextAlignment = TextAlignment.Left;
            Run codeText = new Run();
            codeText.Text = investmentElement.Name;
            codeText.BaselineAlignment = BaselineAlignment.Baseline;
            // Add three parts of sentence to a paragraph, in order.
            Paragraph codeParagraph = new Paragraph();
            codeParagraph.TextAlignment = TextAlignment.Left;
            codeParagraph.FontWeight = FontWeights.Normal;
            codeParagraph.FontSize = 8;
            codeParagraph.Inlines.Add(codeText);

            Grid nameheaderGrid = new Grid();

            nameheaderGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(35, GridUnitType.Star) });
            nameheaderGrid.RowDefinitions.Add(new RowDefinition());
            nameheaderGrid.RowDefinitions.Add(new RowDefinition());

            var nametopbody = new Border
            {
                Style = (Style)GenericResources["TimelineColumnBodyStyle"],
                BorderThickness = new Thickness { Bottom = 0, Top = 0, Left = 0, Right = 0 },
                Background = new SolidColorBrush(Color.FromRgb(25, 25, 25)),
                Height = 35

            };

            Grid.SetColumn(nametopbody, 0);

            Grid.SetRow(nametopbody, 0);

            nameheaderGrid.Children.Add(nametopbody);

            var namebottombody = new Border
            {
                Style = (Style)GenericResources["TimelineColumnBodyStyle"],
                Padding = new Thickness { Bottom = 0, Top = 0, Left = 10, Right = 0 },
                BorderThickness = new Thickness { Bottom = 0, Top = 0, Left = 0, Right = 0 },
                Background = new SolidColorBrush(Color.FromRgb(175, 175, 175)),
                Height = 35,
                

            };

            var tittle = new TextBlock() { Text = Properties.Resources.Name, VerticalAlignment = VerticalAlignment.Center };

            namebottombody.Child = tittle;

            Grid.SetColumn(namebottombody, 0);

            Grid.SetRow(namebottombody, 1);

            nameheaderGrid.Children.Add(namebottombody);

            BlockUIContainer headerblockUiContainer = new BlockUIContainer();
            headerblockUiContainer.Child = nameheaderGrid;

            nameCell.Blocks.Add(headerblockUiContainer);



            headerRow.Cells.Add(nameCell);


            TableCell timelineCell = new TableCell();

            BlockUIContainer blockUiContainer = new BlockUIContainer();

           // AtlasTimelineHeaderGrid atlasTimelineHeaderGrid = new AtlasTimelineHeaderGrid();
            //atlasTimelineHeaderGrid.LifelineNode = investmentElement;
            //atlasTimelineHeaderGrid.Starts = investmentElement.Start;
            //atlasTimelineHeaderGrid.Ends = investmentElement.End;
            //atlasTimelineHeaderGrid.Scale = DateTimeScale.Yearly;
            //blockUiContainer.Child = atlasTimelineHeaderGrid;
            //atlasTimelineHeaderGrid.UpdateTimeline();

            Grid headerGrid = new Grid();
            
            var period = ServiceLocator.Current.GetInstance<IPeriod>();
            
            period.Starts = investmentElement.Start;
            period.PeriodKind = investmentElement.Period.Object.PeriodKind;
            period.Ends = investmentElement.End;
            int count = 0;

            _innerperiods = ServiceLocator.Current.GetInstance<IPeriod>();

            _innerperiods.Starts = period.Starts;
            _innerperiods.Ends = period.Ends;
            _innerperiods.PeriodKind = ReduceDateTimeScale(period.PeriodKind);

            foreach (IPeriod periodPeriod in _innerperiods.Periods)
                headerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(35, GridUnitType.Star), MinWidth = 35 * 2 });
            foreach (IPeriod periodPeriod in period.Periods)
            {
               
                var body = new Border
                {
                    Style = (Style)GenericResources["TimelineColumnBodyStyle"],
                    BorderThickness = new Thickness { Bottom = 0, Top = 0, Left = count == 0 ? 0 : 1, Right = 0 },
                    Background = new SolidColorBrush(Color.FromRgb(25, 25, 25)), Height = 35

                 };

                var text = new TextBlock() { Text = periodPeriod.Name, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 3, 0, 3), Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)) };

                body.Child = text;
                var monthsIndex = _innerperiods.Periods.Count(x => x.Starts >= periodPeriod.Starts && x.Ends < periodPeriod.Ends);
                monthsIndex++;
                Grid.SetColumnSpan(body, monthsIndex);
                Grid.SetColumn(body, count);

                headerGrid.Children.Add(body);

                count += monthsIndex;
            }

           

            Grid headerGrid2 = new Grid();
            count = 0;
            foreach (IPeriod periodPeriod in _innerperiods.Periods)
            {
                //if ((count == 0 || count == _innerperiods.Periods.Count - 1) && ActualWidth>0)
                //{
                //    double columnWidth = (ActualWidth-140/_innerperiods.Days) * (DateTime.DaysInMonth(_innerperiods.Starts.Year,_innerperiods.Starts.Month) -_innerperiods.Starts.Day);
                //    headerGrid2.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(columnWidth) });
                //}
                //else
                var body = new Border
                {
                    Style = (Style)GenericResources["TimelineColumnBodyStyle"],
                    BorderThickness = new Thickness { Bottom = 0, Top = 0, Left = count == 0 ? 0 : 1, Right = 0 },
                    Background = new SolidColorBrush(Color.FromRgb(175, 175, 175)),
                    Height = 35
                };
                headerGrid2.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(35,GridUnitType.Star), MinWidth = 35 * 2 });

                var text = new TextBlock() { Text = periodPeriod.Name, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 0, 3) };
                body.Child = text;
                Grid.SetColumn(body, count);
              
                headerGrid2.Children.Add(body);

                count++;
            }



            Grid containerGrid = new Grid();
            containerGrid.RowDefinitions.Add(new RowDefinition());
            containerGrid.RowDefinitions.Add(new RowDefinition());

            Grid.SetRow(headerGrid2, 1);

            containerGrid.Children.Add(headerGrid);
            containerGrid.Children.Add(headerGrid2);

            blockUiContainer.Child = containerGrid;
            timelineCell.Blocks.Add(blockUiContainer);
            headerRow.Cells.Add(timelineCell);

            headerRowGroup.Rows.Add(headerRow);
            table.RowGroups.Add(headerRowGroup);


           

           
            FlowDocument.Blocks.Add(table);
            

            Table  contentTable = new Table();

            contentTable.Columns.Add(new TableColumn() { Name = "NameColumn", Width = new GridLength(240, GridUnitType.Pixel) });
            contentTable.Columns.Add(new TableColumn() { Name = "TimeLineColumn" });

            TableRowGroup tableRowGroup = new TableRowGroup();

            tableRowGroup.FontWeight = FontWeights.Light;
            tableRowGroup.FontSize = 10;

            ShowInvestmentElementTimeLine(tableRowGroup, investmentElement);

            contentTable.RowGroups.Add(tableRowGroup);

            ScrollViewer scrollViewer = new ScrollViewer() {HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled, VerticalScrollBarVisibility = ScrollBarVisibility.Visible};

            //scrollViewer.Content = contentTable;
           
            BlockUIContainer _finalblockUiContainer = new BlockUIContainer();

            //_finalblockUiContainer.Child = scrollViewer;

            

            FlowDocument.Blocks.Add(contentTable);
        }
        private  DateTimeScale ReduceDateTimeScale(DateTimeScale scale)
        {
            switch (scale)
            {
                case DateTimeScale.Yearly:
                    return DateTimeScale.Monthly;
                case DateTimeScale.Monthly:
                    return DateTimeScale.Daily;
            }

            return DateTimeScale.Weekly;
        }
        private void ShowInvestmentElementTimeLine(TableRowGroup tableRowGroup,IInvestmentElementPresenter investmentElement)
        {
            ShowBudgetTimelIne(tableRowGroup, investmentElement.Budget.EquipmentComponent);
            ShowBudgetTimelIne(tableRowGroup, investmentElement.Budget.ConstructionComponent);
            ShowBudgetTimelIne(tableRowGroup, investmentElement.Budget.OtherExpensesComponent);
            ShowBudgetTimelIne(tableRowGroup, investmentElement.Budget.WorkCapitalComponent);

            foreach (IInvestmentElementPresenter investmentElementPresenter in investmentElement.Items.Items)
            {
                //EasyTimeNode easyTimeNode = new EasyTimeNode();
                TableRow treenodeRow = new TableRow();

                TableCell nameCell = new TableCell() { Padding = new Thickness(0, 0, 3, 0), TextAlignment = TextAlignment.Justify };
                Run codeText = new Run() { BaselineAlignment = BaselineAlignment.Center };
                codeText.Text = investmentElementPresenter.Name;
                // Add three parts of sentence to a paragraph, in order.
                Paragraph codeParagraph = new Paragraph();
                codeParagraph.Margin = new Thickness(3, 3, 3, 3);
                codeParagraph.FontSize = 10;
                codeParagraph.FontFamily = FontFamily;
                codeParagraph.FontWeight = FontWeights.SemiBold;
                codeParagraph.Inlines.Add(codeText);
                nameCell.Blocks.Add(codeParagraph);
                treenodeRow.Cells.Add(nameCell);

                TableCell timelineCell = new TableCell();

                BlockUIContainer blockUiContainer = new BlockUIContainer();


                Grid contentGrid = new Grid();

                Grid timelineGrid = new Grid(); // generating the backgrund grid.

                int count = 0;
                foreach (IPeriod periodPeriod in _innerperiods.Periods)
                {
                   
                        //if ((count == 0 || count == _innerperiods.Periods.Count - 1) && ActualWidth > 0)
                        //{
                        //    double columnWidth = (ActualWidth - 140 / _innerperiods.Days) * (DateTime.DaysInMonth(_innerperiods.Starts.Year, _innerperiods.Starts.Month) - _innerperiods.Starts.Day);
                        //    timelineGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(columnWidth) });
                        //}
                        //else
                            timelineGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(35,GridUnitType.Star), MinWidth = 35*2});

                   var body = new Border
                    {
                        Style = (Style)GenericResources["TimelineColumnBodyStyle"],
                        BorderThickness = new Thickness { Bottom = 0, Top = 0, Left = count == 0 ? 0 : 1, Right = 0 }
                    };
                    Grid.SetColumn(body, count);
                   
                    timelineGrid.Children.Add(body);

                    count++;
                }


                AnotherEasyTimeline easyTimeLine = new AnotherEasyTimeline();
                if (this.ActualWidth > 240 + 10)
                    easyTimeLine.ReferenceWidth = this.ActualWidth - 240;
                var collection = (ObservableCollection<ResourceDictionary>)ColorResources["Gradients2"];

                if (collection != null)
                {
                    int index = _random.Next(0, collection.Count);

                    easyTimeLine.Background = ((LinearGradientBrush)((ResourceDictionary)collection[index])["Brush" + (index + 1).ToString()]);
                }
                else
                    easyTimeLine.Background = Brushes.Gray;
                
                easyTimeLine.Opacity = 0.5;
                easyTimeLine.TreeNode = investmentElementPresenter;
                easyTimeLine.Loaded += EasyTimeLineOnLoaded;


                contentGrid.Children.Add(timelineGrid);
                contentGrid.Children.Add(easyTimeLine);

                blockUiContainer.Child = contentGrid;
                timelineCell.Blocks.Add(blockUiContainer);
                treenodeRow.Cells.Add(timelineCell);

                tableRowGroup.Rows.Add(treenodeRow);

                ShowInvestmentElementTimeLine(tableRowGroup,investmentElementPresenter);
            }

        }

        private void ShowBudgetTimelIne<TComponent, TPlannedSubSpecialityHolders, TPlannedSubSpecialityHolderPresenter, TExecutedSubSpecialityHolders, TExecutedSubSpecialityHolderPresenter>(TableRowGroup tableRowGroup, IBudgetComponentPresenter<TComponent, TPlannedSubSpecialityHolders, TPlannedSubSpecialityHolderPresenter, TExecutedSubSpecialityHolders, TExecutedSubSpecialityHolderPresenter> budgetComponent)
        where TComponent : class, IBudgetComponent
        where TPlannedSubSpecialityHolders : class, IPlannedSubSpecialityHolderViewModel<TComponent, TPlannedSubSpecialityHolderPresenter>
        where TPlannedSubSpecialityHolderPresenter : class, IPlannedSubSpecialityHolderPresenter<TComponent>
        where TExecutedSubSpecialityHolders : class, IExecutedSubSpecialityHolderViewModel<TComponent, TExecutedSubSpecialityHolderPresenter>
        where TExecutedSubSpecialityHolderPresenter : class, IExecutedSubSpecialityHolderPresenter<TComponent>
        {
            foreach (TPlannedSubSpecialityHolderPresenter equipmentComponentPlannedSubSpecialityHolder in
                budgetComponent.PlannedSubSpecialityHolders)
            {
                //EasyTimeNode easyTimeNode = new EasyTimeNode();
                TableRow treenodeRow = new TableRow();

                TableCell nameCell = new TableCell() { Padding = new Thickness(0, 0, 3, 0), TextAlignment = TextAlignment.Justify };
                Run codeText = new Run() { BaselineAlignment = BaselineAlignment.Center };
                codeText.Text = equipmentComponentPlannedSubSpecialityHolder.Name;
                // Add three parts of sentence to a paragraph, in order.
                Paragraph codeParagraph = new Paragraph();
                codeParagraph.Margin = new Thickness(3, 3, 3, 3);
                codeParagraph.FontSize = 10;
                codeParagraph.FontWeight = FontWeights.SemiBold;
                codeParagraph.FontFamily = FontFamily;
                codeParagraph.Inlines.Add(codeText);
                nameCell.Blocks.Add(codeParagraph);
                treenodeRow.Cells.Add(nameCell);

                TableCell timelineCell = new TableCell();

                BlockUIContainer blockUiContainer = new BlockUIContainer();
                Grid contentGrid = new Grid();

                Grid timelineGrid = new Grid(); // generating the backgrund grid.

                int count = 0;
                foreach (IPeriod periodPeriod in _innerperiods.Periods)
                {
                    //if ((count == 0 || count == _innerperiods.Periods.Count - 1) && ActualWidth > 0)
                    //{
                    //    double columnWidth = (ActualWidth - 140 / _innerperiods.Days) * (DateTime.DaysInMonth(_innerperiods.Starts.Year, _innerperiods.Starts.Month) - _innerperiods.Starts.Day);
                    //    timelineGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(columnWidth) });
                    //}
                    //else
                        timelineGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(35,GridUnitType.Star), MinWidth = 35 * 2 });

                    var body = new Border
                    {
                        Style = (Style)GenericResources["TimelineColumnBodyStyle"],
                        BorderThickness = new Thickness { Bottom = 0, Top = 0, Left = count == 0 ? 0 : 1, Right = 0 }
                    };
                    Grid.SetColumn(body, count);

                    timelineGrid.Children.Add(body);

                    count++;
                }


                AnotherEasyTimeline easyTimeLine = new AnotherEasyTimeline();
                if (this.ActualWidth > 240+10)
                    easyTimeLine.ReferenceWidth = this.ActualWidth - 240;
                var collection = (ObservableCollection<ResourceDictionary>)ColorResources["Gradients2"];

                if (collection != null)
                {
                    int index = _random.Next(0, collection.Count);

                    easyTimeLine.Background = ((LinearGradientBrush)((ResourceDictionary)collection[index])["Brush"+(index+1).ToString()]);
                }
                else
                    easyTimeLine.Background = Brushes.Gray;
                
                easyTimeLine.Opacity = 0.75;
                easyTimeLine.TreeNode = equipmentComponentPlannedSubSpecialityHolder;
                easyTimeLine.Loaded += EasyTimeLineOnLoaded;


                contentGrid.Children.Add(timelineGrid);
                contentGrid.Children.Add(easyTimeLine);

                blockUiContainer.Child = contentGrid;

                timelineCell.Blocks.Add(blockUiContainer);
                treenodeRow.Cells.Add(timelineCell);

                tableRowGroup.Rows.Add(treenodeRow);

                foreach (IPlannedActivityPresenter plannedActivityPresenter in
                    equipmentComponentPlannedSubSpecialityHolder.PlannedActivities)
                {
                    //EasyTimeNode easyTimeNode = new EasyTimeNode();
                    TableRow treenodeRow2 = new TableRow();

                    TableCell nameCell2 = new TableCell() { Padding = new Thickness(0, 0, 3, 0), TextAlignment = TextAlignment.Justify };
                    Run codeText2 = new Run() { BaselineAlignment = BaselineAlignment.Center };
                    codeText2.Text = plannedActivityPresenter.Name;
                    // Add three parts of sentence to a paragraph, in order.
                    Paragraph codeParagraph2 = new Paragraph();
                    codeParagraph2.Margin = new Thickness(3, 3, 3, 3);
                    codeParagraph2.FontSize = 8;
                    codeParagraph.FontFamily = FontFamily;
                    codeParagraph2.Inlines.Add(codeText2);
                    nameCell2.Blocks.Add(codeParagraph2);
                    treenodeRow2.Cells.Add(nameCell2);

                    TableCell timelineCell2 = new TableCell();

                    BlockUIContainer blockUiContainer2 = new BlockUIContainer();

                    Grid contentGrid2 = new Grid();

                    Grid timelineGrid2 = new Grid(); // generating the backgrund grid.

                    int count2 = 0;
                    foreach (IPeriod periodPeriod in _innerperiods.Periods)
                    {
                        //    if ((count2 == 0 || count2 == _innerperiods.Periods.Count - 1) && ActualWidth > 0)
                        //    {
                        //        double columnWidth = (ActualWidth - 140 / _innerperiods.Days) * (DateTime.DaysInMonth(_innerperiods.Starts.Year, _innerperiods.Starts.Month) - _innerperiods.Starts.Day);
                        //        timelineGrid2.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(columnWidth) });
                        //    }
                        //    else
                        timelineGrid2.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(35,GridUnitType.Star), MinWidth = 35 * 2 });


                        var body = new Border
                        {
                            Style = (Style)GenericResources["TimelineColumnBodyStyle"],
                            BorderThickness = new Thickness { Bottom = 0, Top = 0, Left = count2 == 0 ? 0 : 1, Right = 0 }
                        };
                        Grid.SetColumn(body, count2);

                        timelineGrid2.Children.Add(body);

                        count2++;
                    }


                    AnotherEasyTimeline easyTimeLine2 = new AnotherEasyTimeline();
                    if (this.ActualWidth > 240 + 10)
                        easyTimeLine2.ReferenceWidth = this.ActualWidth - 240;
                    //var collection = (ObservableCollection<ResourceDictionary>)ColorResources["Gradients2"];

                    if (collection != null)
                    {
                        int index = _random.Next(0, collection.Count);

                        easyTimeLine2.Background = ((LinearGradientBrush)((ResourceDictionary)collection[index])["Brush" + (index + 1).ToString()]);
                    }
                    else
                        easyTimeLine2.Background = Brushes.Gray;
                    
                    easyTimeLine2.Opacity = 0.92;
                    easyTimeLine2.TreeNode = plannedActivityPresenter;
                    easyTimeLine2.Loaded += EasyTimeLineOnLoaded;


                    contentGrid2.Children.Add(timelineGrid2);
                    contentGrid2.Children.Add(easyTimeLine2);

                    blockUiContainer2.Child = contentGrid2;

                    timelineCell2.Blocks.Add(blockUiContainer2);
                    treenodeRow2.Cells.Add(timelineCell2);
                    tableRowGroup.Rows.Add(treenodeRow2);
                }
            }
        }

        private void EasyTimeLineOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (((EasyTimeLine)sender).TreeNode != null && (DataContext as ITreeNode)!=null)
            {
                var period = new Period();

                var globalstartDate = (DataContext as ITreeNode).Start;
                var globalendDate = (DataContext as ITreeNode).End;

                globalstartDate = globalstartDate.AddDays((globalstartDate.Day - 1) * -1);
                globalendDate =
                    globalendDate.AddDays(DateTime.DaysInMonth(globalendDate.Year, globalendDate.Month) -
                                            globalendDate.Day);
                period.Starts =globalstartDate;
                period.Ends = globalendDate;
                double pixels = 0;
                if(ActualWidth>240)
                 pixels = (ActualWidth-240) / period.Days;
                //if (((EasyTimeLine)sender).Parent as FrameworkElement != null)
                //    pixels = (((EasyTimeLine)sender).Parent as FrameworkElement).ActualWidth / period.Days;

                //period.Starts = (DataContext as ITreeNode).Start;
                period.Ends = ((EasyTimeLine)sender).TreeNode.Start;

                double left = pixels * (period.Days - 1);

                period.Starts = ((EasyTimeLine)sender).TreeNode.End;
                period.Ends = globalendDate;

                double right = pixels * (period.Days - 1);

                //period.Starts = (DataContext as ITreeNode).Start;
                //period.Ends = (DataContext as ITreeNode).End;

                period.Starts = ((EasyTimeLine)sender).TreeNode.Start;
                period.Ends = ((EasyTimeLine)sender).TreeNode.End;

                ((EasyTimeLine)sender).Width = pixels * period.Days;

                ((EasyTimeLine)sender).Margin = new Thickness(left, 1, right, 0);

                ((EasyTimeLine)sender).ToolTip = ((EasyTimeLine)sender).TreeNode.Name + "  " + ((EasyTimeLine)sender).TreeNode.Start.ToShortDateString() + " - " + ((EasyTimeLine)sender).TreeNode.End.ToShortDateString();


            }
        }

        private void UIElement_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var investmentElement = DataContext as IInvestmentElementPresenter;
            if (((FrameworkElement)sender).IsVisible && investmentElement != null)
            {
                GenerateTimeline(investmentElement);
            }
        }

        public IList<ITreeNode> InDeepNavigableList
        {
            get { return (IList<ITreeNode>)GetValue(InDeepNavigableListProperty); }
            set { SetValue(InDeepNavigableListProperty, value); }

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
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                //Notify status bar
                StatusBarServices.SignalText(Properties.Resources.PrintingWait);


                // Save all the existing settings.
                double pageHeight = FlowDocument.PageHeight;
                double pageWidth = FlowDocument.PageWidth;
                Thickness pagePadding = FlowDocument.PagePadding;
                double columnGap = FlowDocument.ColumnGap;
                double columnWidth = FlowDocument.ColumnWidth;
                // Make the FlowDocument page match the printed page.

                FlowDocument.PageHeight = printDialog.PrintableAreaHeight;
                FlowDocument.PageWidth = printDialog.PrintableAreaWidth;
                FlowDocument.PagePadding = new Thickness(50);
                printDialog.PrintDocument(
                ((IDocumentPaginatorSource)MyDocumentPageViewer.Document).DocumentPaginator,
                "A Flow Document");
                // Reapply the old settings.
                FlowDocument.PageHeight = pageHeight;
                FlowDocument.PageWidth = pageWidth;
                FlowDocument.PagePadding = pagePadding;
                FlowDocument.ColumnGap = columnGap;
                FlowDocument.ColumnWidth = columnWidth;
            }
        }

        private void DetailedTimeline_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var investmentElement = DataContext as IInvestmentElementPresenter;
            if (((FrameworkElement)sender).IsVisible && investmentElement != null)
            {
                GenerateTimeline(investmentElement);
            }
        }
    }
}
