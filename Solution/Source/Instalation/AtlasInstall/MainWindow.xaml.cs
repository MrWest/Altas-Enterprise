using AtlasInstall.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AtlasInstall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty InstallStateProperty = DependencyProperty.Register("InstallState", typeof(InstallState), typeof(MainWindow), new PropertyMetadata(InstallState.Welcome));
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty LicenseProperty = DependencyProperty.Register("License", typeof(string), typeof(MainWindow), new PropertyMetadata(null));
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ButtonTextProperty = DependencyProperty.Register("ButtonText", typeof(string), typeof(MainWindow), new PropertyMetadata("Next"));
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty DetailsProperty = DependencyProperty.Register("Details", typeof(string), typeof(MainWindow), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty AgreementProperty = DependencyProperty.Register("Agreement", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty CurrentUserProperty = DependencyProperty.Register("CurrentUser", typeof(bool), typeof(MainWindow), new PropertyMetadata(true));
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty AllUsersProperty = DependencyProperty.Register("AllUsers", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty DesktopProperty = DependencyProperty.Register("Desktop", typeof(bool), typeof(MainWindow), new PropertyMetadata(true));
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty StartMenuProperty = DependencyProperty.Register("StartMenu", typeof(bool), typeof(MainWindow), new PropertyMetadata(true));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register("Progress", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty IsInstalledProperty = DependencyProperty.Register("IsInstalled", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

        public BackgroundWorker backgroundWorker;
        public MainWindow()
        {
            InitializeComponent();
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, OnCloseWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, OnMaximizeWindow, OnCanResizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, OnMinimizeWindow, OnCanMinimizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, OnRestoreWindow, OnCanResizeWindow));

            CommandBindings.Add(new CommandBinding(NavigationCommands.NextPage, OnNextPage,  OnCanNextPage));
            CommandBindings.Add(new CommandBinding(NavigationCommands.PreviousPage, OnPreviousPage, OnCanPreviousPage));


            IsInstalled = new InstallProcess().ExistUninstaller();

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += OnDoWork;
            backgroundWorker.ProgressChanged += OnPrgressChanged;
            backgroundWorker.RunWorkerCompleted += OnRunWorkerCompleted;

            backgroundWorker.WorkerReportsProgress = true;
        }

        private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.InstallState = InstallState.Finish;
            ButtonText = "Exit"; ;
        }

        private void OnPrgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage;
            Details += "\n"+ e.UserState.ToString();
        }

        private void OnDoWork(object sender, DoWorkEventArgs e)
        {
            InstallProcess installProcess = new InstallProcess(((IList<bool>)e.Argument)[0], ((IList<bool>)e.Argument)[0], ((IList<bool>)e.Argument)[2]);

            if (!((IList<bool>)e.Argument)[3])
                installProcess.Install(this);
            else
                installProcess.Uninstall(this);
           
        }

        private void OnCanPreviousPage(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.InstallState == InstallState.Agreement || this.InstallState == InstallState.Miscellaneuos;

        }

        private void OnCanNextPage(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute =  (this.InstallState != InstallState.Agreement || Agreement);
        }

        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public InstallState InstallState
        {
            get { return (InstallState)GetValue(InstallStateProperty); }
            set { SetValue(InstallStateProperty, value); }
        }
        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public string License
        {
            get { return (string)GetValue(LicenseProperty); }
            set { SetValue(LicenseProperty, value); }
        }

        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }
        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public string Details
        {
            get { return (string)GetValue(DetailsProperty); }
            set { SetValue(DetailsProperty, value); }
        }

        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public bool IsInstalled
        {
            get { return (bool)GetValue(IsInstalledProperty); }
            set { SetValue(IsInstalledProperty, value); }
        }

        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public bool Agreement
        {
            get { return (bool)GetValue(AgreementProperty); }
            set { SetValue(AgreementProperty, value); }
        }  ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public bool CurrentUser
        {
            get { return (bool)GetValue(CurrentUserProperty); }
            set { SetValue(CurrentUserProperty, value); }
        }  ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public bool AllUsers
        {
            get { return (bool)GetValue(AllUsersProperty); }
            set { SetValue(AllUsersProperty, value); }
        }
        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public bool Desktop
        {
            get { return (bool)GetValue(DesktopProperty); }
            set { SetValue(DesktopProperty, value); }
        } 
        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public bool StartMenu
        {
            get { return (bool)GetValue(StartMenuProperty); }
            set { SetValue(StartMenuProperty, value); }
        }
        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public int Progress
        {
            get { return (int)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }
        private void OnCanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResizeMode == ResizeMode.CanResize || ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private void OnCanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResizeMode != ResizeMode.NoResize;
        }

        private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void OnMaximizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void OnMinimizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void OnRestoreWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }

        private void OnNextPage(object target, ExecutedRoutedEventArgs e)
        {
            if (this.InstallState == InstallState.Welcome)
            {
                if(!IsInstalled)
                {
                    string fileName = Environment.CurrentDirectory + "\\InstallFiles\\Misc\\License.rtf";


                    TextRange range;
                    //FileStream fStream;
                    if (File.Exists(fileName))
                    {
                        var richTextBox = (RichTextBox)Template.FindName("licenseRTb", this);
                        range = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                      //  fStream = new FileStream(fileName, System.IO.FileMode.Open);
                        StreamReader sr = new StreamReader(fileName);
                        range.Load(sr.BaseStream, DataFormats.Rtf);
                        // fStream.Close();
                        sr.Close();
                    }

                    this.InstallState = InstallState.Agreement;
                }
                else
                {
                    this.InstallState = InstallState.Miscellaneuos;
                    ButtonText = "Uninstall";
                }
               
            }
            else
            if (this.InstallState == InstallState.Agreement)
            {
                this.InstallState = InstallState.Miscellaneuos;
                ButtonText = "Install";
            }
            else
            if (this.InstallState == InstallState.Miscellaneuos)
            {
                this.InstallState = InstallState.Install;
                ButtonText = "Cancel";

                //gathering values to pass as installation parameters
                IList<bool> values = new List<bool>();

                values.Add(Desktop);
                values.Add(StartMenu);
                values.Add(AllUsers);
                values.Add(IsInstalled);


                backgroundWorker.RunWorkerAsync(values);
            }
            else
            if (this.InstallState == InstallState.Finish)
            {
                Close();
            }

        }

        private void OnPreviousPage(object target, ExecutedRoutedEventArgs e)
        {
            if (this.InstallState == InstallState.Agreement)
                this.InstallState = InstallState.Welcome;
            else if (this.InstallState == InstallState.Miscellaneuos)
            {
                if (!IsInstalled)
                    this.InstallState = InstallState.Agreement;
                else
                    this.InstallState = InstallState.Welcome;

                ButtonText = "Next";
            }
           
              
        }

        
    public bool IsUserAdministrator()
    {
        bool isAdmin;
        try
        {
            WindowsIdentity user = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(user);
            isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        catch (UnauthorizedAccessException ex)
        {
            isAdmin = false;
        }
        catch (Exception ex)
        {
            isAdmin = false;
        }
        return isAdmin;
    }
}
}
