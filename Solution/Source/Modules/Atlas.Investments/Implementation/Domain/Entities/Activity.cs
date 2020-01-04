using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    /// Describes an entity containning PlannedActivities
    /// </summary>
    public class Activity: BudgetComponentItemBase,IActivity   {


        public Activity()
        {

            StartCalculated = true;
            EndCalculated = true;
            LastCalculatedFinishDate = Period.OriEnd();
            LastCalculatedStartDate = Period.OriStart();
        }
        /// <summary>
        /// Gets or sets the <see cref="IBudgetComponent"/> to which belong the current <see cref="IBudgetComponentItem"/>.
        /// </summary>
        public IBudgetComponent Component
        {
            get { return _budgetComponent; }
            set
            {
                _budgetComponent = value;
            }
        }

        ///// <summary>
        ///// Gets or sets the time interval (<see cref="IInvestmentElementPeriod"/> for  the current <see cref="IInvestmentElement"/>.
        ///// </summary>
        //public IPeriod Period { get; set; }

        public object SubSpeciality { get; set; }
        public DateTime StartDate()
        {
            return Period.OriStart();
        }

        public DateTime FinishDate()
        {

            // get all the menlabor resources
            var plannedResources = PlannedResources.Aggregate(new List<IPlannedResource>(), (list, pr) =>
            {
                if (pr.ResourceKind == ResourceKind.MenLabor)
                    list.Add(pr);
                return list;
            }).ToArray();


            //get all hours quantity already has the hours needed to finish (by one man) TODO arrange that
            var hours = new List<decimal>();
            foreach (IPlannedResource plannedResource in plannedResources)
            {
                if (plannedResource.MenNumber == 0)
                    throw new InvalidOperationException(Resources.MenNumber + " = 0.");
                hours.Add(plannedResource.Quantity / (plannedResource.MenNumber));
            }

            //if nothing here
            if (hours.Count == 0)
                return Period.OriEnd();
            //get max hours amount
            var maxhours = hours.Max();

            int days = (int)maxhours / 8;

            return Period.Starts.AddDays(days);
        }
        private IBudgetComponent _budgetComponent;

        
        public DateTime LastCalculatedFinishDate { get; set; }
        public DateTime LastCalculatedStartDate { get; set; }
        public bool StartCalculated { get; set; }
        public bool EndCalculated { get; set; }

        public ISubSpecialityHolder SubSpecialityHolder { get; set; }
        public object Executor { get; set; }
        public string SubSpecialityHolderId { get;  set; }
    }
}
