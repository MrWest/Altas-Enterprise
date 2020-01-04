using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Defines a cash entry or outgoing over placed in time
    /// </summary>
    public interface ICashMovement:IEntity
    {
        ICashMovementCategory CashMovementCategory { get; set; }
        /// <summary>
        /// date for the current movement of cash
        /// </summary>
        DateTime Date { get; set; }
        
        /// <summary>
        /// amount for the current movement of cash
        /// </summary>
        decimal Amount { get; set; }

        string CashMovementCategoryId { get; set; }
    }
}
