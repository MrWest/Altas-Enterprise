using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Reporting
{
    /// <summary>
    /// To fill table 
    /// </summary>
    public interface IBudgetSummaryConcept
    {
        /// <summary>
        /// concept name
        /// </summary>
        string ConceptName { get; set; }

        ///// <summary>
        ///// money expressed in thousands
        ///// </summary>
        IBudgetSummary ThousandsOfMoney { get; set; }

        ///// <summary>
        ///// money expressed in thousands
        ///// </summary>
        //decimal ThousandsOfMoney{ get; set; }


        /// <summary>
        /// Period to show
        /// </summary>
        //IPeriod Period { get; set; }
    }
}
