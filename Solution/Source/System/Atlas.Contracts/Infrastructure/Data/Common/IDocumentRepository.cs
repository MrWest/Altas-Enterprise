using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data.Common
{
   public  interface IDocumentRepository<TDocument>: IRepository<TDocument>
        where TDocument:class,IDocument
    {
        IEntity Holder { get; set; }
        
    }
}
