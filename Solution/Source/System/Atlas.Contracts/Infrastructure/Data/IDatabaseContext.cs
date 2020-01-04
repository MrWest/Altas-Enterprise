using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data
{
    /// <summary>
    /// Represents the contract defined for a database context. This contract allow to abstarct the infrastructure data
    /// layer from the actual technology there is used currently.
    /// </summary>
    public interface IDatabaseContext : IDisposable
    {
        /// <summary>
        /// Gets a queryable allowing to make queries over all the entities of a certain type.
        /// </summary>
        /// <typeparam name="T">The type of the entities to query.</typeparam>
        /// <returns>A queryable containing all the entities, over which queries can be performed.</returns>
        IQueryable<T> GetAll<T>();
        
        /// <summary>
        /// Generates a new primary key.
        /// </summary>
        /// <returns>A new fresh key.</returns>
        string GenerateKey();

        /// <summary>
        /// Adds an entity to the database.
        /// </summary>
        /// <typeparam name="T">The type of the entity to add.</typeparam>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The new added entity.</returns>
        void Add<T>(T entity) where T : IEntity;

        /// <summary>
        /// Deletes the given entity from the data source.
        /// </summary>
        /// <typeparam name="T">The type of the entity to delete.</typeparam>
        /// <param name="entity">The entity to delete.</param>
        void Delete<T>(T entity) where T : IEntity;

        /// <summary>
        /// Saves the changes made to the data base.
        /// </summary>
        void Save();

        /// <summary>
        /// Drops the changes made to the data base and cancels them all.
        /// </summary>
        void DropChanges();

        /// <summary>
        /// Updates the changes made to the given item in its corresponding item in the currently opened transaction.
        /// </summary>
        /// <typeparam name="T">The type of the entity to update.</typeparam>
        /// <param name="item">
        /// The item to use its changes and apply them to its current transaction corresponding one.
        /// </param>
        void Update<T>(T item) where T : IEntity;

        /// <summary>
        /// Finds the entity which id is the given one.
        /// </summary>
        /// <typeparam name="T">The type of the entity to find.</typeparam>
        /// <param name="id">The identifier of the entity to retrieve.</param>
        /// <returns>The entity containing such identifier; null if no entity has it.</returns>
        T Find<T>(object id) where T : IEntity;

        /// <summary>
        /// Finds the item matching the given specification.
        /// </summary>
        /// <typeparam name="T">The type of the entity to find.</typeparam>
        /// <param name="specification">The specification to evaluate on each item to select the desired one.</param>
        /// <returns>
        /// An item of type <typeparamref name="T"/> being the single item matching <paramref name="specification"/>; or null if the specification
        /// did not evaluate to any item.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// Cannot return an single item matching the specification, because more than one matches it.
        /// </exception>
        T Find<T>(ISpecification<T> specification) where T : IEntity;

        /// <summary>
        /// Iterates over the whole set of elements of type <typeparamref name="T"/> selecting those which evaluation of a specification returns true.
        /// </summary>
        /// <typeparam name="T">The type of the entities to find.</typeparam>
        /// <param name="specification">The specification to evaluate on each element.</param>
        /// <returns>A enumerable of elements of <typeparamref name="T"/> which the evaluation of <paramref name="specification"/> returns true.</returns>
        IEnumerable<T> Where<T>(ISpecification<T> specification) where T : IEntity;

        /// <summary>
        /// Gets all the entities sorted as the defined comparison defines.
        /// </summary>
        /// <typeparam name="T">The type of the entities to retrieve.</typeparam>
        /// <param name="comparison">The <see cref="System.Comparison{T}"/> defining the sorting criteria.</param>
        /// <returns>All entities of type <typeparamref name="T"/> sorted descending.</returns>
        IEnumerable<T> GetSorted<T>(Comparison<T> comparison) where T : IEntity;

        /// <summary>
        /// Merges the changes there are in the <paramref name="currentItems" /> enumerable to the ones in
        /// the <paramref name="formerItems" />, applying a certain logic when there are additions made
        /// as defined in the <paramref name="addAction" />, updates as defined in <paramref name="updateAction" /> or
        /// deletions as defined in <paramref name="deleteAction"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the items being updated.</typeparam>
        /// <param name="formerItems">The status in which there were the items before changes took place.</param>
        /// <param name="currentItems">The items containing the changes to make to the <paramref name="formerItems" />.</param>
        /// <param name="addAction">The action to apply to the added items.</param>
        /// <param name="updateAction">The action to apply to the changed items when they were updated</param>
        /// <param name="deleteAction">The action to apply to the items when they were deleted.</param>
        void Merge<TEntity>(IEnumerable<TEntity> formerItems, IEnumerable<TEntity> currentItems, Action<TEntity> addAction, Action<TEntity> updateAction, Action<TEntity> deleteAction)
            where TEntity : IEntity;
    }
}
