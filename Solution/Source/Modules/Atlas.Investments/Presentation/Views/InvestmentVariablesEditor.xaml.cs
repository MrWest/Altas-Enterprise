using System.Windows;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Services;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    ///     Interaction logic for InvestmentVariablesEditor.xaml
    /// </summary>
    public partial class InvestmentVariablesEditor
    {
        private readonly INavigationServices _navigationServices = ServiceLocator.Current.GetInstance<INavigationServices>();


        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentVariablesEditor" />.
        /// </summary>
        public InvestmentVariablesEditor()
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

        private void UIElement_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var value = (bool)e.NewValue;
            if (value)
            {
                if (AtlasTabControl.FilterCommand != null)
                    AtlasTabControl.FilterCommand.Execute("");
                AtlasTabControl.FilterCommand = (((FrameworkElement)sender).DataContext as ICrudViewModel).SimpleFilterCommand;
            }
        }
    }
}