using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    /// <summary>
    /// Represents the implementation of the domain entity: "Planned Resource".
    /// </summary>
    public class PlannedActivity : Activity , IPlannedActivity
    {
        /// <summary>
        /// Gets or sets the execution (<see cref="IExecutedBudgetComponentItem"/>) of the current
        /// <see cref="IPlannedBudgetComponentItem"/>.
        /// </summary>
        public object Execution { get; set; }

        public PlannedActivity()
        {
        }

        
    }
}
