using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Properties;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    public class InvestmentElementsView: AtlasOptionalContent
    {
        public InvestmentElementsView():base()
        {
         //  DefaultStyleKey = typeof(AtlasOptionalContent);
           ElementsTreeView = new InvestmentElementTreeView();
         //  DataContextChanged += OnDataContextChanged;

            ViewModelLocator.SetAutoWireViewModel(this, true);

            ((InvestmentElementTreeView)ElementsTreeView).AddButtonTooltip = Properties.Resources.AddRootInvElemButtonTooltip;
            ((InvestmentElementTreeView)ElementsTreeView).AddInvestmentElemementButtonTooltip = Properties.Resources.AddInvElemButtonTooltip;
            ((InvestmentElementTreeView)ElementsTreeView).DeleteInvestmentElemementButtonTooltip = Properties.Resources.DeleteInvElemButtonTooltip;

            ((InvestmentElementTreeView)ElementsTreeView).SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Items"));
            ((InvestmentElementTreeView)ElementsTreeView).SetBinding(InvestmentElementTreeView.AddRootInvestmentElementButtonCommandProperty, new Binding("AddCommand") );

        }

        //private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        //{
        //    if (dependencyPropertyChangedEventArgs.NewValue != null && dependencyPropertyChangedEventArgs.NewValue.GetType().Implements<ICrudViewModel>())

        //    {
        //        //_content.SetBinding(ContentPresenter.ContentProperty, ContentBinding);
        //        ((InvestmentElementTreeView)ElementsTreeView).SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Items") { Source = ((ICrudViewModel)dependencyPropertyChangedEventArgs.NewValue).Items });
        //        ((InvestmentElementTreeView)ElementsTreeView).SetBinding(InvestmentElementTreeView.AddRootInvestmentElementButtonCommandProperty, new Binding("AddCommand") { Source = ((ICrudViewModel)dependencyPropertyChangedEventArgs.NewValue).AddCommand });

        //        //((InvestmentElementTreeView)ElementsTreeView).ItemsSource = ((ICrudViewModel)dependencyPropertyChangedEventArgs.NewValue).Items;

        //        //((InvestmentElementTreeView)ElementsTreeView).AddRootInvestmentElementButtonCommand = ((ICrudViewModel)dependencyPropertyChangedEventArgs.NewValue).AddCommand;

        //   }
        //}

    }
}
