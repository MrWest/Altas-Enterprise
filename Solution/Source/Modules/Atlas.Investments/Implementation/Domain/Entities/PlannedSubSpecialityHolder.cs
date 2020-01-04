using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    public class PlannedSubSpecialityHolder: SubSpecialityHolder, IPlannedSubSpecialityHolder
    {
        public IList<IPlannedActivity> PlannedActivities { get; }
        public object Execution { get; set; }
    }
}