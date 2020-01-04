using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CompanyName.Atlas.Contracts.Presentation.Visuals
{
    public class ConfirmLayout:Popup
    {
        private ThicknessAnimation slideOut;
        private DoubleAnimation fadeOut;

        public TaskCompletionSource<object> ContinueClicked { get; set; }
        public ConfirmLayout()
        {
            
            DefaultStyleKey = typeof(ConfirmLayout);
            Focusable = true;
            Focus();
            LostFocus+=OnLostFocus;

            // code creating the visual interface for the ConfirmLayout
            var _child = new StackPanel()
            {
                Background = Brushes.White,
                Orientation = System.Windows.Controls.Orientation.Horizontal,
                Height = 35
            };
            var _text = new TextBlock()
            {
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(10, 0, 10, 0)

            };

            
            _text.SetBinding(TextBlock.TextProperty,new Binding("Text"));
            var _stacpanel = new StackPanel()
            {
                Orientation = System.Windows.Controls.Orientation.Horizontal,
                Height = 35,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right
            };
            var _button = new Button()
            {
                Margin = new Thickness(10, 4, 10, 4),
                IsDefault = true,
                Content = Properties.Resources.Yes,
                CommandParameter = "Yes",
                Width = 54

            };
            _button.SetBinding(ButtonBase.CommandProperty, new Binding("YesNoCommand"));
            _button.Click += Button2OnClick;
           
            var _button2 = new Button()
            {
                Margin = new Thickness(10, 4, 10, 4),
                IsCancel = true,
                Content = Properties.Resources.No,
                Width = 54

            };
           _button2.SetBinding(ButtonBase.CommandProperty, new Binding("YesNoCommand"));
            _button2.Click+=Button2OnClick;

            //once created the elements they're added in order
            _stacpanel.Children.Add(_button);
            _stacpanel.Children.Add(_button2);

            _child.Children.Add(_text);
            _child.Children.Add(_stacpanel);

            Child = _child;

            
            Opened+=OnOpened;
            //Placement = PlacementMode.MousePoint;

            //Width = 70;
            //fadeOut = new DoubleAnimation();
            //fadeOut.To = 0;
            //fadeOut.Duration = TimeSpan.FromSeconds(0.5);
            //fadeOut.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseIn };
            ////fadeOut.Completed += FadeOutOnCompleted;

            //slideOut = new ThicknessAnimation();
            //slideOut.To = new Thickness(0, 0, -100, 0);
            //slideOut.Duration = TimeSpan.FromSeconds(0.5);
            //slideOut.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseIn };
            //slideOut.Completed += FadeOutOnCompleted;
        }

        private async void OnOpened(object sender, EventArgs eventArgs)
        {
         //   await ContinueClicked.Task;
        }


        public void Button2OnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            if (ContinueClicked != null)
                ContinueClicked.TrySetResult(null);

            IsOpen = false; 
        }

        private void OnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            IsOpen = false;
        }

        public void FadeOutOnCompleted(object sender, EventArgs eventArgs)
        {
            IsOpen = false;
        }
    }
}
