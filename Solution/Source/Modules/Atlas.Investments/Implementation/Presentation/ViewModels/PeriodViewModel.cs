using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public class PeriodViewModel: CrudViewModelBase<IInvestmentElementPeriod, IInvestmentElementPeriodPresenter, IInvestmentElementPeriodManagerApplicationServices<IInvestmentElementPeriod>>,
        IPeriodViewModel
    {
        public DateTimeScale PeriodKind { get; set; }

        public DateTime Starts { get;set; }

        public DateTime Ends { get; set; }
    }
}
