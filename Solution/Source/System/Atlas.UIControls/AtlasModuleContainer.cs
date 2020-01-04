using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Security;
using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.UIControls
{
    public class AtlasModuleContainer: ContentControl
    {
        /// <summary>
        /// Dependency property containing the icon for instance of <see cref="SecondLevelNavItem"/>.
        /// </summary>
        public static readonly DependencyProperty SelectedModuleProperty = DependencyProperty.Register("SelectedModule", typeof(ModuleInfo), typeof(AtlasModuleContainer), new PropertyMetadata(null, OnSelectedModuleChanged));

        /// <summary>
        /// Dependency property containing the icon for instance of <see cref="SecondLevelNavItem"/>.
        /// </summary>
        public static readonly DependencyProperty ModulesProperty = DependencyProperty.Register("Modules", typeof(IEnumerable<ModuleInfo>), typeof(AtlasModuleContainer), new PropertyMetadata(null));
       
        /// <summary>
        /// Dependency property containing the icon for instance of <see cref="SecondLevelNavItem"/>.
        /// </summary>
        public static readonly DependencyProperty SelectProperty = DependencyProperty.Register("Select", typeof(ICommand), typeof(AtlasModuleContainer), new PropertyMetadata(null));
        /// <summary>
        /// Dependency property containing the icon for instance of <see cref="SecondLevelNavItem"/>.
        /// </summary>
        public static readonly DependencyProperty CanSelectModuleProperty = DependencyProperty.Register("CanSelectModule", typeof(bool), typeof(AtlasModuleContainer), new PropertyMetadata(false));

        /// <summary>
        /// Initializes a new instance of <see cref="SecondLevelNavItem"/>.
        /// </summary>
        public AtlasModuleContainer()
        {
            DefaultStyleKey = typeof(AtlasModuleContainer);
            //var moduleCatalog = new DirectoryModuleCatalog { ModulePath = GetFullPath(Settings.Default.ModulePath) };
           // IsVisibleChanged+=OnIsVisibleChanged;     
            Loaded+=OnLoaded;
            var moduleCatalog = (ModuleCatalog)ServiceLocator.Current.GetInstance(typeof(ModuleCatalog));
           // DataContext = moduleCatalog;
            Modules = moduleCatalog?.Modules;
            IsVisibleChanged+=OnIsVisibleChanged;
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (IsVisible&&IsLoaded && (Tag as ProgressBar) !=null)
                (Tag as ProgressBar).Visibility = Visibility.Collapsed;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            
            //if (Select == null)
            //{
                Select = new DelegateCommand<ListViewItem>(SelectCommand, CanSelect);
            //}
        }



        //private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        //{
        //   var visibility = (bool) dependencyPropertyChangedEventArgs.NewValue;
        //    if (visibility  && IsLoaded)
        //    {
        //        Select = new DelegateCommand<ListViewItem>(SelectCommand,CanSelect);
        //    }
        //}

        private bool CanSelect(ListViewItem listViewItem)
        {
            if (listViewItem != null)
            {
                var security = (AtlasSecurity)ServiceLocator.Current.GetInstance(typeof(AtlasSecurity));
                var loggedUser = security.CurrentUser;
                var moduleInfo = (ModuleInfo)listViewItem.DataContext;
                if(loggedUser!=null&&moduleInfo!=null)
                return (loggedUser.AllowedModules!=null && loggedUser.AllowedModules.Any(x => x.ModuleName == moduleInfo.ModuleName)) || loggedUser.Rol == AtlasUserRol.Administrator;
            }

            //check if no users yet
            var userviewModel = ServiceLocator.Current.GetInstance<IAtlasUserViewModel>();
            userviewModel.Load();
            if (userviewModel.Items.Count == 0)
                return true;

            return false;
        }

        private ListViewItem _listViewItem;
        private void SelectCommand(ListViewItem listViewItem)
        {
            if (CanSelect(listViewItem))
            {
                //var progressBar = Tag as ProgressBar;
                //progressBar.IsVisibleChanged-=ProgressBarOnIsVisibleChanged;
                //progressBar.IsVisibleChanged += ProgressBarOnIsVisibleChanged;

                _listViewItem = listViewItem;
                //if (progressBar != null) progressBar.Visibility = Visibility.Visible;

                listViewItem.IsSelected = true;
            }
           

           
        }

        private void ProgressBarOnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if(((ProgressBar)sender).IsVisible)
            _listViewItem.IsSelected = true;
        }


        private static string GetFullPath(string relativePath)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        }

        /// <summary>
        /// Gets or sets the object representing the icon to display by the current <see cref="AtlasModuleButton"/>.
        /// </summary>
        public IEnumerable<ModuleInfo> Modules
        {
            get { return (IEnumerable<ModuleInfo>)GetValue(ModulesProperty); }
            set { SetValue(ModulesProperty, value); }
        }
        /// <summary>
        /// Gets or sets the object representing the icon to display by the current <see cref="AtlasModuleButton"/>.
        /// </summary>
        public ICommand Select
        {
            get { return (ICommand)GetValue(SelectProperty); }
            set { SetValue(SelectProperty, value); }
        }
        /// <summary>
        /// Gets or sets the object representing the icon to display by the current <see cref="AtlasModuleButton"/>.
        /// </summary>
        public bool CanSelectModule
        {
            get
            {
                return (bool)GetValue(CanSelectModuleProperty);
            }
            set { SetValue(CanSelectModuleProperty, value); }
        }
        /// <summary>
        /// Gets or sets the object representing the icon to display by the current <see cref="AtlasModuleButton"/>.
        /// </summary>
        public ModuleInfo SelectedModule
        {
            get { return (ModuleInfo)GetValue(SelectedModuleProperty); }
            set { SetValue(SelectedModuleProperty, value); }
        }
        /// <summary>
        /// Called when the template for the current <see cref="AtlasWindow"/> is being applied.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var crap = Modules;
            ProgressBarStyle =  this.FindResource(typeof(ProgressBar)) as Style;
            // _content.SetBinding(ContentPresenter.ContentProperty, ContentBinding);
        }

        private Style ProgressBarStyle { get; set; }
        private static void OnSelectedModuleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var mainView = (AtlasModuleContainer)d;

            //var security = (AtlasSecurity)ServiceLocator.Current.GetInstance(typeof(AtlasSecurity));
            //var loggedUser = security.CurrentUser;
            //var moduleInfo = (ModuleInfo)e.NewValue;
            //if (moduleInfo!=null&&loggedUser!=null && loggedUser.AllowedModules.Any(x => x.ModuleName == moduleInfo.ModuleName))
            //    mainView.SelectedModule = null;
           
            // if (mainView.SelectedModule != null)

        }
    }
}
