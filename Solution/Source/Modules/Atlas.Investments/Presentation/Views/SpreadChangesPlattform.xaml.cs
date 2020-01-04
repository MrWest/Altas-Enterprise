using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application.Budget;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.UIControls;
using CompanyName.Atlas.UIControls.Views;
using Microsoft.Practices.Prism.Commands;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for SpreadChangesPlattform.xaml
    /// </summary>
    public partial class SpreadChangesPlattform : Window
    {
        private ThicknessAnimation slideOut;
        private DoubleAnimation fadeOut;


        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty TransferStateProperty = DependencyProperty.Register("TransferState", typeof(AtlasTransferWizardState), typeof(SpreadChangesPlattform));
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty NextCommandProperty = DependencyProperty.Register("NextCommand", typeof(ICommand), typeof(SpreadChangesPlattform), new PropertyMetadata(null));
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ExportableViewModelsProperty = DependencyProperty.Register(" ExportebleViewModels", typeof(IList<NamedCrudViewModel>), typeof(SpreadChangesPlattform), new PropertyMetadata(null));

        public SpreadChangesPlattform()
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
         
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            SetPlattform();
        }
        private void SetPlattform()
        {
            if (!Equals(ExportableViewModels, null))
            {
              //  var tabControl = (TabControl)this.Template.FindName("ExportTabControl", this);

                if (ExportableViewModels.Count == 2)
                {
                    var treeView = (TreeView)this.Template.FindName("ProjectsTreeView", this);
                    treeView.ItemsSource = ExportableViewModels[0].ViewModel.Items;
                    var listbox = (ListBox)this.Template.FindName("PriceSystemListBox", this);
                    listbox.ItemsSource = ExportableViewModels[1].ViewModel.Items;

                    foreach (INavigable navigable in ExportableViewModels[0].ViewModel.Items)
                    {
                        UnSelectProjects(navigable);
                    }

                }

                //tabControl.Items.Clear();

                //foreach (NamedCrudViewModel namedCrudViewModel in ExportableViewModels)
                //{
                //    //if (!Equals(namedCrudViewModel.ViewModel, null) && !namedCrudViewModel.ViewModel.isLoaded)
                //    //    namedCrudViewModel.ViewModel.Load();
                //    var listbox = new ListBox()
                //    {
                //        ItemsSource = namedCrudViewModel.ViewModel.Items,
                //        SelectionMode = SelectionMode.Multiple
                //    };
                //    listbox.SelectionChanged += ElementsListBox_OnSelectionChanged;
                //    tabControl.Items.Add(new TabItem()
                //    {
                //        Header = namedCrudViewModel.Name,
                //        Content = listbox
                //    });
                //}
            }
        }

        private void UnSelectProjects(INavigable navigable)
        {
            navigable.State = NavigableState.UnSelected;
            
            foreach (INavigable navigableItem in navigable.Items)
            {
                UnSelectProjects(navigableItem);
            }
        }

        private void ElementsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nextButton.IsEnabled = CanGoNext();
            // NextCommand = new DelegateCommand(GoNext, CanGoNext);
        }
        public void FadeOutOnCompleted(object sender, EventArgs eventArgs)
        {
            Close();
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

        private Button nextButton;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            nextButton = (Button)this.Template.FindName("NextButton", this);
        }
        private bool CanGoNext()
        {
            return true;// (((TabControl)this.Template.FindName("ExportTabControl", this)).Items.Cast<TabItem>().Any(x => !Equals(x.Content, null) && ((ListBox)x.Content).SelectedItems.Count > 0) && TransferState == AtlasTransferWizardState.Select) || (TransferState == AtlasTransferWizardState.Path && CanGoNext2()) || (TransferState != AtlasTransferWizardState.Select && TransferState != AtlasTransferWizardState.Path);
        }


        private bool CanGoNext2()
        {
         

            return true;
        }
        private void GoNext()
        {
            if (TransferState == AtlasTransferWizardState.Welcome)
            {
                TransferState = AtlasTransferWizardState.Select;

                nextButton.IsEnabled = CanGoNext();

            }
            //else if (TransferState == AtlasTransferWizardState.Select)
            //{
            //    TransferState = AtlasTransferWizardState.Path;
            //    nextButton.IsEnabled = CanGoNext();
            //}
            else //if (TransferState == AtlasTransferWizardState.Select)
            {
                SpreadChanges();

                TransferState = AtlasTransferWizardState.Finish;

                ((Button)this.Template.FindName("CancelButton", this)).Content = UIControls.Properties.Resources.Finish;
                ((Button)this.Template.FindName("NextButton", this)).IsEnabled = false;

            }
        }

        private void SpreadChanges()
        {

            if (!Equals(DataContext, null) && !Equals(ExportableViewModels, null) && ExportableViewModels.Count == 2)
            {

                foreach (IBudgetComponentItemChangesSpreadder spreadder in ExportableViewModels[0].ViewModel.Items.Cast<IBudgetComponentItemChangesSpreadder>())
                {
                    var state = (spreadder as INavigable)?.State;
                    if (state != NavigableState.UnSelected)
                    {
                        ProjectsSpread(spreadder as INavigable);
                        spreadder.SpreadChanges(DataContext as IBudgetComponentItem);
                    }
                      
                }
                var listbox = (ListBox)this.Template.FindName("PriceSystemListBox", this);

                foreach (IBudgetComponentItemChangesSpreadder spreeadder in listbox.SelectedItems.Cast<IBudgetComponentItemChangesSpreadder>())
                {
                    spreeadder.SpreadChanges(DataContext as IBudgetComponentItem);
                }
            }
        }

        void ProjectsSpread( INavigable navigable)
        {
            foreach (IBudgetComponentItemChangesSpreadder spreadder in navigable.Items)
            {
                var state = (spreadder as INavigable)?.State;
                if (state != NavigableState.UnSelected)
                {
                    ProjectsSpread(spreadder as INavigable);
                    spreadder.SpreadChanges(DataContext as IBudgetComponentItem);
                }
                   
            }
        }

        private void ButtonBase_OnClick3(object sender, RoutedEventArgs e)
        {
            ((Border)Template.FindName("ExportBorder", this)).BeginAnimation(Border.OpacityProperty, fadeOut);
            ((Border)Template.FindName("ExportBorder", this)).BeginAnimation(Border.MarginProperty, slideOut);

        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((AtlasTabControl)sender).SelectedIndex != -1 && ExportableViewModels != null)
            {
                ((AtlasTabControl)sender).FilterCommand =
                    ExportableViewModels[((AtlasTabControl)sender).SelectedIndex].ViewModel.SimpleFilterCommand;

            }
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
                nextButton.IsEnabled = CanGoNext();
            }
            else if (TransferState == AtlasTransferWizardState.Path)
            {
                TransferState = AtlasTransferWizardState.Select;
                nextButton.IsEnabled = CanGoNext();
            }
            else //if (TransferState == AtlasTransferWizardState.Select)
            {
                TransferState = AtlasTransferWizardState.Path;
            }

            
        }

        private void NextButton_OnClick(object sender, RoutedEventArgs e)
        {
            GoNext();
        }

    }
}
