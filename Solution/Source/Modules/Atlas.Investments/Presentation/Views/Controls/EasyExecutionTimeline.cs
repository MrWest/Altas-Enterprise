using System;
using System.Windows;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using CompanyName.Atlas.UIControls;

namespace CompanyName.Atlas.Investments.Presentation.Views.Controls
{
    public class EasyExecutionTimeline: EasyTimeLine
    {

        /// <summary>
        ///     Dependency property used to contain the value of the zoom factor in instances of <see cref="AtlasTimelineGrid" />.
        /// </summary>
        public static readonly DependencyProperty EstimatedWidthProperty = DependencyProperty.Register("EstimatedWidth", typeof(double), typeof(EasyExecutionTimeline), new PropertyMetadata(0.0));


        /// <summary>
        ///     Dependency property used to contain the value of the zoom factor in instances of <see cref="AtlasTimelineGrid" />.
        /// </summary>
        public static readonly DependencyProperty ExecutionWidthProperty = DependencyProperty.Register("ExecutionWidth", typeof(double), typeof(EasyExecutionTimeline), new PropertyMetadata(0.0));

        /// <summary>
        ///     Dependency property used to contain the value of the zoom factor in instances of <see cref="AtlasTimelineGrid" />.
        /// </summary>
        public static readonly DependencyProperty PlannedWidthProperty = DependencyProperty.Register("PlannedWidth", typeof(double), typeof(EasyExecutionTimeline), new PropertyMetadata(0.0));

        public EasyExecutionTimeline()
        {
            DefaultStyleKey = typeof(EasyExecutionTimeline);
            HorizontalAlignment = HorizontalAlignment.Left;
        }

        public DateTime ExecutionEndDate()
        {

            var period = new Period();

            period.Starts = (TreeNode).Start;
            period.Ends = (TreeNode).End;

            double executionDays = (double) (period.Days * ((TreeNode as IBudgetSummary).ExecutionPercent) /100);
            return period.Starts.AddDays( executionDays);

        }

        public DateTime EstimatedEndDate()
        {

            var period = new Period();

            period.Starts = (TreeNode).Start;

            period.Ends = DateTime.Today;
            int currentdays = period.Days;

            if ((TreeNode as IBudgetSummary).ExecutionPercent==0 && period.Ends.CompareTo(DateTime.Today) < 0)
               return period.Starts.AddDays(currentdays);



            period.Ends = (TreeNode).End;
            int plannedDays = period.Days;
           

            var executionDays = (double) (plannedDays * ((TreeNode as IBudgetSummary).ExecutionPercent) / 100);


            var executionReason = executionDays / currentdays;


            int estimatedDays = 0;
            if (executionReason>0)
             estimatedDays = (int) (plannedDays / executionReason);


           


            return period.Starts.AddDays(estimatedDays);

        }

        protected override void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (TreeNode != null && TreeNode.Parent != null && TreeNode.Parent as ITreeNode != null)
            {
                var period = new Period();

                period.Starts = (TreeNode.Parent as ITreeNode).Start;
                period.Ends = (TreeNode.Parent as ITreeNode).End;

                double pixels = ActualWidth / period.Days;
                if (Parent as FrameworkElement != null)
                    pixels = (Parent as FrameworkElement).ActualWidth / period.Days;

                period.Starts = (TreeNode.Parent as ITreeNode).Start;
                period.Ends = TreeNode.Start;

                double left = pixels * (period.Days - 1);

                period.Starts = TreeNode.End;
                period.Ends = (TreeNode.Parent as ITreeNode).End;

                double right = pixels * (period.Days - 1);

                period.Starts = (TreeNode.Parent as ITreeNode).Start;
                period.Ends = (TreeNode.Parent as ITreeNode).End;

                period.Starts = TreeNode.Start;
                period.Ends = TreeNode.End;

                PlannedWidth = pixels * period.Days;

                period.Ends = ExecutionEndDate();
                ExecutionWidth = pixels * period.Days;

                period.Ends = EstimatedEndDate();
                EstimatedWidth = pixels * period.Days;

                Margin = new Thickness(left, 1, 0, 0);

                ToolTip = TreeNode.Name + "  " + Properties.Resources.Execution + ": "+ (TreeNode as IBudgetSummary).ExecutionPercent.ToString()
                    +"%"+" - " + Properties.Resources.EstimatedFinishDate + ": " + EstimatedEndDate().ToShortDateString();


            }



        }

        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public double ExecutionWidth
        {
            get { return (double)GetValue(ExecutionWidthProperty); }
            set { SetValue(ExecutionWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public double EstimatedWidth
        {
            get { return (double)GetValue(EstimatedWidthProperty); }
            set { SetValue(EstimatedWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public double PlannedWidth
        {
            get { return (double)GetValue(PlannedWidthProperty); }
            set { SetValue(PlannedWidthProperty, value); }
        }
    }
}