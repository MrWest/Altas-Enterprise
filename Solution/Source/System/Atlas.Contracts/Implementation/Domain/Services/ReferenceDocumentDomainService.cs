using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
   public class ReferenceDocumentDomainService:DocumentDomainService<IReferenceDocument>, IReferenceDocumentDomainService
    {
        public override IReferenceDocument Create()
        {
            IReferenceDocument document = base.Create();
           
            document.PublishDate = DateTime.Today;
            return document;
        }
    }
}
