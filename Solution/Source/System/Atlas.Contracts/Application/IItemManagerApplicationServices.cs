using System;
using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Application
{
    /// <summary>
    /// Describes the contract of an application service (used in the application layer) to manage entities.
    /// </summary>
    /// <typeparam name="T">The type of the entities to manage.</typeparam>
    public interface IItemManagerApplicationServices<T> : IDisposable where T : class, IEntity
    {
        /// <summary>
        /// Gets the entities from the data source.
        /// </summary>
        IEnumerable<T> Items { get; }


        /// <summary>
        /// Adds a new item to the system.
        /// </summary>
        /// <param name="item">The item to add.</param>
        void Add(T item);

        /// <summary>
        /// Deletes the given item from the system.
        /// </summary>
        /// <param name="item">The item to be deleted.</param>
        void Delete(T item);

        /// <summary>
        /// Deletes all the items from the system.
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// Creates a new instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>A new instance of type <typeparamref name="T"/>.</returns>
        T Create();

        /// <summary>
        /// Saves the changes made to the given item.
        /// </summary>
        /// <param name="item">The item to save it changes.</param>
        void Update(T item);

        /// <summary>
        /// Determines whether the given item can be added.
        /// </summary>
        /// <param name="item">The item to determine whether it can be added or not.</param>
        /// <returns>True if the item given at <paramref name="item"/> can be added; false otherwise.</returns>
        bool CanAdd(T item);

        /// <summary>
        /// Determines whether there is allowed to add a new item.
        /// </summary>
        /// <returns>True if there is allowed to add a new item; false otherwise.</returns>
        bool CanAddNew();

        /// <summary>
        /// Determines whether there can be updated the given item.
        /// </summary>
        /// <param name="item">The item of type <typeparamref name="T"/> to determine whether it can be updated or not.</param>
        /// <returns>True if <paramref name="item"/> is not null and can be updated; false otherwise.</returns>
        bool CanUpdate(T item);

        /// <summary>
        /// Determines whether the given item can be deleted.
        /// </summary>
        /// <param name="item">The item to determine whether it can be deleted or not.</param>
        /// <returns>True if <paramref name="item"/> can be deleted; false otherwise.</returns>
        bool CanDelete(T item);

        /// <summary>
        /// Validates the given item returning the validation results.
        /// </summary>
        /// <param name="item">The item to validate.</param>
        /// <returns>
        /// An IEnumerable{T} of validation entries represented by objects where each object represents the content of an error.
        /// If the enumerable is empty it means that <paramref name="item"/> is valid.
        /// </returns>
        IEnumerable<string> Validate(T item);
        /// <summary>
        /// Finds an entity given the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find(object id);
    }
}
