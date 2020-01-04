using CompanyName.Atlas.Contracts.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Application.Common
{
    public interface IDocumentManagerApplicationServices<TDocument>: IItemManagerApplicationServices<TDocument>
        where TDocument:class,IDocument
    {
        IEntity Holder { get; set; }
    }
}
