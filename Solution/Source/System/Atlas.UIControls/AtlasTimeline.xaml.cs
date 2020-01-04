using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Features;
using CompanyName.Atlas.Contracts.Presentation.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    ///     Interaction logic for AtlasTimeline.xaml
    /// </summary>
    public partial class AtlasTimeline : IPrinttableContainer
    {
        /// <summary>
        ///     Dependency property contaning a value representing the date time scale instances of <see cref="AtlasTimeline" />
        ///     are representing.
        /// </summary>
        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register("Scale", typeof(DateTimeScale), typeof(AtlasTimeline), new PropertyMetadata(default(DateTimeScale), AtlasTimeline_OnScaleChanged));

        /// <summary>
        ///     Dependency property contaning a value representing the date time scale instances of <see cref="AtlasTimeline" />
        ///     are representing.
        /// </summary>
        public static readonly DependencyProperty PeriodProperty = DependencyProperty.Register("Period", typeof(IPeriodPresenter), typeof(AtlasTimeline), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the current view where there is currently in the budget component items tab controls of a
        /// <see cref="PlanningExecutionView"/>.
        /// </summary>
        public static readonly DependencyProperty ViewProperty = DependencyProperty.Register("View", typeof(object), typeof(AtlasTimeline), new PropertyMetadata(null,ViewPropertyChangedCallback));

        private static void ViewPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {

            ((AtlasTimeline)dependencyObject).TimelineHeader.UpdateTimeline();
            ((AtlasTimeline)dependencyObject).TimelineLifelines.UpdateTimeline();
        }

        public void UpdateTimeLine()
        {
            //if (!_BackgroundWorker.IsBusy)
            //{
            //   // StatusBarServices.ForceSignalLoading();
            //    _BackgroundWorker.RunWorkerAsync();
            //}

            //TimelineHeader.UpdateTimeline();
            //TimelineLifelines.UpdateTimeline();

            InternalSignalText();


        }
        private readonly CompositeCommand _zoomInCommand, _zoomOutCommand;

        private bool _isControlPressed;
        private bool _isDragging;
        private Point _mouseOffset;

        private IStatusBarServices _statusBarServices;
        
        protected IStatusBarServices StatusBarServices
        {
            get { return _statusBarServices ?? (_statusBarServices = ServiceLocator.Current.GetInstance<IStatusBarServices>()); }
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="AtlasTimeline" />.
        /// </summary>
        public AtlasTimeline()
        {
            InitializeComponent();
           
            _zoomInCommand = new CompositeCommand();
            _zoomInCommand.RegisterCommand(TimelineHeader.ZoomInCommand);
            _zoomInCommand.RegisterCommand(TimelineLifelines.ZoomInCommand);

            _zoomOutCommand = new CompositeCommand();
            _zoomOutCommand.RegisterCommand(TimelineHeader.ZoomOutCommand);
            _zoomOutCommand.RegisterCommand(TimelineLifelines.ZoomOutCommand);

            SizeChanged += AtlasTimeline_OnSizeChanged;
            Loaded +=OnLoaded;
            //_BackgroundWorker = new BackgroundWorker();
            //_BackgroundWorker.DoWork += UdateTimelineOnDoWork;
            //_BackgroundWorker.RunWorkerCompleted += UdateTimelineOnRunWorkerCompleted;
        }

        private async void InternalSignalText()
        {


          //  await AsyncSignalText().ContinueWith(Continues);


            TimelineHeader.UpdateTimeline();
            TimelineLifelines.UpdateTimeline();
        }
        private async void Continues(Task task)
        {

           
        }
        protected virtual  Task AsyncSignalText()
        {
            StatusBarServices.ForceSignalLoading();

              
            return new Task(Action);

        }

        private void Action()
        {
            
        }


        //private void UdateTimelineOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    StatusBarServices.SignalReady();
        //}

        //private void UdateTimelineOnDoWork(object sender, DoWorkEventArgs e)
        //{
        //    StatusBarServices.ForceSignalLoading();
        //}

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (!ya)
            {
                if (DataContext != null && DataContext.GetType().Implements(typeof(ITreeNode)))
                {
                    var talla = (ITreeNode)DataContext;
                    var period = ServiceLocator.Current.GetInstance<IPeriod>();
                    period.Starts = talla.Start;
                    period.Ends = talla.End;

                    TimelineHeader.Zoom = ActualWidth / (TimelineHeader.ColumnWidth * GetDateElements(TimelineHeader.Scale, period));
                    TimelineLifelines.Zoom = ActualWidth / (TimelineLifelines.ColumnWidth * GetDateElements(TimelineLifelines.Scale, period));

                    UpdateTimeLine();

                    ya = true;
                }

            }
        }

        private bool ya;
       


        /// <summary>
        ///     Gets or sets the date time scale the current <see cref="AtlasTimeline" /> is representing.
        /// </summary>
        public DateTimeScale Scale
        {
            get { return (DateTimeScale)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        /// <summary>
        ///     Gets or sets the date time scale the current <see cref="AtlasTimeline" /> is representing.
        /// </summary>
        public IPeriodPresenter Period 
        {
            get { return (IPeriodPresenter)GetValue(PeriodProperty); }
            set { SetValue(PeriodProperty, value); }
        }

        ///     Gets or sets the date time scale the current <see cref="AtlasTimeline" /> is representing.
        /// </summary>
        public object View
        {
            get { return (IPeriodPresenter)GetValue(ViewProperty); }
            set { SetValue(ViewProperty, value); }
        }


        private void UpdateTimelineGridsSize(double width)
        {
            UpdateTimelineGridsSize(width, TimelineHeader.Scale);
        }

        private void UpdateTimelineGridsSize(double width, DateTimeScale scale)
        {
            TimelineHeader.UpdateSize(width, scale);
            TimelineLifelines.UpdateSize(width, scale);

            CommandManager.InvalidateRequerySuggested();
        }

        
        private void ZoomInCommand_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _zoomInCommand != null && _zoomInCommand.CanExecute((double?)ActualWidth);
        }

        private void ZommInCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _zoomInCommand.Execute(ActualWidth);
        }

        private void ZoomOutCommand_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _zoomOutCommand != null && _zoomOutCommand.CanExecute(ActualWidth);
        }

        private void ZommOutCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _zoomOutCommand.Execute(ActualWidth);
        }

        private void MoveLeftCommand_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = HeaderScrollViewer != null && HeaderScrollViewer.HorizontalOffset > 0.0;
        }

        private void MoveLeftCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            HeaderScrollViewer.PageLeft();
            LifelineScrollViewer.PageLeft();
        }

        private void MoveRightCommand_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = HeaderScrollViewer != null && ActualWidth + HeaderScrollViewer.HorizontalOffset < TimelineHeader.ActualWidth;
        }

        private void MoveRightCommand_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            HeaderScrollViewer.PageRight();
            LifelineScrollViewer.PageRight();
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

            HeaderScrollViewer.ScrollToHorizontalOffset(LifelineScrollViewer.HorizontalOffset + deltaX);
            LifelineScrollViewer.ScrollToHorizontalOffset(LifelineScrollViewer.HorizontalOffset + deltaX);
            LifelineScrollViewer.ScrollToVerticalOffset(LifelineScrollViewer.VerticalOffset + deltaY);
        }

        private void Lifelines_OnKeyDown(object sender, KeyEventArgs e)
        {
            _isControlPressed = e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl;
        }

        private void Lifelines_OnKeyUp(object sender, KeyEventArgs e)
        {
            _isControlPressed = false;
        }

        private void Lifelines_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!_isControlPressed)
                return;

            if (e.Delta > 0 && _zoomInCommand.CanExecute(ActualWidth))
                _zoomInCommand.Execute(ActualWidth);
            else if (e.Delta < 0 && _zoomOutCommand.CanExecute(ActualWidth))
                _zoomOutCommand.Execute(ActualWidth);

            e.Handled = true;
        }

        private void AtlasTimeline_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateTimelineGridsSize(e.NewSize.Width);
            TimelineLifelines.UpdateTimeline();
        }

      
        private static void AtlasTimeline_OnScaleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                var timeline = (AtlasTimeline)d;              
             
                timeline.UpdateTimelineGridsSize(timeline.ActualWidth, (DateTimeScale)e.NewValue);
                timeline.TimelineLifelines.UpdateTimeline();
            }
            catch (InvalidCastException)
            {
            }
        }

      //  protected BackgroundWorker _BackgroundWorker;

      

        private void AtlasTimeline_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext!=null && DataContext.GetType().Implements(typeof(ITreeNode)))
            {
                var talla = (ITreeNode)DataContext;
                var period = ServiceLocator.Current.GetInstance<IPeriod>();
                period.Starts = talla.Start;
                period.Ends = talla.End;

                TimelineHeader.Zoom = ActualWidth / (TimelineHeader.ColumnWidth * GetDateElements(TimelineHeader.Scale,period)) ;
                TimelineLifelines.Zoom = ActualWidth / (TimelineLifelines.ColumnWidth * GetDateElements(TimelineLifelines.Scale,period));

                UpdateTimeLine();
               

            }

        }

        public int GetDateElements(DateTimeScale scale,IPeriod period)
        {
            Tuple<DateTime, DateTime> dates = Tuple.Create(period.Starts, period.Ends);
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

            return elements+1;
        }

        public void Print()
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {



                printDialog.PrintVisual(TimeLineBorder,
            "AtlasTimeline");

            }
        }
    }
}