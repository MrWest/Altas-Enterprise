using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.UIControls.Views
{
    /// <summary>
    /// Interaction logic for AtlasUserManagePage.xaml
    /// </summary>
    public partial class AtlasUserManagePage : UserControl, IView
    {
        public AtlasUserManagePage()
        {
            InitializeComponent();
            //var viewModel = CreateAndInitialize<IAtlasModuleMainSubjectViewModel>();
            //viewModel.AssemblyName = "Atlas.Investments";
            //viewModel.Load();
            //DataContext = viewModel;
            
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

     
    }
}
