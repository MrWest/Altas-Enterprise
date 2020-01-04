using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Presentation.Transfer;
using CompanyName.Atlas.UIControls;
using CompanyName.Atlas.UIControls.Views;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for InvestmentExportPlattform.xaml
    /// </summary>
    public partial class InvestmentExportPlattform 
    {

        private ThicknessAnimation slideOut;
        private DoubleAnimation fadeOut;

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty TransferStateProperty = DependencyProperty.Register("TransferState", typeof(AtlasTransferWizardState), typeof(InvestmentExportPlattform));
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty NextCommandProperty = DependencyProperty.Register("NextCommand", typeof(ICommand), typeof(InvestmentExportPlattform), new PropertyMetadata(null));


        public InvestmentExportPlattform()
        {
            InitializeComponent();
            fadeOut = new DoubleAnimation();
            fadeOut.To = 0;
            fadeOut.Duration = TimeSpan.FromSeconds(0.5);
            fadeOut.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseIn };
            //fadeOut.Completed += FadeOutOnCompleted;

            slideOut = new ThicknessAnimation();
            slideOut.To = new Thickness(600, 0, 0, 0);
            slideOut.Duration = TimeSpan.FromSeconds(0.5);
            slideOut.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseIn };
            slideOut.Completed += FadeOutOnCompleted;
            //fadeOut.BeginTime = new TimeSpan(0,0,0,0,0);
            //fadeOut.EasingFunction = new CubicEase();
            //EasingMode.EaseIn;// = new TimeSpan(0, 0, 0, 0, 35); 
            NextCommand = new DelegateCommand(GoNext, CanGoNext);
            KeyDown += AtlasImportPlatform_OnKeyDown;
        }

        private void GoNext()
        {
            if (TransferState == AtlasTransferWizardState.Welcome)
            {
                TransferState = AtlasTransferWizardState.Select;
                NextCommand = new DelegateCommand(GoNext, CanGoNext);
            }
            else if (TransferState == AtlasTransferWizardState.Select)
            {
                TransferState = AtlasTransferWizardState.Path;
                NextCommand = new DelegateCommand(GoNext, CanGoNext2);
            }
            else //if (TransferState == AtlasTransferWizardState.Select)
            {

                var elements = ((ListBox)Template.FindName("ElementsListBox", this)).SelectedItems.Cast<IExportable>();
                var newDatabaseContext = new Db4ODatabaseContext(((TextBox)Template.FindName("PathTextBox", this)).Text);
                foreach (IExportable exportable in elements)
                {
                    exportable.Export(newDatabaseContext);
                }

                TransferState = AtlasTransferWizardState.Finish;
            }
        }

        private bool CanGoNext()
        {
            return (((ListBox)Template.FindName("ElementsListBox", this)).SelectedItem != null && TransferState == AtlasTransferWizardState.Select) || TransferState != AtlasTransferWizardState.Select;
        }

        private bool CanGoNext2()
        {
            string path = ((TextBox)Template.FindName("PathTextBox", this)).Text;
            if (path.Contains(System.IO.Path.VolumeSeparatorChar))
                return System.IO.Path.GetDirectoryName(path) != null;

            return false;
        }
        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public ICommand NextCommand
        {
            get { return (ICommand)GetValue(NextCommandProperty); }
            set { SetValue(NextCommandProperty, value); }
        }
        public AtlasTransferWizardState TransferState
        {
            get { return (AtlasTransferWizardState)GetValue(TransferStateProperty); }
            set { SetValue(TransferStateProperty, value); }
        }
        public void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {



            if (TransferState == AtlasTransferWizardState.Select)
            {
                TransferState = AtlasTransferWizardState.Welcome;
            }
            else if (TransferState == AtlasTransferWizardState.Path)
            {
                TransferState = AtlasTransferWizardState.Select;
            }
            else //if (TransferState == AtlasTransferWizardState.Select)
            {
                TransferState = AtlasTransferWizardState.Path;
            }






        }

        public void FadeOutOnCompleted(object sender, EventArgs eventArgs)
        {
            Close();
        }

        private void ButtonBase_OnClick2(object sender, RoutedEventArgs e)
        {
            if (TransferState == AtlasTransferWizardState.Welcome)
            {
                TransferState = AtlasTransferWizardState.Select;
            }
            else //if (TransferState == AtlasTransferWizardState.Select)
            {

                var elements = ((ListBox)Template.FindName("ElementsListBox", this)).SelectedItems.Cast<IExportable>();
                var newDatabaseContext = new Db4ODatabaseContext(
                    System.IO.Path.Combine(System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Desktop), "Export.adb"));
                foreach (IExportable exportable in elements)
                {
                    exportable.Export(newDatabaseContext);
                }

                TransferState = AtlasTransferWizardState.Finish;
            }

        }
        private void ButtonBase_OnClick3(object sender, RoutedEventArgs e)
        {
            ((Border)Template.FindName("ExportBorder", this)).BeginAnimation(Border.OpacityProperty, fadeOut);
            ((Border)Template.FindName("ExportBorder", this)).BeginAnimation(Border.MarginProperty, slideOut);

        }
        private void ButtonBase_OnClick4(object sender, RoutedEventArgs e)
        {
            SaveFileDialog openFileDialog = new SaveFileDialog();

            openFileDialog.Filter = "Atlas DataBase files (*.adb)|*.adb|All files (*.*)|*.*";
            openFileDialog.FileOk += OpenFileDialogOnFileOk;
            openFileDialog.ShowDialog(this);

        }

        private void OpenFileDialogOnFileOk(object sender, CancelEventArgs cancelEventArgs)
        {
            ((TextBox)Template.FindName("PathTextBox", this)).Text = ((SaveFileDialog)sender).FileName;
            NextCommand = new DelegateCommand(GoNext, CanGoNext2);

            // ((Button)Template.FindName("NextButton", this)).IsEnabled=File.Exists(((TextBox)Template.FindName("PathTextBox", this)).Text);
        }

        private void PathTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //OnPropertyChanged("FileExist");
            ((Button)Template.FindName("NextButton", this)).IsEnabled = File.Exists(((TextBox)Template.FindName("PathTextBox", this)).Text);
            NextCommand = new DelegateCommand(GoNext, CanGoNext2);
        }

        private void AtlasImportPlatform_OnLoaded(object sender, RoutedEventArgs e)
        {
            // ((Button)Template.FindName("NextButton", this)).IsEnabled = File.Exists(((TextBox)Template.FindName("PathTextBox", this)).Text);
        }

        private void AtlasImportPlatform_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                ButtonBase_OnClick3(sender, null);
        }


    }
}
