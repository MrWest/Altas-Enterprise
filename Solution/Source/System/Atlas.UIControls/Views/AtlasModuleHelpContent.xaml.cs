using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using CompanyName.Atlas.Contracts.Presentation.Common;

namespace CompanyName.Atlas.UIControls.Views
{
    /// <summary>
    /// Interaction logic for AtlasModuleHelpContent.xaml
    /// </summary>
    public partial class AtlasModuleHelpContent : UserControl
    {
        /// <summary>
        /// Dependency property containing the Filter command for instances of <see cref="FilterBox"/>.
        /// </summary>
        public static readonly DependencyProperty HelpContentTreatProperty = DependencyProperty.Register("HelpContentTreat", typeof(HelpContentTreat), typeof(AtlasModuleHelpContent));

        public HelpContentTreat HelpContentTreat
        {
            get { return (HelpContentTreat) GetValue(HelpContentTreatProperty); }
            set
            {
                SetValue(HelpContentTreatProperty,value);
            }
        }
        public AtlasModuleHelpContent()
        {
            InitializeComponent();
            HelpContentTreat = new HelpContentTreat();
            //IAtlasModuleMainSubjectViewModel

        }

        private void AtlasModuleHelpContent_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!e.NewSize.IsEmpty && e.NewSize.Width > 600)
                this.Focus();
        }

        private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation(0.8, 1, TimeSpan.FromSeconds(0.2));
            ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(-30, 0, 30, 0), new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.2));

            ((Border)sender).BeginAnimation(OpacityProperty, opacityAnimation);
            ((Border)sender).BeginAnimation(MarginProperty, thicknessAnimation);
        }

        private void Hyperlink_OnClick(object sender, RoutedEventArgs e)
        {

            if (HelpContentTreat.SubjectConceptPresenter != ((IRelatedConceptPresenter)((Hyperlink)sender).DataContext).OwnerSubjectConcept)
                HelpContentTreat.SubjectConceptPresenter = ((IRelatedConceptPresenter)((Hyperlink)sender).DataContext).OwnerSubjectConcept;

            HelpContentTreat.SubjectConceptPresenter = ((IRelatedConceptPresenter)((Hyperlink)sender).DataContext).SubjectConceptPresenter;
            ListDockPanel.Visibility = Visibility.Collapsed;
            CurrentStackPanel.Visibility = Visibility.Visible;
        }

        private void AtlasHelpContentTreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            HelpContentTreat.ModuleGenericSubject = (IAtlasModuleGenericSubjectPresenter) AtlasHelpContentTreeView.SelectedItem;
            HelpContentTreat.SubjectConceptPresenter = null;
            ListDockPanel.Visibility = Visibility.Visible;
            CurrentStackPanel.Visibility = Visibility.Collapsed;
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((ListView) sender).SelectedItem = null;
        }

        private string assemblyName;

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var atlasModuleMainSubjectViewModel = DataContext as IAtlasModuleMainSubjectViewModel;
            if (atlasModuleMainSubjectViewModel != null)
            {
                    if (atlasModuleMainSubjectViewModel.AssemblyName != "Atlas.Help_Content")
                    {
                        assemblyName = atlasModuleMainSubjectViewModel.AssemblyName;
                        atlasModuleMainSubjectViewModel.AssemblyName = "Atlas.Help_Content";
                        // viewModel.Collection = ServiceLocator.Current.GetInstance<IInvestmentProvider>().Investments;
                        atlasModuleMainSubjectViewModel.Load();
                    }
                   
                
            }
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            var atlasModuleMainSubjectViewModel = DataContext as IAtlasModuleMainSubjectViewModel;
            if (atlasModuleMainSubjectViewModel != null)
            {
                    //  var viewModel = ServiceLocator.Current.GetInstance<IAtlasModuleMainSubjectViewModel>();


                    if (atlasModuleMainSubjectViewModel.AssemblyName == "Atlas.Help_Content")
                    {


                    if (assemblyName == null)
                        assemblyName = atlasModuleMainSubjectViewModel.AssemblyName;

                    atlasModuleMainSubjectViewModel.AssemblyName = assemblyName;
                   
                    // viewModel.Collection = ServiceLocator.Current.GetInstance<IInvestmentProvider>().Investments;
                    atlasModuleMainSubjectViewModel.Load();
                    }
                    else
                    {
                        assemblyName = atlasModuleMainSubjectViewModel.AssemblyName;
                    }

                    //  viewModel.Raised += OnInteractionRequested;
                    //  viewModel.Raised -= OnInteractionRequested;

                    //  return viewModel;
                }
               
            
        }
    }
}
