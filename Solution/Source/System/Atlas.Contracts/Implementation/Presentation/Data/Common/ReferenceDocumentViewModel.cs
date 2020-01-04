using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class ReferenceDocumentViewModel: DocumentViewModel<IReferenceDocument, IReferenceDocumentPresenter, IReferenceDocumentManagerApplicationServices>, IReferenceDocumentViewModel
    {
    }
}
