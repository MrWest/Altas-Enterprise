using System;
using System.Windows;

namespace CompanyName.Atlas.UIControls
{
    public class StatusBarConfirmationView:ConfirmationView
    {
       public StatusBarConfirmationView()
       {
           DefaultStyleKey = typeof(StatusBarConfirmationView);
            Loaded+=OnLoaded;
          
       }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            IsCollapsed = false;
        }
    }
}