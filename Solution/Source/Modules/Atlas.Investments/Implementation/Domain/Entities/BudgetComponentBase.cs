using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    /// Implementation of the domain entity: "Budget Component". This is a component of a budget belonging to a certain
    /// investment element. A set of these form a budget.
    /// </summary>
    public abstract class BudgetComponentBase : EntityBase, IBudgetComponent///,IActivityPlanner 
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BudgetComponentBase"/> deriver.
        /// </summary>
        protected BudgetComponentBase()
        {
           // PlannedResources = new List<IPlannedResource>();
            PlannedActivities = new List<IPlannedActivity>();
          //  ExecutedResources = new List<IExecutedResource>();
            ExecutedActivities = new List<IExecutedActivity>();

            LastCalculatedFinishDate = DateTime.Today;
            LastCalculatedStartDate = DateTime.Today;
        }


        /// <summary>
        /// Gets or sets the budget to which belong the current <see cref="BudgetComponentBase"/>.
        /// </summary>
        public IBudget Budget { get; set; }



        /// <summary>
        /// Gets the list of planned activities composing the current <see cref="BudgetComponentBase"/>
        /// </summary>
        public IList<IPlannedActivity> PlannedActivities { get; private set; }

        /// <summary>
        /// Gets the list of executed resources composing the current <see cref="BudgetComponentBase"/>.
        /// </summary>
     //   public IList<IExecutedResource> ExecutedResources { get; private set; }

        /// <summary>
        /// Gets the list of executed activities composing the current <see cref="BudgetComponentBase"/>
        /// </summary>
        public IList<IExecutedActivity> ExecutedActivities { get; private set; }


        public DateTime StartDate()
        {
            if (!StartCalculated)
            {
                LastCalculatedStartDate = DateTime.Today;
                bool first = true;
                foreach (IPlannedActivity plannedActivity in PlannedActivities)
                {
                    if (first)
                    {
                            LastCalculatedStartDate = plannedActivity.StartDate();
                        first = false;
                    }
                    else
                    {
                        if (LastCalculatedStartDate.CompareTo(plannedActivity.StartDate()) > 0)
                                LastCalculatedStartDate = plannedActivity.StartDate();
                    }
                }

                StartCalculated = true;
            }
           
           
            return LastCalculatedStartDate;
        }

        public DateTime FinishDate()
        {
            if (!EndCalculated)
            {
                LastCalculatedFinishDate = DateTime.Today;
                bool first = true;
                foreach (IPlannedActivity plannedActivity in PlannedActivities)
                {
                    if (first)
                    {
                        LastCalculatedFinishDate = plannedActivity.FinishDate();
                        first = false;
                    }
                    else
                    {
                        if (LastCalculatedFinishDate.CompareTo(plannedActivity.FinishDate()) < 0)
                            LastCalculatedFinishDate = plannedActivity.FinishDate();
                    }
                }

                EndCalculated = true;
            }

            return LastCalculatedFinishDate;
        }

        
        public DateTime LastCalculatedFinishDate { get; set; }
        public DateTime LastCalculatedStartDate { get; set; }
        public bool StartCalculated { get; set; }
        public bool EndCalculated { get; set; }
    }
}
