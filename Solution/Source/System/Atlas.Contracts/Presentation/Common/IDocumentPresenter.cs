using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
   public interface IDocumentPresenter<TDocument>:IPresenter<TDocument>
        where TDocument: class,IDocument 
    {
        IPresenter Holder { get; set; }
        ICommand OpenCommand { get; }
        string Code { get; set; }
        String Author { get; set; }
        String FilePath { get; set; }
        //IEnumerable<string> FindAutorByContains(string text);
        //IEnumerable<string> FindSourceByContains(string text);
    }
}
