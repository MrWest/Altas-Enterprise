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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for CashMovementTreeView.xaml
    /// </summary>
    public partial class CashMovementTreeView 
    {
        public CashMovementTreeView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Dependency property for the text of the Add button of <see cref="CashMovementTreeView"/> instances.
        /// </summary>
        public static readonly DependencyProperty AddButtonEntryTextProperty = DependencyProperty.Register("AddButtonEntryText", typeof(string), typeof(CashMovementTreeView), new PropertyMetadata(Properties.Resources.AddEntry));
        /// <summary>
        /// Dependency property for the text of the Add button of <see cref="CashMovementTreeView"/> instances.
        /// </summary>
        public static readonly DependencyProperty AddButtonOutgoingTextProperty = DependencyProperty.Register("AddButtonOutgoingText", typeof(string), typeof(CashMovementTreeView), new PropertyMetadata(Properties.Resources.AddOutgoing));

        /// <
        /// <summary>
        /// Dependency property for the tooltip of the Add button of <see cref="CashMovementTreeView"/> instances.
        /// </summary>
        public static readonly DependencyProperty AddButtonTooltipProperty = DependencyProperty.Register("AddButtonTooltip", typeof(object), typeof(CashMovementTreeView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property for the tooltip of the Add child Investment element button of <see cref="CashMovementTreeView"/> instances.
        /// </summary>
        public static readonly DependencyProperty AddInvestmentElemementButtonTooltipProperty = DependencyProperty.Register("AddInvestmentElemementButtonTooltip", typeof(object), typeof(CashMovementTreeView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property for the tooltip of the Delete Investment element button of <see cref="CashMovementTreeView"/> instances.
        /// </summary>
        public static readonly DependencyProperty DeleteInvestmentElemementButtonTooltipProperty = DependencyProperty.Register("DeleteInvestmentElemementButtonTooltip", typeof(object), typeof(CashMovementTreeView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the command that the big Add button that
        /// <see cref="CashMovementTreeView"/> instances contains, representing the operation of adding a new root
        /// investment element.
        /// </summary>
        public static readonly DependencyProperty AddRootInvestmentElementButtonCommandProperty = DependencyProperty.Register("AddRootInvestmentElementButtonCommand", typeof(ICommand), typeof(CashMovementTreeView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the command that allows to add a child investment element to the one currently focused 
        /// in an instance of <see cref="CashMovementTreeView"/>.
        /// </summary>
        public static readonly DependencyProperty AddInvestmentElementButtonCommandProperty = DependencyProperty.Register("AddInvestmentElementButtonCommand", typeof(ICommand), typeof(CashMovementTreeView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the command that allows to delete a child investment element to the one currently focused 
        /// in an instance of <see cref="CashMovementTreeView"/>.
        /// </summary>
        public static readonly DependencyProperty DeleteInvestmentElementButtonCommandProperty = DependencyProperty.Register("DeleteInvestmentElementButtonCommand", typeof(ICommand), typeof(CashMovementTreeView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property holding the value saying whether a <see cref="CashMovementTreeView"/> is collased or not.
        /// </summary>
        public static readonly DependencyProperty IsCollapsedProperty = DependencyProperty.Register("IsCollapsed", typeof(bool), typeof(CashMovementTreeView), new PropertyMetadata(false, OnIsCollapsedChanged));

        /// <summary>
        /// Defines the routed event fired when the a <see cref="CashMovementTreeView"/> gets collapsed.
        /// </summary>
        public static readonly RoutedEvent CollapsedEvent = EventManager.RegisterRoutedEvent("Collapsed", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(CashMovementTreeView));

        /// <summary>
        /// Defines the routed event fired when the a <see cref="CashMovementTreeView"/> gets expanded.
        /// </summary>
        public static readonly RoutedEvent ExpandedEvent = EventManager.RegisterRoutedEvent("Expanded", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(CashMovementTreeView));

        /// <summary>
        /// Dependency property containing the command that allows to delete a child investment element to the one currently focused 
        /// in an instance of <see cref="CashMovementTreeView"/>.
        /// </summary>
        public static readonly DependencyProperty ShowCollapseButtomProperty = DependencyProperty.Register("ShowCollapseButtom", typeof(Visibility), typeof(CashMovementTreeView), new PropertyMetadata(System.Windows.Visibility.Collapsed));

        /// <summary>
        /// Dependency property containing the command that allows to delete a child investment element to the one currently focused 
        /// in an instance of <see cref="CashMovementTreeView"/>.
        /// </summary>
        public static readonly DependencyProperty ButtonAngleProperty = DependencyProperty.Register("ButtonAngle", typeof(int), typeof(CashMovementTreeView), new PropertyMetadata(0));


        /// <summary>
        /// Gets or sets the text for the Add button.
        /// </summary>
        public string AddButtonEntryText
        {
            get { return (string)GetValue(AddButtonEntryTextProperty); }
            set { SetValue(AddButtonEntryTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text for the Add button.
        /// </summary>
        public string AddButtonOutgoingText
        {
            get { return (string)GetValue(AddButtonOutgoingTextProperty); }
            set { SetValue(AddButtonOutgoingTextProperty, value); }
        }
        /// <summary>
        /// Gets or sets the text for the Add button.
        /// </summary>
        public int ButtonAngle
        {
            get { return (int)GetValue(ButtonAngleProperty); }
            set { SetValue(ButtonAngleProperty, value); }
        }
        /// <summary>
        /// Gets or sets the text for the Add button.
        /// </summary>
        public Visibility ShowCollapseButtom
        {
            get { return (Visibility)GetValue(ShowCollapseButtomProperty); }
            set { SetValue(ShowCollapseButtomProperty, value); }
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
        /// Gets whether the current <see cref="CashMovementTreeView"/> is collapsed or not.
        /// </summary>
        public bool IsCollapsed
        {
            get { return (bool)GetValue(IsCollapsedProperty); }
            set { SetValue(IsCollapsedProperty, value); }
        }


        /// <summary>
        /// Occurs when the current <see cref="CashMovementTreeView"/> gets collapsed.
        /// </summary>
        public event RoutedEventHandler Collapsed
        {
            add { AddHandler(CollapsedEvent, value); }
            remove { RemoveHandler(CollapsedEvent, value); }
        }

        /// <summary>
        /// Occurs when the current <see cref="CashMovementTreeView"/> gets exanded.
        /// </summary>
        public event RoutedEventHandler Expanded
        {
            add { AddHandler(ExpandedEvent, value); }
            remove { RemoveHandler(ExpandedEvent, value); }
        }


        private static void OnIsCollapsedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (CashMovementTreeView)d;
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
