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
using CompanyName.Atlas.Investments.Presentation.ViewModels.Reporting;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for CustomReportTreeViewContainer.xaml
    /// </summary>
    public partial class CustomReportTreeViewContainer 
    {
        /// <summary>
        /// Dependency property containing the Print command for instances of <see cref="PrintBox"/>.
        /// </summary>
        public static readonly DependencyProperty PrintCommandProperty = DependencyProperty.Register("PrintCommand", typeof(ICommand), typeof(CustomReportTreeViewContainer));

        /// <summary>
        /// Dependency property containing the Print command for instances of <see cref="PrintBox"/>.
        /// </summary>
        public static readonly DependencyProperty SelectedSettingsProperty = DependencyProperty.Register("SelectedSettings", typeof(IInvestmentCustomReportSettingsPresenter), typeof(CustomReportTreeViewContainer));

        public CustomReportTreeViewContainer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the command that triggsers the Printing process.
        /// </summary>
        public IInvestmentCustomReportSettingsPresenter SelectedSettings
        {
            get { return (IInvestmentCustomReportSettingsPresenter)GetValue(SelectedSettingsProperty); }
            set { SetValue(SelectedSettingsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command that triggers the Printing process.
        /// </summary>
        public ICommand PrintCommand
        {
            get { return (ICommand)GetValue(PrintCommandProperty); }
            set { SetValue(PrintCommandProperty, value); }
        }

        private void CustomReportTreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SelectedSettings = CustomReportTreeView.SelectedItem as IInvestmentCustomReportSettingsPresenter;
        }
    }
}
