using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Defines a cash movement in or out for the cash flow
    /// </summary>
    public interface ICashMovementCategory : INomenclator
    {
        /// <summary>
        /// Superior Category
        /// </summary>
        IEntity SuperiorCategory { get; set; }

        string SuperiorCategoryId { get; set; }
        /// <summary>
        /// sub cathegories en cash movements
        /// </summary>
        IList<ICashMovementCategory> SubCategories { get; set; }
        /// <summary>
        /// Movements of cash throw time
        /// </summary>
        IList<ICashMovement> Movements { get; set; } 
    }
}
