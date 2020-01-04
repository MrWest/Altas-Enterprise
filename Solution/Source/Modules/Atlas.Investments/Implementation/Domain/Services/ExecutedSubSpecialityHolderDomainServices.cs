using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    public class ExecutedSubSpecialityHolderDomainServices: SubSpecialityHolderDomainServices<IExecutedSubSpecialityHolder>, IExecutedSubSpecialityHolderDomainServices
    {
        public IEnumerable<IExecutedSubSpecialityHolder> Execute(IEnumerable<IPlannedSubSpecialityHolder> dbPlannedItems)
        {
            if (dbPlannedItems == null)
                throw new ArgumentNullException("plannedItems");

            // Get the unexecuted planned items
            var unexecutedItems = from plannedItem in dbPlannedItems where plannedItem.Execution == null select plannedItem;

            // And create the execution for them, by relating them with their corresponding new execution item
            return unexecutedItems.Aggregate(new List<IExecutedSubSpecialityHolder>(), (list, plannedItem) =>
            {
                IExecutedSubSpecialityHolder itemExecution = Create();
                itemExecution =  SetComponent(itemExecution, BudgetComponent);
                itemExecution.Plannification = plannedItem.Id;
                plannedItem.Execution = itemExecution.Id; //Will not work


                // CatchResourses(itemExecution, plannedItem );
                list.Add(itemExecution);

                return list;
            });
        }

        protected IExecutedSubSpecialityHolder SetComponent( IExecutedSubSpecialityHolder addedItem, IBudgetComponent component)
        {
            addedItem.BudgetComponent = component;
            return addedItem;
        }
    }
}