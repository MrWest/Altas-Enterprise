using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
   public interface IDocumentViewModel<TDocuement,TPresenter>: ICrudViewModel<TDocuement, TPresenter>
        where TDocuement: class,IDocument
       where TPresenter:class,IDocumentPresenter<TDocuement>
    {
        IPresenter Holder { get; set; }
    }
}
