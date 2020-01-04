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
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Presentation.Views.Arrangement
{
    /// <summary>
    /// Interaction logic for CashFlowItemView.xaml
    /// </summary>
    public partial class WorkCapitalItemView : UserControl
    {
        public WorkCapitalItemView()
        {
            InitializeComponent();
        }

        //private void UIElement_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //   (((TextBlock)sender).DataContext as IWorkCapitalCashFlowPresenter).isVisible = (bool)e.NewValue;
        //}
    }
}
