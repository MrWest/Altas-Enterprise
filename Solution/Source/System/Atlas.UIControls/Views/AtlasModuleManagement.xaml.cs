using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Implementation.Modularity;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace CompanyName.Atlas.UIControls.Views
{
    /// <summary>
    /// Interaction logic for AtlasModuleManagement.xaml
    /// </summary>
    public partial class AtlasModuleManagement : UserControl, IView
    {
        /// <summary>
        /// Dependency property to contain the command that is executed by the Delete Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty DeleteButtonCommandProperty = DependencyProperty.Register("DeleteButtonCommand", typeof(ICommand), typeof(AtlasModuleManagement), new PropertyMetadata(null));
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty AddButtonCommandProperty = DependencyProperty.Register("AddButtonCommand", typeof(ICommand), typeof(AtlasModuleManagement), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property used to contain the tooltip for the Minimize button of windows of type <see cref="AtlasWindow"/>.
        /// </summary>
        public static readonly DependencyProperty ModulePathProperty = DependencyProperty.Register("ModulePath", typeof(string), typeof(AtlasModuleManagement), new PropertyMetadata(null));
        
        public AtlasModuleManagement()
        {
            InitializeComponent();
            DeleteButtonCommand = new DelegateCommand<ModuleInfo>(DeleteMethod,CanDeleteMethod);
            AddButtonCommand = new DelegateCommand<ModuleInfo>(ExecuteMethod, CanExecuteMethod);
            Raised+=OnRaised;
        }

        private bool CanExecuteMethod(ModuleInfo arg)
        {
            return true;
        }

        private void ExecuteMethod(ModuleInfo moduleInfo)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Easy Management files (*.dll)|*.dll|All files (*.*)|*.*";
            openFile.FileOk += new System.ComponentModel.CancelEventHandler(openFileDialog1_FileOk);

            openFile.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
              try
            {
                var fi = new FileInfo(((OpenFileDialog)sender).FileName);
           
            fi.CopyTo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.Combine( ModulePath,((OpenFileDialog)sender).SafeFileName)));
            //blur effect over atlaswindow
            var mainWindow = ServiceLocator.Current.GetInstance(typeof(Window));
            ((IModuleCatalog)mainWindow.GetType().GetProperty("ModuleCatalog").GetValue(mainWindow)).AddModule(new ModuleInfo(((OpenFileDialog)sender).SafeFileName, "Atlas Module"));
                mainWindow.GetType().GetMethod("Notify").Invoke(mainWindow, null);
                ((CommonBootstrapper)mainWindow.GetType().GetProperty("Bootstrapper").GetValue(mainWindow)).GoInitializeModules();//AddModule(new ModuleInfo(((OpenFileDialog)sender).SafeFileName, "Atlas Module"));  
            }
            catch (Exception exception)
            {

                IConfirmation confirmation = new Confirmation { Content = exception.Message, Title = "Error" };
                confirmation = new ConfirmationWithReason<IConfirmation>(confirmation) { Reason = NotificationReason.Question };


                Notify(new InteractionRequestedEventArgs(confirmation, null));
            }
           
        }

        private void OnRaised(object sender, InteractionRequestedEventArgs interactionRequestedEventArgs)
        {
           return;
        }

        private bool CanDeleteMethod(ModuleInfo moduleInfo)
        {
           return System.IO.File.Exists(moduleInfo.Ref);
        }

        private void DeleteMethod(ModuleInfo moduleInfo)
        {

            IConfirmation confirmation = Confirm("Desea realmente desinstalar este Modulo?..", "Confirmacion");

            if (!confirmation.Confirmed)
                return;
            try
            {
                var substring = moduleInfo.Ref.Substring(0, 8);

                var path = moduleInfo.Ref.Replace(substring, "");
                 substring = substring.Substring(6, 1);
                 path = path.Replace(substring, @"\");

                var fi = new FileInfo(path);
                fi.Delete();
            }
            catch (Exception e)
            {

                confirmation = new Confirmation { Content = e.Message, Title = "Error" };
                confirmation = new ConfirmationWithReason<IConfirmation>(confirmation) { Reason = NotificationReason.Question };


                Notify(new InteractionRequestedEventArgs(confirmation, null));
            }

            
        }

        /// <summary>
        /// Displays a message box using the content and title from the confirmation object contained in the given event arguments and
        /// places the interaction's result in the same given event arguments.
        /// </summary>
        /// <param name="view">The view being extended.</param>
        /// <param name="eventArgs">
        /// The interaction requested event arguments where the data to be used in the Message Box is.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="view"/> or <paramref name="eventArgs"/> is null.
        /// </exception>
        public static bool Confirm(InteractionRequestedEventArgs eventArgs)
        {
            if (eventArgs == null)
                throw new ArgumentNullException("eventArgs");

            return InteractionRequestHelpers.Confirm(eventArgs);
        }

        /// <summary>
        /// Displays a message box using the content and title from the notification object contained in the given event arguments and
        /// places the interaction's result in the same given event arguments.
        /// </summary>
        /// <param name="view">The view being extended.</param>
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

            return InteractionRequestHelpers.Notify(eventArgs);
        }
        /// <summary>
        /// Requests to interact to the user in order to get confirmation about something.
        /// </summary>
        /// <param name="text">The text explaining what is to be confirmed.</param>
        /// <param name="title">A title for the confirmation element with which the user will deal to confirm or not.</param>
        /// <returns>
        /// A <see cref="IConfirmation"/> object containing the user response, it's an interaction request context object used in these
        /// cases
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="text"/> or <paramref name="title"/> is null.
        /// </exception>
        protected virtual IConfirmation Confirm(string text, string title)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            if (title == null)
                throw new ArgumentNullException("title");

            IConfirmation confirmation = new Confirmation { Content = text, Title = title };
            confirmation = new ConfirmationWithReason<IConfirmation>(confirmation) { Reason = NotificationReason.Question };

            return Interact(confirmation);
        }
        /// <summary>
        /// Requests an interaction implying the given notification.
        /// </summary>
        /// <typeparam name="TNotification">The type of the notification involved in the interaction.</typeparam>
        /// <param name="notification">The actual notification that must be displayed to the user to establish the interaction.</param>
        /// <exception cref="ArgumentNullException"><paramref name="notification"/> is null.</exception>
        /// <returns>The given notification with the values possibly modified as result of the interaction.</returns>
        public TNotification Interact<TNotification>(TNotification notification) where TNotification : INotification
        {
            return Interact(notification, null);
        }
        /// <summary>
        /// Requests an interaction implying the given notification and a callback.
        /// </summary>
        /// <typeparam name="TNotification">The type of the notification involved in the interaction.</typeparam>
        /// <param name="notification">The actual notification that must be displayed to the user to establish the interaction.</param>
        /// <param name="callback">A callback that is to be executed when the interaction is finishing.</param>
        /// <exception cref="ArgumentNullException">Either <paramref name="notification"/> or <paramref name="callback"/> is null.</exception>
        /// <returns>The given notification with the values possibly modified as result of the interaction.</returns>
        public virtual TNotification Interact<TNotification>(TNotification notification, Action callback) where TNotification : INotification
        {
            if (Equals(notification, null))
                throw new ArgumentNullException("notification");

            Confirm( new InteractionRequestedEventArgs(notification, callback));
            return notification;
        }
        private EventHandlerManager<InteractionRequestedEventArgs> _raisedEventHandlers = new EventHandlerManager<InteractionRequestedEventArgs>();




        /// <summary>
        /// Fired when the interaction is needed.
        /// </summary>
        public virtual event EventHandler<InteractionRequestedEventArgs> Raised
        {
            add { _raisedEventHandlers += value; }
            remove { _raisedEventHandlers -= value; }
        }
        /// <summary>
        /// Gets or sets the command that is executed by the Delete Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public ICommand DeleteButtonCommand
        {
            get { return (ICommand)GetValue(DeleteButtonCommandProperty); }
            set { SetValue(DeleteButtonCommandProperty, value); }
        }
        /// <summary>
        /// Gets or sets the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public ICommand AddButtonCommand
        {
            get { return (ICommand)GetValue(AddButtonCommandProperty); }
            set { SetValue(AddButtonCommandProperty, value); }
        }

        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public string ModulePath
        {
            get { return (string)GetValue(ModulePathProperty); }
            set { SetValue(ModulePathProperty, value); }
        }
        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
           // throw new NotImplementedException();
        }

        private void ModulesDataGrid_OnCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
          //  throw new NotImplementedException();
        }
    }
}
