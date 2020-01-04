using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CompanyName.Atlas.Contracts.Presentation.Common;

namespace CompanyName.Atlas.UIControls.Converters
{
    /// <summary>
    ///     This is value converter allowing to take a timeline zoom and columns width value and transforming them into a
    ///     margin for lifeline objects.
    /// </summary>
    public struct LifelineMarginConverter : IMultiValueConverter
    {
        /// <summary>
        ///     Converts the given timeline zoom and columns width values into a margin for a lifeline.
        /// </summary>
        /// <param name="values">The timeline zoom and columns width values to use in the margin calculations.</param>
        /// <param name="targetType">Not used.</param>
        /// <param name="parameter">Expected a <see cref="Tuple{T1, T2}" /> with the lifeline and date scale.</param>
        /// <param name="culture">Not used.</param>
        /// <returns>The <see cref="Thickness" /> being the margin for the lifeline.</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool isFirst = false;
            if (values[0] != null && values[1] != null)
            {
                var lifeline = (Tuple<ITreeNode, DateTimeScale, bool>)parameter;
                DateTime startDate = lifeline.Item1.Start, endDate = lifeline.Item1.End;
                DateTimeScale scale = lifeline.Item2;

                bool isRoot = lifeline.Item3;
                isFirst = !Equals(lifeline.Item1.Parent, null) && lifeline.Item1.Parent.Items.Items.Count > 0 && Equals(lifeline.Item1.Parent.Items.Items[0], lifeline.Item1);


                try
                {
                    double zoom = (double)values[0], width = (double)values[1];
                    int lifelineDatesDelta = GetLifelineColumnSpan(lifeline.Item1, startDate, scale);



                    lifeline.Item1.TimeLineThickness = GetMarginForYearScale(zoom, width, startDate, endDate, isFirst,
                        lifeline.Item1, lifeline.Item1.Parent as ITreeNode, isRoot, scale);

                    return GetMarginForYearScale(zoom, width, startDate, endDate, isFirst, lifeline.Item1,
                        lifeline.Item1.Parent as ITreeNode, isRoot, scale);

                }
                catch (Exception e)
                {
                    return new Thickness(0);
                }


            }
            return new Thickness(0, isFirst ? 1 : 0, 0, 1); ;
        }

        /// <summary>
        ///     Not supported.
        /// </summary>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        private static Thickness GetMarginForYearScale(double zoom, double width, DateTime startDate, DateTime endDate,
            bool isFirst, ITreeNode current, ITreeNode parent, bool isRoot, DateTimeScale scale)
        {

            //if (!Equals(parent, null)&& !Equals(parent.Tag, null))
            //{
            //    width = (double) parent.Tag;
            //}

            //  current.Tag = width;

            double pixels = 1 * width / DateTime.DaysInMonth(startDate.Year, startDate.Month);
            double leftMargin = pixels * (startDate.Day - 1);




            int daysInMonth = DateTime.DaysInMonth(endDate.Year, endDate.Month);
            pixels = zoom * width / daysInMonth;
            double rightMargin = pixels * (daysInMonth - endDate.Day);




            if (!Equals(parent, null) && zoom > 0 && !isRoot)
            {

                int startDatesDelta = GetLifelineColumn(parent.Start, startDate, scale);

                if (startDatesDelta == 0)
                {

                    leftMargin = pixels * (startDate.Day - 1) - pixels * (parent.Start.Day - 1);//parent.TimeLineThickness.Left;
                }

                if (parent.End.Year == endDate.Year && parent.End.Month == endDate.Month)
                {
                    rightMargin = pixels * (parent.End.Day - endDate.Day);
                }
            }

            return new Thickness(leftMargin, isFirst ? 1 : 0, rightMargin, 1);
        }


        private int GetLifelineColumnSpan(ITreeNode lifeline, DateTime lifelineStartDate, DateTimeScale scale)
        {
            switch (scale)
            {
                case DateTimeScale.Yearly:
                    return lifelineStartDate.GetMonthDelta(lifeline.End) + 1;
                default:
                    return (lifeline.End - lifelineStartDate).Days + 1;
            }
        }
        private static int GetLifelineColumn(DateTime startDate, DateTime lifelineStartDate, DateTimeScale scale)
        {
            switch (scale)
            {
                case DateTimeScale.Yearly:
                    return startDate.GetMonthDelta(lifelineStartDate);
                default:
                    return (lifelineStartDate - startDate).Days;
            }
        }
    }
}