using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    public interface IPlannedSubSpecialityHolder:ISubSpecialityHolder
    {
      
        /// <summary>
        /// Gets the list of planned activities composing the current <see cref="IBudgetComponent"/>.
        /// </summary>
        IList<IPlannedActivity> PlannedActivities { get; }

        object Execution { get; set; }
    }
}