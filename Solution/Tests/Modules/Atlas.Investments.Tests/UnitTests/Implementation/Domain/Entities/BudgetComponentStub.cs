using System.Collections.Generic;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Domain.Entities
{
    public class BudgetComponentStub : IEquipmentComponent, IConstructionComponent, IOtherExpensesComponent, IWorkCapitalComponent
    {
        public BudgetComponentStub()
        {
            PlannedResources = new List<IPlannedResource>();
            PlannedActivities = new List<IPlannedActivity>();
            ExecutedResources = new List<IExecutedResource>();
            ExecutedActivities = new List<IExecutedActivity>();
        }


        public IBudget Budget { get; set; }

        public IList<IPlannedResource> PlannedResources { get; set; }

        public IList<IPlannedActivity> PlannedActivities { get; set; }

        public IList<IExecutedResource> ExecutedResources { get; set; }

        public IList<IExecutedActivity> ExecutedActivities { get; set; }

        public object Id { get; set; }

        public string FullName { get; set; }
    }
}
