using System;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using CompanyName.Atlas.Contracts.Domain.Validation;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Validation.EnterpriseLibrary
{
    /// <summary>
    /// Implementation of the <see cref="IValidatorFactory"/> adapting such interface to the Enterprise Library's Validation Application Block's implementation.
    /// </summary>
    public class EntLibValidatorFactory : IValidatorFactory
    {
        /// <summary>
        /// Creates a validator to check for errors in entities.
        /// </summary>
        /// <typeparam name="T">The type of the entities to validate by the returned validator.</typeparam>
        /// <returns>An instance implementing IValidator{T} allowing to validate entities of <typeparamref name="T"/> type.</returns>
        public IValidator<T> CreateValidator<T>()
        {
            Validator<T> validator = EnterpriseLibraryContainer.Current.GetInstance<ValidatorFactory>().CreateValidator<T>();

            return new EntLibValidator<T>(validator);
        }

        /// <summary>
        /// Creates a validator to check for errors in entities.
        /// </summary>
        /// <param name="type">The type of the object to validate.</param>
        /// <returns>An instance implementing <see cref="IValidator"/> allowing to validate entities.</returns>
        public IValidator CreateValidator(Type type)
        {
            Validator validator = EnterpriseLibraryContainer.Current.GetInstance<ValidatorFactory>().CreateValidator(type);

            return new EntLibValidator(validator);
        }
    }
}
