using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using CompanyName.Atlas.Contracts.Application.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class ReferenceDocumentPresenter: DocumentPresenter<IReferenceDocument, IReferenceDocumentManagerApplicationServices>, IReferenceDocumentPresenter
    {
        public ReferenceDocumentPresenter(IReferenceDocument nomenclator) : base(nomenclator)
        {
        }
        public string KeyWords
        {
            get { return Object.KeyWords; }
            set
            {
                SetProperty(v => Object.KeyWords = v, value);
                OnPropertyChanged(() => KeyWords);
            }
        }
        public DateTime PublishDate
        {
            get { return Object.PublishDate; }
            set
            {
                SetProperty(v => Object.PublishDate = v, value);
                OnPropertyChanged(() => PublishDate);
            }
        }

    }
}
