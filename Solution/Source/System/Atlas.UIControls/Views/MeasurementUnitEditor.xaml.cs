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
    /// Interaction logic for MeasurementUnitEditor.xaml
    /// </summary>
    public partial class MeasurementUnitEditor 
    {
        public MeasurementUnitEditor()
        {
            InitializeComponent();
         //   DataContext = CreateAndInitialize<IMeasurementUnitViewModel>();
          //  IsVisibleChanged += OnIsVisibleChanged;

        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (IsVisible)
            {
                DataContext = CreateAndInitialize<IMeasurementUnitViewModel>();
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
      
    }
}
