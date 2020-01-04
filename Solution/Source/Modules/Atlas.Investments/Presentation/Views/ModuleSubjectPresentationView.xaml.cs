using CompanyName.Atlas.Contracts.Presentation.Services;
using Microsoft.Practices.ServiceLocation;
using System.Windows;
using System.Windows.Controls;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ModuleSubjectPresentationView.xaml
    /// </summary>
    public partial class ModuleSubjectPresentationView : UserControl
    {
       
        public ModuleSubjectPresentationView()
        {
            InitializeComponent();
            IsVisibleChanged += InvestmentVariablesEditor_IsVisibleChanged;
        }

        private readonly INavigationServices _navigationServices = ServiceLocator.Current.GetInstance<INavigationServices>();
        private void InvestmentVariablesEditor_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //   var isVisible = e.NewValue as bool?;

            if (IsVisible)
                _navigationServices.ShowOptionalNavigationContent();
            //else
            //    _navigationServices.HideOptionalNavigationContent();
        }
    }
}
