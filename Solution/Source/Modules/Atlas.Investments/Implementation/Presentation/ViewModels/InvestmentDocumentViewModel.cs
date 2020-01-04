using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public class InvestmentDocumentViewModel : DocumentViewModel<IInvestmentDocument, IInvestmentDocumentPresenter, IInvestmentDocumentManagerApplicationServices>, IInvestmentDocumentViewModel
    {
      
    }
}
