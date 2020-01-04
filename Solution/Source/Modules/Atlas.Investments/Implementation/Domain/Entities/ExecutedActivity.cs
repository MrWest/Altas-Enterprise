using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    /// Represents the implementation of the domain entity: "Executed Resource".
    /// </summary>
    public class ExecutedActivity : Activity, IExecutedActivity
    {

        public object Planification { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the current <see cref="ExecutedBudgetComponentItemBase"/> if its is unplanned. If there is
        /// a planification set for it (Planification is not null) then its quantity is returned, and any new value will be rejected.
        /// </summary>
        public override decimal Quantity
        {
            get { return (ExecutionLog != null && ExecutionLog.Count > 0) ? ExecutionLog.Sum(x => x.Amount) : base.Quantity; }
            set
            {
                base.Quantity = value;

            }
        }

        //public decimal ExecutedQuantity
        //{
        //    get
        //    {
        //        return (ExecutionLog != null && ExecutionLog.Count > 0) ? ExecutionLog.Sum(x => x.Amount) : Quantity;
        //    }

        //}

        private IExecutionManagerApplicationServices _executionServices;


        public IList<IExecution> ExecutionLog
        {
            get
            {
                if (_executionServices == null)
                {
                    _executionServices = ServiceLocator.Current.GetInstance<IExecutionManagerApplicationServices>();
                    _executionServices.ExecutedActivity = this;
                }
                return _executionServices.Items.ToList();
            }
            set
            {
                
            }
        }

        //private IBudgetComponent _budgetComponent;

        ///// <summary>
        ///// Gets or sets the <see cref="IBudgetComponent"/> to which belong the current <see cref="IBudgetComponentItem"/>.
        ///// </summary>
        //public IBudgetComponent Component
        //{
        //    get { return _budgetComponent; }
        //    set
        //    {
        //        _budgetComponent = value;
        //    }
        //}

        /// <summary>
        /// Gets or sets the planification (<see cref="IPlannedBudgetComponentItem"/>) of the current
        /// <see cref="IExecutedBudgetComponentItem"/>.
        /// </summary>
        // public object Planification { get; set; }
    }
}
