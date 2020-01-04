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
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Configuration.Presentation.Views
{
    /// <summary>
    /// Interaction logic for AtlasUsersManagementView.xaml
    /// </summary>
    public partial class AtlasUsersManagementView : UserControl, IView
    {
        public AtlasUsersManagementView()
        {
            InitializeComponent();
            viewmodel = CreateAndInitialize<IAtlasUserViewModel>();
            DataContext = viewmodel;
            //UserAtlasDataGrid.ItemsSource = viewmodel.Items;
            //UserAtlasDataGrid.AddButtonCommand = viewmodel.AddCommand;
            //UserAtlasDataGrid.DeleteButtonCommand = viewmodel.DeleteCommand;
            //AtlasTabControl.FilterCommand = viewmodel.SimpleFilterCommand;
            // IsVisibleChanged += InvestmentVariablesEditor_IsVisibleChanged;

            //var moduleCatalog = (ModuleCatalog)ServiceLocator.Current.GetInstance(typeof(ModuleCatalog));
            //ModulesDataGrid.ItemsSource = moduleCatalog?.Modules;
            IsVisibleChanged += InvestmentVariablesEditor_IsVisibleChanged;
        }

        private readonly INavigationServices _navigationServices = ServiceLocator.Current.GetInstance<INavigationServices>();
        private readonly IAtlasUserViewModel viewmodel;

        private void InvestmentVariablesEditor_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //   var isVisible = e.NewValue as bool?;

            if (IsVisible)
            {
                _navigationServices.HideOptionalNavigationContent();
                if (Equals(DataContext, null))
                    DataContext = viewmodel;
            }
                
            else
                AtlasTabControl.FilterCommand?.Execute("");
        }

        protected internal virtual void OnInteractionRequested(object sender, InteractionRequestedEventArgs e)
        {
            this.Execute(e);
        }

        /// <summary>
        /// Gets a new instance of a crud view model and loads it.
        /// </summary>
        /// <returns>A new and initialized instance of <typeparamref name="TViewModel"/>.</returns>
        protected TViewModel CreateAndInitialize<TViewModel>()
            where TViewModel : ICrudViewModel
        {
            var viewModel = ServiceLocator.Current.GetInstance<TViewModel>();


            viewModel.Load();
            viewModel.Raised += OnInteractionRequested;
            //  viewModel.Raised -= OnInteractionRequested;

            return viewModel;
        }

        ////private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        ////{
        ////    bool state = (bool) ((CheckBox) e.Source).IsChecked;
        ////    bool change = false;

        ////    if (state)
        ////    {
        ////        var user = UserAtlasDataGrid.SelectedItem as IAtlasUserPresenter;
        ////        var allowedModules = user.AllowedModules;
        ////        var module = ModulesDataGrid.SelectedItem as ModuleInfo;

        ////        if (module != null && allowedModules.All(x => x.ModuleName != module.ModuleName))
        ////        {
        ////            allowedModules.Add(module);
        ////            change = true;
        ////        }

        ////        if (change)
        ////        {

        ////            var final = new List<ModuleInfo>();
        ////            foreach (ModuleInfo allowedModule in allowedModules)
        ////            {
        ////                final.Add(allowedModule);
        ////            }
        ////            (UserAtlasDataGrid.SelectedItem as IAtlasUserPresenter).AllowedModules = final;
        ////        }

        ////    }
        ////    else
        ////    {
        ////        var user = UserAtlasDataGrid.SelectedItem as IAtlasUserPresenter;
        ////        var allowedModules = user.AllowedModules;
        ////        var module = ModulesDataGrid.SelectedItem as ModuleInfo;
        ////        if (module != null)
        ////        {
        ////            var auxModule = allowedModules.FirstOrDefault(x => x.ModuleName == module.ModuleName);
        ////            if (auxModule != null)
        ////            {
        ////                allowedModules.Remove(auxModule);
        ////                change = true;
        ////            }
        ////            if (change)
        ////            {
        ////                var final = new List<ModuleInfo>();
        ////                foreach (ModuleInfo allowedModule in allowedModules)
        ////                {
        ////                    final.Add(allowedModule);
        ////                }

        ////                (UserAtlasDataGrid.SelectedItem as IAtlasUserPresenter).AllowedModules = final;
        ////            }


        ////        }

        ////    }


        ////}

        ////private void DataGrid_OnCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        ////{
        ////    var shit = e.Row;
        ////    var user = UserAtlasDataGrid.SelectedItem as IAtlasUserPresenter;
        ////    var allowedModules = user.AllowedModules;
        ////}

        ////private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        ////{
        ////    var auxColection = ModulesDataGrid.ItemsSource.GetEnumerator();
        ////    var array = new List<object>();
        ////    while (auxColection.MoveNext())
        ////    {
        ////        array.Add(auxColection.Current);
        ////    }
        ////    var selItem = ModulesDataGrid.SelectedItem;
        ////    //  ModulesDataGrid.Items.Clear();
        ////    ModulesDataGrid.ItemsSource = array;
        ////    ModulesDataGrid.SelectedItem = selItem;
        ////}


        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!Equals(((PasswordBox) sender).DataContext, null))
            {
                var User = ((PasswordBox) sender).DataContext as IAtlasUserPresenter;
                if (!Equals(User, null))
                    User.Password = ((PasswordBox) sender).Password;
            }
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!Equals(((PasswordBox)sender).DataContext, null))
            {
                var User = ((PasswordBox)sender).DataContext as IAtlasUserPresenter;
                if (!Equals(User, null))
                     ((PasswordBox)sender).Password = User.Password;
            }
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (((CheckBox) sender).DataContext != null)
            {
                var isChecked = ((CheckBox) sender).IsChecked;
                if (isChecked != null)
                    ((AtlasUserAllowedModulePresenter) ((CheckBox) sender).DataContext).Allowed =
                        isChecked.Value;
            }
        }
    }
}
