using CompanyName.Atlas.Contracts.Presentation.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    ///     This is an concrete atlas timeline grid, representing the heades containing the mayor and minor scales of a certain
    ///     date scale.
    /// </summary>
    public class AtlasTimelineHeaderGrid : AtlasTimelineGrid
    {
        /// <summary>
        ///     Draws the header of the column described by the provided data.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        /// <param name="index">The index of the column.</param>
        /// <param name="lifelineNode">
        ///     The <see cref="ITreeNode{T}" /> contaning the <see cref="ILifeline" /> to plot in the
        ///     timeline.
        /// </param>
        protected override void DrawColumnContent(string name, int index, ITreeNode lifelineNode)
        {
            var header = new Label
            {
                Content = name,
                Style = (Style)GenericResources["TimelineHeaderTextStyle"],
                BorderThickness = new Thickness { Bottom = 1, Top = 0, Left = index == 0 ? 0 : 1, Right = 0 }
            };

            SetRow(header, 1);
            SetColumn(header, index);
            Children.Add(header);
        }

        /// <summary>
        ///     Draws all the rows necessary for the current timeline grid according to a given tree node.
        /// </summary>
        /// <param name="lifelineNode">
        ///     The <see cref="ITreeNode{T}" /> contaning the <see cref="ILifeline" /> to calculates its
        ///     begining and ending dates.
        /// </param>
        protected override void DrawRows(ITreeNode lifelineNode)
        {
            RowDefinitions.Clear();

            // Initially adding the two rows of the timeline, the title and header ones
            var topRow = new RowDefinition { Height = new GridLength(35, GridUnitType.Pixel) };
            var headerRow = new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) };
            RowDefinitions.Add(topRow);
            RowDefinitions.Add(headerRow);
        }

        /// <summary>
        ///     Draws the actual content to be displayed in the current timeline, which in this case the drawn content are the
        ///     headers of the major date scale.
        /// </summary>
        /// <param name="lifelineNode">
        ///     The <see cref="ITreeNode{T}" /> contaning the <see cref="ILifeline" /> to plot in the
        ///     timeline.
        /// </param>
        protected override void DrawContent(ITreeNode lifelineNode)
        {
            switch (Scale)
            {
                case DateTimeScale.Yearly:
                    PlaceTopBarAccordingToScaleInYears();
                    break;
                case DateTimeScale.Monthly:
                    PlaceTopBarAccordingToScaleInMonths();
                    break;
            }
        }


        private void PlaceTopBarAccordingToScaleInYears()
        {
            // Find the date values for the alignment of the top header texts
            Tuple<DateTime, DateTime> dates = StartAndEndDates;
            DateTime lifelineStart = dates.Item1, lifelineEnd = dates.Item2;
            int years = lifelineEnd.Year - lifelineStart.Year;

            // Create the text and align them in the current grid
            for (int year = 0, column = 0, columnSpan = 12; year <= years; year++)
            {
                Label textBlock = CreateTopHeaderTextControl(year == 0);
                textBlock.Content = (lifelineStart.Year + year).ToString(CultureInfo.CurrentCulture);

                SetRow(textBlock, 0);
                SetColumn(textBlock, column);

                // Increment the column index and span for the next text header
                if (year == 0)
                    column += years == 0 ? lifelineEnd.Month - lifelineStart.Month : (columnSpan = 13 - lifelineStart.Month);
                else
                    column += columnSpan = 12;

                SetColumnSpan(textBlock, columnSpan);

                Children.Add(textBlock);
            }
        }

        private void PlaceTopBarAccordingToScaleInMonths()
        {
            IDictionary<int, string> months = MonthDictionary;

            Func<int, int, int> getDaysInMonth = DateTime.DaysInMonth;

            // Find the date values for the alignment of the top header texts
            Tuple<DateTime, DateTime> dates = StartAndEndDates;
            DateTime lifelineStart = dates.Item1, lifelineEnd = dates.Item2;
            int days = (lifelineEnd - lifelineStart).Days;

            // Now position the top bar headers accordingly to the monthly scale
            DateTime currentDate = lifelineStart;
            for (int column = 0,
                daysInMonth = getDaysInMonth(currentDate.Year, currentDate.Month),
                columnSpan = daysInMonth - lifelineStart.Day + 1;
                column < days;
                currentDate = new DateTime(currentDate.Year, currentDate.Month, 1) + TimeSpan.FromDays(daysInMonth),
                column += columnSpan,
                daysInMonth = getDaysInMonth(currentDate.Year, currentDate.Month),
                columnSpan = daysInMonth)
            {
                Label control = CreateTopHeaderTextControl(column == 0);
                string month = months[currentDate.Month - 1];
                string year = "{0}".EasyFormat(currentDate.Year);
                control.Content = "{0}/{1}".EasyFormat(month, year);

                SetRow(control, 0);
                SetColumn(control, column);
                SetColumnSpan(control, columnSpan);

                Children.Add(control);
            }
        }

        private Label CreateTopHeaderTextControl(bool isFirst = false)
        {
            var control = new Label
            {
                Style = (Style)GenericResources["TimelineTopHeaderTextStyle"],
                BorderThickness = new Thickness(isFirst ? 0 : 1, 0, 0, 0)
            };

            return control;
        }
    }
}