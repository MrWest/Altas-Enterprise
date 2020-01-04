using System;
using System.Collections.Generic;
using System.IO;
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
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Presentation.Views
{
    /// <summary>
    /// Interaction logic for InvestmentRelatedDocument.xaml
    /// </summary>
    public partial class InvestmentRelatedDocument 
    {
        public InvestmentRelatedDocument()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
            
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
           if(ContentPlace.ContentTemplate == null)
                ContentPlace.ContentTemplate = (DataTemplate)FindResource("InvestmentComponentDataTemplate");
        }

        private void DocumentsDataGrid_OnDrop(object sender, DragEventArgs e)
        {
            if (((AtlasDataGrid)sender).DataContext != null)
            {
                string[] formats = e.Data.GetFormats();
                String file = ((string[])e.Data.GetData(formats[7]))[0].ToString();

                var documentsVM = ((AtlasDataGrid)sender).DataContext as IInvestmentDocumentViewModel;
                var newDocument = ServiceLocator.Current.GetInstance<IInvestmentDocument>();
                FileInfo fi = new FileInfo(file);
                newDocument.Name = fi.Name.Replace(fi.Extension, "");
                newDocument.FilePath = file;
                newDocument.RecieveDate = fi.LastWriteTime;

                var presenter = ServiceLocator.Current.GetInstance<IInvestmentDocumentPresenter>();
                presenter.Object = newDocument;
                documentsVM.AddCommand.Execute(presenter);
                //   documentsVM.Load();
            }


        }

        private void DocumentsDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var contentBinding = new Binding { Source = ((AtlasDataGrid)sender).SelectedItem };
            ContentPlace.SetBinding(ContentProperty, contentBinding);
        }
    }
}
