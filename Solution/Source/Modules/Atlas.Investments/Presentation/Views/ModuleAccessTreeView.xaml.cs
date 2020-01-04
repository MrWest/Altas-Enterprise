using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyName.Atlas.UIControls;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ModuleAccessTreeView.xaml
    /// </summary>
    public partial class ModuleAccessTreeView
    {
        /// <summary>
        /// Dependency property for the text of the Add button of <see cref="ModuleAccessTreeView"/> instances.
        /// </summary>
        public static readonly DependencyProperty AddButtonTextProperty = DependencyProperty.Register("AddButtonText", typeof(string), typeof(ModuleAccessTreeView), new PropertyMetadata(Properties.Resources.Add));

        /// <summary>
        /// Dependency property for the tooltip of the Add button of <see cref="ModuleAccessTreeView"/> instances.
        /// </summary>
        public static readonly DependencyProperty AddButtonTooltipProperty = DependencyProperty.Register("AddButtonTooltip", typeof(object), typeof(ModuleAccessTreeView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property for the tooltip of the Add child Investment element button of <see cref="ModuleAccessTreeView"/> instances.
        /// </summary>
        public static readonly DependencyProperty AddInvestmentElemementButtonTooltipProperty = DependencyProperty.Register("AddInvestmentElemementButtonTooltip", typeof(object), typeof(ModuleAccessTreeView), new PropertyMetadata(null));
        
        /// <summary>
        /// Dependency property for the tooltip of the Delete Investment element button of <see cref="ModuleAccessTreeView"/> instances.
        /// </summary>
        public static readonly DependencyProperty DeleteInvestmentElemementButtonTooltipProperty = DependencyProperty.Register("DeleteInvestmentElemementButtonTooltip", typeof(object), typeof(ModuleAccessTreeView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the command that the big Add button that
        /// <see cref="ModuleAccessTreeView"/> instances contains, representing the operation of adding a new root
        /// investment element.
        /// </summary>
        public static readonly DependencyProperty AddRootInvestmentElementButtonCommandProperty = DependencyProperty.Register("AddRootInvestmentElementButtonCommand", typeof(ICommand), typeof(ModuleAccessTreeView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the command that allows to add a child investment element to the one currently focused 
        /// in an instance of <see cref="ModuleAccessTreeView"/>.
        /// </summary>
        public static readonly DependencyProperty AddInvestmentElementButtonCommandProperty = DependencyProperty.Register("AddInvestmentElementButtonCommand", typeof(ICommand), typeof(ModuleAccessTreeView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the command that allows to delete a child investment element to the one currently focused 
        /// in an instance of <see cref="ModuleAccessTreeView"/>.
        /// </summary>
        public static readonly DependencyProperty DeleteInvestmentElementButtonCommandProperty = DependencyProperty.Register("DeleteInvestmentElementButtonCommand", typeof(ICommand), typeof(ModuleAccessTreeView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property holding the value saying whether a <see cref="ModuleAccessTreeView"/> is collased or not.
        /// </summary>
        public static readonly DependencyProperty IsCollapsedProperty = DependencyProperty.Register("IsCollapsed", typeof(bool), typeof(ModuleAccessTreeView), new PropertyMetadata(false, OnIsCollapsedChanged));

        /// <summary>
        /// Defines the routed event fired when the a <see cref="ModuleAccessTreeView"/> gets collapsed.
        /// </summary>
        public static readonly RoutedEvent CollapsedEvent = EventManager.RegisterRoutedEvent("Collapsed", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(ModuleAccessTreeView));

        /// <summary>
        /// Defines the routed event fired when the a <see cref="ModuleAccessTreeView"/> gets expanded.
        /// </summary>
        public static readonly RoutedEvent ExpandedEvent = EventManager.RegisterRoutedEvent("Expanded", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(ModuleAccessTreeView));

        /// <summary>
        /// Dependency property containing the Filter command for instances of <see cref="FilterBox"/>.
        /// </summary>
        public static readonly DependencyProperty FilterCommandProperty = DependencyProperty.Register("FilterCommand", typeof(ICommand), typeof(ModuleAccessTreeView));


        /// <summary>
        /// Initializes a new instance of <see cref="ModuleAccessTreeView"/>.
        /// </summary>
        public ModuleAccessTreeView()
        {
            InitializeComponent();

        }


        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public ICommand FilterCommand
        {
            get { return (ICommand)GetValue(FilterCommandProperty); }
            set { SetValue(FilterCommandProperty, value); }
        }


        /// <summary>
        /// Gets or sets the text for the Add button.
        /// </summary>
        public string AddButtonText
        {
            get { return (string)GetValue(AddButtonTextProperty); }
            set { SetValue(AddButtonTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tooltip of the Add button.
        /// </summary>
        public object AddButtonTooltip
        {
            get { return GetValue(AddButtonTooltipProperty); }
            set { SetValue(AddButtonTooltipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tooltip of the Add child Investment Element button.
        /// </summary>
        public object AddInvestmentElemementButtonTooltip
        {
            get { return GetValue(AddInvestmentElemementButtonTooltipProperty); }
            set { SetValue(AddInvestmentElemementButtonTooltipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tooltip of the Delete Investment Element button.
        /// </summary>
        public object DeleteInvestmentElemementButtonTooltip
        {
            get { return GetValue(DeleteInvestmentElemementButtonTooltipProperty); }
            set { SetValue(DeleteInvestmentElemementButtonTooltipProperty, value); }
        }

        /// <summary>
        /// Gets the <see cref="ICommand"/> representing the operation of inserting a new root investment element.
        /// </summary>
        public ICommand AddRootInvestmentElementButtonCommand
        {
            get { return (ICommand)GetValue(AddRootInvestmentElementButtonCommandProperty); }
            set { SetValue(AddRootInvestmentElementButtonCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command that allows to add a child investment element to the one currently focused.
        /// </summary>
        public ICommand AddInvestmentElementButtonCommand
        {
            get { return (ICommand)GetValue(AddInvestmentElementButtonCommandProperty); }
            set { SetValue(AddInvestmentElementButtonCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command that allows to delete a child investment element to the one currently focused.
        /// </summary>
        public ICommand DeleteInvestmentElementButtonCommand
        {
            get { return (ICommand)GetValue(DeleteInvestmentElementButtonCommandProperty); }
            set { SetValue(DeleteInvestmentElementButtonCommandProperty, value); }
        }

        /// <summary>
        /// Gets whether the current <see cref="ModuleAccessTreeView"/> is collapsed or not.
        /// </summary>
        public bool IsCollapsed
        {
            get { return (bool)GetValue(IsCollapsedProperty); }
            set { SetValue(IsCollapsedProperty, value); }
        }


        /// <summary>
        /// Occurs when the current <see cref="ModuleAccessTreeView"/> gets collapsed.
        /// </summary>
        public event RoutedEventHandler Collapsed
        {
            add { AddHandler(CollapsedEvent, value); }
            remove { RemoveHandler(CollapsedEvent, value); }
        }

        /// <summary>
        /// Occurs when the current <see cref="ModuleAccessTreeView"/> gets exanded.
        /// </summary>
        public event RoutedEventHandler Expanded
        {
            add { AddHandler(ExpandedEvent, value); }
            remove { RemoveHandler(ExpandedEvent, value); }
        }


        private static void OnIsCollapsedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (ModuleAccessTreeView)d;
            var value = (bool)e.NewValue;

            if (value)
                view.RaiseCollapsed();
            else
                view.RaiseExpanded();
        }

        private void RaiseCollapsed()
        {
            RaiseEvent(new RoutedEventArgs(CollapsedEvent));
        }

        private void RaiseExpanded()
        {
            RaiseEvent(new RoutedEventArgs(ExpandedEvent));
        }
    }
}
