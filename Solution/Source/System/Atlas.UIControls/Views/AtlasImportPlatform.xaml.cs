using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Data;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Transfer;
using CompanyName.Atlas.UIControls.Annotations;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace CompanyName.Atlas.UIControls.Views
{
    /// <summary>
    /// Interaction logic for AtlasImportPlatform.xaml
    /// </summary>
    public partial class AtlasImportPlatform : Window, INotifyPropertyChanged
    {
        private ThicknessAnimation slideOut;
        private DoubleAnimation fadeOut;
        private DataBasePathContainer actualDatacontex = (DataBasePathContainer)ServiceLocator.Current.GetInstance(typeof(DataBasePathContainer));



        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty TransferStateProperty = DependencyProperty.Register("TransferState", typeof(AtlasTransferWizardState), typeof(AtlasImportPlatform));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty FileExistProperty = DependencyProperty.Register("FileExist", typeof(bool), typeof(AtlasImportPlatform));


        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty NextCommandProperty = DependencyProperty.Register("NextCommand", typeof(ICommand), typeof(AtlasImportPlatform), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty BackCommandProperty = DependencyProperty.Register("BackCommand", typeof(ICommand), typeof(AtlasImportPlatform), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register("CancelCommand", typeof(ICommand), typeof(AtlasImportPlatform), new PropertyMetadata(null));
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ExportableViewModelsProperty = DependencyProperty.Register(" ExportebleViewModels", typeof(IList<NamedCrudViewModel>), typeof(AtlasImportPlatform), new PropertyMetadata(null));

        public AtlasImportPlatform()
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

            NextCommand = new DelegateCommand(GoNext, CanGoNext);

            Loaded+=OnLoaded;


        }
        public IList<NamedCrudViewModel> ExportableViewModels
        {
            get { return (IList<NamedCrudViewModel>)GetValue(ExportableViewModelsProperty); }
            set
            {
                if (Equals(value, null))
                    return;
                SetValue(ExportableViewModelsProperty, value);
            }
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
          //  SetPlattform();
        }
        private void SetPlattform()
        {
            if (!Equals(ExportableViewModels, null))
            {
                var tabControl = (TabControl)this.Template.FindName("ImportTabControl", this);

                tabControl.Items.Clear();

                foreach (NamedCrudViewModel namedCrudViewModel in ExportableViewModels)
                {
                    if (!Equals(namedCrudViewModel.ViewModel, null))
                        namedCrudViewModel.ViewModel.Load();
                    var listbox = new ListBox()
                    {
                        ItemsSource = namedCrudViewModel.ViewModel.Items,
                        SelectionMode = SelectionMode.Multiple
                    };
                    listbox.SelectionChanged += ElementsListBox_OnSelectionChanged;

                    var checkBox = new CheckBox() { Margin = new Thickness(5, 0, 0, 0) };
                    checkBox.Tag = listbox;
                    checkBox.Checked+=CheckBoxOnChecked;
                    checkBox.Unchecked+=CheckBoxOnUnchecked;
                    var texblock = new TextBlock() {Text = namedCrudViewModel.Name, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center };
                    var dockPanel = new DockPanel() { Margin = new Thickness(0, 0, 10, 0) };
                    dockPanel.Children.Add(texblock);
                    dockPanel.Children.Add(checkBox);
                    tabControl.Items.Add(new TabItem()
                    {
                        Header = dockPanel,
                        Content = listbox
                    });
                }
            }
        }

        private void CheckBoxOnUnchecked(object sender, RoutedEventArgs routedEventArgs)
        {
            var checkBox = sender as CheckBox;

            if (checkBox != null && checkBox.Tag is ListBox)
            {
                (checkBox.Tag as ListBox).UnselectAll();
            }
        }

        private void CheckBoxOnChecked(object sender, RoutedEventArgs routedEventArgs)
        {
            var checkBox = sender as CheckBox;

            if (checkBox != null && checkBox.Tag is ListBox)
            {
                (checkBox.Tag as ListBox).SelectAll();
            }
        }

        private Button nextButton;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            nextButton = (Button)this.Template.FindName("NextButton", this);
        }

        private bool CanGoNext()
        {
            return (((TabControl)this.Template.FindName("ImportTabControl", this)).Items.Cast<TabItem>().Any(x => !Equals(x.Content, null) && ((ListBox)x.Content).SelectedItems.Count > 0) && TransferState == AtlasTransferWizardState.Select) || (TransferState == AtlasTransferWizardState.Path && CanGoNext2()) || (TransferState != AtlasTransferWizardState.Select && TransferState != AtlasTransferWizardState.Path);
        }
        private bool CanGoNext2()
        {
            string path = ((TextBox)Template.FindName("PathTextBox", this)).Text;
            if (path.Contains(System.IO.Path.VolumeSeparatorChar))
                return System.IO.Path.GetDirectoryName(path) != null;

            return false;
        }
        private void Import()
        {
            if (!Equals(ExportableViewModels, null))
            {
                var tabControl = (TabControl)this.Template.FindName("ImportTabControl", this);

                //  tabControl.Items.Clear();
                var newDatabaseContext = new Db4ODatabaseContext(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Atlas Enterprise\\Atlas Suite\\Db4o", "db.adb"));
                foreach (TabItem tabItem in tabControl.Items)
                {

                    if (!Equals(tabItem.Content, null) && !Equals(tabItem.Content as ListBox, null))
                    {
                        var listBox = tabItem.Content as ListBox;


                        foreach (IExportable exportable in listBox.SelectedItems.Cast<IExportable>())
                        {
                            exportable.Export(newDatabaseContext);
                        }
                    }
                }
                newDatabaseContext.Close();
               newDatabaseContext.Dispose();
            }
            
            (actualDatacontex.DatabaseContext as Db4ODatabaseContext).Close();
            (actualDatacontex.DatabaseContext as Db4ODatabaseContext).Dispose();
            actualDatacontex.DataBasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Atlas Enterprise\\Atlas Suite\\Db4o", "db.adb");
            actualDatacontex.DatabaseContext = new Db4ODatabaseContext(actualDatacontex.DataBasePath);

            foreach (NamedCrudViewModel namedCrudViewModel in ExportableViewModels)
            {
                if (!Equals(namedCrudViewModel.ViewModel, null))
                    namedCrudViewModel.ViewModel.Load();

            }

           

            // (actualDatacontex.DatabaseContext as Db4ODatabaseContext).;
        }


        private void GoNext()
        {
            if (TransferState == AtlasTransferWizardState.Welcome)
            {
                TransferState = AtlasTransferWizardState.Select;

                (actualDatacontex.DatabaseContext as Db4ODatabaseContext).Close();
                actualDatacontex.DataBasePath = ((TextBox)Template.FindName("PathTextBox", this)).Text;
                actualDatacontex.DatabaseContext = new Db4ODatabaseContext(actualDatacontex.DataBasePath);
                SetPlattform();
                nextButton.IsEnabled = CanGoNext();
            }
            else 
            {

              Import();
                    
                TransferState = AtlasTransferWizardState.Finish;

                ((Button)this.Template.FindName("CancelButton", this)).Content = Properties.Resources.Finish;
                ((Button)this.Template.FindName("NextButton", this)).IsEnabled = false;
            }
        }

        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public bool FileExist
        {
            get { return File.Exists(((TextBox)Template.FindName("PathTextBox", this)).Text);  }
            //set { SetValue(FileExistProperty, value); }
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
            else //if (TransferState == AtlasTransferWizardState.Select)
            {
                TransferState = AtlasTransferWizardState.Select;
                     
            }



            nextButton.IsEnabled = CanGoNext();


        }

        public void FadeOutOnCompleted(object sender, EventArgs eventArgs)
        {
            Close();
        }

        //private void ButtonBase_OnClick2(object sender, RoutedEventArgs e)
        //{

            
        //    if (TransferState == AtlasTransferWizardState.Welcome)
        //    {
        //        TransferState = AtlasTransferWizardState.Select;
             
        //        //var importDatacontex = new Db4ODatabaseContext(
        //        //    System.IO.Path.Combine(System.Environment.GetFolderPath(
        //        //    System.Environment.SpecialFolder.Desktop), "Export.adb"));


        //        actualDatacontex.DataBasePath = ((TextBox) Template.FindName("PathTextBox", this)).Text;
        //        //System.IO.Path.Combine(System.Environment.GetFolderPath(
        //        //System.Environment.SpecialFolder.Desktop), "Export.adb");

        //        // DataContext = (ICrudViewModel)ServiceLocator.Current.GetInstance(DataContext.GetType());
        //        // ((ICrudViewModel) DataContext).Load();
        //    }
        //    else //if (TransferState == AtlasTransferWizardState.Select)
        //    {

        //        var elements = ((ListBox)Template.FindName("ElementsListBox", this)).SelectedItems.Cast<IExportable>();

                

        //        var localDatabaseContext = new Db4ODatabaseContext();
        //        foreach (IExportable exportable in elements)
        //        {
        //            exportable.Export(localDatabaseContext);
        //        }
        //        localDatabaseContext.Dispose();
        //        localDatabaseContext.Close();

        //        actualDatacontex.DataBasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db.adb");
        //      //  DataContext = (ICrudViewModel)ServiceLocator.Current.GetInstance(DataContext.GetType());
        //        ((ICrudViewModel)DataContext).Load();
        //        TransferState = AtlasTransferWizardState.Finish;
        //    }

        //}
        private void ButtonBase_OnClick3(object sender, RoutedEventArgs e)
        {
            actualDatacontex.DataBasePath = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Atlas Enterprise\\Atlas Suite\\Db4o", "db.adb");
            //((ICrudViewModel)DataContext).Load();
            ((Border)Template.FindName("ImportBorder", this)).BeginAnimation(Border.OpacityProperty, fadeOut);
            ((Border)Template.FindName("ImportBorder", this)).BeginAnimation(Border.MarginProperty, slideOut);

        }

        private void ButtonBase_OnClick4(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Atlas DataBase files (*.adb)|*.adb|All files (*.*)|*.*";
            openFileDialog.FileOk+=OpenFileDialogOnFileOk;
            openFileDialog.ShowDialog(this);

        }

        private void OpenFileDialogOnFileOk(object sender, CancelEventArgs cancelEventArgs)
        {
            ((TextBox) Template.FindName("PathTextBox", this)).Text = ((OpenFileDialog) sender).FileName;

            nextButton.IsEnabled = CanGoNext();
            // ((Button)Template.FindName("NextButton", this)).IsEnabled=File.Exists(((TextBox)Template.FindName("PathTextBox", this)).Text);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PathTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //OnPropertyChanged("FileExist");
            ((Button)Template.FindName("NextButton", this)).IsEnabled = File.Exists(((TextBox)Template.FindName("PathTextBox", this)).Text);
            nextButton.IsEnabled = CanGoNext();
        }

        private void AtlasImportPlatform_OnLoaded(object sender, RoutedEventArgs e)
        {
            ((Button)Template.FindName("NextButton", this)).IsEnabled = File.Exists(((TextBox)Template.FindName("PathTextBox", this)).Text);
            nextButton.IsEnabled = CanGoNext();
        }

        private void AtlasImportPlatform_OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Escape)
            ButtonBase_OnClick3(sender,null);
        }

        private void ElementsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nextButton.IsEnabled = CanGoNext();
        }

        private void NextButton_OnClick(object sender, RoutedEventArgs e)
        {
            GoNext();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((AtlasTabControl) sender).SelectedIndex != -1 && ExportableViewModels!=null)
            {
                ((AtlasTabControl) sender).FilterCommand =
                    ExportableViewModels[((AtlasTabControl) sender).SelectedIndex].ViewModel.SimpleFilterCommand;

            }
        }

        private void SelectAllAll(object sender, RoutedEventArgs e)
        {
            var atlasTabControl = ((AtlasTabControl)Template.FindName("ImportTabControl",this));
            foreach (TabItem tabItem in atlasTabControl.Items)
            {
                if (tabItem.Header is DockPanel && (tabItem.Header as DockPanel).Children[1] is CheckBox)
                {
                    var checkBox = ((tabItem.Header as DockPanel).Children[1] as CheckBox);
                    checkBox.IsChecked = true;

                }
            }
        }

        private void UnSelectAllAll(object sender, RoutedEventArgs e)
        {
            var atlasTabControl = ((AtlasTabControl)Template.FindName("ImportTabControl", this));
            foreach (TabItem tabItem in atlasTabControl.Items)
            {
                if (tabItem.Header is DockPanel && (tabItem.Header as DockPanel).Children[1] is CheckBox)
                {
                    var checkBox = ((tabItem.Header as DockPanel).Children[1] as CheckBox);
                    checkBox.IsChecked = false;

                }
            }
        }
    }
}
