using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    /// This is the base class of the executed items of a budget component.
    /// </summary>
    public abstract class ExecutedBudgetComponentItemBase : BudgetComponentItemBase, IExecutedBudgetComponentItem
    {
        //private string _name, _code, _description;
        //private decimal _quantity;


        /// <summary>
        /// Gets or sets the planification (<see cref="IPlannedBudgetComponentItem"/>) of the current
        /// <see cref="IExecutedBudgetComponentItem"/>.
        /// </summary>
        public object Planification { get; set; }

        public decimal ExecutedQuantity
        {
            get
            {
                return (ExecutionLog != null && ExecutionLog.Count > 0)?ExecutionLog.Sum(x => x.Amount):Quantity;
            }
             
        }

        public IList<IExecution> ExecutionLog { get; set; }

        /// <summary>
        /// Gets or sets the name of the current <see cref="ExecutedBudgetComponentItemBase"/> if its is unplanned. If there is
        /// a planification set for it (Planification is not null) then its name is returned, and any new value will be rejected.
        /// </summary>
        //public override string Name
        //{
        //    get { return Planification != null ? Planification.Name : _name; }
        //    set
        //    {
        //        if (Planification == null)
        //            _name = value;
        //    }
        //}

        ///// <summary>
        ///// Gets or sets the description of the current <see cref="ExecutedBudgetComponentItemBase"/> if its is unplanned. If there is
        ///// a planification set for it (Planification is not null) then its description is returned, and any new value will be rejected.
        ///// </summary>
        //public override string Description
        //{
        //    get { return Planification != null ? Planification.Description : _description; }
        //    set
        //    {
        //        if (Planification == null)
        //            _description = value;
        //    }
        //}

        ///// <summary>
        ///// Gets or sets the code of the current <see cref="ExecutedBudgetComponentItemBase"/> if its is unplanned. If there is
        ///// a planification set for it (Planification is not null) then its code is returned, and any new value will be rejected.
        ///// </summary>
        //public override string Code
        //{
        //    get { return Planification != null ? Planification.Code : _code; }
        //    set
        //    {
        //        if (Planification == null)
        //            _code = value;
        //    }
        //}

        ///// <summary>
        ///// Gets or sets the quantity of the current <see cref="ExecutedBudgetComponentItemBase"/> if its is unplanned. If there is
        ///// a planification set for it (Planification is not null) then its quantity is returned, and any new value will be rejected.
        ///// </summary>
        //public override decimal Quantity
        //{
        //    get { return Planification != null ? Planification.Quantity : _quantity; }
        //    set
        //    {
        //        if (Planification == null)
        //            _quantity = value;
        //    }
        //}

        //private GetPlannification(object plannification)
        //{
            
        //}

    }
}
