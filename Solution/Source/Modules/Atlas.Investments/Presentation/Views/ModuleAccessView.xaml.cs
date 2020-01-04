using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using IView = Microsoft.Practices.Prism.Mvvm.IView;
using CompanyName.Atlas.UIControls;
using System.Windows.Controls;
using System.Windows.Data;
using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ModuleAccessView.xaml
    /// </summary>
    public partial class ModuleAccessView : AtlasOptionalContent
    {
               /// <summary>
        /// Initializes a new instance of <see cref="ModuleAccessView"/>.
        /// </summary>
        public ModuleAccessView():base()
        {
            InitializeComponent();
            ExportableViewModels = new List<NamedCrudViewModel>();

            //ExportableViewModels.Add(new NamedCrudViewModel() { Name = Properties.Resources.Projects, ViewModel = ServiceLocator.Current.GetInstance<IInvestmentViewModel>() });
            var viewModel = ServiceLocator.Current.GetInstance<IAtlasModuleMainSubjectViewModel>();
            viewModel.AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            ExportableViewModels.Add(new NamedCrudViewModel() { Name = Properties.Resources.ModuleHelpContent, ViewModel = viewModel });
            ExportableViewModels.Add(new NamedCrudViewModel() { Name = Properties.Resources.MeasurementUnit, ViewModel = ServiceLocator.Current.GetInstance<IMeasurementUnitViewModel>() });
            ExportableViewModels.Add(new NamedCrudViewModel() { Name = Properties.Resources.Currency, ViewModel = ServiceLocator.Current.GetInstance<ICurrencyViewModel>() });

            //ElementsTreeView = new ModuleAccessTreeView();
            //((ModuleAccessTreeView)ElementsTreeView).AddButtonTooltip = Properties.Resources.AddRootInvElemButtonTooltip;
            //((ModuleAccessTreeView)ElementsTreeView).AddInvestmentElemementButtonTooltip = Properties.Resources.AddInvElemButtonTooltip;
            //((ModuleAccessTreeView)ElementsTreeView).DeleteInvestmentElemementButtonTooltip = Properties.Resources.DeleteInvElemButtonTooltip;

            //((ModuleAccessTreeView)ElementsTreeView).SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Items"));
            //((ModuleAccessTreeView)ElementsTreeView).SetBinding(InvestmentElementTreeView.AddRootInvestmentElementButtonCommandProperty, new Binding("AddCommand"));

        }



    }
    
}
