using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    /// <summary>
    /// Respresent a certain entity who has a cost attribute
    /// </summary>
    public interface ICosttable
    {
        /// <summary>
        /// Calculates the cost
        /// </summary>
        decimal Cost { get; }

        /// <summary>
        /// Gtes the Calculated Cost
        /// </summary>
       // decimal CalculatedCost { get; set;  }

        bool IsCostCalculated { get; set; }
    }
}
