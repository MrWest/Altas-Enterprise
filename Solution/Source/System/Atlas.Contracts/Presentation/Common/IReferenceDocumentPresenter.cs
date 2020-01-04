using CompanyName.Atlas.Contracts.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface IReferenceDocumentPresenter : IDocumentPresenter<IReferenceDocument>
    {
        string KeyWords { get; set; }
        DateTime PublishDate { get; set; }
    }
}
