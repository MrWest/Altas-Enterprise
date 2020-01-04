using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Contracts.Presentation.Common;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    public interface IInvestmentDocumentPresenter: IDocumentPresenter<IInvestmentDocument>
    {
       
        DocumentType DocumentType { get; set; }
        /// <summary>
        ///     Gets or sets the code of the underlying Document.
        /// </summary>
       
        String Institution { get; set; }
        IOsdePresenter Osde { get; set; }
        IOacePresenter Oace { get; set; }
        DateTime RecieveDate { get; set; }
        DateTime DeliverDate { get; set; }

       

    }
}
