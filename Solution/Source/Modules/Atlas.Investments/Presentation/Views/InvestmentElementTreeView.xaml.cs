using System;
using System.Windows;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Features;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for InvestmentElementTreeView.xaml
    /// </summary>
    public partial class InvestmentElementTreeView : ICanAdd
    {
        /// <summary>
        /// Dependency property for the text of the Add button of <see cref="InvestmentElementTreeView"/> instances.
        /// </summary>
        public static readonly DependencyProperty AddButtonTextProperty = DependencyProperty.Register("AddButtonText", typeof(string), typeof(InvestmentElementTreeView), new PropertyMetadata(Properties.Resources.Add));

        /// <summary>
        /// Dependency property for the tooltip of the Add button of <see cref="InvestmentElementTreeView"/> instances.
        /// </summary>
        public static readonly DependencyProperty AddButtonTooltipProperty = DependencyProperty.Register("AddButtonTooltip", typeof(object), typeof(InvestmentElementTreeView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property for the tooltip of the Add child Investment element button of <see cref="InvestmentElementTreeView"/> instances.
        /// </summary>
        public static readonly DependencyProperty AddInvestmentElemementButtonTooltipProperty = DependencyProperty.Register("AddInvestmentElemementButtonTooltip", typeof(object), typeof(InvestmentElementTreeView), new PropertyMetadata(null));
        
        /// <summary>
        /// Dependency property for the tooltip of the Delete Investment element button of <see cref="InvestmentElementTreeView"/> instances.
        /// </summary>
        public static readonly DependencyProperty DeleteInvestmentElemementButtonTooltipProperty = DependencyProperty.Register("DeleteInvestmentElemementButtonTooltip", typeof(object), typeof(InvestmentElementTreeView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the command that the big Add button that
        /// <see cref="InvestmentElementTreeView"/> instances contains, representing the operation of adding a new root
        /// investment element.
        /// </summary>
        public static readonly DependencyProperty AddRootInvestmentElementButtonCommandProperty = DependencyProperty.Register("AddRootInvestmentElementButtonCommand", typeof(ICommand), typeof(InvestmentElementTreeView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the command that allows to add a child investment element to the one currently focused 
        /// in an instance of <see cref="InvestmentElementTreeView"/>.
        /// </summary>
        public static readonly DependencyProperty AddInvestmentElementButtonCommandProperty = DependencyProperty.Register("AddInvestmentElementButtonCommand", typeof(ICommand), typeof(InvestmentElementTreeView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property containing the command that allows to delete a child investment element to the one currently focused 
        /// in an instance of <see cref="InvestmentElementTreeView"/>.
        /// </summary>
        public static readonly DependencyProperty DeleteInvestmentElementButtonCommandProperty = DependencyProperty.Register("DeleteInvestmentElementButtonCommand", typeof(ICommand), typeof(InvestmentElementTreeView), new PropertyMetadata(null));

        /// <summary>
        /// Dependency property holding the value saying whether a <see cref="InvestmentElementTreeView"/> is collased or not.
        /// </summary>
        public static readonly DependencyProperty IsCollapsedProperty = DependencyProperty.Register("IsCollapsed", typeof(bool), typeof(InvestmentElementTreeView), new PropertyMetadata(false, OnIsCollapsedChanged));

        /// <summary>
        /// Defines the routed event fired when the a <see cref="InvestmentElementTreeView"/> gets collapsed.
        /// </summary>
        public static readonly RoutedEvent CollapsedEvent = EventManager.RegisterRoutedEvent("Collapsed", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(InvestmentElementTreeView));

        /// <summary>
        /// Defines the routed event fired when the a <see cref="InvestmentElementTreeView"/> gets expanded.
        /// </summary>
        public static readonly RoutedEvent ExpandedEvent = EventManager.RegisterRoutedEvent("Expanded", RoutingStrategy.Tunnel, typeof(RoutedEventHandler), typeof(InvestmentElementTreeView));

        /// <summary>
        /// Dependency property holding the value saying whether a <see cref="InvestmentElementTreeView"/> is collased or not.
        /// </summary>
        public static readonly DependencyProperty CanAddProperty = DependencyProperty.Register("CanAdd", typeof(bool), typeof(InvestmentElementTreeView), new PropertyMetadata(false));
        
        /// <summary>
        /// Dependency property containing the Filter command for instances of <see cref="FilterBox"/>.
        /// </summary>
        public static readonly DependencyProperty FilterCommandProperty = DependencyProperty.Register("FilterCommand", typeof(ICommand), typeof(InvestmentElementTreeView));

        public static readonly DependencyProperty InEditionProperty =
         DependencyProperty.Register("InEdition", typeof(bool), typeof(InvestmentElementTreeView), new PropertyMetadata(false));


        /// <summary>
        /// Initializes a new instance of <see cref="InvestmentElementTreeView"/>.
        /// </summary>
        public InvestmentElementTreeView()
        {
            InitializeComponent();

            KeyDown += OnKeyDown;
            SelectedItemChanged += OnSelectedItemChanged;
            IsVisibleChanged += OnIsVisibleChanged;
        }


        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            _navigationServices.ObjectToPasteOn = null;
        }

        private void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> routedPropertyChangedEventArgs)
        {
            _navigationServices.ObjectToPasteOn = SelectedItem as ICopyPasteable;
        }

        private readonly INavigationServices _navigationServices = ServiceLocator.Current.GetInstance<INavigationServices>();
        private bool _isControlPressed;

        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if(Equals(SelectedItem,null))
                return;
           
           
            if (_isControlPressed && keyEventArgs.Key == Key.V)
            {
                _navigationServices.ObjectToPasteOn = SelectedItem as ICopyPasteable;
                _navigationServices.Paste();
                //var investmentElement = (INavigable) SelectedItem;

                //if (Equals(investmentElement, null))
                //    return;
                //investmentElement.DoNotify();
              

            }
            if (_isControlPressed && keyEventArgs.Key == Key.C)
            {
                if (SelectedItem as IInvestmentPresenter == null)
                _navigationServices.CopiedObject = ( (IPresenter) SelectedItem).Object as IEntity;
              
            }

            _isControlPressed = keyEventArgs.Key == Key.LeftCtrl || keyEventArgs.Key == Key.RightCtrl;

        }

        /// <summary>
        /// Gets whether this control is in edition mode.
        /// </summary>
        public bool InEdition
        {
            get { return (bool)GetValue(InEditionProperty); }
            set { SetValue(InEditionProperty, value); }
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
        /// Gets whether the current <see cref="InvestmentElementTreeView"/> is collapsed or not.
        /// </summary>
        public bool IsCollapsed
        {
            get { return (bool)GetValue(IsCollapsedProperty); }
            set { SetValue(IsCollapsedProperty, value); }
        }

        /// <summary>
        /// Gets whether the current <see cref="InvestmentElementTreeView"/> is collapsed or not.
        /// </summary>
        public bool CanAdd
        {
            get { return (bool)GetValue(CanAddProperty); }
            set { SetValue(CanAddProperty, value); }
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
        /// Occurs when the current <see cref="InvestmentElementTreeView"/> gets collapsed.
        /// </summary>
        public event RoutedEventHandler Collapsed
        {
            add { AddHandler(CollapsedEvent, value); }
            remove { RemoveHandler(CollapsedEvent, value); }
        }

        /// <summary>
        /// Occurs when the current <see cref="InvestmentElementTreeView"/> gets exanded.
        /// </summary>
        public event RoutedEventHandler Expanded
        {
            add { AddHandler(ExpandedEvent, value); }
            remove { RemoveHandler(ExpandedEvent, value); }
        }


        private static void OnIsCollapsedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (InvestmentElementTreeView)d;
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
