using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Domain.Specification.EntityFramework;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data
{
    /// <summary>
    ///     Contract specified for the entities repositories. A repository is the abstraction (in the form of a collection) of
    ///     the universe of entities of a certain time, all extracted from a data source.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        ///     Gets all the entities from the data source.
        /// </summary>
        IEnumerable<IEntity> Entities { get; }


        /// <summary>
        ///     Adds a new entity to the data source.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The added entity.</returns>
        IEntity Add(IEntity entity);

        /// <summary>
        ///     Deletes the given entity from the data source.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(IEntity entity);

        /// <summary>
        ///     Deletes all the entities from the data source.
        /// </summary>
        void DeleteAll();

        /// <summary>
        ///     Updates the changes made to the given item in its corresponding item in the currently opened transaction.
        /// </summary>
        /// <param name="item">
        ///     The item to use its changes and apply them to its current transaction corresponding one.
        /// </param>
        void Update(IEntity item);

        /// <summary>
        ///     Finds the item with the given identifier.
        /// </summary>
        /// <param name="id">The identifier to the find item having it.</param>
        /// <returns>An object of type <see cref="IEntity" /> being the item having the identifier; null if not found any</returns>
        IEntity Find(object id);

        /// <summary>
        ///     Gets all the items witch identifiers are the provided ones.
        /// </summary>
        /// <param name="ids">The <see cref="Array" /> identifiers to find the entities matching them.</param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}" /> containing all the found entities matching any of the given
        ///     <paramref name="ids" />;
        /// </returns>
        IEnumerable<IEntity> FindById(params object[] ids);

        /// <summary>
        ///     Finds the item matching the given specification.
        /// </summary>
        /// <param name="specification">
        ///     The <see cref="ISpecification{TEntity}" /> to evaluate on each item of type
        ///     <see cref="IEntity" /> to select the desired one.
        /// </param>
        /// <returns>
        ///     An item of type <see cref="IEntity" /> being the single item matching <paramref name="specification" />; or null if
        ///     the specification did not evaluate to any item.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        ///     Cannot return an single item matching the specification, because more than one matches it.
        /// </exception>
        IEntity Find(ISpecification<IEntity> specification);

        /// <summary>
        ///     Iterates over the whole set of elements of type <see cref="IEntity"/> selecting those which evaluation of a
        ///     specification returns true.
        /// </summary>
        /// <param name="specification">The specification to evaluate on each element.</param>
        /// <returns>
        ///     A enumerable of elements of <see cref="IEntity"/> which the evaluation of
        ///     <paramref name="specification" /> returns true.
        /// </returns>
        IEnumerable<IEntity> Where(ISpecification<IEntity> specification);

        /// <summary>
        ///     Gets all the entities sorted as the defined comparison defines.
        /// </summary>
        /// <param name="comparison">The <see cref="System.Comparison{T}" /> defining the sorting criteria.</param>
        /// <returns>All entities of type  <see cref="IEntity"/> sorted descending.</returns>
        IEnumerable<IEntity> GetSorted(Comparison<IEntity> comparison);

        /// <summary>
        ///     Merges the changes there are in the <paramref name="currentItems" /> enumerable to the ones in
        ///     the <paramref name="formerItems" />.
        /// </summary>
        /// <param name="formerItems">The status in which there were the items before changes took place.</param>
        /// <param name="currentItems">The items containing the changes to make to the <paramref name="formerItems" />.</param>
        void Merge(IEnumerable<IEntity> formerItems, IEnumerable<IEntity> currentItems);
    }


    /// <summary>
    ///     Contract specified for the entities repositories. A repository is the abstraction (in the form of a collection) of
    ///     the universe of entities of a certain time, all extracted from a data source.
    /// </summary>
    /// <typeparam name="T">The type of the entities managed by this repository.</typeparam>
    public interface IRepository<T> : IRepository where T : IEntity
    {
        /// <summary>
        ///     Gets all the entities from the data source.
        /// </summary>
        new IEnumerable<T> Entities { get; }


        /// <summary>
        ///     Adds a new entity to the data source.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The added entity.</returns>
        T Add(T entity);

        /// <summary>
        ///     Deletes the given entity from the data source.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T entity);

        /// <summary>
        ///     Updates the changes made to the given item in its corresponding item in the currently opened transaction.
        /// </summary>
        /// <param name="item">
        ///     The item to use its changes and apply them to its current transaction corresponding one.
        /// </param>
        void Update(T item);

        /// <summary>
        ///     Finds the item with the given identifier.
        /// </summary>
        /// <param name="id">The identifier to the find item having it.</param>
        /// <returns>An object of type <typeparamref name="T" /> being the item having the identifier; null if not found any</returns>
        new T Find(object id);

        /// <summary>
        ///     Gets all the items witch identifiers are the provided ones.
        /// </summary>
        /// <param name="ids">The <see cref="Array" /> identifiers to find the entities matching them.</param>
        /// <returns>
        ///     A <see cref="IEnumerable{T}" /> containing all the found entities matching any of the given
        ///     <paramref name="ids" />;
        /// </returns>
        new IEnumerable<T> FindById(params object[] ids);

        /// <summary>
        ///     Finds the item matching the given specification.
        /// </summary>
        /// <param name="specification">
        ///     The specfication to evaluate on each item of type <typeparamref name="T" /> to select the
        ///     desired one.
        /// </param>
        /// <returns>
        ///     An item of type <typeparamref name="T" /> being the single item matching <paramref name="specification" />; or null
        ///     if the specification
        ///     did not evaluate to any item.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        ///     Cannot return an single item matching the specification, because more than one matches it.
        /// </exception>
        T Find(ISpecification<T> specification);

        /// <summary>
        ///     Iterates over the whole set of elements of type <typeparamref name="T" /> selecting those which evaluation of a
        ///     specification returns true.
        /// </summary>
        /// <param name="specification">The specification to evaluate on each element.</param>
        /// <returns>
        ///     A enumerable of elements of <typeparamref name="T" /> which the evaluation of
        ///     <paramref name="specification" /> returns true.
        /// </returns>
        IEnumerable<T> Where(ISpecification<T> specification);

        IEnumerable<T> Where(Func<T, bool> condition);


        /// <summary>
        ///     Gets all the entities sorted as the defined comparison defines.
        /// </summary>
        /// <param name="comparison">The <see cref="System.Comparison{T}" /> defining the sorting criteria.</param>
        /// <returns>All entities of type <typeparamref name="T" /> sorted descending.</returns>
        IEnumerable<T> GetSorted(Comparison<T> comparison);

        /// <summary>
        ///     Merges the changes there are in the <paramref name="currentItems" /> enumerable to the ones in
        ///     the <paramref name="formerItems" />.
        /// </summary>
        /// <param name="formerItems">The status in which there were the items before changes took place.</param>
        /// <param name="currentItems">The items containing the changes to make to the <paramref name="formerItems" />.</param>
        void Merge(IEnumerable<T> formerItems, IEnumerable<T> currentItems);

        T GetClone(T entity);
    }
}