using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    ///     Implementation of the base contract
    ///     <see cref="ExecutedBudgetComponentItemManagerApplicationServicesBase{TItem,TComponent,TRepository,TDomainServices}" />
    ///     , representing the application services involved in responding the executed budget component items CRUD-operations
    ///     coming from upper layers.
    /// </summary>
    /// <typeparam name="TItem">The type of the executed budget component items managed here.</typeparam>
    /// <typeparam name="TComponent">The type of budget component to which belong the excuted items managed here.</typeparam>
    /// <typeparam name="TRepository">
    ///     The type of the repository used to carry on with the data operations regarding to the executed items.
    /// </typeparam>
    /// <typeparam name="TDomainServices">
    ///     The type of the domain services used to enforce the business layers for the executed items managed here.
    /// </typeparam>
    public abstract class ExecutedBudgetComponentItemManagerApplicationServicesBase<TItem, TComponent, TRepository, TDomainServices> :
        BudgetComponentItemManagerApplicationServicesBase<TItem, TComponent, TRepository, TDomainServices>,
        IExecutedBudgetComponentItemManagerApplicationServices<TItem>
        where TItem : class, IExecutedBudgetComponentItem
        where TComponent : class, IBudgetComponent
        where TRepository : IBudgetComponentActivityRepository<TItem, TComponent>
        where TDomainServices : IExecutedBudgetComponentItemDomainServices<TItem, TComponent>
    {
        /// <summary>
        ///     When overridden in a deriver it gets the corresponding planned items repository allowing to access the executed
        ///     items in the current application services being their execution.
        /// </summary>
        protected abstract IRepository PlannedItemRepository { get; }


        /// <summary>
        ///     Determines whether there can be executed the given planned items.
        /// </summary>
        /// <typeparam name="TPlanned">The type of the planned items to try executing.</typeparam>
        /// <param name="plannedItems">
        ///     An <see cref="IEnumerable{T}" /> of planned items being the ones to determine whether there can
        ///     be executed.
        /// </param>
        /// <returns>True when there is at least one of <see cref="plannedItems" /> that can be executed; false otherwise.</returns>
        [CachesResult]
        public bool CanExecute<TPlanned>(IEnumerable<TPlanned> plannedItems) where TPlanned : class, IPlannedBudgetComponentItem
        {
            if (plannedItems == null)
                throw new ArgumentNullException("plannedItems");

            var dbPlannedItems = GetPlannedDatabaseItems(plannedItems);

            return DomainServices.CanExecute(dbPlannedItems);
        }

        /// <summary>
        ///     Executes the given planned items.
        /// </summary>
        /// <param name="plannedItems">The <see cref="IEnumerable{T}" /> of planned items to execute.</param>
        /// <returns>
        ///     The count of <see cref="IPlannedBudgetComponentItem" /> in <paramref name="plannedItems" /> that were actually
        ///     executed.
        /// </returns>
        [Validate]
        [ResetsCache]
        [Commit]
        public IEnumerable<TItem> Execute<TPlanned>(IEnumerable<TPlanned> plannedItems) where TPlanned : class, IPlannedBudgetComponentItem
        {
            if (plannedItems == null)
                throw new ArgumentNullException("plannedItems");

            var dbPlannedItems = GetPlannedDatabaseItems(plannedItems).ToArray();

            IEnumerable<TItem> result = DomainServices.Execute(dbPlannedItems).ToArray();

            foreach (TItem executedItem in result)
                Repository.Add(executedItem);

            return result;
        }

        private IEnumerable<TPlanned> GetPlannedDatabaseItems<TPlanned>(IEnumerable<TPlanned> plannedItems) where TPlanned : class, IPlannedBudgetComponentItem
        {
            IRepository plannedItemRepository = PlannedItemRepository;

            IEnumerable<object> ids = from plannedItem in plannedItems select plannedItem.Id;
            IEnumerable<TPlanned> dbPlannedItems = plannedItemRepository.FindById(ids.ToArray()).Cast<TPlanned>();

            return dbPlannedItems;
        }
    }
}