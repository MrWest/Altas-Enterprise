using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Presentation.Common;

namespace CompanyName.Atlas.UIControls
{
    public class EasyTimeLine: ContentControl
    {
        protected DoubleAnimation spreadIn;
        /// <summary>
        ///     Dependency property used to contain the value of the zoom factor in instances of <see cref="AtlasTimelineGrid" />.
        /// </summary>
        public static readonly DependencyProperty TreeNodeProperty = DependencyProperty.Register("TreeNode", typeof(ITreeNode), typeof(EasyTimeLine), new PropertyMetadata(null));
        /// <summary>
        ///     Dependency property used to contain the value of the zoom factor in instances of <see cref="AtlasTimelineGrid" />.
        /// </summary>
        public static readonly DependencyProperty DeepVisibilityProperty = DependencyProperty.Register("DeepVisibility", typeof(Visibility), typeof(EasyTimeLine), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        ///     Dependency property used to contain the value of the zoom factor in instances of <see cref="AtlasTimelineGrid" />.
        /// </summary>
        public static readonly DependencyProperty InnerTextVisibilityProperty = DependencyProperty.Register("InnerTextVisibility", typeof(Visibility), typeof(EasyTimeLine), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        ///     Dependency property used to contain the value of the zoom factor in instances of <see cref="AtlasTimelineGrid" />.
        /// </summary>
        public static readonly DependencyProperty ReferenceWidthProperty = DependencyProperty.Register("ReferenceWidth", typeof(double), typeof(EasyTimeLine), new PropertyMetadata(1.0,PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var easytimeline = dependencyObject as EasyTimeLine;
            if (easytimeline!=null && easytimeline.IsVisible && easytimeline.IsLoaded)
                easytimeline?.SetMyWidth();
        }

        public EasyTimeLine()
        {
            DefaultStyleKey = typeof(EasyTimeLine);
            Loaded+=OnLoaded;
            RequestBringIntoView+=OnRequestBringIntoView;
            //LayoutUpdated +=OnLayoutUpdated;
            IsVisibleChanged+=OnIsVisibleChanged;

        }

        private void OnRequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            //if (IsVisible && IsLoaded)
            //    SetMyWidth();
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(IsVisible && IsLoaded)
                SetMyWidth();
        }


        /// <summary>
        ///     Gets or sets the zoom factor in the current timeline.
        /// </summary>
        public double ReferenceWidth
        {
            get { return (double)GetValue(ReferenceWidthProperty); }
            set { SetValue(ReferenceWidthProperty, value); }
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            SetMyWidth();
        }

        public void SetMyWidth()
        {
            if (TreeNode != null && TreeNode.Parent != null && TreeNode.Parent as ITreeNode != null)
            {
                var period = new Period();

                period.Starts = (TreeNode.Parent as ITreeNode).Start;
                period.Ends = (TreeNode.Parent as ITreeNode).End;

                double pixels = ActualWidth / period.Days;
                if (Parent as FrameworkElement != null)
                    pixels = (Parent as FrameworkElement).ActualWidth / period.Days;
                if (ReferenceWidth > 1)
                {
                    pixels = ReferenceWidth / period.Days;
                }

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

                Width = pixels * period.Days;

             

                //if ((Parent as FrameworkElement != null) && Width >= (Parent as FrameworkElement).ActualWidth)
                //    Width =(pixels * period.Days) - 5;

                Margin = new Thickness(left, 1, right, 0);

                ToolTip = TreeNode.Name + "  " + TreeNode.Start.ToShortDateString() + " - " + TreeNode.End.ToShortDateString();

                //if (Width < 50)
                //    InnerTextVisibility = Visibility.Hidden;

                if (spreadIn != null)
                {
                    spreadIn.To = ActualWidth;
                    spreadIn.From = ActualWidth * 0.75;
                }

                if (Width < 5)
                    ToolTip = ToolTip;

            }
        }

        //public override void OnApplyTemplate()
        //{
        //    base.OnApplyTemplate();
        //    if (TreeNode != null && TreeNode.Parent != null && TreeNode.Parent as ITreeNode != null)
        //    {
        //        var period = new Period();

        //        period.Starts = (TreeNode.Parent as ITreeNode).Start;
        //        period.Ends = (TreeNode.Parent as ITreeNode).End;

               
        //        double pixels = ActualWidth / period.Days;
        //        if (Parent as Control != null)
        //            pixels = (Parent as Control).ActualWidth / period.Days;

        //        period.Starts = (TreeNode.Parent as ITreeNode).Start;
        //        period.Ends = TreeNode.Start;

        //        double left = pixels * (period.Days - 1);

        //        period.Starts = TreeNode.End;
        //        period.Ends = (TreeNode.Parent as ITreeNode).End;

        //        double right = pixels * (period.Days - 1);


        //        period.Starts = TreeNode.Start;
        //        period.Ends = TreeNode.End;

        //        Width = pixels * period.Days;

        //        Margin = new Thickness(left, 1, right, 0);

        //    }
        //}

        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public ITreeNode TreeNode
        {
            get { return (ITreeNode)GetValue(TreeNodeProperty); }
            set { SetValue(TreeNodeProperty, value); }
        }
        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public Visibility DeepVisibility
        {
            get { return (Visibility)GetValue(DeepVisibilityProperty); }
            set { SetValue(DeepVisibilityProperty, value); }
        }

        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public Visibility InnerTextVisibility
        {
            get { return (Visibility)GetValue(InnerTextVisibilityProperty); }
            set { SetValue(InnerTextVisibilityProperty, value); }
        }
        
    }

    public class AnotherEasyTimeline : EasyTimeLine
    {
      
        public AnotherEasyTimeline()
        {
            DefaultStyleKey = typeof(AnotherEasyTimeline);
            //IsVisibleChanged+= OnIsVisibleChanged;
            
            //spreadIn = new DoubleAnimation();
            //spreadIn.To = ActualWidth;
            //spreadIn.From = ActualWidth * 0.75;
            //spreadIn.Duration = TimeSpan.FromSeconds(0.5);
            //spreadIn.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut };
        }

        //private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        //{
        //    if (IsVisible)
        //    {

        //        var border = (Border) Template.FindName("BackgroundContent",this);
        //        border.BeginAnimation(WidthProperty, spreadIn);
        //        //fadeOut.Completed += FadeOutOnCompleted;
        //    }
        //}
    }
}