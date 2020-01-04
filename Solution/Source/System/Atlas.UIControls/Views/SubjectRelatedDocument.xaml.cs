using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.UIControls.Views
{
    /// <summary>
    /// Interaction logic for InvestmentRelatedDocument.xaml
    /// </summary>
    public partial class SubjectRelatedDocument : UserControl
    {
        public SubjectRelatedDocument()
        {
            InitializeComponent();
        }

        private void DocumentsDataGrid_OnDrop(object sender, DragEventArgs e)
        {
            if (((AtlasDataGrid)sender).DataContext != null)
            {
                string[] formats = e.Data.GetFormats();
                String file = ((string[])e.Data.GetData(formats[7]))[0].ToString();

                //var documentsVM = ((AtlasDataGrid)sender).DataContext as IAtlasModuleGenericSubjectv;
                var newDocument = ServiceLocator.Current.GetInstance<IDocument>();
                FileInfo fi = new FileInfo(file);
                newDocument.Name = fi.Name.Replace(fi.Extension, "");
                newDocument.FilePath = file;
                //newDocument.RecieveDate = fi.LastWriteTime;

                //var presenter = ServiceLocator.Current.GetInstance<IInvestmentDocumentPresenter>();
                //presenter.Object = newDocument;
                //documentsVM.AddCommand.Execute(presenter);
                //   documentsVM.Load();
            }


        }
    }
}
