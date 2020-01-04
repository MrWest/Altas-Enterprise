using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Implementation.Application.Common
{
    public class ReferenceDocumentManagerApplicationServices: DocumentManagerApplicationServices<IReferenceDocument, IReferenceDocumentRepository, IReferenceDocumentDomainService>, IReferenceDocumentManagerApplicationServices
    {
    }
}
