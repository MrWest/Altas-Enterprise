using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Investments.Domain.Entities
{
    /// <summary>
    /// Contract of a domain entity called: "Budget component". This is a base contract, not targeting concrete classes
    /// but targeting a base one. Derivers of the base class implementing this interface will be the actual budget
    /// components.
    /// </summary>
    public interface IBudgetComponent  : IEntity,IPeriodCalculator
    {
        /// <summary>
        /// Gets or sets the budget to which belong the current <see cref="IBudget"/>.
        /// </summary>
        IBudget Budget { get; set; }

       

        /// <summary>
        /// Gets the list of planned activities composing the current <see cref="IBudgetComponent"/>.
        /// </summary>
        IList<IPlannedActivity> PlannedActivities { get; }

        /// <summary>
        /// Gets the list of executed resources composing the current <see cref="IBudgetComponent"/>.
        /// </summary>
        //IList<IExecutedResource> ExecutedResources { get; }

        /// <summary>
        /// Gets the list of executed activities composing the current <see cref="IBudgetComponent"/>.
        /// </summary>
        IList<IExecutedActivity> ExecutedActivities { get; }

      
    }
}
