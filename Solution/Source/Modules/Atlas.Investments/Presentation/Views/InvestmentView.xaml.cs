using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using IView = Microsoft.Practices.Prism.Mvvm.IView;
using System.Windows.Controls;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.Prism.Mvvm;
using System.Windows.Data;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for InvestmentElementsView.xaml
    /// </summary>w
    public partial  class InvestmentView : AtlasOptionalContent
    {
               /// <summary>
        /// Initializes a new instance of <see cref="InvestmentElementsView"/>.
        /// </summary>
        public InvestmentView():base()
        {
           InitializeComponent();
            ExportableViewModels = new List<NamedCrudViewModel>();

            ExportableViewModels.Add(new NamedCrudViewModel(){Name = Properties.Resources.Projects, ViewModel = ServiceLocator.Current.GetInstance<IInvestmentViewModel>()});
            ExportableViewModels.Add(new NamedCrudViewModel(){Name = Properties.Resources.PriceSystem, ViewModel = ServiceLocator.Current.GetInstance<IPriceSystemViewModel>()});
            ExportableViewModels.Add(new NamedCrudViewModel() { Name = Properties.Resources.MeasurementUnit, ViewModel = ServiceLocator.Current.GetInstance<IMeasurementUnitViewModel>()});
            ExportableViewModels.Add(new NamedCrudViewModel() { Name = Properties.Resources.Currency, ViewModel = ServiceLocator.Current.GetInstance<ICurrencyViewModel>()});

            //ElementsTreeView = new InvestmentElementTreeView();
            ////ViewModelLocator.SetAutoWireViewModel(this, true);

            //((InvestmentElementTreeView)ElementsTreeView).AddButtonTooltip = Properties.Resources.AddRootInvElemButtonTooltip;
            //((InvestmentElementTreeView)ElementsTreeView).AddInvestmentElemementButtonTooltip = Properties.Resources.AddInvElemButtonTooltip;
            //((InvestmentElementTreeView)ElementsTreeView).DeleteInvestmentElemementButtonTooltip = Properties.Resources.DeleteInvElemButtonTooltip;

            //((InvestmentElementTreeView)ElementsTreeView).SetBinding(ItemsControl.ItemsSourceProperty, new Binding("Items"));
            //((InvestmentElementTreeView)ElementsTreeView).SetBinding(InvestmentElementTreeView.AddRootInvestmentElementButtonCommandProperty, new Binding("AddCommand"));

        }

        //private ICrudViewModel ViewModel
        //{
        //    get
        //    {

        //        return DataContext as ICrudViewModel;
        //    }
        //}


        ///// <summary>
        ///// Invoked when the datacontext for the current view has been changed. Makes sure that the interactions channels
        ///// between this view and the new investment element view model are wired up.
        ///// </summary>
        ///// <param name="sender">The object sending the event invoking this method.</param>
        ///// <param name="e">The event arguments containing the details about the datacontext change.</param>
        //protected override void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    base.OnDataContextChanged(sender, e);

        //    var viewModel = e.NewValue as IInvestmentViewModel;
        //    if (viewModel == null)
        //        return;

        //    RecursivelySetupInteractions(viewModel);
        //}

        //private void RecursivelySetupInteractions(ICrudViewModel viewModel)
        //{
        //    viewModel.AddedItem += SetupInteraction;
        //    viewModel.DeletedItem += BreakInteraction;

        //    foreach (IInvestmentElementPresenter invElemPresenter in viewModel.Items)
        //    {
        //        invElemPresenter.Elements.Raised += OnInteractionRequested;
        //        RecursivelySetupInteractions(invElemPresenter.Elements);
        //    }
        //}

        //private void RecursivelyBreakInteractions(ICrudViewModel viewModel)
        //{
        //    viewModel.AddedItem -= SetupInteraction;
        //    viewModel.DeletedItem -= BreakInteraction;
        //    viewModel.Raised -= OnInteractionRequested;

        //    foreach (IInvestmentElementPresenter invElemPresenter in viewModel.Items)
        //        RecursivelyBreakInteractions(invElemPresenter.Elements);
        //}

        //private void SetupInteraction(object sender, ItemEventArgs e)
        //{
        //    var invElemPresenter = (IInvestmentElementPresenter)e.Item;
        //    invElemPresenter.Elements.Raised += OnInteractionRequested;
        //    RecursivelySetupInteractions(invElemPresenter.Elements);
        //}

        //private void BreakInteraction(object sender, ItemEventArgs e)
        //{
        //    var invElemPresenter = (IInvestmentElementPresenter)e.Item;
        //    RecursivelyBreakInteractions(invElemPresenter.Elements);
        //}


        //#region Command event handlers

        //private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    var invElemPresenter = e.Parameter as IInvestmentElementPresenter;

        //    e.CanExecute = invElemPresenter != null && invElemPresenter.Elements.AddCommand.CanExecute(null);
        //}

        //private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    var invElemPresenter = e.Parameter as IInvestmentElementPresenter;

        //    if (invElemPresenter == null)
        //    {
        //        if (ViewModel != null)
        //            ViewModel.AddCommand.Execute(e.Parameter);
        //    }
        //    else
        //    {
        //        invElemPresenter.Elements.AddCommand.Execute(null);
        //        invElemPresenter.IsExpanded = true;
        //    }
        //}

        //private void DeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        //{
        //    var invElemPresenter = e.Parameter as IInvestmentElementPresenter;

        //    if (invElemPresenter == null || invElemPresenter.Parent == null)
        //    {
        //        var type = e.Parameter.GetType();
        //        e.CanExecute = ViewModel != null && ViewModel.DeleteCommand.CanExecute(e.Parameter);
        //    }
        //    else
        //    {
        //        var parent = invElemPresenter.Parent as IInvestmentElementPresenter;
        //        e.CanExecute = parent != null && parent.Elements.DeleteCommand.CanExecute(e.Parameter);
        //    }
        //}

        //private void DeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        //{
        //    var invElemPresenter = e.Parameter as IInvestmentElementPresenter;

        //    if (invElemPresenter == null || invElemPresenter.Parent == null)
        //    {
        //        if (ViewModel != null)
        //            ViewModel.DeleteCommand.Execute(e.Parameter);
        //    }
        //    else
        //    {
        //        var parent = invElemPresenter.Parent as IInvestmentElementPresenter;
        //        if (parent != null)
        //            parent.Elements.DeleteCommand.Execute(e.Parameter);
        //    }
        //}

        //#endregion
    }

}
