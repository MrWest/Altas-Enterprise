using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Describes an entity containning PlannedActivities
    /// </summary>
    public interface IActivity: IBudgetComponentItem
    {

        /// <summary>
        /// budget Component related to this entity
        /// </summary>
        //    IBudgetComponent Component { get; set; }

        /// <summary>
        /// budget Component related to this entity
        /// </summary>
        ISubSpecialityHolder SubSpecialityHolder { get; set; }
        string SubSpecialityHolderId { get; set; }



        /// <summary>
        /// Get or sets the Expense Concept id of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        object SubSpeciality { get; set; }


        /// <summary>
        /// Get or sets the Expense Concept id of the current <see cref="BudgetComponentItemBase"/>.
        /// </summary>
        object Executor { get; set; }
    }
}
