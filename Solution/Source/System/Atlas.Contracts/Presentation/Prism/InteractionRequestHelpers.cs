using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Effects;
using CompanyName.Atlas.Contracts.Presentation.Utility;
using CompanyName.Atlas.Contracts.Presentation.Visuals;
using CompanyName.Atlas.Contracts.Presentation.Visuals.Structures;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.ServiceLocation;


namespace CompanyName.Atlas.Contracts.Presentation.Prism
{
    /// <summary>
    /// This class provides helper methods to aid the process of establishing interactions between the user and view
    /// models, for confirmation, notification or any other kind of custom interaction.
    /// </summary>
    // TODO: Test this!
    public static class InteractionRequestHelpers
    {
        /// <summary>
        /// Displays a message box using the content and title from the confirmation object contained in the given event arguments and
        /// places the interaction's result in the same given event arguments.
        /// </summary>
        /// <param name="eventArgs">
        /// The interaction requested event arguments where the data to be used in the Message Box is.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="eventArgs"/> is null.
        /// </exception>
        public  static bool Confirm(InteractionRequestedEventArgs eventArgs)
        {
            if (eventArgs == null)
                throw new ArgumentNullException("eventArgs");

            // Construct the text and title of the Message Box
            var confirmation = eventArgs.Context as IConfirmation;
            if (confirmation == null)
                return false;

            string text = confirmation.Content.ToString(), title = confirmation.Title;

            // Attempt to extract the icon if possible
            var icon = MessageBoxImage.Question;

            var notificationWithReason = confirmation as INotificationWithReason;
            if (notificationWithReason != null)
                switch (notificationWithReason.Reason)
                {
                    case NotificationReason.Error:
                        icon = MessageBoxImage.Error;
                        break;
                    case NotificationReason.Question:
                        icon = MessageBoxImage.Question;
                        break;
                    case NotificationReason.Warning:
                        icon = MessageBoxImage.Warning;
                        break;
                    default:
                        icon = MessageBoxImage.Information;
                        break;
                }

            //blur effect over atlaswindow
            var mainWindow = (Window)ServiceLocator.Current.GetInstance(typeof(Window));
            BlurEffect blurEffect = new BlurEffect();
            blurEffect.Radius = 5;
            mainWindow.Effect = blurEffect;
           // var crap = mainWindow.Name;
            var response = new InteractionStructure() {Text = text, Title = title,Confirmation = confirmation };
            
            //mainWindow.ConfirmLayout.DataContext = response;

            //mainWindow.ConfirmLayout.ContinueClicked = new TaskCompletionSource<object>();
            //mainWindow.ConfirmLayout.Closed+=InteractionOnClosed;

            //if (!mainWindow.ConfirmLayout.IsOpen)
            //    mainWindow.ConfirmLayout.IsOpen = true;
            //mainWindow.ConfirmLayout.CaptureMouse();
            //mainWindow.ConfirmLayout.Focus();
            //var shit = mainWindow.ConfirmLayout.Parent;

            var interaction = new InteractionWindow()
            {
                DataContext = response,
                Owner = mainWindow,
                WindowStartupLocation = WindowStartupLocation.Manual,
                RenderSize = mainWindow.RenderSize,
                ResizeMode = ResizeMode.NoResize
                


            };

            interaction.ConfirmLayout.IsOpen = true;
           
            interaction.ShowDialog();
          
            
       //    await mainWindow.ConfirmLayout.ContinueClicked.Task;
         



          //  await WaitAsync(mainWindow);


            //interaction.FadeOutOnCompleted(interaction, eventArgs);
            //interaction.BringIntoView(mainWindow.RestoreBounds);
            


            // Display the Message Box to the user with information gathered
           // var response = MessageBox.Show(text, title, MessageBoxButton.YesNo, icon);

            // remove blur effect
            mainWindow.Effect = null;

            // Set the response for the caller
           // confirmation.Confirmed = response == MessageBoxResult.Yes;
           // confirmation.Confirmed = response.Confirmation.Confirmed;// == MessageBoxResult.Yes;

            // Finally call the specified callback (if any) with the context used in this interaction
            if (eventArgs.Callback != null)
                eventArgs.Callback();

            return true;
        }
      //  private static TaskCompletionSource<object> continueClicked;



        private static AsyncEventListener itChanged = new AsyncEventListener();
        private static async Task WaitAsync(ConfirmableWindow mainWindow)
        {
            
            mainWindow.ConfirmLayout.Closed += itChanged.Listen;

            // ... make it change ...

            await itChanged.Successfully;
            mainWindow.ConfirmLayout.Closed -= itChanged.Listen;
        }

        private static void InteractionOnClosed(object sender, EventArgs eventArgs)
        {

            var shit =((ConfirmLayout) (sender));
            //((ConfirmLayout) (sender)).Dispatcher.InvokeShutdown();
        }

        /// <summary>
        /// Displays a message box using the content and title from the notification object contained in the given event arguments and
        /// places the interaction's result in the same given event arguments.
        /// </summary>
        /// <param name="eventArgs">
        /// The interaction requested event arguments where the data to be used in the Message Box is.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="eventArgs"/> is null.
        /// </exception>
        public static bool Notify(InteractionRequestedEventArgs eventArgs)
        {
            if (eventArgs == null)
                throw new ArgumentNullException("eventArgs");

            // Construct the text and title of the Message Box
            var notification = eventArgs.Context as INotification;
            if (notification == null)
                return false;

            string text = notification.Content.ToString(), title = notification.Title;

            // Attempt to extract the icon if possible
            var icon = MessageBoxImage.Information;

            var notificationWithReason = notification as INotificationWithReason;
            if (notificationWithReason != null)
                switch (notificationWithReason.Reason)
                {
                    case NotificationReason.Error:
                        icon = MessageBoxImage.Error;
                        break;
                    case NotificationReason.Question:
                        icon = MessageBoxImage.Question;
                        break;
                    case NotificationReason.Warning:
                        icon = MessageBoxImage.Warning;
                        break;
                    default:
                        icon = MessageBoxImage.Information;
                        break;
                }

            // Display the Message Box to the user with information gathered
            MessageBox.Show(text, title, MessageBoxButton.OK, icon);

            // Finally call the specified callback (if any) with the context used in this interaction
            if (eventArgs.Callback != null)
                eventArgs.Callback();

            return true;
        }
    }
}
