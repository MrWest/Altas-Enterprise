using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public  class DocumentViewModel<TDocument,TPresenter,TService>: CrudViewModelBase<TDocument, TPresenter, TService>, IDocumentViewModel<TDocument,TPresenter>
        where TDocument: class, IDocument
        where TPresenter:class,IDocumentPresenter<TDocument>
        where TService: class, IDocumentManagerApplicationServices<TDocument>
    {
        public IPresenter Holder { get; set; }

        /// <summary>
        /// Loads the items from the data source.
        /// </summary>
        public override void Load()
        {
            foreach (TPresenter presenter in Items)
                presenter.PropertyChanged -= OnPresenterPropertyChanged;

            Items.Clear();

            ExecuteUsingServices(services =>
            {
                foreach (TDocument item in GetItems(services))
                {
                    TPresenter presenter = CreatePresenterFor(item);
                    if (Holder != null)
                    {
                        presenter.Holder = Holder;
                    }

                    presenter.PropertyChanged += OnPresenterPropertyChanged;

                    Items.Add(presenter);
                }
            });
        }


        // </summary>
        ////     Gets the application services used to send the data operations originated in the current
        ///   
        /// <returns>A new instance of <typeparamref name="TServices" />.</returns>
        protected override TService CreateServices()
        {
            TService services = base.CreateServices();
            services.Holder = (IEntity)Holder.Object;

            return services;
        }

        protected override TPresenter CreatePresenterFor(TDocument item)
        {
            if (item == null)
                throw new ArgumentNullException("IDocument");

            TPresenter presenter = base.CreatePresenterFor(item);
            presenter.Holder = Holder;
            presenter.Object.Holder = (IEntity)Holder.Object;

            return presenter;
        }

        public DocumentViewModel()
        {
            AddCommand = new DelegateCommand<TPresenter>(ExecuteMethod, CanExecuteMethod);
        }

        private bool CanExecuteMethod(TPresenter documentPresenter)
        {
            return true;
        }

        private OpenFileDialog openFileDialog;
        private void ExecuteMethod(TPresenter documentPresenter)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = @"Documents files (*.pdf)|*.pdf|(*.xls)|*.xls|(*.xlsx)|*.xlsx|(*.docx)|*.docx|(*.doc)|*.doc|(*.jpg)|*.jpg|(*.easy)|*.easy|All files (*.*)|*.*";
            openFileDialog.FileOk += OpenFileDialogOnFileOk;
            try
            {
                openFileDialog.ShowDialog((System.Windows.Window)ServiceLocator.Current.GetInstance(typeof(ContentControl)));
            }
            catch (Exception)
            {

                openFileDialog.ShowDialog();
            }

        }

        private void OpenFileDialogOnFileOk(object sender, CancelEventArgs cancelEventArgs)
        {
            if (File.Exists(((OpenFileDialog)sender).FileName))
            {
                FileInfo fi = new FileInfo(((OpenFileDialog)sender).FileName);
                var document = CreateServices().Create();
                document.Name = fi.Name.Replace(fi.Extension, "");
                document.Description = "Descripcion para " + fi.Name;
                document.FilePath = ((OpenFileDialog)sender).FileName;
                base.Add(CreatePresenterFor(document));
            }
        }

        public ICommand AddCommand { get; private set; }
    }
}
