using System;
using System.Collections.Generic;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Tests.Implementation.Domain.Entities
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

        public string Id { get; set; }

        public string FullName { get; set; }
        public DateTime StartDate()
        {
            throw new NotImplementedException();
        }

        public DateTime FinishDate()
        {
            throw new NotImplementedException();
        }

        public bool StartCalculated { get; set; }
        public bool EndCalculated { get; set; }
        public DateTime LastCalculatedFinishDate { get; set; }
        public DateTime LastCalculatedStartDate { get; set; }
        public IWorkCapitalCashFlow WorkCapitalCashFlow { get; set; }
        public IWorkCapitalCashFlow ExecutedWorkCapitalCashFlow { get; set; }
        public string WorkCapitalCashFlowId { get; set; }
        public string ExecutedWorkCapitalCashFlowId { get; set; }
    }
}
