using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using CompanyName.Atlas.UIControls.Converters;
using Microsoft.Practices.Prism.Commands;
using CompanyName.Atlas.Contracts.Presentation.Common;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    ///     This is the control offering the chances to plot visually the representation of the time distribution of intervals
    ///     or interval-like elements.
    /// </summary>
    public abstract class AtlasTimelineGrid : Grid
    {
        /// <summary>
        ///     Dependency property used to containing the timeline item being represented in instances of
        ///     <see cref="AtlasTimelineGrid" />.
        /// </summary>
        public static readonly DependencyProperty LifelineNodeProperty = DependencyProperty.Register("LifelineNode", typeof(ITreeNode), typeof(AtlasTimelineGrid), new PropertyMetadata(AtlasTimelineGrid_OnIntervalNodeChanged));

        /// <summary>
        ///     Dependency property containing the datetime scale used in <see cref="AtlasTimelineGrid" /> instances.
        /// </summary>
        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register("Scale", typeof(DateTimeScale), typeof(AtlasTimelineGrid), new PropertyMetadata(default(DateTimeScale), AtlasTimelineGrid_OnScaleChanged));

        /// <summary>
        ///     Dependency property used to contain the value of the zoom factor in instances of <see cref="AtlasTimelineGrid" />.
        /// </summary>
        public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register("Zoom", typeof(double), typeof(AtlasTimelineGrid), new PropertyMetadata(1.0));

        /// <summary>
        ///     Dependency used to contain the value of the minimum width of the columns in instances of
        ///     <see cref="AtlasTimelineGrid" />.
        /// </summary>
        public static readonly DependencyProperty ColumnWidthProperty = DependencyProperty.Register("ColumnWidth", typeof(double), typeof(AtlasTimelineGrid), new PropertyMetadata(150.0));

        /// <summary>
        ///     Dependency property containing the value being the variantion suffered by the zoom when zooming, that is, the value
        ///     used to decrement or increment the zoom value of instances of <see cref="AtlasTimelineGrid" />.
        /// </summary>
        public static readonly DependencyProperty ZoomFactorProperty = DependencyProperty.Register("ZoomFactor", typeof(double), typeof(AtlasTimelineGrid), new PropertyMetadata(0.2));

        /// <summary>
        ///     Dependency property contaning a value representing the date time scale instances of <see cref="AtlasTimeline" />
        ///     are representing.
        /// </summary>
        public static readonly DependencyProperty StartsProperty = DependencyProperty.Register("Starts", typeof(DateTime), typeof(AtlasTimelineGrid), new PropertyMetadata(DateTime.Today, DatePropertyChangedCallback));



        /// <summary>
        ///     Dependency property contaning a value representing the date time scale instances of <see cref="AtlasTimeline" />
        ///     are representing.
        /// </summary>
        public static readonly DependencyProperty EndsProperty = DependencyProperty.Register("Ends", typeof(DateTime), typeof(AtlasTimelineGrid), new PropertyMetadata(DateTime.Today, DatePropertyChangedCallback));


        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty IsRootProperty = DependencyProperty.Register("IsRoot", typeof(bool), typeof(AtlasTimelineGrid), new PropertyMetadata(true));

        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty ZoomInCommandProperty = DependencyProperty.Register("ZoomInCommand", typeof(ICommand), typeof(AtlasTimelineGrid), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty ZoomOutCommandProperty = DependencyProperty.Register("ZoomOutCommand", typeof(ICommand), typeof(AtlasTimelineGrid), new PropertyMetadata(null));



        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public bool IsRoot
        {
            get { return (bool)GetValue(IsRootProperty); }
            set { SetValue(IsRootProperty, value); }
        }
        // TODO: Remove
        //private static LifelineTreeNode _node;

        /// <summary>
        ///     This is the generic resources dictionary in the current assembly.
        /// </summary>
        protected readonly ResourceDictionary GenericResources = new ResourceDictionary
        {
            Source = new Uri("/Atlas.UIControls;component/Themes/AtlasTimelineGrid.xaml", UriKind.RelativeOrAbsolute)
        };


        /// <summary>
        ///     Initializes a new instance of the <see cref="AtlasTimelineGrid" />
        /// </summary>
        protected AtlasTimelineGrid()
        {
            DefaultStyleKey = typeof(AtlasTimelineGrid);

            ZoomInCommand = new DelegateCommand<double?>(ZoomInCommand_Executed, ZoomInCommand_CanExecute);
            ZoomOutCommand = new DelegateCommand<double?>(ZoomOutCommand_Executed, ZoomOutCommand_CanExecute);


            // TODO: Remove
            //  InitializeNode();

            UseLayoutRounding = SnapsToDevicePixels = true;
            IsEnabledChanged += OnIsEnabledChanged;
            //Loaded+=OnLoaded;

        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            UpdateTimeline();
        }

        private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            UpdateTimeline();
        }

        /// <summary>
        ///     Gets or sets the date time scale the current <see cref="AtlasTimeline" /> is representing.
        /// </summary>
        public DateTime Starts
        {
            get { return (DateTime)GetValue(StartsProperty); }
            set { SetValue(StartsProperty, value); }
        }
        /// <summary>
        ///     Gets or sets the date time scale the current <see cref="AtlasTimeline" /> is representing.
        /// </summary>
        public DateTime Ends
        {
            get { return (DateTime)GetValue(EndsProperty); }
            set { SetValue(EndsProperty, value); }
        }

        //private void UpdateTimeline()
        //{
        //    UpdateTimeline();
        //    UpdateTimeline();

        //    CommandManager.InvalidateRequerySuggested();
        //}

        protected static void DatePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            try
            {
                var timeline = (AtlasTimelineGrid)dependencyObject;

                timeline.UpdateTimeline();
                timeline.UpdateSize(timeline.ActualWidth);
            }
            catch (InvalidCastException)
            {
            }
        }

        /// <summary>
        ///     Gets or sets the current node that is represented in the timeline.
        /// </summary>
        public ITreeNode LifelineNode
        {
            get { return (ITreeNode)GetValue(LifelineNodeProperty); }
            set { SetValue(LifelineNodeProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the scale of dates current used in the timeline.
        /// </summary>
        public DateTimeScale Scale
        {
            get { return (DateTimeScale)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the zoom factor in the current timeline.
        /// </summary>
        public double Zoom
        {
            get { return (double)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the value being the variantion suffered by the zoom when zooming, that is, the value used to decrement
        ///     or increment the zoom value.
        /// </summary>
        public double ZoomFactor
        {
            get { return (double)GetValue(ZoomFactorProperty); }
            set { SetValue(ZoomFactorProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the minimum column width of the current timeline.
        /// </summary>
        public double ColumnWidth
        {
            get { return (double)GetValue(ColumnWidthProperty); }
            set { SetValue(ColumnWidthProperty, value); }
        }

        /// <summary>
        ///     Gets the command that allows to zoom in the current timeline grid.
        /// </summary>
        //public ICommand ZoomInCommand { get;  set; }
        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public ICommand ZoomInCommand
        {
            get { return (ICommand)GetValue(ZoomInCommandProperty); }
            set { SetValue(ZoomInCommandProperty, value); }
        }
        /// <summary>
        ///     Gets the command that allows to zoom out the current timeline grid.
        /// </summary>
        //public ICommand ZoomOutCommand { get;  set; }

        public ICommand ZoomOutCommand
        {
            get { return (ICommand)GetValue(ZoomOutCommandProperty); }
            set { SetValue(ZoomOutCommandProperty, value); }
        }
        /// <summary>
        ///     Calculates and gets the start and end dates of the current timeline.
        /// </summary>
        protected Tuple<DateTime, DateTime> StartAndEndDates
        {
            get
            {
                if (LifelineNode == null)
                {
                    return null;
                }

                //List<ILifeline> startDates = (from l in LifelineNode.Children select l.Value).ToList();
                //List<ILifeline> endDates = (from l in LifelineNode.Children select l.Value).ToList();
                //startDates.Sort((x, y) => x.Start.CompareTo(y.Start));
                //endDates.Sort((x, y) => x.End.CompareTo(y.End));

                //return startDates.Count == 0 || endDates.Count == 0 ? Tuple.Create(LifelineNode.Value.Start, LifelineNode.Value.End) : Tuple.Create(startDates.First().Start, endDates.Last().End);

                return Tuple.Create(LifelineNode.Start, LifelineNode.End);

            }
        }

        protected static IDictionary<int, string> MonthDictionary
        {
            get
            {
                IDictionary<int, string> columns = DateTimeUtilities.MonthNames
                    .Aggregate(new Dictionary<int, string>(), (dict, month) =>
                    {
                        dict[dict.Count] = month.Capitalize();
                        return dict;
                    });
                return columns;
            }
        }

        protected int DateElements
        {
            get { return GetDateElements(Scale); }
        }

        private IEnumerable<string> YearScaleColumns
        {
            get
            {
                // Get the names of the periods, translated to the current culture (if available)
                IDictionary<int, string> columns = MonthDictionary;

                // Find the interval which Start date is the formest and the one which End date is the latest
                Tuple<DateTime, DateTime> dates = StartAndEndDates;
                DateTime startDate = dates.Item1, endDate = dates.Item2;

                // Calculate the months total
                int months = startDate.GetMonthDelta(endDate);

                // Yield the name of the column name
                int monthIterator = startDate.Month;
                for (int monthIndex = 0; monthIndex <= months; monthIndex++, monthIterator++)
                {
                    monthIterator = monthIterator == 13 ? 1 : monthIterator;
                    yield return columns[monthIterator - 1];
                }
            }
        }

        private IEnumerable<string> MonthScaleColumns
        {
            get
            {
                Tuple<DateTime, DateTime> dates = StartAndEndDates;
                DateTime startDate = dates.Item1, endDate = dates.Item2;

                double days = (endDate - startDate).Days;
                DateTime currentDate;
                for (int increment = 0, day = 1;
                    increment <= days;
                    increment++, day = day == DateTime.DaysInMonth(currentDate.Year, currentDate.Month) ? 1 : day + 1)
                {
                    currentDate = startDate + TimeSpan.FromDays(increment);
                    yield return "{0}".EasyFormat(CultureInfo.CurrentCulture, currentDate.Day);
                }
            }
        }

        private IEnumerable<string> Columns
        {
            get
            {
                switch (Scale)
                {
                    case DateTimeScale.Yearly:
                        {
                            foreach (string column in YearScaleColumns) yield return column;
                            break;
                        }
                    case DateTimeScale.Monthly:
                        {
                            foreach (string column in MonthScaleColumns) yield return column;
                            break;
                        }
                }
            }
        }


        /// <summary>
        /// Updates the current timeline grid size according to the given container's width;
        /// </summary>
        /// <param name="containerWidth">The width of the current timeline container.</param>
        /// <param name="scale">The <see cref="DateTimeScale"/> to use in the size calculation.</param>
        public void UpdateSize(double containerWidth, DateTimeScale scale)
        {
            int elements = GetDateElements(scale);
            if (IsRoot)
                ColumnWidth = containerWidth == 0 ? ColumnWidth : containerWidth / (elements + 1);
            Zoom = elements == 0 ? 1 : containerWidth / (ColumnWidth * (elements + 1));

        }

        /// <summary>
        /// Updates the current timeline grid size according to the given container's width;
        /// </summary>
        /// <param name="containerWidth">The width of the current timeline container.</param>
        /// <param name="scale">The <see cref="DateTimeScale"/> to use in the size calculation.</param>
        public void UpdateSize(double containerWidth)
        {
            int elements = GetDateElements(Scale);
            if (IsRoot)
                ColumnWidth = containerWidth == 0 ? ColumnWidth : containerWidth / (elements + 1);
            Zoom = elements == 0 ? 1 : containerWidth / (ColumnWidth * (elements + 1));

        }
        /// <summary>
        ///     When overridden in a deriver it draws the content of the timeline grid.
        /// </summary>
        /// <param name="lifelineNode">
        ///     The <see cref="ITreeNode{T}" /> contaning the <see cref="ILifeline" /> to plot in the
        ///     timeline.
        /// </param>
        protected abstract void DrawContent(ITreeNode lifelineNode);

        /// <summary>
        ///     When overridden in a deriver it draws the content of a column according to some provided data.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        /// <param name="index">The index of the column.</param>
        /// <param name="lifelineNode">
        ///     The <see cref="ITreeNode{T}" /> contaning the <see cref="ILifeline" /> to plot in the
        ///     timeline.
        /// </param>
        protected abstract void DrawColumnContent(string name, int index, ITreeNode lifelineNode);

        /// <summary>
        ///     When overridden in a deriver it draws all the rows for the timeline grid.
        /// </summary>
        /// <param name="lifelineNode">
        ///     The <see cref="ITreeNode{T}" /> contaning the <see cref="ILifeline" /> to calculates its
        ///     begining and ending dates.
        /// </param>
        protected abstract void DrawRows(ITreeNode lifelineNode);


        private void UpdateColumns(ITreeNode lifelineNode)
        {
            int index = 0;
            foreach (string name in Columns)
            {
                // Create the column
                var column = new ColumnDefinition();
                if (!Equals(lifelineNode.Parent, null) && !IsRoot && index == 0)
                {
                    int daysInMonth = DateTime.DaysInMonth(lifelineNode.Start.Year, lifelineNode.Start.Month);
                    column.MaxWidth = (Zoom * ColumnWidth / daysInMonth) * (daysInMonth - (lifelineNode.Start.Day - 1));
                }
                // Make sure the column width is automatically recalculated when Zoom or ColumnWidth change
                var widthBinding = new MultiBinding { Converter = new TimelineColumnWidthConverter() };
                widthBinding.Bindings.Add(new Binding("Zoom") { Source = this });
                widthBinding.Bindings.Add(new Binding("ColumnWidth") { Source = this });
                column.SetBinding(ColumnDefinition.WidthProperty, widthBinding);

                ColumnDefinitions.Add(column);

                DrawColumnContent(name, index++, lifelineNode);
            }
        }

        public int GetDateElements(DateTimeScale scale)
        {
            Tuple<DateTime, DateTime> dates = StartAndEndDates;
            int elements = 0;
            if (dates == null)
            {
                return 0;
            }
            switch (scale)
            {
                case DateTimeScale.Yearly:
                    // Calculate the elements according to the year scale
                    elements = dates.Item1.GetMonthDelta(dates.Item2);
                    break;
                case DateTimeScale.Monthly:
                    // Calculate the elements according to the month scale
                    elements = (dates.Item2 - dates.Item1).Days;
                    break;
            }

            return elements;
        }


        // TODO: Remove
        //private void InitializeNode()
        //{
        //    if (_node == null)
        //    {
        //        var r = new Random();
        //        _node = new LifelineTreeNode();
        //        for (int i = 0; i < r.Next(15, 40); i++)
        //        {
        //            var start = new DateTime(r.Next(2013, 2016), r.Next(1, 12), r.Next(1, 28));

        //            DateTime end;

        //            do
        //            {
        //                end = new DateTime(r.Next(2013, 2016), r.Next(1, 12), r.Next(1, 28));
        //            } while (end <= start);

        //            var lifeline = new Lifeline(start, end);
        //            var child = new LifelineTreeNode(lifeline, null);

        //            _node.Children.Add(child);
        //        }
        //    }
        //    LifelineNode = _node;
        //}
        // private int count = 0;
        public void UpdateTimeline()
        {
            ///   count++;
            if (IsEnabled && ActualWidth>0)
            {
                // Below starts the current atlas timeline grid generic columns and rows accomodations
                try
                {
                    var lifelineNode = LifelineNode;

                    // Remove all childs
                    Children.Clear();
                    ColumnDefinitions.Clear();
                    RowDefinitions.Clear();

                    // Create or update the columns
                    UpdateColumns(lifelineNode);

                    // Create or update the rows
                    DrawRows(lifelineNode);

                    // Then draw the custom content for the actual timeline grid
                    DrawContent(lifelineNode);
                }
                catch (NullReferenceException)
                {
                    Debug.WriteLine("Provided a null time line item in the LifelineNode property. Nulls are not allowed!");
                }
                catch (InvalidCastException)
                {
                    Debug.WriteLine("Provided a null time line item in the LifelineNode property. Nulls are not allowed!");
                }
            }



            if (IsEnabled && ActualWidth == 0 && DataContext != null && !IsRoot)
            {
                SizeChanged -= OnSizeChanged;
                SizeChanged += OnSizeChanged;
            }

        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            var parent = this.Parent as FrameworkElement;

            if (!sizeChangedEventArgs.NewSize.IsEmpty && parent != null &&
                parent.ActualWidth == sizeChangedEventArgs.NewSize.Width && Zoom == 1.0)
            {
                UpdateTimeline();
            }
        }

        private static void AtlasTimelineGrid_OnIntervalNodeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                var timeline = (AtlasTimelineGrid)d;
                timeline.UpdateTimeline();
            }
            catch (InvalidCastException)
            {
            }
        }

        private static void AtlasTimelineGrid_OnScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                var timeline = (AtlasTimelineGrid)d;
                timeline.UpdateTimeline();
            }
            catch (InvalidCastException)
            {
            }
        }


        protected bool ZoomInCommand_CanExecute(double? containerWidth)
        {
            return true;
        }

        protected virtual void ZoomInCommand_Executed(double? containerWidth)
        {
            Zoom += ZoomFactor;
            //if (IsEnabled && ActualWidth > 0)
            //{
            //    // Below starts the current atlas timeline grid generic columns and rows accomodations
            //    try
            //    {
            //        var lifelineNode = LifelineNode;

            //        // Remove all childs
            //        Children.Clear();
            //        ColumnDefinitions.Clear();
            //        RowDefinitions.Clear();

            //        // Create or update the columns
            //        UpdateColumns(lifelineNode);

            //        // Create or update the rows
            //        DrawRows(lifelineNode);

            //        // Then draw the custom content for the actual timeline grid
            //        DrawContent(lifelineNode);
            //    }
            //    catch (NullReferenceException)
            //    {
            //        Debug.WriteLine("Provided a null time line item in the LifelineNode property. Nulls are not allowed!");
            //    }
            //    catch (InvalidCastException)
            //    {
            //        Debug.WriteLine("Provided a null time line item in the LifelineNode property. Nulls are not allowed!");
            //    }
            //}
        }
        protected virtual void ZoomOutCommand_Executed(double? containerWidth)
        {
            if (!containerWidth.HasValue)
                return;

            double zoomFactor = ZoomFactor;
            int elements = DateElements;

            /* Adjust the zoom value if it, after the used in the zooming operation will come the atlas timeline shorter that the
             * container */
            double widthAfterZoom = (Zoom - zoomFactor) * ColumnWidth * elements;
            if (IsRoot)
                ColumnWidth = containerWidth.Value == 0 ? ColumnWidth : containerWidth.Value / (elements + 1);

            if (widthAfterZoom <= containerWidth.Value)
                Zoom = containerWidth.Value / (ColumnWidth * (elements + 1));
            else
                Zoom -= zoomFactor;


            //if (IsEnabled && ActualWidth > 0)
            //{
            //    // Below starts the current atlas timeline grid generic columns and rows accomodations
            //    try
            //    {
            //        var lifelineNode = LifelineNode;

            //        // Remove all childs
            //        Children.Clear();
            //        ColumnDefinitions.Clear();
            //        RowDefinitions.Clear();

            //        // Create or update the columns
            //        UpdateColumns(lifelineNode);

            //        // Create or update the rows
            //        DrawRows(lifelineNode);

            //        // Then draw the custom content for the actual timeline grid
            //        DrawContent(lifelineNode);
            //    }
            //    catch (NullReferenceException)
            //    {
            //        Debug.WriteLine("Provided a null time line item in the LifelineNode property. Nulls are not allowed!");
            //    }
            //    catch (InvalidCastException)
            //    {
            //        Debug.WriteLine("Provided a null time line item in the LifelineNode property. Nulls are not allowed!");
            //    }
            //}
        }

        protected bool ZoomOutCommand_CanExecute(double? containerWidth)
        {
            return ColumnDefinitions.Any() && containerWidth != null && ActualWidth > containerWidth;
        }

        /// <summary>
        /// reset the zoom property to fit the current view of the <see cref="ILifeline"/> element
        /// </summary>
        /// <param name="containerWidth"></param> actual width of the view
        public void ResetZoom(double? containerWidth)
        {
            ZoomOutCommand_Executed(containerWidth);
        }
    }
}