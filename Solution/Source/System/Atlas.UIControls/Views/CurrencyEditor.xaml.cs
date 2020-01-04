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
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.UIControls.Views
{
    /// <summary>
    /// Interaction logic for CurrencyEditor.xaml
    /// </summary>
    public partial class CurrencyEditor 
    {
        public CurrencyEditor()
        {
            InitializeComponent();
           
           // IsVisibleChanged+=OnIsVisibleChanged;

        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (IsVisible)
            {
                DataContext = CreateAndInitialize<ICurrencyViewModel>();
            }
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
        private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                ((AtlasDataGrid)sender).AddButtonCommand = ((ICrudViewModel)e.NewValue).AddCommand;
                ((AtlasDataGrid)sender).DeleteButtonCommand = ((ICrudViewModel)e.NewValue).DeleteCommand;
                ((AtlasDataGrid)sender).SelectedItem = ((ICrudViewModel)e.NewValue).SelectedItem;
            }
        }
    }
}
