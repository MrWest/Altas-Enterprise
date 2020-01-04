using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Defines an entity for managing the work capital cash flow
    /// </summary>
    public interface IWorkCapitalCashFlow: IEntity
    {
        IWorkCapitalComponent WorkCapital { get; set; }
        ICashEntry CashEntries { get; set; }
        ICashOutgoing CashOutgoings { get; set; }
        string CashEntriesId { get; set; }
        string CashOutgoingsId { get; set; }
        DateTimeScale DateTimeScale { get; set; }
        DateTime Starts { get; set; }
        DateTime Ends { get; set; }
        bool IsWorkCapitalCalculated { get; set; }
        decimal CalculatedWorkCapital { get; set; }
    }
}
