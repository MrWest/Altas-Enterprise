using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    public class UnderGroup : PriceSystemGroup, IUnderGroup
    {
        [ForeignKey("RegularGroupId")]
        public IRegularGroup RegularGroup { get; set; }

        private IList<IUnderGroupActivity> _activities;

        public IList<IUnderGroupActivity> Activities
        {
            get
            {
                if (_activities == null)
                    _activities = new List<IUnderGroupActivity>();
                return _activities;
            }
        }

        private IList<IUnderGroupResource> _resources;

        public IList<IUnderGroupResource> Resources
        {
            get
            {
                if (_resources == null)
                    _resources = new List<IUnderGroupResource>();
                return _resources;
            }
        }
       
        public DateTime StartDate()
        {
            DateTime start = DateTime.Today;
            bool first = true;
            foreach (IUnderGroupActivity plannedActivity in Activities)
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
            foreach (IUnderGroupActivity plannedActivity in Activities)
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

        public string RegularGroupId { get; set; }
    }
}
