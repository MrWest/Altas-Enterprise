using System;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget
{
    /// <summary>
    ///     Implementation of the repository managing the data operations for the executed budget component items.
    /// </summary>
    /// <typeparam name="TItem">The type of the executed items of a budget component.</typeparam>
    /// <typeparam name="TComponent">The type of the budget component to which belong the executed items.</typeparam>
    //public abstract class ExecutedBudgetComponentItemRepositoryBase<TComponent> :
    //    BudgetComponentItemRepositoryBase<IExecutedActivity>,
    //    IRelatedRepository<IExecutedActivity, TComponent>
    //    where TItem : class, IExecutedBudgetComponentItem
    //    where TComponent : class, IBudgetComponent
    //{
    //    /// <summary>
    //    ///     Initializes a new instance of <see cref="ExecutedBudgetComponentItemRepositoryBase{TItem,TComponent}" /> given a
    //    ///     database context.
    //    /// </summary>
    //    /// <param name="databaseContext">
    //    ///     The <see cref="IDb4ODatabaseContext" /> being the means for the repository to make the data operations in the
    //    ///     database.
    //    /// </param>
    //    protected ExecutedBudgetComponentItemRepositoryBase(IDb4ODatabaseContext databaseContext)
    //        : base(databaseContext)
    //    {
    //    }


    //    /// <summary>
    //    ///     Adds the given executed item to the current repository.
    //    /// </summary>
    //    /// <param name="executedItem">The executed item to add.</param>
    //    /// <exception cref="ArgumentNullException"><paramref name="executedItem" /> is null.</exception>
    //    /// <returns>The added executed item.</returns>
    //    public override TItem Add(TItem executedItem)
    //    {
    //        TItem addedExecutedItem = base.Add(executedItem);

    //        //if (executedItem.Planification != null)
    //        //    this.Relate(executedItem, executedItem.Planification, DatabaseContext);

    //        return addedExecutedItem;
    //    }

    //    /// <summary>
    //    ///     Updates the changed made a given executed item.
    //    /// </summary>
    //    /// <param name="executedItem">The executed item to update.</param>
    //    /// <exception cref="ArgumentNullException"><paramref name="executedItem" /> is null.</exception>
    //    public override void Update(TItem executedItem)
    //    {
    //        base.Update(executedItem);

    //        SetupRelationWithItsPlanification(executedItem);
    //    }

    //    /// <summary>
    //    ///     Deletes a given executed item from the current repository.
    //    /// </summary>
    //    /// <param name="executedItem">The executed item to delete.</param>
    //    /// <exception cref="ArgumentNullException"><paramref name="executedItem" /> is null.</exception>
    //    public override void Delete(TItem executedItem)
    //    {
    //        if (executedItem == null)
    //            throw new ArgumentNullException("executedItem");

    //        //TItem dbExecutedItem = Find(executedItem);
    //        //if (dbExecutedItem == null)
    //        //    executedItem.Planification = null;

    //        base.Delete(executedItem);
    //    }

    //    /// <summary>
    //    ///     Relates the given executed resource with its planification.
    //    /// </summary>
    //    /// <param name="executedItem">The executed resource to get associated a planification (<paramref name="plannedItem" />.</param>
    //    /// <param name="plannedItem">This is the planification to be associated to <paramref name="executedItem" />.</param>
    //    /// <exception cref="ArgumentNullException">
    //    ///     Either <paramref name="executedItem" /> or <paramref name="plannedItem" /> is null.
    //    /// </exception>
    //    // TODO: Move this to an extension to generalize this algorithm. This is modeling a one to one with cascade update relationship of two entities.
    //    public void Relate(TItem executedItem, IPlannedBudgetComponentItem plannedItem)
    //    {
    //        if (executedItem == null)
    //            throw new ArgumentNullException("executedItem");
    //        if (plannedItem == null)
    //            throw new ArgumentNullException("plannedItem");

    //        //if (executedItem.Planification != null && !Equals(executedItem.Planification.Id, plannedItem.Id))
    //        //{
    //        //    var dbExecutedItem = DatabaseContext.Find<TItem>(executedItem.Id);
    //        //    if (dbExecutedItem.Planification != null)
    //        //    {
    //        //        dbExecutedItem.Planification.Execution = null;
    //        //        DatabaseContext.Store(dbExecutedItem.Planification);
    //        //    }

    //        //    executedItem.Planification.Execution = null;
    //        //}

    //        //if (plannedItem.Execution != null && !Equals(plannedItem.Execution.Id, executedItem.Id))
    //        //{
    //        //    var dbPlannedItem = DatabaseContext.Find<IPlannedBudgetComponentItem>(plannedItem.Id);
    //        //    if (dbPlannedItem.Execution != null)
    //        //    {
    //        //        dbPlannedItem.Execution.Planification = null;
    //        //        DatabaseContext.Store(dbPlannedItem.Execution);
    //        //    }

    //        //    plannedItem.Execution.Planification = null;
    //        //}

    //        //executedItem.Planification = plannedItem;
    //        //plannedItem.Execution = executedItem;
    //    }

    //    /// <summary>
    //    ///     Breaks the relation between the given executed resource with its planification.
    //    /// </summary>
    //    /// <param name="executedItem">
    //    ///     The executed resource to get disassociated from its planification (<paramref name="plannedItem" />.
    //    /// </param>
    //    /// <param name="plannedItem">This is the planification to be associated to <paramref name="executedItem" />.</param>
    //    /// <exception cref="ArgumentNullException">
    //    ///     Either <paramref name="executedItem" /> or <paramref name="plannedItem" /> is null.
    //    /// </exception>
    //    public void Unrelate(TItem executedItem, IPlannedBudgetComponentItem plannedItem)
    //    {
    //        //if (executedItem == null)
    //        //    throw new ArgumentNullException("executedItem");
    //        //if (plannedItem == null)
    //        //    throw new ArgumentNullException("plannedItem");

    //        //executedItem.Planification = null;
    //        //plannedItem.Execution = null;
    //    }

    //    /// <summary>
    //    ///     Saves the changes made to the given planification in order to get its references changes persisted.
    //    /// </summary>
    //    /// <param name="plannedItem">
    //    ///     The planned item representing the planification which references must be persisted.
    //    /// </param>
    //    /// <exception cref="ArgumentNullException"><paramref name="plannedItem" /> is null.</exception>
    //    // TODO: Move this to an extension to generalize this algorithm. This is modeling a one to one with cascade update relationship of two entities.
    //    public void SaveReference(IPlannedBudgetComponentItem plannedItem)
    //    {
    //        //if (plannedItem == null)
    //        //    throw new ArgumentNullException("plannedItem");

    //        var dbPlannedItem = DatabaseContext.Find<IPlannedBudgetComponentItem>(plannedItem.Id);
    //        //if (dbPlannedItem == null)
    //        //    return;

    //        //if (plannedItem.Execution == null)
    //        //{
    //        //    if (dbPlannedItem.Execution != null)
    //        //    {
    //        //        dbPlannedItem.Execution.Planification = null;
    //        //        DatabaseContext.Store(dbPlannedItem.Execution);
    //        //    }

    //        //    dbPlannedItem.Execution = null;
    //        //}
    //        //else
    //        //    dbPlannedItem.Execution = DatabaseContext.Find<TItem>(plannedItem.Execution.Id);

    //        DatabaseContext.Store(dbPlannedItem);
    //    }

    //    /// <summary>
    //    ///     Saves the changes made to the references of the given executed item.
    //    /// </summary>
    //    /// <param name="executedItem">The executed item which references will be saved.</param>
    //    /// <exception cref="ArgumentNullException"><paramref name="executedItem" /> is null.</exception>
    //    // TODO: Move this to an extension to generalize this algorithm. This is modeling a one to one with cascade update relationship of two entities.
    //    public override void SaveReference(TItem executedItem)
    //    {
    //        var dbExecutedItem = DatabaseContext.Find<TItem>(executedItem.Id);
    //        //if (dbExecutedItem == null)
    //        //    return;

    //        //if (executedItem.Planification == null)
    //        //{
    //        //    if (dbExecutedItem.Planification != null)
    //        //    {
    //        //        dbExecutedItem.Planification.Execution = null;
    //        //        DatabaseContext.Store(dbExecutedItem.Planification);
    //        //    }

    //        //    dbExecutedItem.Planification = null;
    //        //}
    //        //else
    //        //    dbExecutedItem.Planification = DatabaseContext.Find<IPlannedBudgetComponentItem>(executedItem.Planification.Id);

    //        DatabaseContext.Store(dbExecutedItem);
    //    }


    //    private void SetupRelationWithItsPlanification(TItem executedItem, bool relate = true)
    //    {
    //        //Action<TItem, IPlannedBudgetComponentItem, IDb4ODatabaseContext> method = relate
    //        //    ? (Action<TItem, IPlannedBudgetComponentItem, IDb4ODatabaseContext>)this.Relate
    //        //    : this.Unrelate;

    //        //IPlannedBudgetComponentItem plannedItem = executedItem.Planification;
    //        //if (plannedItem != null)
    //        //    method(executedItem, plannedItem, DatabaseContext);
    //        //else
    //        //    SaveReference(executedItem);
    //    }
    //}
}