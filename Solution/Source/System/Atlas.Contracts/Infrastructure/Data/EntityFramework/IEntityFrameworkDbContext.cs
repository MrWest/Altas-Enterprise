using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Domain.Specification.EntityFramework;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using Db4objects.Db4o;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework
{
    public interface IEntityFrameworkDbContext<T> : IDatabaseContext
        where T: EntityBase
    {

        DbSet<T> Entities { get; set; }
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
        void Add(T entity);

        /// <summary>
        /// Deletes the given entity from the data source.
        /// </summary>
        /// <typeparam name="T">The type of the entity to delete.</typeparam>
        /// <param name="entity">The entity to delete.</param>
        void Delete(T entity);

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
        void Update(T item);

        /// <summary>
        /// Finds the entity which id is the given one.
        /// </summary>
        /// <typeparam name="T">The type of the entity to find.</typeparam>
        /// <param name="id">The identifier of the entity to retrieve.</param>
        /// <returns>The entity containing such identifier; null if no entity has it.</returns>
        T Find(object id);

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
        T Find(ISpecification<T> specification);

        /// <summary>
        /// Iterates over the whole set of elements of type <typeparamref name="T"/> selecting those which evaluation of a specification returns true.
        /// </summary>
        /// <typeparam name="T">The type of the entities to find.</typeparam>
        /// <param name="specification">The specification to evaluate on each element.</param>
        /// <returns>A enumerable of elements of <typeparamref name="T"/> which the evaluation of <paramref name="specification"/> returns true.</returns>
        IEnumerable<T> Where(IQueryable<T> queryable );

        // IEnumerable<T> Where(string sql, object[] parameters);

        // IEnumerable<object> GetSorted(Comparison<object> comparison);
        IEnumerable<T> Where(IQueryable<T> queryable, string Parameter);

        IEnumerable<T> RoughSQL(string sql);
        string DbConnectionString { get; }
    }
}