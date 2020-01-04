using System.Windows;
using System.Windows.Controls;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Investments.Presentation.Views.Arrangement
{
    /// <summary>
    /// Interaction logic for BudgetView.xaml
    /// </summary>
    public partial class UserAccesView : UserControl
    {
        public UserAccesView()
        {
            InitializeComponent();
        }

        private void UserRolesView_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var value = (bool)e.NewValue;
            if (value)
            {
                if(AtlasTabControl.FilterCommand!=null)
                    AtlasTabControl.FilterCommand.Execute("");
                AtlasTabControl.FilterCommand = (((FrameworkElement)sender).DataContext as ICrudViewModel).SimpleFilterCommand;
            }
               
        }
    }
}
