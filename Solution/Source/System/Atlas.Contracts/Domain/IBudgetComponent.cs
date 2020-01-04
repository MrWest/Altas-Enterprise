using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Represents one of the four budget components of a budget, Equipment in this case.
    /// </summary>
    public interface IBudgetComponent  : IEntity
    {
        IList<IPlannedResourceBudget> PlannedResources { get; set; }

        IList<IExecutedResourceBudget> ExecutedResources { get; set; }


        IList<IPlannedActivityBudget> PlannedActivities { get; set; }

        IList<IExecutedActivityBudget> ExecutedActivities { get; set; }

        
        bool ShowAll { get; set; }

        decimal PlannedActivitiesCost { get; }

        decimal ExecutedActivitiesCost { get; }

        decimal PlannedResourcesCost { get; }
        decimal ExecutedResourcesCost { get; }

        decimal FinancialExecutedResourcesCost { get; }
        
        decimal PlannedCost { get; }
        decimal ExecutedCost { get; }
        int GeneralProgress { get; }
        int ResourseProgress { get; }
        int ActivityProgress { get; }

        decimal GetPlannedByCurrency(ICurrency currency);

        //DateTime MinCollectionDatePlannedResources();
        //DateTime MaxCollectionDatePlannedResources();

        //DateTime MinCollectionDatePlannedActivities();

        //DateTime MaxCollectionDatePlannedActivities();

        //Period PeriodInherited();

    }
}
