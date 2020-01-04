using System.Collections.Generic;

namespace CompanyName.Atlas.Contracts.Domain.Validation
{
    /// <summary>
    /// Describes the contract of a validator.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Validates the given entity.
        /// </summary>
        /// <param name="entity">The entity to validate.</param>
        /// <returns>
        /// A enumerable of strings representing the descriptions if there found errors; a null or empty enumerable in case <paramref name="entity"/> is valid.
        /// </returns>
        IEnumerable<string> Validate(object entity);
    }


    /// <summary>
    /// Describes the contract of a validator.
    /// </summary>
    /// <typeparam name="T">The type of the entity to validate.</typeparam>
    public interface IValidator<in T> : IValidator
    {
        /// <summary>
        /// Validates the given entity.
        /// </summary>
        /// <param name="entity">The entity of type <typeparamref name="T"/> to validate.</param>
        /// <returns>
        /// A enumerable of strings representing the descriptions if there found errors; a null or empty enumerable in case <paramref name="entity"/> is valid.
        /// </returns>
        IEnumerable<string> Validate(T entity);
    }
}
