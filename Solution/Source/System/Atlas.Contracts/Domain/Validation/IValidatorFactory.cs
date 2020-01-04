using System;

namespace CompanyName.Atlas.Contracts.Domain.Validation
{
    /// <summary>
    /// This is the contract of a factory of entities validators.
    /// </summary>
    public interface IValidatorFactory
    {
        /// <summary>
        /// Creates a validator to check for errors in entities.
        /// </summary>
        /// <typeparam name="T">The type of the entities to validate by the returned validator.</typeparam>
        /// <returns>An instance implementing IValidator{T} allowing to validate entities of <typeparamref name="T"/> type.</returns>
        IValidator<T> CreateValidator<T>();

        /// <summary>
        /// Creates a validator to check for errors in entities.
        /// </summary>
        /// <param name="type">The type of the object to validate.</param>
        /// <returns>An instance implementing <see cref="IValidator"/> allowing to validate entities.</returns>
        IValidator CreateValidator(Type type);
    }
}
