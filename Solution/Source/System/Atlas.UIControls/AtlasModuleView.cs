using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using CompanyName.Atlas.Contracts;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Security;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Features;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.UIControls.Annotations;
using CompanyName.Atlas.UIControls.Converters;
using CompanyName.Atlas.UIControls.Views;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;
using Application = System.Windows.Application;
using Binding = System.Windows.Data.Binding;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using ProgressBar = System.Windows.Controls.ProgressBar;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    /// Generic interface for Atlas Modules
    /// </summary>
    public class AtlasModuleView : ContentControl, INavigationServices, INotifyPropertyChanged
    {
        private Selector _categoriesSelector;
        private ContentControl _optionalNavigationContent;
        private ContentControl _subSystemMenuContent;
        private ContentControl _defaultContent;
        private ContentPresenter _content;
        ///// <summary>
        ///// Dependency property used to contain the collection of navigation items a <see cref="AtlasWindow"/> navigationbar has.
        ///// </summary>
        public static readonly DependencyProperty NavigationProperty = DependencyProperty.Register("Navigation", typeof(ObservableCollection<FirstLevelNavItem>), typeof(AtlasModuleView), new PropertyMetadata(new ObservableCollection<FirstLevelNavItem>()));

        ///// <summary>
        ///// Dependency property containing the default content for the instances of <see cref="AtlasWindow"/> having no custom content.
        ///// </summary>
        public static readonly DependencyProperty DefaultContentProperty = DependencyProperty.Register("DefaultContent", typeof(object), typeof(AtlasModuleView), new PropertyMetadata(null));

        ///// <summary>
        ///// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        ///// </summary>
        public static readonly DependencyProperty IsRootNavBarCollapsedProperty = DependencyProperty.Register("IsRootNavBarCollapsed", typeof(bool), typeof(AtlasModuleView), new PropertyMetadata(false, OnCollapsedOrExpandedRootNavBar));

        ///// <summary>
        ///// Dependency property used to contain the content of the status bar of the <see cref="AtlasWindow"/> instances.
        ///// </summary>
        public static readonly DependencyProperty StatusBarProperty = DependencyProperty.Register("StatusBar", typeof(object), typeof(AtlasModuleView), new PropertyMetadata(null));

        ///// <summary>
        ///// Dependency property used to contain the optional navigation content of <see cref="AtlasWindow"/> instances.
        ///// </summary>
        public static readonly DependencyProperty OptionalNavigationContentProperty = DependencyProperty.Register("OptionalNavigationContent", typeof(object), typeof(AtlasModuleView), new PropertyMetadata(null));

         ///// <summary>
        ///// Dependency property used to contain the optional navigation content of <see cref="AtlasWindow"/> instances.
        ///// </summary>
        public static readonly DependencyProperty SubSystemMenuContentProperty = DependencyProperty.Register("SubSystemMenuContent", typeof(object), typeof(AtlasModuleView), new PropertyMetadata(null));

       

        ///// <summary>
        ///// Dependency property containing the value of the whether there is visible or not the optional navigation content of <see cref="AtlasWindow"/> instances.
        ///// </summary>
        public static readonly DependencyProperty OptionalNavigationVisibilityProperty = DependencyProperty.Register("OptionalNavigationVisibility", typeof(Visibility), typeof(AtlasModuleView), new PropertyMetadata(Visibility.Visible));

        ///// <summary>
        ///// Dependency property used to hold the method that allows to parse the content of instances of <see cref="AtlasWindow"/>
        ///// according to the navigation mechanism.
        ///// </summary>
        public static readonly DependencyProperty ContentConverterMethodProperty = DependencyProperty.Register("ContentConverterMethod", typeof(Func<object, object>), typeof(AtlasModuleView), new PropertyMetadata(null, OnContentConverterMethodChanged));

        /// <summary>
        /// Dependency property used to hold the method that allows to parse the content of instances of <see cref="AtlasMainPage"/>
        /// according to the navigation mechanism.
        /// </summary>
        public static readonly DependencyProperty SelectedContentProperty = DependencyProperty.Register("SelectedContent", typeof(object), typeof(AtlasModuleView), new PropertyMetadata(null, OnSelectedContentChanged));

        /// <summary>
        /// Dependency property containing the icon for instance of <see cref="SecondLevelNavItem"/>.
        /// </summary>
        public static readonly DependencyProperty ModuleProperty = DependencyProperty.Register("Module", typeof(ModuleInfo), typeof(AtlasModuleView), new PropertyMetadata(null, OnModuleChanged));

      

        ///// <summary>
        ///// Routed event defining the event raised when the root navigation bar got collapsed.
        ///// </summary>
        public static readonly RoutedEvent RootNavBarCollapsedEvent = EventManager.RegisterRoutedEvent("RootNavBarCollapsed", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(AtlasModuleView));

        ///// <summary>
        ///// Routed event defining the event raised when the root navigation bar got expanded.
        ///// </summary>
        public static readonly RoutedEvent RootNavBarExpandedEvent = EventManager.RegisterRoutedEvent("RootNavBarExpanded", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(AtlasModuleView));

        /// <summary>
        /// Routed event defining the event raised when the root navigation bar got expanded.
        /// </summary>
        public static readonly RoutedEvent ContentChangedEvent = EventManager.RegisterRoutedEvent("ContentChanged", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(AtlasModuleView));

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty IsSomeModuleLoadingProperty = DependencyProperty.Register("IsSomeModuleLoading", typeof(bool), typeof(AtlasModuleView), new PropertyMetadata(false, PropertyChangedCallback));
        ///// <summary>
        ///// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        ///// </summary>
        public static readonly DependencyProperty ShowHelpProperty = DependencyProperty.Register("ShowHelp", typeof(bool), typeof(AtlasModuleView), new PropertyMetadata(false));
        ///// <summary>
        ///// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        ///// </summary>
        public static readonly DependencyProperty ModuleHelpViewModelProperty = DependencyProperty.Register("ModuleHelpViewModel", typeof(IAtlasModuleMainSubjectViewModel), typeof(AtlasModuleView), new PropertyMetadata(null));


        /// <summary>
        /// Routed event defining the event raised when the root navigation bar got expanded.
        /// </summary>
        // public static readonly RoutedEvent AppearEvent = EventManager.RegisterRoutedEvent("Appear", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(AtlasModuleView));

        /// <summary>
        /// Routed event defining the event raised when the root navigation bar got expanded.
        /// </summary>
        //  public static readonly RoutedEvent DisappearEvent = EventManager.RegisterRoutedEvent("Disappear", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(AtlasModuleView));
       private BackgroundWorker _backgroundWorker;

        private ProgressBar _progressBar;
        // private DoubleAnimation opacityAnimation;
        // private ThicknessAnimation thicknessAnimation;
        /// <summary>
        /// Initializes a new instance of <see cref="AtlasWindow"/>.
        /// </summary>
        public AtlasModuleView()
        {
            DefaultStyleKey = typeof(AtlasModuleView);
            CopyCommand = new DelegateCommand(Copy);
            PasteCommand = new DelegateCommand(Paste);

            _backgroundWorker = new BackgroundWorker();

            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;

            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
            _backgroundWorker.ProgressChanged += BackgroundWorkerOnProgressChanged;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorkerOnRunWorkerCompleted;

            _progressBar = new ProgressBar() { IsIndeterminate = true, Width = 320};
          //  StatusBar = _progressBar;
            //  IsVisibleChanged+=OnIsVisibleChanged;
            //   opacityAnimation = new DoubleAnimation(0.8, 1, TimeSpan.FromSeconds(0.2));
            // thicknessAnimation = new ThicknessAnimation(new Thickness(-30, 0, 30, 0), new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.2));
            //    opacityAnimation.Completed +=OpacityAnimationOnCompleted;

        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            
            var setupUri = (string) doWorkEventArgs.Argument;

            _navigationResources = (ResourceDictionary)Application.LoadComponent(new Uri(setupUri, UriKind.RelativeOrAbsolute));

            _navigationConfig = (IList)_navigationResources[GlobalParameters.NavigationResourceKey];

        }

        private void BackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
           

            if (Navigation != null && Navigation.Any(x => !_navigationConfig.Cast<FirstLevelNavItem>().Any(y => y.Text == x.Text)))

            {
                Navigation.Clear();
                if (_navigationServices == null)
                    _navigationServices = ServiceLocator.Current.GetInstance<INavigationServices>();
                _navigationServices.HideOptionalNavigationContent();
            }

            int reason = _navigationConfig.Count > 0 ? 100 / _navigationConfig.Count : 0;
            int count = 0;

            foreach (FirstLevelNavItem navigationItem in _navigationConfig)
            {
                Navigation.Add(navigationItem);
                //count++;
                //_progressBar.Value = count * reason;
            }

            //_navigationSetupUri = navigationSetupUri;

            //IsSomeModuleLoading = false;
            //IsSomeModuleLoaded = true;

            StatusBar = null;

            StatusBar = null;
            IsSomeModuleLoading = false;
            IsSomeModuleLoaded = true;
        }

        private string _moduleName;
        public void SetupModule()
        {
            StatusBar = _progressBar;
            OnPropertyChanged("StatusBar");
            StatusBarServices.SignalLoading();
            IsSomeModuleLoading = true;
            IsSomeModuleLoaded = false;
            _moduleName = Module.ModuleName;

            Thread thread = new Thread(LoadModule);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
           

           
           // StatusBar = null;
            IsSomeModuleLoading = false;
            IsSomeModuleLoaded = true;

            //  _backgroundWorker.RunWorkerAsync();

        }

        private void LoadModule()
        {
        
            // Get the dispatcher from the current window, and use it to invoke
            // the update code.
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
            (ThreadStart)delegate () {
                SetupNavigation("/{0};component/Presentation/Views/Navigation.xaml".EasyFormat(_moduleName));
                SetupSubSystemMenu("/{0};component/Presentation/Views/AtlasSubSystemMenu.xaml".EasyFormat(_moduleName), null);
                SetupSubSystemDefaultContent("/{0};component/Presentation/Views/DefaultContent.xaml".EasyFormat(_moduleName), null);

            }
            );
        }

        private void BackgroundWorkerOnProgressChanged(object sender, ProgressChangedEventArgs progressChangedEventArgs)
        {
            _progressBar.Value = progressChangedEventArgs.ProgressPercentage;
        }

      

        private bool CanDoPaste()
        {
            return !Equals(CopiedObject, null) && !Equals(ObjectToPasteOn, null);
               
        }

        private bool CanDoCopy()
        {
            return !Equals(ObjectToPasteOn, null) && ObjectToPasteOn.GetType().Implements<IPresenter>();
        }

        private void Copy()
        {
            if (CanDoCopy())
            {
                CopiedObject = (ObjectToPasteOn as IPresenter).Object as IEntity;
                OnPropertyChanged(nameof(CanPaste));
            }
        }

        //private void OpacityAnimationOnCompleted(object sender, EventArgs eventArgs)
        //{
        //    Visibility = System.Windows.Visibility.Collapsed;
        //}

        //private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        //{
        //    bool isVisible = (bool) dependencyPropertyChangedEventArgs.NewValue;

        //    if (isVisible)
        //    {
        //        RaiseAppear();
        //        this.BeginAnimation(ContentControl.OpacityProperty, opacityAnimation);
        //    }

        //    else
        //    {
        //        RaiseDisappear();
        //        this.BeginAnimation(ContentControl.OpacityProperty, opacityAnimation);
        //    }
               
        //}

        /// <summary>
        /// Gets or sets the object representing the icon to display by the current <see cref="AtlasModuleButton"/>.
        /// </summary>
        public ModuleInfo Module
        {
            get { return (ModuleInfo)GetValue(ModuleProperty); }
            set { SetValue(ModuleProperty, value); }
        }
        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public object DefaultContent
        {
            get { return GetValue(DefaultContentProperty); }
            set { SetValue(DefaultContentProperty, value); }
        }

        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public IAtlasModuleMainSubjectViewModel ModuleHelpViewModel
        {
            get { return (IAtlasModuleMainSubjectViewModel)GetValue(ModuleHelpViewModelProperty); }
            set { SetValue(ModuleHelpViewModelProperty, value); }
        }
        ///// <summary>
        ///// Gets of sets whether the root navigation bar is collapsed or not.
        ///// </summary>
        public bool IsRootNavBarCollapsed
        {
            get { return (bool)GetValue(IsRootNavBarCollapsedProperty); }
            set { SetValue(IsRootNavBarCollapsedProperty, value); }
        }
        ///// <summary>
        ///// Gets of sets whether the root navigation bar is collapsed or not.
        ///// </summary>
        public bool ShowHelp
        {
            get { return (bool)GetValue(ShowHelpProperty); }
            set { SetValue(ShowHelpProperty, value); }
        }
        
        ///// <summary>
        ///// Gets or sets the collection of <see cref="FirstLevelNavItem"/> that are displayed in the current 
        ///// <see cref="AtlasWindow"/> menu.
        ///// </summary>
        public ObservableCollection<FirstLevelNavItem> Navigation
        {
            get { return (ObservableCollection<FirstLevelNavItem>)GetValue(NavigationProperty); }
            set { SetValue(NavigationProperty, value); }
        }

        ///// <summary>
        ///// Gets or sets the status bar's content.
        ///// </summary>
        public object StatusBar
        {
            get { return GetValue(StatusBarProperty); }
            set { SetValue(StatusBarProperty, value); }
        }

        ///// <summary>
        ///// Gets or sets the optional navigation content for the current <see cref="AtlasWindow"/>.
        ///// </summary>
        public object OptionalNavigationContent
        {
            get { return GetValue(OptionalNavigationContentProperty); }
            set { SetValue(OptionalNavigationContentProperty, value); }
        }

        ///// <summary>
        ///// Gets or sets the optional navigation content for the current <see cref="AtlasWindow"/>.
        ///// </summary>
        public object SubSystemMenuContent
        {
            get { return GetValue(SubSystemMenuContentProperty); }
            set { SetValue(SubSystemMenuContentProperty, value); }
        }
        ///// <summary>
        ///// Gets or sets whether the optional navigation is visible or not.
        ///// </summary>
        public Visibility OptionalNavigationVisibility
        {
            get { return (Visibility)GetValue(OptionalNavigationVisibilityProperty); }
            set { SetValue(OptionalNavigationVisibilityProperty, value); }
        }

        /// <summary>
        /// Gets the method that allows to parse the content of the current <see cref="AtlasWindow"/> according to the navigation
        /// mechanism.
        /// </summary>
        public Func<object, object> ContentConverterMethod
        {
            get { return (Func<object, object>)GetValue(ContentConverterMethodProperty); }
            set { SetValue(ContentConverterMethodProperty, value); }
        }


        /// <summary>
        /// Gets the method that allows to parse the content of the current <see cref="AtlasMainPage"/> according to the navigation
        /// mechanism.
        /// </summary>
        public object SelectedContent
        {
            get { return GetValue(SelectedContentProperty); }
            set { SetValue(SelectedContentProperty, value); }
        }

        ///// <summary>
        ///// Returns the binding that for the content of the current window, content that is determined by using as input the navigation
        ///// configuration.
        ///// </summary>
        private BindingBase ContentBinding
        {
            get
            {
                if (_categoriesSelector == null || _optionalNavigationContent == null || _subSystemMenuContent == null)
                    return null;

                var firstSubBinding = new Binding("SelectedItem") { Source = _categoriesSelector };
                var secondSubBinding = new Binding("Content") { Source = _optionalNavigationContent };
                var thirdSubBinding = new Binding("Content") { Source = _subSystemMenuContent };
                var binding = new MultiBinding
                {
                    Converter = new AtlasWindowContentConverter(),
                    ConverterParameter = ContentConverterMethod
                };
                binding.Bindings.Add(firstSubBinding);
                binding.Bindings.Add(secondSubBinding);
                binding.Bindings.Add(thirdSubBinding);

                return binding;
            }
        }

        private string _optionalUri;
        public string OptionalUri
        {
            get
            {
                return _optionalUri;
            }

            set
            {
                _optionalUri = value;
            }
        }




        /// <summary>
        /// Occurs when the root navigation bar is collapsed.
        /// </summary>
        public event RoutedEventHandler RootNavBarCollapsed
        {
            add { AddHandler(RootNavBarCollapsedEvent, value); }
            remove { RemoveHandler(RootNavBarCollapsedEvent, value); }
        }

        ///// <summary>
        ///// Occurs when the root navigation bar is expanded.
        ///// </summary>
        public event RoutedEventHandler RootNavBarExpanded
        {
            add { AddHandler(RootNavBarExpandedEvent, value); }
            remove { RemoveHandler(RootNavBarExpandedEvent, value); }
        }

        ///// <summary>
        ///// Occurs when the root navigation bar is expanded.
        ///// </summary>
        //public event RoutedEventHandler Appear
        //{
        //    add { AddHandler(AppearEvent, value); }
        //    remove { RemoveHandler(AppearEvent, value); }
        //}

        ///// <summary>
        ///// Occurs when the root navigation bar is expanded.
        ///// </summary>
        //public event RoutedEventHandler Disappear
        //{
        //    add { AddHandler(DisappearEvent, value); }
        //    remove { RemoveHandler(DisappearEvent, value); }
        //}
        /// <summary>
        /// Called when the template for the current <see cref="AtlasWindow"/> is being applied.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _categoriesSelector = (Selector)Template.FindName("categories", this);
            _optionalNavigationContent = (ContentControl)Template.FindName("optionalNavigationContent", this);
            _subSystemMenuContent = (ContentControl)Template.FindName("subSystemMenu", this);
            
            _content = (ContentPresenter)Template.FindName("content", this);

             _content.SetBinding(ContentPresenter.ContentProperty, ContentBinding);

           ((AtlasModuleHelpContent)Template.FindName("AtlasModuleHelpContent", this)).MouseLeave+=OnMouseLeave;
        }

        private void OnMouseLeave(object sender, MouseEventArgs mouseEventArgs)
        {
            ShowHelp = false;
           
        }

        private static void OnCollapsedOrExpandedRootNavBar(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mainView = (AtlasModuleView)d;
            var collapsed = (bool)e.NewValue;

            if (collapsed)
                mainView.RaiseRootNavBarCollapsed();
            else
                mainView.RaiseRootNavBarExpanded();
        }

        private static void OnContentConverterMethodChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = (AtlasModuleView)d;
            window.ContentConverterMethod = e.NewValue as Func<object, object>;
            if (window.ContentBinding!=null)
            window._content.SetBinding(ContentPresenter.ContentProperty, window.ContentBinding);
         //   window.RaiseContentChanged();
        }

        private static void OnSelectedContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = (AtlasModuleView)d;
            window.RaiseContentChanged();
        }
        private IStatusBarServices _statusBarServices;

        private IStatusBarServices StatusBarServices
        {
            get
            {
                return _statusBarServices ?? (_statusBarServices = ServiceLocator.Current.GetInstance<IStatusBarServices>());
            }
        }

        public object IFirstLevelNavItem { get; private set; }

        private static void OnModuleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
            var mainView = (AtlasModuleView)d;
            if (mainView.Module != null)
            {
               // mainView.StatusBarServices.SignalText("Cargndo...");
              //  mainView.StatusBarServices.SignalLoading();
              

               mainView.SetupModule();

                //string optionalNavControlUri = "/{0};component/Presentation/Views/InvestmentElementsView.xaml".EasyFormat(mainView.Module.ModuleName);
                //mainView.SetupOptionalNavigation(optionalNavControlUri, control => (control).GetType().GetProperty("InvestmentElementTreeView").GetValue(control));

            }



        }


        public void SetupNavigation()
        {
          //  Assembly callingAssembly = Assembly.GetCallingAssembly();
          //  string assembly = callingAssembly.GetName().Name;

           // SetupNavigation("/{0};component/Presentation/Views/Navigation.xaml".EasyFormat(assembly));
        }

        private IList _navigationConfig;
        private ResourceDictionary _navigationResources;
        /// <summary>
        /// Setups the navigation configuration using the navigation resources located at a xaml laying in the provided uri.
        /// </summary>
        /// <param name="navigationSetupUri">
        /// A URI pointing to the module's navigation resources. This navigation resources declaratively must specify 
        /// all the navigation configuration.
        /// </param>
        public void SetupNavigation(string navigationSetupUri)
        {
            if(_navigationSetupUri != navigationSetupUri)
            {
                _navigationResources = (ResourceDictionary)Application.LoadComponent(new Uri(navigationSetupUri, UriKind.RelativeOrAbsolute));

                _navigationConfig = (IList)_navigationResources[GlobalParameters.NavigationResourceKey];

            }


            if (Navigation != null && Navigation.Any(x => !_navigationConfig.Cast<FirstLevelNavItem>().Any(y => y.Text == x.Text)))

            {
                Navigation.Clear();
                if (_navigationServices == null)
                    _navigationServices = ServiceLocator.Current.GetInstance<INavigationServices>();
                _navigationServices.HideOptionalNavigationContent();
            }

            int reason = _navigationConfig.Count > 0 ? 100 / _navigationConfig.Count : 0;
            int count = 0;

            foreach (FirstLevelNavItem navigationItem in _navigationConfig)
            {
                if(!Navigation.Contains(navigationItem))
                Navigation.Add(navigationItem);
                //count++;
                //_progressBar.Value = count * reason;
            }

            _navigationSetupUri = navigationSetupUri;

            IsSomeModuleLoading = false;
            IsSomeModuleLoaded = true;



            StatusBar = null;
                
          //  var yet = StatusBarServices.StatusText;
           

            
           
        }

        private void SetNavigationItems(IList navigationConfig, int count, int reason)
        {
            foreach (FirstLevelNavItem navigationItem in navigationConfig)
            {
                Navigation.Add(navigationItem);
                //count++;
                //_progressBar.Value = count * reason;
            }
        }

        private  INavigationServices _navigationServices;

        /// <summary>
        /// Dependency property to contain the command that is executed by the Add Button of the current <see cref="AtlasDataGrid"/>.
        /// </summary>
        public static readonly DependencyProperty IsSomeModuleLoadedProperty = DependencyProperty.Register("IsSomeModuleLoaded", typeof(bool), typeof(AtlasModuleView), new PropertyMetadata(false, PropertyChangedCallback2));

        private static void PropertyChangedCallback2(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
           var atlasWindow = (AtlasWindow)ServiceLocator.Current.GetInstance(typeof(Window));
           
            var state = (bool)dependencyPropertyChangedEventArgs.NewValue;
           
            //IsSomeModuleLoading = !state;
            atlasWindow.IsSomeModuleLoaded = state;
        }
        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var atlasWindow = (AtlasWindow)ServiceLocator.Current.GetInstance(typeof(Window));

            var state = (bool)dependencyPropertyChangedEventArgs.NewValue;

            atlasWindow.IsSomeModuleLoading = state;
            
        }
        ///// <summary>
        ///// Gets or sets the default content that will be displayed in the current <see cref="AtlasWindow"/> when there is none shown.
        ///// </summary>
        public bool IsSomeModuleLoaded
        {
            get { return (bool)GetValue(IsSomeModuleLoadedProperty); }
            set { SetValue(IsSomeModuleLoadedProperty, value); }
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
              

            }
        }

        private string _navigationSetupUri;
       
        private ICopyPasteable _objectToPasteOn;

        /// <summary>
        /// Sets up an optional navigation control using resources located the given xaml's URI.
        /// </summary>
        /// <param name="optionalNavigationUri">
        /// The URI to the xaml where the navigation setup is located.
        /// </param>
        /// <param name="getContentMethod">
        /// A method that allows to get the control containing the data context of the optional navigation structure.
        /// For instance: if a certain window uses optional navigation, its content's data context will be set according to the
        /// control returned by <paramref name="getContentMethod"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="optionalNavigationUri"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="optionalNavigationUri"/> specifies an invalid URI.</exception>

        public void SetupOptionalNavigation(string optionalNavigationUri, Func<object, object> getContentMethod)
        {
            OptionalUri = optionalNavigationUri;
            OptionalNavigationContent = (System.Windows.Controls.Control)Application.LoadComponent(new Uri(optionalNavigationUri, UriKind.RelativeOrAbsolute));
            ContentConverterMethod = getContentMethod;
        }

        public void SetupSubSystemMenu(string subSystemMenuUri, Func<object, object> getContentMethod)
        {
            SubSystemMenuContent = (System.Windows.Controls.Control)Application.LoadComponent(new Uri(subSystemMenuUri, UriKind.RelativeOrAbsolute));

            //    var viewModel = ServiceLocator.Current.GetInstance<IAtlasModuleMainSubjectViewModel>();
            var viewModel = (IAtlasModuleMainSubjectViewModel)ServiceLocator.Current.GetInstance(typeof(IAtlasModuleMainSubjectViewModel));
            viewModel.AssemblyName = Module.ModuleName; 
           
            // viewModel.Collection = ServiceLocator.Current.GetInstance<IInvestmentProvider>().Investments;
            viewModel.Load();
            ModuleHelpViewModel = viewModel;
            // ContentConverterMethod = getContentMethod;
        }

        public void SetupSubSystemDefaultContent(string subSystemMenuUri, Func<object, object> getContentMethod)
        {
            DefaultContent = (System.Windows.Controls.Control)Application.LoadComponent(new Uri(subSystemMenuUri, UriKind.RelativeOrAbsolute));

          
        }
        public void ShowOptionalNavigationContent()
        {
            OptionalNavigationVisibility = Visibility.Visible;
        }

        public void HideOptionalNavigationContent()
        {
            OptionalNavigationVisibility = Visibility.Collapsed;
        }

        private void RaiseRootNavBarCollapsed()
        {
            RaiseEvent(new RoutedEventArgs(RootNavBarCollapsedEvent));
        }

        private void RaiseRootNavBarExpanded()
        {
            RaiseEvent(new RoutedEventArgs(RootNavBarExpandedEvent));
        }

        private void RaiseContentChanged()
        {
            RaiseEvent(new RoutedEventArgs(ContentChangedEvent));
        }

        public IEntity CopiedObject { get; set; }
        public void Paste()
        {
         
            if(Equals(CopiedObject,null))
                return;
            if (Equals(ObjectToPasteOn, null))
                return;
            if (((ObjectToPasteOn as IPresenter).Object as IEntity).Id.ToString() == CopiedObject.Id.ToString())
                return;
            try
            {
                ObjectToPasteOn.CopiedObject = CopiedObject;
                ObjectToPasteOn.Paste();
            }
            catch (Exception e)
            {
               // Console.WriteLine(e);
                throw new Exception(e.Message);
            }
            
        }

        public ICopyPasteable ObjectToPasteOn
        {
            get { return _objectToPasteOn; }
            set
            {
                _objectToPasteOn = value;
                OnPropertyChanged(nameof(CanCopy));
            }
        }

        public ICommand CopyCommand { get; }
        public ICommand PasteCommand { get; }

        public bool CanCopy { get { return CanDoCopy(); } }

        public bool CanPaste { get { return CanDoPaste(); } }

        //private void RaiseAppear()
        //{
        //    RaiseEvent(new RoutedEventArgs(AppearEvent));
        //}
        //private void RaiseDisappear()
        //{
        //    RaiseEvent(new RoutedEventArgs(DisappearEvent));
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
