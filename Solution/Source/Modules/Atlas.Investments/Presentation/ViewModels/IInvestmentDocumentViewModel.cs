using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Contracts.Presentation.Common;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    public interface IInvestmentDocumentViewModel : IDocumentViewModel<IInvestmentDocument, IInvestmentDocumentPresenter>
    {
        
    }
}
