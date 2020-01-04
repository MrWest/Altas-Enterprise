using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    public class WorkCapitalCashFlow:EntityBase,IWorkCapitalCashFlow 
    {
        public WorkCapitalCashFlow()
        {
            CashEntries = ServiceLocator.Current.GetInstance<ICashEntry>();
            CashOutgoings = ServiceLocator.Current.GetInstance<ICashOutgoing>();
            Starts = DateTime.Today;
            Ends = DateTime.Today;
            DateTimeScale = DateTimeScale.Monthly;
        }
        public IWorkCapitalComponent WorkCapital { get; set; }

        [ForeignKey("CashEntriesId")]
        public ICashEntry CashEntries { get; set; }

        [ForeignKey("CashOutgoingsId")]
        public ICashOutgoing CashOutgoings { get; set; }

        public string CashEntriesId { get; set; }
        public string CashOutgoingsId { get; set; }
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        public DateTimeScale DateTimeScale { get; set; }
        public bool IsWorkCapitalCalculated { get; set; }
        public decimal CalculatedWorkCapital { get; set; }
    }
}
