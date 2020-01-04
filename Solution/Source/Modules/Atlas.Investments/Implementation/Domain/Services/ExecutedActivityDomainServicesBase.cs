using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    /// Base class of the domain services managing the business rules of the executed resources of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component which items business rules are handled here.</typeparam>
    public  class ExecutedActivityDomainServicesBase:
        ActivityDomainServicesBase<IExecutedActivity>, IExecutedActivityDomainServices
        //where TComponent : class, IBudgetComponent
    {

       
        /// <summary>
        ///     Determines whether there can be executed any of the given planned items.
        /// </summary>
        /// <typeparam name="TPlanned">The type of the planned items to determine whether there can executed or not.</typeparam>
        /// <param name="plannedItems">
        ///     A <see cref="IEnumerable{T}" /> of planned items to determine whether there can be executed at least one of them.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="plannedItems"/> is null.</exception>
        /// <returns>True if there is at least one planned item unexecuted; false otherwise.</returns>
        //public override bool CanExecute(IEnumerable<IPlannedActivity> plannedItems) 
        //{
        //    if (plannedItems == null)
        //        throw new ArgumentNullException("plannedItems");

        //    return plannedItems.Any(x => x.Execution == null);
        //}

        /// <summary>
        ///     Executes all the planned items still not executed.
        /// </summary>
        /// <typeparam name="TPlanned">The type of the planned items to execute.</typeparam>
        /// <param name="plannedItems">
        ///     The <see cref="IEnumerable{T}" /> of planned items to executed the unexecuted ones among them.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="plannedItems"/> is null.</exception>
        /// <returns>An <see cref="int" /> representing how many planned items have been actually executed.</returns>
        public IEnumerable<IExecutedActivity> Execute(IEnumerable<IPlannedActivity> plannedItems) //where TPlanned : class, IPlannedActivity
        {
            if (plannedItems == null)
                throw new ArgumentNullException("plannedItems");

            // Get the unexecuted planned items
            var unexecutedItems = from plannedItem in plannedItems where plannedItem.Execution == null select plannedItem;

            // And create the execution for them, by relating them with their corresponding new execution item
            return unexecutedItems.Aggregate(new List<IExecutedActivity>(), (list, plannedItem) =>
            {
                IExecutedActivity itemExecution = Create();
                itemExecution = SetComponent(itemExecution, SubSpecialityHolder);
                itemExecution.Planification = plannedItem.Id;
                plannedItem.Execution = itemExecution.Id; //Will not work


                // CatchResourses(itemExecution, plannedItem );
                list.Add(itemExecution);

                return list;
            });
        }


        protected IExecutedActivity SetComponent(IExecutedActivity addedItem, ISubSpecialityHolder component)
        {
            addedItem.SubSpecialityHolder = component;
            return addedItem;
        }
        /// <summary>
        /// Sets the creation data to the given executed resource.
        /// </summary>
        /// <param name="executedActivity">The <see cref="IExecutedActivity"/> to set the data to.</param>
        /// <exception cref="ArgumentNullException"><paramref name="executedActivity"/> is null.</exception>
        protected override void SetDataToNew(IExecutedActivity executedActivity)
        {
            if (executedActivity == null)
                throw new ArgumentNullException("executedActivity");

            executedActivity.Name = Resources.NewExecutedActivityName;
            executedActivity.Description = Resources.NewExecutedActivityDescription;
            var period = ServiceLocator.Current.GetInstance<IPeriod>();
            period.Holder = executedActivity;
            executedActivity.Period = period;
            //  executedActivity.Code = Guid.NewGuid().ToString();
        }
    }
}
