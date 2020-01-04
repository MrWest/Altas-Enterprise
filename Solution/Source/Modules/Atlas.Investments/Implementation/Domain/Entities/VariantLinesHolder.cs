using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    public class VariantLinesHolder:BudgetComponentItemBase,IVariantLinesHolder
    {
        public VariantLinesHolder()
        {
          PlannedResources = new List<IPlannedResource>();
          PlannedActivities = new List<IPlannedActivity>();
        }

        public  IEntity Component { get; set; }

        /// <summary>
        /// Gets the list of planned resources composing the current <see cref="BudgetComponentBase"/>.
        /// </summary>
        public IList<IPlannedResource> PlannedResources { get; private set; }

        public IBudget Budget { get; set; }

        /// <summary>
        /// Gets the list of planned activities composing the current <see cref="BudgetComponentBase"/>
        /// </summary>
        public IList<IPlannedActivity> PlannedActivities { get; private set; }

        public IList<IExecutedActivity> ExecutedActivities { get; private set; }
        public DateTime StartDate()
        {
            DateTime start = DateTime.Today;
            bool first = true;
            foreach (IPlannedActivity plannedActivity in PlannedActivities)
            {
                if (first)
                {
                    start = plannedActivity.StartDate();
                    first = false;
                }
                else
                {
                    if (start.CompareTo(plannedActivity.StartDate()) > 0)
                        start = plannedActivity.StartDate();
                }
            }




            return start;
        }

        public DateTime FinishDate()
        {
            DateTime end = DateTime.Today;
            bool first = true;
            foreach (IPlannedActivity plannedActivity in PlannedActivities)
            {
                if (first)
                {
                    end = plannedActivity.FinishDate();
                    first = false;
                }
                else
                {
                    if (end.CompareTo(plannedActivity.FinishDate()) < 0)
                        end = plannedActivity.FinishDate();
                }
            }
            return end;
        }

        public bool StartCalculated { get; set; }
        public bool EndCalculated { get; set; }
    }
}
