using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CompanyName.Atlas.Investments.Properties;
using CompanyName.Atlas.UIControls.Annotations;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Rerporting
{
    public class CustomReportSettings : DependencyObject,INotifyPropertyChanged
    {
        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty CustomReporNameProperty =
            DependencyProperty.Register("CustomReporName", typeof(string), typeof(CustomReportSettings),
                new PropertyMetadata(Resources.CustomReport));
        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty ShowInvestmentElementsProperty = DependencyProperty.Register("ShowInvestmentElements", typeof(bool), typeof(CustomReportSettings), new PropertyMetadata(true));
        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty ShowBudgetComponentsProperty = DependencyProperty.Register("ShowBudgetComponents", typeof(bool), typeof(CustomReportSettings), new PropertyMetadata(true));

        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty ShowSubSpecialitiesProperty = DependencyProperty.Register("ShowSubSpecialities", typeof(bool), typeof(CustomReportSettings), new PropertyMetadata(true));
        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty ShowActivitiesProperty = DependencyProperty.Register("ShowActivities", typeof(bool), typeof(CustomReportSettings), new PropertyMetadata(true));

        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty ShowResourcesProperty = DependencyProperty.Register("ShowResources", typeof(bool), typeof(CustomReportSettings), new PropertyMetadata(true));

        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty ShowEquipmentProperty = DependencyProperty.Register("ShowEquipment", typeof(bool), typeof(CustomReportSettings), new PropertyMetadata(true));

        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty ShowConstructionProperty = DependencyProperty.Register("ShowConstruction", typeof(bool), typeof(CustomReportSettings), new PropertyMetadata(true));

        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty ShowOthersProperty = DependencyProperty.Register("ShowOthers", typeof(bool), typeof(CustomReportSettings), new PropertyMetadata(true));

        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty ShowWorkCapitalProperty = DependencyProperty.Register("ShowWorkCapital", typeof(bool), typeof(CustomReportSettings), new PropertyMetadata(true));


        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty ShowMUProperty = DependencyProperty.Register("ShowMU", typeof(bool), typeof(CustomReportSettings), new PropertyMetadata(true,PropertyChangedCallback));

       

        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty ShowQuantityProperty = DependencyProperty.Register("ShowQuantity", typeof(bool), typeof(CustomReportSettings), new PropertyMetadata(true));

        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty ShowCurrencyProperty = DependencyProperty.Register("ShowCurrency", typeof(bool), typeof(CustomReportSettings), new PropertyMetadata(true));

        
        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty ShowUCProperty = DependencyProperty.Register("ShowUC", typeof(bool), typeof(CustomReportSettings), new PropertyMetadata(true));

        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty ShowCostProperty = DependencyProperty.Register("ShowCost", typeof(bool), typeof(CustomReportSettings), new PropertyMetadata(true));


        /// <summary>
        /// Dependency property managing the value of the collpased/expanded state of the root navigation bar in the current <see cref="MainView"/>.
        /// </summary>
        public static readonly DependencyProperty PreViewCommandProperty = DependencyProperty.Register("PreViewCommand", typeof(ICommand), typeof(CustomReportSettings), new PropertyMetadata(null));


        public CustomReportSettings()
        {

        }

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (((CustomReportSettings) dependencyObject).ShowMU)
            {
                ((CustomReportSettings) dependencyObject).ShowActivities = true;
                ((CustomReportSettings) dependencyObject).ShowResources = true;
            }
        }
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public string CustomReporName
        {
            get { return (string)GetValue(CustomReporNameProperty); }
            set { SetValue(CustomReporNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public bool ShowInvestmentElements
        {
            get { return (bool)GetValue(ShowInvestmentElementsProperty); }
            set { SetValue(ShowInvestmentElementsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public bool ShowBudgetComponents
        {
            get { return (bool)GetValue(ShowBudgetComponentsProperty); }
            set { SetValue(ShowBudgetComponentsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public bool ShowSubSpecialities
        {
            get { return (bool)GetValue(ShowSubSpecialitiesProperty); }
            set { SetValue(ShowSubSpecialitiesProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public bool ShowActivities
        {
            get { return (bool)GetValue(ShowActivitiesProperty); }
            set { SetValue(ShowActivitiesProperty, value); }
        }
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public bool ShowResources
        {
            get { return (bool)GetValue(ShowResourcesProperty); }
            set { SetValue(ShowResourcesProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public bool ShowEquipment
        {
            get { return (bool)GetValue(ShowEquipmentProperty); }
            set { SetValue(ShowEquipmentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public bool ShowConstruction
        {
            get { return (bool)GetValue(ShowConstructionProperty); }
            set { SetValue(ShowConstructionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public bool ShowOthers
        {
            get { return (bool)GetValue(ShowOthersProperty); }
            set { SetValue(ShowOthersProperty, value); }
        }
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public bool ShowWorkCapital
        {
            get { return (bool)GetValue(ShowWorkCapitalProperty); }
            set { SetValue(ShowWorkCapitalProperty, value); }
        }
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public bool ShowMU
        {
            get { return (bool)GetValue(ShowMUProperty); }
            set { SetValue(ShowMUProperty, value); }
        }
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public bool ShowQuantity
        {
            get { return (bool)GetValue(ShowQuantityProperty); }
            set { SetValue(ShowQuantityProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public bool ShowCurrency
        {
            get { return (bool)GetValue(ShowCurrencyProperty); }
            set { SetValue(ShowCurrencyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public bool ShowUC
        {
            get { return (bool)GetValue(ShowUCProperty); }
            set { SetValue(ShowUCProperty, value); }
        }
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public bool ShowCost
        {
            get { return (bool)GetValue(ShowCostProperty); }
            set { SetValue(ShowCostProperty, value); }
        }
        /// <summary>
        /// Gets or sets the command that triggers the filtering process.
        /// </summary>
        public ICommand PreViewCommand
        {
            get { return (ICommand)GetValue(PreViewCommandProperty); }
            set { SetValue(PreViewCommandProperty, value); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}