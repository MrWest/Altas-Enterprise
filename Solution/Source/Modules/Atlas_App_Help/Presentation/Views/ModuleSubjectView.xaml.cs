using System.Collections.Generic;
using System.Reflection;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas_App_Help.Presentation.Views
{
    /// <summary>
    /// Interaction logic for ModuleSubjectView.xaml
    /// </summary>
    public partial class ModuleSubjectView : AtlasOptionalContent
    {
               /// <summary>
        /// Initializes a new instance of <see cref="ModuleSubjectView"/>.
        /// </summary>
        public ModuleSubjectView():base()
        {
            InitializeComponent();
            ExportableViewModels = new List<NamedCrudViewModel>();
            var viewModel = ServiceLocator.Current.GetInstance<IAtlasModuleMainSubjectViewModel>();
            viewModel.AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            ExportableViewModels.Add(new NamedCrudViewModel() { Name = Properties.Resources.ModuleHelpContent, ViewModel = viewModel });

            //ElementsTreeView = new ModuleAccessTreeView();
            //((ModuleAccessTreeView)ElementsTreeView).AddButtonTooltip = Properties.Resources.AddRootInvElemButtonTooltip;
            //((ModuleAccessTreeView)ElementsTreeView).AddInvestmentElemementButtonTooltip = Properties.Resources.AddInvElemButtonTooltip;
            //((ModuleAccessTreeView)ElementsTreeView).DeleteInvestmentElemementButtonTooltip = Properties.Resources.DeleteInvElemButtonTooltip;

            //((ModuleAccessTreeView)ElementsTreeView).SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Items"));
            //((ModuleAccessTreeView)ElementsTreeView).SetBinding(InvestmentElementTreeView.AddRootInvestmentElementButtonCommandProperty, new Binding("AddCommand"));

        }



    }
    
}
