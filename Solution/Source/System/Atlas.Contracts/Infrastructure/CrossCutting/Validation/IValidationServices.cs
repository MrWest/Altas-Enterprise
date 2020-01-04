using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation
{
    /// <summary>
    /// Defines the contract for services used to validate domain entities.
    /// </summary>
    public interface IValidationServices
    {
        /// <summary>
        /// Validates an entity.
        /// </summary>
        /// <param name="entity">The entity to validate.</param>
        /// <returns>
        /// An enumerable of strings with errors found in the validation of <paramref name="entity"/>; or en empty enumerable if no
        /// error was found.
        /// </returns>
        IEnumerable<string> Validate(object entity);
    }


    /// <summary>
    /// Defines the contract for services used to validate domain entities.
    /// </summary>
    /// <typeparam name="T">The type of the entities to validate.</typeparam>
    public interface IValidationServices<T> : IValidationServices where T : IEntity
    {
        /// <summary>
        /// Validates an entity.
        /// </summary>
        /// <param name="entity">The entity to validate.</param>
        /// <returns>
        /// An enumerable of strings with errors found in the validation of <paramref name="entity"/>; or en empty enumerable if no
        /// error was found.
        /// </returns>
        IEnumerable<string> Validate(T entity);
    }
}
