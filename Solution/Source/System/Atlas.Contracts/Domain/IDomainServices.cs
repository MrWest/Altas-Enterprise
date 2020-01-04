using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Domain
{
    /// <summary>
    /// Describes the contract of a domain service. A service used to enforce the business rules for a certain
    /// operation where a set of entities of some type are involved..
    /// </summary>
    /// <typeparam name="T">The type of the entities handled by this service.</typeparam>
    public interface IDomainServices<T> where T : IEntity
    {
        /// <summary>
        /// Creates a new instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>A new instance of type <typeparamref name="T"/>.</returns>
        T Create();

        /// <summary>
        /// Determines whether the given item can be added.
        /// </summary>
        /// <param name="item">The item to determine whether it can be added or not.</param>
        /// <returns>True if the item given at <paramref name="item"/> can be added; false otherwise.</returns>
        bool CanAdd(T item);

        /// <summary>
        /// Determines whether there is allowed to add a new item.
        /// </summary>
        /// <returns>True if there is allowed to add a new item.</returns>
        bool CanAdd();

        /// <summary>
        /// Determines whether the given item can be deleted.
        /// </summary>
        /// <param name="item">The item to determine whether it can be deleted or not.</param>
        /// <returns>True if <paramref name="item"/> can be deleted; false otherwise.</returns>
        bool CanDelete(T item);

        /// <summary>
        /// Determines whether there can be updated the given item.
        /// </summary>
        /// <param name="item">The item of type <typeparamref name="T"/> to determine whether it can be updated or not.</param>
        /// <returns>True if the item can be updated; false otherwise.</returns>
        bool CanUpdate(T item);

        /// <summary>
        /// Validates the given item returning the validation results.
        /// </summary>
        /// <param name="item">The item to validate.</param>
        /// <returns>
        /// An enumerable of validation entries represented by objects where each object represents the content of an error.
        /// If the enumerable is empty it means that <paramref name="item"/> is valid.
        /// </returns>
        IEnumerable<string> Validate(T item);
    }
}
