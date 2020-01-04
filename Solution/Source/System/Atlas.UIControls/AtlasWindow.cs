using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Security;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Visuals;
using CompanyName.Atlas.UIControls.Annotations;
using CompanyName.Atlas.UIControls.Converters;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;
using SystemCommands = Microsoft.Windows.Shell.SystemCommands;
using CompanyName.Atlas.Contracts.Presentation.Services;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    /// This is the base class of suite's windows.
    /// </summary>
    public class AtlasWindow : Window,INotifyPropertyChanged
    {
        private Selector _categoriesSelector;
        private ContentControl _optionalNavigationContent;
        private ContentPresenter _content;

        public Bootstrapper Bootstrapper { get; set; }

        /// <summary>
        /// Dependency property used to contain the collection of menu items a <see cref="AtlasWindow"/> menu has.
        /// </summary>
        public static readonly DependencyProperty AtlasModuleViewProperty = DependencyProperty.Register("AtlasModuleView", typeof(AtlasModuleView), typeof(AtlasWindow), new PropertyMetadata(null));


        
        /// <summary>
        /// Dependency property used to contain the tooltip for the Minimize button of windows of type <see cref="AtlasWindow"/>.
        /// </summary>
        public static readonly DependencyProperty MinimizeButtonToolTipProperty = DependencyProperty.Register("MinimizeButtonToolTip", typeof(object), typeof(AtlasWindow), new PropertyMetadata(Properties.Resources.Minimize));

        /// <summary>
        /// Dependency property used to contain the tooltip for the Maximize button of windows of type <see cref="AtlasWindow"/>.
        /// </summary>
        public static readonly DependencyProperty MaximizeButtonToolTipProperty = DependencyProperty.Register("MaximizeButtonToolTip", typeof(object), typeof(AtlasWindow), new PropertyMetadata(Properties.Resources.Maximize));

        /// <summary>
        /// Dependency property used to contain the tooltip for the Restore button of windows of type <see cref="AtlasWindow"/>.
        /// </summary>
        public static readonly DependencyProperty RestoreButtonToolTipProperty = DependencyProperty.Register("RestoreButtonToolTip", typeof(object), typeof(AtlasWindow), new PropertyMetadata(Properties.Resources.Restore));

        /// <summary>
        /// Dependency property used to contain the tooltip for the Close button of windows of type <see cref="AtlasWindow"/>.
        /// </summary>
        public static readonly DependencyProperty CloseButtonToolTipProperty = DependencyProperty.Register("CloseButtonToolTip", typeof(object), typeof(AtlasWindow), new PropertyMetadata(Properties.Resources.Close));

        /// <summary>
        /// Dependency property used to contain the tooltip for the Minimize button of windows of type <see cref="AtlasWindow"/>.
        /// </summary>
        public static readonly DependencyProperty LoginMediaPathProperty = DependencyProperty.Register("LoginMediaPath", typeof(string), typeof(AtlasWindow), new PropertyMetadata(null));
        
         /// <summary>
        /// Dependency property used to contain the tooltip for the Minimize button of windows of type <see cref="AtlasWindow"/>.
        /// </summary>
        public static readonly DependencyProperty LoginImgPathProperty = DependencyProperty.Register("LoginImgPath", typeof(string), typeof(AtlasWindow), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property used to contain the tooltip for the Minimize button of windows of type <see cref="AtlasWindow"/>.
        /// </summary>
        public static readonly DependencyProperty ModulePathProperty = DependencyProperty.Register("ModulePath", typeof(string), typeof(AtlasWindow), new PropertyMetadata(null));
        
        
       
        /// <summary>
        /// Dependency property containing the value of the whether there is visible or not the optional navigation content of <see cref="AtlasWindow"/> instances.
        /// </summary>
        public static readonly DependencyProperty IsLoggedProperty = DependencyProperty.Register("IsLogged", typeof(bool), typeof(AtlasWindow), new PropertyMetadata(false));
       
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty LoginCommandProperty = DependencyProperty.Register("LoginCommand", typeof(ICommand), typeof(AtlasWindow), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty BackButtonCommandProperty = DependencyProperty.Register("BackButtonCommand", typeof(ICommand), typeof(AtlasWindow), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ModuleCatalogProperty = DependencyProperty.Register("ModuleCatalog", typeof(IModuleCatalog), typeof(AtlasWindow), new PropertyMetadata(null));
        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
       // public static readonly DependencyProperty ModulesProperty = DependencyProperty.Register("Modules", typeof(IEnumerable<ModuleInfo>), typeof(AtlasWindow), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty ViewStateProperty = DependencyProperty.Register("ViewState", typeof(AtlasViewState), typeof(AtlasWindow), new PropertyMetadata(AtlasViewState.FrontPage));

        /// <summary>
        /// Dependency property containing the icon for instance of <see cref="SecondLevelNavItem"/>.
        /// </summary>
        public static readonly DependencyProperty SelectedModuleProperty = DependencyProperty.Register("SelectedModule", typeof(ModuleInfo), typeof(AtlasWindow), new PropertyMetadata(null, OnSelectedModuleChanged));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty IsSomeModuleLoadedProperty = DependencyProperty.Register("IsSomeModuleLoaded", typeof(bool), typeof(AtlasWindow), new PropertyMetadata(false));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty IsSomeModuleLoadingProperty = DependencyProperty.Register("IsSomeModuleLoading", typeof(bool), typeof(AtlasWindow), new PropertyMetadata(false));


        /// <summary>
        /// Initializes a new instance of <see cref="AtlasWindow"/>.
        /// </summary>
        public AtlasWindow()
        {
            DefaultStyleKey = typeof(AtlasWindow);

            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, OnCloseWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, OnMaximizeWindow, OnCanResizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, OnMinimizeWindow, OnCanMinimizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, OnRestoreWindow, OnCanResizeWindow));
            LoginCommand = new DelegateCommand<ILoginInfoHolder>(Login, CanLogin);
            BackButtonCommand = new DelegateCommand(GoBack, CanGoBack);
            IsLogged = false;
            
            // LoginCommand.Execute(null);
        }

        private bool CanGoBack()
        {
            return true;
        }

        private void GoBack()
        {
            if (ViewState == AtlasViewState.MainPage)
            {
                ViewState = AtlasViewState.FrontPage;
                var atlasSecurity = (AtlasSecurity)ServiceLocator.Current.GetInstance(typeof(AtlasSecurity));
                atlasSecurity.CurrentUser = null;
                return;
            }
            if (ViewState == AtlasViewState.Module)
            {
                ViewState = AtlasViewState.MainPage;
                IsSomeModuleLoaded = false;
                IsSomeModuleLoading = false;
                SelectedModule = null;
                return;
            }
        }


        public void Login(ILoginInfoHolder loginInfo)
        {
            loginInfo.UnsuccessfullText = null;
            var atlasUserViewModel = ServiceLocator.Current.GetInstance<IAtlasUserViewModel>();
            atlasUserViewModel.Load();
            if (atlasUserViewModel.Items.Count == 0)
            {
                ViewState = AtlasViewState.MainPage;
                return;
            }

            var loggedUser =
                atlasUserViewModel.Items.FirstOrDefault(
                    x => x.Name == loginInfo.UserName && ((x.Password == loginInfo.PasswordBox.Password)||
                        (x.Password ==null && loginInfo.PasswordBox.Password=="")));
            if (loggedUser != null)
            {
                var atlasSecurity = (AtlasSecurity)ServiceLocator.Current.GetInstance(typeof(AtlasSecurity));
                atlasSecurity.CurrentUser = loggedUser;
                ViewState = AtlasViewState.MainPage;
                return;
            }

            loginInfo.UnsuccessfullText = Properties.Resources.UnsuccessfullText;
            //IsLogged = true;
        }

        public bool CanLogin(ILoginInfoHolder loginInfo)
        {
            return true;
        }
        
             ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public AtlasModuleView AtlasModuleView
        {
            get { return (AtlasModuleView) GetValue(AtlasModuleViewProperty); }
            set { SetValue(AtlasModuleViewProperty, value); }
        }
        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public IModuleCatalog ModuleCatalog
        {
            get { return (IModuleCatalog) GetValue(ModuleCatalogProperty); }
            set { SetValue(ModuleCatalogProperty, value); }
        }
        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        //public IEnumerable<ModuleInfo> Modules
        //{
        //    get { return ((IModuleCatalog)GetValue(ModulesProperty)).Modules; }
        //    set { SetValue(ModulesProperty, value); }
        //}
         ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public string LoginMediaPath
        {
            get { return (string) GetValue(LoginMediaPathProperty); }
            set { SetValue(LoginMediaPathProperty, value); }
        }
        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public string LoginImgPath
        {
            get { return (string)GetValue(LoginImgPathProperty); }
            set { SetValue(LoginImgPathProperty, value); }
        }
        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public string ModulePath
        {
            get { return (string)GetValue(ModulePathProperty); }
            set { SetValue(ModulePathProperty, value); }
        }
        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public AtlasViewState ViewState
        {
            get { return (AtlasViewState)GetValue(ViewStateProperty); }
            set { SetValue(ViewStateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the object representing the icon to display by the current <see cref="AtlasModuleButton"/>.
        /// </summary>
        public ModuleInfo SelectedModule
        {
            get { return (ModuleInfo)GetValue(SelectedModuleProperty); }
            set { SetValue(SelectedModuleProperty, value); }
        }


        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        //public object DefaultContent
        //{
        //    get { return GetValue(DefaultContentProperty); }
        //    set { SetValue(DefaultContentProperty, value); }
        //}

        ///// <summary>
        ///// Gets of sets whether the root navigation bar is collapsed or not.
        ///// </summary>
        //public bool IsRootNavBarCollapsed
        //{
        //    get { return (bool)GetValue(IsRootNavBarCollapsedProperty); }
        //    set { SetValue(IsRootNavBarCollapsedProperty, value); }
        //}

        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public bool IsLogged
        {
            get { return (bool)GetValue(IsLoggedProperty); }
            set { SetValue(IsLoggedProperty, value); }
        }

          /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public ICommand LoginCommand
        {
            get { return (ICommand)GetValue(LoginCommandProperty); }
            set { SetValue(LoginCommandProperty, value); }
        }

           /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        public ICommand BackButtonCommand
        {
            get { return (ICommand)GetValue(BackButtonCommandProperty); }
            set { SetValue(BackButtonCommandProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasWindow"/> menu.
        /// </summary>
        //public ObservableCollection<MenuItem> Menu
        //{
        //    get { return (ObservableCollection<MenuItem>)GetValue(MenuProperty); }
        //    set { SetValue(MenuProperty, value); }
        //}

        ///// <summary>
        ///// Gets or sets the collection of <see cref="FirstLevelNavItem"/> that are displayed in the current 
        ///// <see cref="AtlasWindow"/> menu.
        ///// </summary>
        //public ObservableCollection<FirstLevelNavItem> Navigation
        //{
        //    get { return (ObservableCollection<FirstLevelNavItem>)GetValue(NavigationProperty); }
        //    set { SetValue(NavigationProperty, value); }
        //}

        ///// <summary>
        ///// Gets or sets the status bar's content.
        ///// </summary>
        //public object StatusBar
        //{
        //    get { return GetValue(StatusBarProperty); }
        //    set { SetValue(StatusBarProperty, value); }
        //}

        /// <summary>
        /// Gets or sets the tooltip of the current <see cref="AtlasWindow"/> Minimize button.
        /// </summary>
        public object MinimizeButtonToolTip
        {
            get { return GetValue(MinimizeButtonToolTipProperty); }
            set { SetValue(MinimizeButtonToolTipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tooltip of the current <see cref="AtlasWindow"/> Maximize button.
        /// </summary>
        public object MaximizeButtonToolTip
        {
            get { return GetValue(MaximizeButtonToolTipProperty); }
            set { SetValue(MaximizeButtonToolTipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tooltip of the current <see cref="AtlasWindow"/> Restore button.
        /// </summary>
        public object RestoreButtonToolTip
        {
            get { return GetValue(RestoreButtonToolTipProperty); }
            set { SetValue(RestoreButtonToolTipProperty, value); }
        }
        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public bool IsSomeModuleLoaded
        {
            get { return (bool)GetValue(IsSomeModuleLoadedProperty); }
            set { SetValue(IsSomeModuleLoadedProperty, value);
                OnPropertyChanged("IsSomeModuleLoaded");

            }
        }
        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public bool IsSomeModuleLoading
        {
            get { return (bool)GetValue(IsSomeModuleLoadingProperty); }
            set
            {
                SetValue(IsSomeModuleLoadingProperty, value);
                OnPropertyChanged("IsSomeModuleLoading");

            }
        }
        
        /// <summary>
        /// Gets or sets the tooltip of the current <see cref="AtlasWindow"/> Close button.
        /// </summary>
        public object CloseButtonToolTip
        {
            get { return GetValue(CloseButtonToolTipProperty); }
            set { SetValue(CloseButtonToolTipProperty, value); }
        }

        ///// <summary>
        ///// Gets or sets the optional navigation content for the current <see cref="AtlasWindow"/>.
        ///// </summary>
        //public object OptionalNavigationContent
        //{
        //    get { return GetValue(OptionalNavigationContentProperty); }
        //    set { SetValue(OptionalNavigationContentProperty, value); }
        //}

        ///// <summary>
        ///// Gets or sets whether the optional navigation is visible or not.
        ///// </summary>
        //public Visibility OptionalNavigationVisibility
        //{
        //    get { return (Visibility)GetValue(OptionalNavigationVisibilityProperty); }
        //    set { SetValue(OptionalNavigationVisibilityProperty, value); }
        //}

        /// <summary>
        /// Gets the method that allows to parse the content of the current <see cref="AtlasWindow"/> according to the navigation
        /// mechanism.
        /// </summary>
        //public Func<object, object> ContentConverterMethod
        //{
        //    get { return (Func<object, object>)GetValue(ContentConverterMethodProperty); }
        //    set { SetValue(ContentConverterMethodProperty, value); }
        //}

        ///// <summary>
        ///// Returns the binding that for the content of the current window, content that is determined by using as input the navigation
        ///// configuration.
        ///// </summary>
        //private BindingBase ContentBinding
        //{
        //    get
        //    {
        //        if (_categoriesSelector == null || _optionalNavigationContent == null)
        //            return null;

        //        var firstSubBinding = new Binding("SelectedItem") { Source = _categoriesSelector };
        //        var secondSubBinding = new Binding("Content") { Source = _optionalNavigationContent };
        //        var binding = new MultiBinding
        //        {
        //            Converter = new AtlasWindowContentConverter(),
        //            ConverterParameter = ContentConverterMethod
        //        };
        //        binding.Bindings.Add(firstSubBinding);
        //        binding.Bindings.Add(secondSubBinding);

        //        return binding;
        //    }
        //}


        /// <summary>
        /// Occurs when the root navigation bar is collapsed.
        /// </summary>
        //public event RoutedEventHandler RootNavBarCollapsed
        //{
        //    add { AddHandler(RootNavBarCollapsedEvent, value); }
        //    remove { RemoveHandler(RootNavBarCollapsedEvent, value); }
        //}

        ///// <summary>
        ///// Occurs when the root navigation bar is expanded.
        ///// </summary>
        //public event RoutedEventHandler RootNavBarExpanded
        //{
        //    add { AddHandler(RootNavBarExpandedEvent, value); }
        //    remove { RemoveHandler(RootNavBarExpandedEvent, value); }
        //}

        /// <summary>
        /// Called when the template for the current <see cref="AtlasWindow"/> is being applied.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _categoriesSelector = (Selector)Template.FindName("categories", this);
            _optionalNavigationContent = (ContentControl)Template.FindName("optionalNavigationContent", this);
            _content = (ContentPresenter)Template.FindName("content", this);

           // _content.SetBinding(ContentPresenter.ContentProperty, ContentBinding);
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

        private static void OnSelectedModuleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var mainView = (AtlasWindow)d;
          
            var security = (AtlasSecurity)ServiceLocator.Current.GetInstance(typeof(AtlasSecurity));
            var loggedUser = security.CurrentUser;
           
            var userviewModel = ServiceLocator.Current.GetInstance<IAtlasUserViewModel>();
            userviewModel.Load();
            
            if (e.NewValue != null)
            {
                var moduleInfo = (ModuleInfo)e.NewValue;
                if (loggedUser != null && loggedUser.AllowedModules != null && loggedUser.AllowedModules.All(x => x.ModuleName != moduleInfo.ModuleName)&&userviewModel.Items.Count>0)
                    mainView.SelectedModule = null;

                if ((mainView.SelectedModule != null && loggedUser != null && loggedUser.AllowedModules != null && loggedUser.AllowedModules.Any(x => x.ModuleName == moduleInfo.ModuleName))|| userviewModel.Items.Count ==0)
                {
               
                    mainView.ViewState = AtlasViewState.Module;

                }

                if (loggedUser != null && loggedUser.AllowedModules == null)
                {
                    mainView.ViewState = AtlasViewState.Module;
                }
            }

        }
        private IStatusBarServices _statusBarServices;

        private string LoadingText
        {
            get
            {
                return Properties.Resources.Loading; 
            }
        }
        public void Notify()
        {
            OnPropertyChanged("ModuleCatalog");
        }
        //private static void OnCollapsedOrExpandedRootNavBar(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var mainView = (AtlasWindow)d;
        //    var collapsed = (bool)e.NewValue;

        //    if (collapsed)
        //        mainView.RaiseRootNavBarCollapsed();
        //    else
        //        mainView.RaiseRootNavBarExpanded();
        //}

        //private static void OnContentConverterMethodChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var window = (AtlasWindow)d;
        //    window.ContentConverterMethod = e.NewValue as Func<object, object>;
        //   // window._content.SetBinding(ContentPresenter.ContentProperty, window.ContentBinding);
        //}

        //private void RaiseRootNavBarCollapsed()
        //{
        //    RaiseEvent(new RoutedEventArgs(RootNavBarCollapsedEvent));
        //}

        //private void RaiseRootNavBarExpanded()
        //{
        //    RaiseEvent(new RoutedEventArgs(RootNavBarExpandedEvent));
        //}
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));

        }

        
    }
}
