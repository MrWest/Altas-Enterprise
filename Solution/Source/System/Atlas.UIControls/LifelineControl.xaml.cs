using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.Prism.Commands;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    ///     Interaction logic for LifelineControl.xaml
    /// </summary>
    public partial class LifelineControl
    {
        private const int BackgroundMaxColor = 225, BackgroundMinColor = 100;
        private static readonly Random SeedRandom = new Random();

        private bool _isControlPressed;
        private bool _isDragging;
        private Point _mouseOffset;
        /// <summary>
        ///     Dependency property used to contain the brush for the expander button when the mouse is over it in instances of
        ///     <see cref="LifelineControl" />.
        /// </summary>
        public static readonly DependencyProperty ExpanderMouseOverForegroundProperty = DependencyProperty.Register("ExpanderMouseOverForeground", typeof(Brush), typeof(LifelineControl), new PropertyMetadata(Brushes.Black));


        /// <summary>
        ///     Dependency property used to contain the brush for the expander button when the mouse is pressed in it in instances
        ///     of <see cref="LifelineControl" />.
        /// </summary>
        public static readonly DependencyProperty ExpanderPressedForegroundProperty = DependencyProperty.Register("ExpanderPressedForeground", typeof(Brush), typeof(LifelineControl), new PropertyMetadata(Brushes.Black));


        /// <summary>
        ///     Dependency property used to contain the brush for the Mini Report Area
        ///     of <see cref="LifelineControl" />.
        /// </summary>
        public static readonly DependencyProperty MiniReportBackgroundProperty = DependencyProperty.Register("MiniReportBackgroundProperty", typeof(Brush), typeof(LifelineControl), new PropertyMetadata(Brushes.Black));

        /// <summary>
        ///     Dependency property used to contain the brush for the Mini Report Area
        ///     of <see cref="LifelineControl" />.
        /// </summary>
        public static readonly DependencyProperty ControlBackgroundProperty = DependencyProperty.Register("ControlBackgroundProperty", typeof(Brush), typeof(LifelineControl), new PropertyMetadata(Brushes.Black));

        /// <summary>
        ///     Dependency property used to contain the value of the zoom factor in instances of <see cref="AtlasTimelineGrid" />.
        /// </summary>
        public static readonly DependencyProperty ZoomProperty = DependencyProperty.Register("Zoom", typeof(double), typeof(LifelineControl), new PropertyMetadata(1.0));


        private readonly Random _random = new Random(SeedRandom.Next(int.MaxValue));
        private readonly Color _whiteColor = Colors.White;


        /// <summary>
        ///     Initializes a new instance of <see cref="LifelineControl" />.
        /// </summary>
        public LifelineControl()
        {
            InitializeComponent();

            GenerateRandomColors();
            ZoomInCommand = new DelegateCommand<double?>(ZoomInCommand_Executed, ZoomInCommand_CanExecute);
            ZoomOutCommand = new DelegateCommand<double?>(ZoomOutCommand_Executed, ZoomOutCommand_CanExecute);

        }

        private bool ZoomInCommand_CanExecute(double? arg)
        {
            return true;
        }

        private void ZoomInCommand_Executed(double? obj)
        {
            TimelineLifelines.ZoomInCommand.Execute(obj);
        }

        private bool ZoomOutCommand_CanExecute(double? arg)
        {
            return true;
        }

        private void ZoomOutCommand_Executed(double? obj)
        {
            TimelineLifelines.ZoomOutCommand.Execute(obj);
        }

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


        public ICommand ZoomOutCommand
        {
            get { return (ICommand)GetValue(ZoomOutCommandProperty); }
            set { SetValue(ZoomOutCommandProperty, value); }
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
        ///     Gets or sets the brush for the expander button when the mouse is over it.
        /// </summary>
        public Brush ExpanderMouseOverForeground
        {
            get { return (Brush)GetValue(ExpanderMouseOverForegroundProperty); }
            set { SetValue(ExpanderMouseOverForegroundProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the brush for the expander button when the mouse is pressed over it.
        /// </summary>
        public Brush ExpanderPressedForeground
        {
            get { return (Brush)GetValue(ExpanderPressedForegroundProperty); }
            set { SetValue(ExpanderPressedForegroundProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the brush for the Mini Report Area.
        /// </summary>
        public Brush MiniReportBackground
        {
            get { return (Brush)GetValue(MiniReportBackgroundProperty); }
            set { SetValue(MiniReportBackgroundProperty, value); }
        }
        /// <summary>
        ///     Gets or sets the brush for the Mini Report Area.
        /// </summary>
        public Brush ControlBackground
        {
            get { return (Brush)GetValue(ControlBackgroundProperty); }
            set { SetValue(ControlBackgroundProperty, value); }
        }

        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty ZoomInCommandProperty = DependencyProperty.Register("ZoomInCommand", typeof(ICommand), typeof(LifelineControl), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property used to contain the string value containing the text to be displayed by the Add button.
        /// </summary>
        public static readonly DependencyProperty ZoomOutCommandProperty = DependencyProperty.Register("ZoomOutCommand", typeof(ICommand), typeof(LifelineControl), new PropertyMetadata(null));


        protected readonly ResourceDictionary ColorResources = new ResourceDictionary
        {
            Source = new Uri("/Atlas.UIControls;component/Assets/GaussChart.xaml", UriKind.RelativeOrAbsolute)
        };

        private void GenerateRandomColors()
        {
            var element = DataContext as ITreeNode;

            var collection = (ObservableCollection<ResourceDictionary>)ColorResources["Gradients"];
            LinearGradientBrush gradientColor = null;
            if (collection != null)
            {
                int index = _random.Next(0, collection.Count);

                 gradientColor = ((LinearGradientBrush) ((ResourceDictionary) collection[index])["Brush" + (index + 1).ToString()]);
            }
          

            byte backgroundRed = (byte)((_random.Next(BackgroundMinColor, BackgroundMaxColor)+_whiteColor.R)/1.9),
                backgroundGreen = (byte)((_random.Next(BackgroundMinColor, BackgroundMaxColor) + _whiteColor.G) / 1.9),
                backgroundBlue = (byte)((_random.Next(BackgroundMinColor, BackgroundMaxColor) + _whiteColor.B) / 1.9);


            //  if (element != null)
            //  {
            //      if (element.BackgroundColorBrush == null)
            //          element.BackgroundColorBrush = new SolidColorBrush(Color.FromRgb(backgroundRed, backgroundGreen, backgroundBlue));


            //      ControlBackground = element.BackgroundColorBrush;

            //  }
            //else


            byte foregroundRed = (byte)(backgroundRed / 2),
              foregroundGreen = (byte)(backgroundGreen / 2),
              foregroundBlue = (byte)(backgroundBlue / 2);

            byte mouseOverForegroundRed = (byte)(backgroundRed / 1.3),
                mouseOverForegroundGreen = (byte)(backgroundGreen / 1.3),
                mouseOverForegroundBlue = (byte)(backgroundBlue / 1.3);

            byte pressedRed = (byte)(backgroundRed / 2),
                pressedGreen = (byte)(backgroundGreen / 2),
                pressedBluen = (byte)(backgroundBlue / 2);

            int miniareaRed = (int)(backgroundRed / 0.85),
                miniareaGreen = (int)(backgroundGreen / 0.85),
                miniareaBlue = (int)(backgroundBlue / 0.85);

            double factor = 0.85;

            while (miniareaRed > 255 || miniareaGreen > 255 || miniareaBlue > 255)
            {
                factor += 0.01;
                miniareaRed = (int)(backgroundRed / factor);
                miniareaGreen = (int)(backgroundGreen / factor);
                miniareaBlue = (int)(backgroundBlue / factor);
            }

          
            if(gradientColor!=null)
                ControlBackground = gradientColor;
            else
            ControlBackground = new SolidColorBrush(Color.FromRgb(backgroundRed, backgroundGreen, backgroundBlue));

            Foreground = new SolidColorBrush(Color.FromRgb(foregroundRed, foregroundGreen, foregroundBlue));
            ExpanderMouseOverForeground = new SolidColorBrush(Color.FromRgb(mouseOverForegroundRed, mouseOverForegroundGreen, mouseOverForegroundBlue));
            ExpanderPressedForeground = new SolidColorBrush(Color.FromRgb(pressedRed, pressedGreen, pressedBluen));
            MiniReportBackground = new SolidColorBrush(Color.FromRgb((byte)miniareaRed, (byte)miniareaGreen, (byte)miniareaBlue));

        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            
        }

        private void LifelineControl_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //if ((DataContext as ITreeNode).Name.Contains("#2")&& !e.NewSize.IsEmpty
            //    && e.NewSize.Width > this.VisualOffset.Y)
            //{
            //    var shit = NameDockPanel.ActualWidth;
            //    var crap = e.NewSize.Width;
            //    NameDockPanel.Width = e.NewSize.Width - this.VisualOffset.Y;
            //}
            //  UpdateTimelineGridsSize(e.NewSize.Width);
            //if (!this.DesiredSize.IsEmpty&&!e.NewSize.IsEmpty && e.NewSize.Width > this.VisualOffset.Y && this.VisualOffset.Y > 0 
            //    &&this.DesiredSize.Width<e.NewSize.Width)
            //{
               

            //    //period.Starts = lifeline.Start;
            //    //period.Ends = lifeline.End;
            //    Width = this.DesiredSize.Width;
              
            //}
        }

        //private void UpdateTimelineGridsSize(double width)
        //{
        //    UpdateTimelineGridsSize(width, Scale);
        //}

        //private void UpdateTimelineGridsSize(double width, DateTimeScale scale)
        //{
        //   // TimelineHeader.UpdateSize(width, scale);
        //    TimelineLifelines.UpdateSize(width, scale);

        //    CommandManager.InvalidateRequerySuggested();
        //}
        private void Lifelines_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!_isControlPressed)
                return;

            //if (e.Delta > 0 && _zoomInCommand.CanExecute(ActualWidth))
            //    _zoomInCommand.Execute(ActualWidth);
            //else if (e.Delta < 0 && _zoomOutCommand.CanExecute(ActualWidth))
            //    _zoomOutCommand.Execute(ActualWidth);

            e.Handled = true;
        }
        private void Lifelines_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Signal to start dragging
            _isDragging = true;

            // Record the starting dragging point
            _mouseOffset = e.GetPosition(TimelineLifelines);

            // Start capturing the mouse
            TimelineLifelines.CaptureMouse();
        }

        private void Lifelines_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Signal to end dragging
            _isDragging = false;

            // Quit capturing mouse 
            TimelineLifelines.ReleaseMouseCapture();
        }

        private void Lifelines_OnMouseMove(object sender, MouseEventArgs e)
        {
            
            if (!_isDragging)
                return;

            // Get the mouse position relative to the Timeline. 
            Point point = e.GetPosition(TimelineLifelines);

            double deltaX = _mouseOffset.X - point.X;
            double deltaY = _mouseOffset.Y - point.Y;

            if (deltaX.Equals(0.0) && deltaY.Equals(0.0))
                return;

            //HeaderScrollViewer.ScrollToHorizontalOffset(LifelineScrollViewer.HorizontalOffset + deltaX);
            //LifelineScrollViewer.ScrollToHorizontalOffset(LifelineScrollViewer.HorizontalOffset + deltaX);
            //LifelineScrollViewer.ScrollToVerticalOffset(LifelineScrollViewer.VerticalOffset + deltaY);
        }

        private void LifelineControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ToolTip = (DataContext as ITreeNode).Start.ToShortDateString() + " - " + (DataContext as ITreeNode).End.ToShortDateString();
                if (ActualWidth < 80)
                    ToolTip = (DataContext as ITreeNode).Name + "  " + (DataContext as ITreeNode).Start.ToShortDateString() + " - " + (DataContext as ITreeNode).End.ToShortDateString();

               
            }

        }


       
    }
}