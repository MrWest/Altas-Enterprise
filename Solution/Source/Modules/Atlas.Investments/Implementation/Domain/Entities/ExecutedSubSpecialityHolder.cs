using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    public class ExecutedSubSpecialityHolder: SubSpecialityHolder, IExecutedSubSpecialityHolder
    {
       
        public IList<IExecutedActivity> ExecutedActivities { get; }
        public object Plannification { get; set; }
    }
}