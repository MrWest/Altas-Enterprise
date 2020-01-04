using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyName.Atlas.UIControls
{
    public class InnerLifeLineControl : ContentControl
    {
        /// <summary>
        ///     Dependency property used to contain the value of the zoom factor in instances of <see cref="AtlasTimelineGrid" />.
        /// </summary>
        public static readonly DependencyProperty InnerContentMaxWidthProperty = DependencyProperty.Register("InnerContentMaxWidth", typeof(double), typeof(InnerLifeLineControl), new PropertyMetadata(1.0));


        public InnerLifeLineControl()
        {
            DefaultStyleKey = typeof(InnerLifeLineControl);
          
        }

       

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (Parent as FrameworkElement != null && (Parent as FrameworkElement).ActualWidth > 10)
                InnerContentMaxWidth = (Parent as FrameworkElement).ActualWidth;
          ;
        }


        /// <summary>
        ///     Gets or sets the zoom factor in the current timeline.
        /// </summary>
        public double InnerContentMaxWidth
        {
            get { return (double)GetValue(InnerContentMaxWidthProperty); }
            set { SetValue(InnerContentMaxWidthProperty, value); }
        }
    }
}