using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using CompanyName.Atlas.Investments.Presentation.Views.Tools;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for InvestmentMenu.xaml
    /// </summary>
    public partial class AtlasSubSystemMenu : UserControl
    {
        public AtlasSubSystemMenu()
        {
            InitializeComponent();


           
          //  DataContext = viewModel;
            //AtlasModuleHelpContent.DataContext = viewModel;
        }


        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
           Window accessImport = new ToolsWindow() { Content = new AccessImport()};
            
            accessImport.ShowDialog();
        }
    }
}
