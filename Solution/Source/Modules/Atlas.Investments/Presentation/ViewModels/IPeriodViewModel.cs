using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    //TODO erase
    /// <summary>
    /// Representation of a period facing the user interface. (created just in case)
    /// </summary>
    interface IPeriodViewModel
    {
        DateTimeScale PeriodKind { get; set; }

        DateTime Starts { get; set; }
        
        DateTime Ends { get; set; }
      

    }
}
