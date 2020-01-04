using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompanyName.Atlas.Contracts.Presentation.Visuals
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class InteractionWindow:Window
    {
       
        private ThicknessAnimation slideOut;
        private DoubleAnimation fadeOut;
         public ConfirmLayout ConfirmLayout { get; set; }

       
        public InteractionWindow()
        {
            
            
            InitializeComponent();

            ConfirmLayout = new ConfirmLayout()
            {
                Name = "ConfirmLayout",
                PlacementTarget = this,
                PopupAnimation = PopupAnimation.Slide,
                StaysOpen = false,
                Placement = PlacementMode.MousePoint
            };
           
            ConfirmLayout.Closed += ConfirmLayoutOnClosed;
            //if (!ConfirmLayout.IsOpen)
            //    ConfirmLayout.IsOpen = true;
            //ConfirmLayout.CaptureMouse();
            //ConfirmLayout.Focus();

            fadeOut = new DoubleAnimation();
            fadeOut.To = 0;
            fadeOut.Duration = TimeSpan.FromSeconds(0.5);
            fadeOut.EasingFunction = new CubicEase(){EasingMode = EasingMode.EaseIn};
            //fadeOut.Completed += FadeOutOnCompleted;

            slideOut = new ThicknessAnimation();
            slideOut.To = new Thickness(600,0,0,0);
            slideOut.Duration = TimeSpan.FromSeconds(0.5);
            slideOut.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseIn };
            slideOut.Completed += FadeOutOnCompleted;
            //fadeOut.BeginTime = new TimeSpan(0,0,0,0,0);
            //fadeOut.EasingFunction = new CubicEase();
            //EasingMode.EaseIn;// = new TimeSpan(0, 0, 0, 0, 35); 
        }

        private void ConfirmLayoutOnClosed(object sender, EventArgs eventArgs)
        {
            Close();
        }

        public void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

            
            
            
            ((Border)Template.FindName("InteractBorder", this)).BeginAnimation(Border.OpacityProperty, fadeOut);
            ((Border)Template.FindName("InteractBorder", this)).BeginAnimation(Border.MarginProperty, slideOut);

          
          


        }

        public void FadeOutOnCompleted(object sender, EventArgs eventArgs)
        {
            Close();
        }
    }
}
