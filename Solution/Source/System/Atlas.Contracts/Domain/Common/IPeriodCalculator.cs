using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    public interface IPeriodCalculator
    {
        DateTime StartDate();
        DateTime FinishDate();
        bool StartCalculated { get; set; }
        bool EndCalculated { get; set; }
        DateTime LastCalculatedFinishDate { get; set; }
        DateTime LastCalculatedStartDate { get; set; }
    }
}
