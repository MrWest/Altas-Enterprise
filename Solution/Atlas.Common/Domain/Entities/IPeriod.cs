using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    public enum PeriodKind { Anual, Mensual, Semanal, Diario };
    public interface IPeriod:INomenclator
    {
        PeriodKind PeriodKind { get; set; }

        DateTime Starts { get; set; }


        DateTime Ends { get; set; }
       
        int Days { get; }

        IList<IPeriod> GetPeriods();
       
      
    }
}
