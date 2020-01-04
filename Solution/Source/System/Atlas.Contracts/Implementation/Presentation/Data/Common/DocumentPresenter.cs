using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public abstract class DocumentPresenter<TDocument,TService>: CodedNomenclatorPresenterBase<TDocument,TService>, IDocumentPresenter<TDocument>
        where TDocument:class,IDocument
        where TService:class,IDocumentManagerApplicationServices<TDocument>
    {
        public DocumentPresenter(TDocument nomenclator) : base(nomenclator)
        {
        }
        public IPresenter Holder { get; set; }

        private ICommand _openCommand;
        public ICommand OpenCommand
        {
            get
            {

                return _openCommand ?? new DelegateCommand(Execute, CanExecute);
            }
        }

        private bool CanExecute()
        {
            return Object.IsAviable();
        }

        private void Execute()
        {
            Object.Open();
        }
        public string FilePath
        {
            get { return Object.FilePath; }
            set
            {
                SetProperty(v => Object.FilePath = v, value);
                OnPropertyChanged(() => OpenCommand);
            }
        }
        /////// <summary>
        ///////     Gets or sets the code of the underlying investment.
        /////// </summary>
        ////public string Code
        ////{
        ////    get { return Object.Code; }
        ////    set
        ////    {
        ////        SetProperty(v => Object.Code = v, value);
        ////        OnPropertyChanged(() => OpenCommand);
        ////    }
        ////}
        public bool IsAviable()
        {
            return Object.IsAviable();
        }

        public void Open()
        {
            Object.Open();
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

        public string Author
        {
            get { return Object.Author; }
            set
            {
                SetProperty(v => Object.Author = v, value);
                OnPropertyChanged(() => Author);
            }
        }
    }
}
