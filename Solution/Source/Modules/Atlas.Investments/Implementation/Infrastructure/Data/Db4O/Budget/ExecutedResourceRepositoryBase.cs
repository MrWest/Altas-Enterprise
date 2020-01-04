using System;
using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget
{
    /// <summary>
    ///     Base class of the repository managing the data operations related to the executed resources of a certain budget
    ///     component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the executed resources.</typeparam>
    public abstract class ExecutedResourceRepositoryBase<TComponent> :
        ExecutedBudgetComponentItemRepositoryBase<IExecutedResource, TComponent>
        where TComponent : class, IBudgetComponent
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="ExecutedResourceRepositoryBase{TComponent}" /> with a
        ///     <see cref="IDb4ODatabaseContext" />.
        /// </summary>
        /// <param name="databaseContext">
        ///     The instance of <see cref="IDb4ODatabaseContext" /> that carries on the actual raw data operations the
        ///     initializing repository performs.
        /// </param>
        protected ExecutedResourceRepositoryBase(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }


        /// <summary>
        ///     Gets the collection of the executed resources in the budget component in which it will be contained the items
        ///     managed in the current <see cref="ExecutedResourceRepositoryBase{TComponent}" />.
        /// </summary>
        protected override Func<TComponent, IList<IExecutedResource>> GetItemCollection
        {
            get { return x => x.ExecutedResources; }
        }
    }
}