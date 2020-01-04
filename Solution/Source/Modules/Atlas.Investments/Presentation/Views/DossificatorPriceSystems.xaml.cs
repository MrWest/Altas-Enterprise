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
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.Investments.Presentation.Views.ViewTypes;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for DossificatorPriceSystems.xaml
    /// </summary>
    public partial class DossificatorPriceSystems 
    {
        private readonly INavigationServices _navigationServices = ServiceLocator.Current.GetInstance<INavigationServices>();

        private readonly CompositeCommand _filterCommand = new CompositeCommand();

        /// <summary>
        /// Dependency property containing the current view where there is currently in the budget component items tab controls of a
        /// <see cref="DossificatorPriceSystems"/>.
        /// </summary>
        public static readonly DependencyProperty ViewProperty = DependencyProperty.Register("View", typeof(PSDossificatiorViewType), typeof(DossificatorPriceSystems), new PropertyMetadata(PSDossificatiorViewType.StandAlong));

        /// <summary>
        /// Dependency property containing the criteria to use in filtering the budget component items displayed in
        /// <see cref="DossificatorPriceSystems"/> instances.
        /// </summary>
        public static readonly DependencyProperty FilterCriteriaProperty = DependencyProperty.Register("FilterCriteria", typeof(string), typeof(DossificatorPriceSystems), new PropertyMetadata(null));

        
        public DossificatorPriceSystems()
        {
            InitializeComponent();
            IsVisibleChanged += InvestmentVariablesEditor_IsVisibleChanged;
        }


        private void InvestmentVariablesEditor_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var isVisible = e.NewValue as bool?;

            if (isVisible.HasValue && isVisible.Value)
                _navigationServices.HideOptionalNavigationContent();
            else
                _navigationServices.ShowOptionalNavigationContent();
        }
        /// <summary>
        /// Gets or sets the view where there is currently in the budget component items tab controls.
        /// </summary>
        public PSDossificatiorViewType View
        {
            get { return (PSDossificatiorViewType)GetValue(ViewProperty); }
            set { SetValue(ViewProperty, value); }
        }

        /// <summary>
        /// Gets or sets the filtering criteria to use when filtering the budget component items by name.
        /// </summary>
        public string FilterCriteria
        {
            get { return (string)GetValue(FilterCriteriaProperty); }
            set { SetValue(FilterCriteriaProperty, value); }
        }
    }
}
