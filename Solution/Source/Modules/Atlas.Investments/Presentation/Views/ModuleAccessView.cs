using CompanyName.Atlas.UIControls;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    public class ModuleAccessView: InvestmentElementsView
    {
        public ModuleAccessView():base()
        {
            //DefaultStyleKey = typeof(AtlasOptionalContent);
            //ElementsTreeView = new InvestmentElementTreeView();
            ////  DataContextChanged += OnDataContextChanged;

            //ViewModelLocator.SetAutoWireViewModel(this, true);

            //((InvestmentElementTreeView)ElementsTreeView).AddButtonTooltip = Properties.Resources.AddRootInvElemButtonTooltip;
            //((InvestmentElementTreeView)ElementsTreeView).AddInvestmentElemementButtonTooltip = Properties.Resources.AddInvElemButtonTooltip;
            //((InvestmentElementTreeView)ElementsTreeView).DeleteInvestmentElemementButtonTooltip = Properties.Resources.DeleteInvElemButtonTooltip;

            //((InvestmentElementTreeView)ElementsTreeView).SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Items"));
            //((InvestmentElementTreeView)ElementsTreeView).SetBinding(InvestmentElementTreeView.AddRootInvestmentElementButtonCommandProperty, new Binding("AddCommand"));

        }
    }
}
