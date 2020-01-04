using System;
using System.Windows;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    /// Interaction logic for Timeline.xaml
    /// </summary>
    public partial class Timeline
    {
        public Timeline()
        {
            InitializeComponent();
        }


        #region Dependency properties

        #region ZoomFactor
        
        public static readonly DependencyProperty ZoomFactorProperty = DependencyProperty.Register("ZoomFactor", typeof(double), typeof(Timeline), new PropertyMetadata(1d));

        public double ZoomFactor
        {
            get { return (double)GetValue(ZoomFactorProperty); }
            set { SetValue(ZoomFactorProperty, value); }
        } 
        
        #endregion

        #region DateScale
        
        public static readonly DependencyProperty DateScaleProperty = DependencyProperty.Register("DateScale", typeof(DateScale), typeof(Timeline), new PropertyMetadata(default(DateScale)));

        public DateScale DateScale
        {
            get { return (DateScale)GetValue(DateScaleProperty); }
            set { SetValue(DateScaleProperty, value); }
        }

        #endregion

        #region ScaleElementBaseWidth

        public static readonly DependencyProperty ScaleElementBaseWidthProperty = DependencyProperty.Register("ScaleElementBaseWidth", typeof(double), typeof(Timeline), new PropertyMetadata(80d));

        public double ScaleElementBaseWidth
        {
            get { return (double)GetValue(ScaleElementBaseWidthProperty); }
            set { SetValue(ScaleElementBaseWidthProperty, value); }
        }

        #endregion

        #endregion


        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            DrawScales();
        }


        private void DrawScales()
        {
            switch (DateScale)
            {
                case DateScale.Year:
                    DrawYearScales();
                    break;
                case DateScale.Month:
                    break;
                case DateScale.Week:
                    break;
                default:
                    break;
            }
        }

        private void DrawYearScales()
        {
            
        }
    }
}
