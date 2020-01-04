using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CompanyName.Atlas.Contracts.Presentation.Features;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ReportViews.xaml
    /// </summary>
    public partial class ReportViews : UserControl,IPrinttableContainer, IExporttableContainer
    {
        public ReportViews()
        {
            InitializeComponent();
        }

        public void Print()
        {

            if (BudgetTabControl.SelectedIndex == 0)
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(ScrollViewer, "A Flow Document");
                }

                // DocumentPageViewer.Print();
            }
            if (BudgetTabControl.SelectedIndex == 1)
                    Generals.Print();
            if (BudgetTabControl.SelectedIndex == 1)
                ReportsGenerator.Print();
            if (TimelineTabControl.SelectedIndex == 0)
                DetailedTimeline.Print();
            if (TimelineTabControl.SelectedIndex == 1)
                ProjectionTimeline.Print();
            //DocumentPageViewer2.Print();
           
          
          
        }

        private void BudgetTabControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            ((TabControl) sender).Tag = false;
        }

        public void Export()
        {
            if (BudgetTabControl.SelectedIndex == 1)
                ReportsGenerator.Export();
        }
    }
}
