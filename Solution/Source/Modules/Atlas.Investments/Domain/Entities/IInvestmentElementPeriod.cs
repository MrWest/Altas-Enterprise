using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
     /// <summary>
     /// Represents a period for an investment Element
     /// </summary>
    public interface IInvestmentElementPeriod: IPeriod
    {
        /// <summary>
        /// Gets the investment element to which belong the current <see cref="InvestmentElementPeriod"/>.
        /// </summary>
        IInvestmentElement InvestmentElement { get; set; }
    }
}
