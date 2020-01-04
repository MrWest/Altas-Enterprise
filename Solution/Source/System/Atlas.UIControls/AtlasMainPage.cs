using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using CompanyName.Atlas.UIControls.Converters;
using Microsoft.Practices.Prism.Modularity;

namespace CompanyName.Atlas.UIControls
{
    /// <summary>
    /// Interface for lobby Atlas System
    /// </summary>
    public class AtlasMainPage:ContentControl
    {
        private Selector _categoriesSelector;
        private ContentControl _optionalNavigationContent;
        private ContentPresenter _content;
        /// <summary>
        /// Dependency property used to contain the collection of menu items a <see cref="AtlasMainPage"/> menu has.
        /// </summary>
        public static readonly DependencyProperty MenuProperty = DependencyProperty.Register("Menu", typeof(ObservableCollection<MenuItem>), typeof(AtlasMainPage), new PropertyMetadata(new ObservableCollection<MenuItem>()));

        /// <summary>
        /// Dependency property used to contain the collection of navigation items a <see cref="AtlasMainPage"/> navigationbar has.
        /// </summary>
        public static readonly DependencyProperty NavigationProperty = DependencyProperty.Register("Navigation", typeof(ObservableCollection<FirstLevelNavItem>), typeof(AtlasMainPage), new PropertyMetadata(new ObservableCollection<FirstLevelNavItem>()));

        /// <summary>
        /// Dependency property containing the default content for the instances of <see cref="AtlasMainPage"/> having no custom content.
        /// </summary>
        public static readonly DependencyProperty DefaultContentProperty = DependencyProperty.Register("DefaultContent", typeof(object), typeof(AtlasMainPage), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty IsRootNavBarCollapsedProperty = DependencyProperty.Register("IsRootNavBarCollapsed", typeof(bool), typeof(AtlasMainPage), new PropertyMetadata(false, OnCollapsedOrExpandedRootNavBar));

        /// <summary>
        /// Dependency property used to contain the content of the status bar of the <see cref="AtlasMainPage"/> instances.
        /// </summary>
        public static readonly DependencyProperty StatusBarProperty = DependencyProperty.Register("StatusBar", typeof(object), typeof(AtlasMainPage), new PropertyMetadata(null));

       /// <summary>
        /// Dependency property used to contain the optional navigation content of <see cref="AtlasMainPage"/> instances.
        /// </summary>
        public static readonly DependencyProperty OptionalNavigationContentProperty = DependencyProperty.Register("OptionalNavigationContent", typeof(object), typeof(AtlasMainPage), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the value of the whether there is visible or not the optional navigation content of <see cref="AtlasMainPage"/> instances.
        /// </summary>
        public static readonly DependencyProperty OptionalNavigationVisibilityProperty = DependencyProperty.Register("OptionalNavigationVisibility", typeof(Visibility), typeof(AtlasMainPage), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Dependency property used to hold the method that allows to parse the content of instances of <see cref="AtlasMainPage"/>
        /// according to the navigation mechanism.
        /// </summary>
        public static readonly DependencyProperty ContentConverterMethodProperty = DependencyProperty.Register("ContentConverterMethod", typeof(Func<object, object>), typeof(AtlasMainPage), new PropertyMetadata(null, OnContentConverterMethodChanged));

        /// <summary>
        /// Dependency property used to hold the method that allows to parse the content of instances of <see cref="AtlasMainPage"/>
        /// according to the navigation mechanism.
        /// </summary>
        public static readonly DependencyProperty SelectedContentProperty = DependencyProperty.Register("SelectedContent", typeof(object), typeof(AtlasMainPage), new PropertyMetadata(null, OnSelectedContentChanged));

      
        /// <summary>
        /// Routed event defining the event raised when the root navigation bar got collapsed.
        /// </summary>
        public static readonly RoutedEvent RootNavBarCollapsedEvent = EventManager.RegisterRoutedEvent("RootNavBarCollapsed", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(AtlasMainPage));

        /// <summary>
        /// Routed event defining the event raised when the root navigation bar got expanded.
        /// </summary>
        public static readonly RoutedEvent RootNavBarExpandedEvent = EventManager.RegisterRoutedEvent("RootNavBarExpanded", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(AtlasMainPage));

        /// <summary>
        /// Routed event defining the event raised when the root navigation bar got expanded.
        /// </summary>
        public static readonly RoutedEvent ContentChangedEvent = EventManager.RegisterRoutedEvent("ContentChanged", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(AtlasMainPage));



        /// <summary>
        /// Dependency property containing the icon for instance of <see cref="SecondLevelNavItem"/>.
        /// </summary>
        public static readonly DependencyProperty ModulesProperty = DependencyProperty.Register("Modules", typeof(IEnumerable<ModuleInfo>), typeof(AtlasMainPage), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the object representing the icon to display by the current <see cref="AtlasModuleButton"/>.
        /// </summary>
        public IEnumerable<ModuleInfo> Modules
        {
            get { return (IEnumerable<ModuleInfo>)GetValue(ModulesProperty); }
            set { SetValue(ModulesProperty, value); }
        }
         /// <summary>
        /// Initializes a new instance of <see cref="AtlasMainPage"/>.
        /// </summary>
        public AtlasMainPage()
        {
            DefaultStyleKey = typeof(AtlasMainPage);

            //var progressBar = new ProgressBar
            //{
            //    IsIndeterminate = true,
               
               
            //    Visibility = Visibility.Collapsed
            //};
            //StatusBar = progressBar;
        }


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
            ProgressBarStyle = this.FindResource(typeof(ProgressBar)) as Style;
            // _content.SetBinding(ContentPresenter.ContentProperty, ContentBinding);
        }

        private Style ProgressBarStyle { get; set; }
        /// <summary>
        /// Gets or sets the default content that will be displayed in the current <see cref="AtlasMainPage"/> when there is none shown.
        /// </summary>
        public object DefaultContent
        {
            get { return GetValue(DefaultContentProperty); }
            set { SetValue(DefaultContentProperty, value); }
        }

        /// <summary>
        /// Gets of sets whether the root navigation bar is collapsed or not.
        /// </summary>
        public bool IsRootNavBarCollapsed
        {
            get { return (bool)GetValue(IsRootNavBarCollapsedProperty); }
            set { SetValue(IsRootNavBarCollapsedProperty, value); }
        }

        /// <summary>
        /// Gets or sets the collection of <see cref="FirstLevelNavItem"/> that are displayed in the current 
        /// <see cref="AtlasMainPage"/> menu.
        /// </summary>
        public ObservableCollection<FirstLevelNavItem> Navigation
        {
            get { return (ObservableCollection<FirstLevelNavItem>)GetValue(NavigationProperty); }
            set { SetValue(NavigationProperty, value); }
        }
        /// <summary>
        /// Gets or sets the status bar's content.
        /// </summary>
        public object StatusBar
        {
            get { return GetValue(StatusBarProperty); }
            set { SetValue(StatusBarProperty, value); }
        }
        /// <summary>
        /// Gets or sets the collection of menu items that are displayed in the current <see cref="AtlasMainPage"/> menu.
        /// </summary>
        public ObservableCollection<MenuItem> Menu
        {
            get { return (ObservableCollection<MenuItem>)GetValue(MenuProperty); }
            set { SetValue(MenuProperty, value); }
        }
        /// <summary>
        /// Gets the method that allows to parse the content of the current <see cref="AtlasMainPage"/> according to the navigation
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
        
        /// <summary>
        /// Occurs when the root navigation bar is collapsed.
        /// </summary>
        public event RoutedEventHandler RootNavBarCollapsed
        {
            add { AddHandler(RootNavBarCollapsedEvent, value); }
            remove { RemoveHandler(RootNavBarCollapsedEvent, value); }
        }

        /// <summary>
        /// Gets or sets the optional navigation content for the current <see cref="AtlasWindow"/>.
        /// </summary>
        public object OptionalNavigationContent
        {
            get { return GetValue(OptionalNavigationContentProperty); }
            set { SetValue(OptionalNavigationContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets whether the optional navigation is visible or not.
        /// </summary>
        public Visibility OptionalNavigationVisibility
        {
            get { return (Visibility)GetValue(OptionalNavigationVisibilityProperty); }
            set { SetValue(OptionalNavigationVisibilityProperty, value); }
        }
        /// <summary>
        /// Occurs when the root navigation bar is expanded.
        /// </summary>
        public event RoutedEventHandler RootNavBarExpanded
        {
            add { AddHandler(RootNavBarExpandedEvent, value); }
            remove { RemoveHandler(RootNavBarExpandedEvent, value); }
        }

        /// <summary>
        /// Occurs when the root navigation bar is expanded.
        /// </summary>
        public event RoutedEventHandler ContentChanged
        {
            add { AddHandler(ContentChangedEvent, value); }
            remove { RemoveHandler(ContentChangedEvent, value); }
        }
        private static void OnCollapsedOrExpandedRootNavBar(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mainView = (AtlasMainPage)d;
            var collapsed = (bool)e.NewValue;

            if (collapsed)
                mainView.RaiseRootNavBarCollapsed();
            else
                mainView.RaiseRootNavBarExpanded();
        }

        /// <summary>
        /// Returns the binding that for the content of the current window, content that is determined by using as input the navigation
        /// configuration.
        /// </summary>
        private BindingBase ContentBinding
        {
            get
            {
               // if (_categoriesSelector == null || _optionalNavigationContent == null)
                 if (_categoriesSelector == null )
                    return null;

                var firstSubBinding = new Binding("SelectedItem") { Source = _categoriesSelector };
                var secondSubBinding = new Binding("Content") { Source = _optionalNavigationContent };
                var binding = new MultiBinding
                {
                    Converter = new AtlasWindowContentConverter(),
                    ConverterParameter = ContentConverterMethod
                };
                binding.Bindings.Add(firstSubBinding);
                binding.Bindings.Add(secondSubBinding);

                return binding;
            }
        }
        private static void OnContentConverterMethodChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = (AtlasMainPage)d;
            window.ContentConverterMethod = e.NewValue as Func<object, object>;
            window._content.SetBinding(ContentPresenter.ContentProperty, window.ContentBinding);
           // window.RaiseContentChanged();
        }

       
        private static void OnSelectedContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = (AtlasMainPage)d;
            window.RaiseContentChanged();
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
    }
}
