using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Application.Budget.WorkCapital
{
    public interface IWorkCapitalCashFlowItemManagerApplicationServices : IItemManagerApplicationServices<IWorkCapitalCashFlow>
    {
        IList<ICashMovement> GeRemainingCashHistory(IPeriod period, ICashEntry cashEntries, ICashOutgoing cashOutgoings);

        decimal GetWorkCapital(IPeriod period, ICashEntry cashEntries, ICashOutgoing cashOutgoings);
        IWorkCapitalComponent WorkCapitalComponent { get; set; }
    }
}
