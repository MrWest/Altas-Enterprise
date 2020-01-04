using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.UIControls.Converters;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Binding = System.Windows.Data.Binding;
using Control = System.Windows.Controls.Control;
using HorizontalAlignment = System.Windows.HorizontalAlignment;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    ///     This is a timeline grid containing some lifelines according to the children of a certain lifeline tree node.
    /// </summary>
    public class AtlasTimelineLifelinesGrid : AtlasTimelineGrid
    {
        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty RowsHeightProperty = DependencyProperty.Register("RowsHeight", typeof(int), typeof(AtlasTimelineLifelinesGrid), new PropertyMetadata(50));

        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty LifeLineItemsProperty = DependencyProperty.Register("LifeLineItems", typeof(IEnumerable<INavigable>), typeof(AtlasTimelineLifelinesGrid), new PropertyMetadata(null, DatePropertyChangedCallback));



        public AtlasTimelineLifelinesGrid()
        {
          
          //  Loaded+=OnLoaded;
          // Initialized+=OnInitialized;
          
        }

        private void OnInitialized(object sender, EventArgs eventArgs)
        {
            UpdateTimeline();// throw new NotImplementedException();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (IsLoaded && !IsRoot)//  if(!IsRoot&&!sizeChangedEventArgs.NewSize.IsEmpty&&ActualWidth==sizeChangedEventArgs.NewSize.Width)
                UpdateTimeline();
        }

       

        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public IEnumerable<INavigable> LifeLineItems
        {
            get { return (IEnumerable<INavigable>)GetValue(LifeLineItemsProperty); }
            set { SetValue(LifeLineItemsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public int RowsHeight
        {
            get { return (int)GetValue(RowsHeightProperty); }
            set { SetValue(RowsHeightProperty, value); }
        }


        /// <summary>
        ///     Draws the content of a column according to some provided data. The content drawn is a border filling the whole gap
        ///     to the end of the column.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        /// <param name="index">The index of the column.</param>
        /// <param name="lifelineNode">
        ///     The <see cref="ITreeNode{T}" /> contaning the <see cref="ILifeline" /> to plot in the
        ///     timeline.
        /// </param>
        protected override void DrawColumnContent(string name, int index, ITreeNode lifelineNode)
        {
            var body = new Border
            {
                Style = (Style)GenericResources["TimelineColumnBodyStyle"],
                BorderThickness = new Thickness { Bottom = 0, Top = 0, Left = index == 0 ? 0 : 1, Right = 0 }
            };

            SetRow(body, 0);
            SetColumn(body, index);
            SetRowSpan(body, lifelineNode.Items.Items.Count + 1);
            Children.Add(body);
        }

        /// <summary>
        ///     When overridden in a deriver it draws all the rows for the timeline grid.
        /// </summary>
        /// <param name="lifelineNode">
        ///     The <see cref="ITreeNode{T}" /> contaning the <see cref="ILifeline" /> to calculates its
        ///     begining and ending dates.
        /// </param>
        protected override void DrawRows(ITreeNode lifelineNode)
        {
            // Include as many rows as lifelines are inside the given one
            int childrenCount = ((INavigableViewModel)lifelineNode.Items).FiltredItems.Count();
            for (int i = 0; i < childrenCount; i++)
            {
                var row = new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Auto)
                };
                RowDefinitions.Add(row);
            }

            var lastRow = new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };
            RowDefinitions.Add(lastRow);
        }

        /// <summary>
        ///     Draws the lifelines of the current timeline grid.
        /// </summary>
        /// <param name="lifelineNode">
        ///     The <see cref="ITreeNode{T}" /> contaning the <see cref="ILifeline" /> to plot in the
        ///     timeline.
        /// </param>
        protected override void DrawContent(ITreeNode lifelineNode)
        {
            //if (IsInitialized )
            //{
                DateTime startDate = StartAndEndDates.Item1;

                int row = 0;
                foreach (ITreeNode lifeline in ((INavigableViewModel)lifelineNode.Items).FiltredItems)
                {
                    // Create the lifeline control
                    Control lifelineControl = CreateLifelineControl(lifeline);

                    // Place it in its corresponding row
                    SetRow(lifelineControl, row);

                    // Place it in its corresponding column
                    PositionLifelineInItsColumn(lifelineControl, lifeline, startDate, row++ == 0);

                    Children.Add(lifelineControl);
                }
            //}
          
        }


        private Control CreateLifelineControl(ITreeNode lifeline)
        {

            if (!IsRoot && ActualWidth>0)
            {
                var period = new Period();
                period.Starts = (DataContext as ITreeNode).Start;
                period.Ends = (DataContext as ITreeNode).End;

                double pixels = ActualWidth / period.Days;

                period.Starts = lifeline.Start;
                period.Ends = lifeline.End;

                var width = pixels * period.Days;
                return new LifelineControl
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    DataContext = lifeline,
                    Zoom = Zoom,
                    Width = width

                };
            }

            return new LifelineControl
            {
                DataContext = lifeline,
                Zoom = Zoom

            };



        }

        private void PositionLifelineInItsColumn(FrameworkElement control, ITreeNode lifeline, DateTime startDate, bool isFirst)
        {
            // Set the lifeline control column and its span
            DateTime lifelineStartDate = lifeline.Start;
            int startDatesDelta = GetLifelineColumn(startDate, lifelineStartDate);
            int lifelineDatesDelta = GetLifelineColumnSpan(lifeline, lifelineStartDate);
            SetColumn(control, startDatesDelta);
            SetColumnSpan(control, lifelineDatesDelta);

            // Make sure that the margins are kept in sync as it must according to the zoom and column width
            var marginBinding = new MultiBinding
            {
                Converter = new LifelineMarginConverter(),
                ConverterParameter = Tuple.Create(lifeline, Scale, IsRoot)
            };
            marginBinding.Bindings.Add(new Binding("Zoom") { Source = this });
            marginBinding.Bindings.Add(new Binding("ColumnWidth") { Source = this });
            control.SetBinding(MarginProperty, marginBinding);
        }

        private int GetLifelineColumn(DateTime startDate, DateTime lifelineStartDate)
        {
            switch (Scale)
            {
                case DateTimeScale.Yearly:
                    return startDate.GetMonthDelta(lifelineStartDate);
                default:
                    return (lifelineStartDate - startDate).Days;
            }
        }

        private int GetLifelineColumnSpan(ITreeNode lifeline, DateTime lifelineStartDate)
        {
            switch (Scale)
            {
                case DateTimeScale.Yearly:
                    return lifelineStartDate.GetMonthDelta(lifeline.End) + 1;
                default:
                    return (lifeline.End - lifelineStartDate).Days + 1;
            }
        }

        //protected override void ZoomInCommand_Executed(double? containerWidth)
        //{
        //    Zoom += ZoomFactor;

        //    foreach (UIElement element in Children.Cast<UIElement>())
        //    {
        //        var lifelineControl = element as LifelineControl;
        //        lifelineControl?.ZoomInCommand.Execute(containerWidth);
        //    }
        //}
        protected override void ZoomOutCommand_Executed(double? containerWidth)
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

            foreach (UIElement element in Children.Cast<UIElement>())
            {
                var lifelineControl = element as LifelineControl;
                lifelineControl?.ZoomOutCommand.Execute(containerWidth);
            }

        }

    }
}