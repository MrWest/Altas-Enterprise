using System.Collections.Generic;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    public class UnderGroupActivity: PlannedActivity, IUnderGroupActivity
    {
        public IUnderGroup UnderGroup { get; set; }
        public decimal ExecutedQuantity { get; }
        public IList<IExecution> ExecutionLog { get; set; }
        public object Planification { get; set; }
        public string UnderGroupId { get; set; }
    }
}