using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    /// Represents a period for an investment Element
    /// </summary>
    public class InvestmentElementPeriod: Period, IInvestmentElementPeriod
    {
       
        /// <summary>
        /// Gets the investment element to which belong the current <see cref="InvestmentElementPeriod"/>.
        /// </summary>
        public IInvestmentElement InvestmentElement { get; set; }
    }
}
