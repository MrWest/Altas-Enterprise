using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    /// Describes a Section feature. Created to arrange data in a price system
    /// </summary>
    public class Section: PriceSystem, ISection//,IActivityPlanner 
    {
      
        private IList<IPlannedActivity> _plannedActivities;

        /// <summary>
        ///     Gets or sets the parent Section (<see cref="ISection" />) of the current one.
        /// </summary>
        public IPriceSystem AboveSection { get; set; }

        public IBudget Budget { get; set; }

        /// <summary>
        /// Gets the list of planned Activities composing the current <see cref="ISection"/>.
        /// </summary>
        public IList<IPlannedActivity> PlannedActivities { get { return _plannedActivities??new List<IPlannedActivity>(); } }

        public IList<IExecutedActivity> ExecutedActivities { get; private set; }

        private IList<ISection> _sections;
        ///// <summary>
        ///// Gets the list of Sections for the current <see cref="ISection"/>.
        ///// </summary>
        public IList<ISection> Sections { get { return _sections ?? new List<ISection>(); } }

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
