using CompanyName.Atlas.Contracts.Presentation.Services;
using Microsoft.Practices.ServiceLocation;
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
using CompanyName.Atlas.Contracts.Presentation.Features;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.Investments.Presentation.Views.ViewTypes;
using CompanyName.Atlas.UIControls;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for InvestmentElementTimeline.xaml
    /// </summary>
    public partial class InvestmentElementTimeline : IPrinttableContainer
    {
        /// <summary>
        /// the contructor what did you know
        /// </summary>
        public InvestmentElementTimeline()
        {
            InitializeComponent();

            // var shit = DataContext;
            IsVisibleChanged += InvestmentVariablesEditor_IsVisibleChanged;
        }

        private readonly INavigationServices _navigationServices = ServiceLocator.Current.GetInstance<INavigationServices>();

        private bool first = true;

        private void InvestmentVariablesEditor_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //   var isVisible = e.NewValue as bool?;

            if (IsVisible)
            {
                _navigationServices.ShowOptionalNavigationContent();

                //if (DataContext != null && first)
                //{
                //    ((IInvestmentElementPresenter)DataContext).Budget.SecondView = BudgetViewType.All;
                //    ((IInvestmentElementPresenter)DataContext).SecondView = BudgetViewType.All;
                //    AtlasTimeline.UpdateTimeLine();
                //    first = false;
                //}
                   
            }
               
            //else
            //    _navigationServices.HideOptionalNavigationContent();
        }

        public void Print()
        {
           ((IPrinttableContainer)AtlasTimeline).Print();

        }
    }
}
