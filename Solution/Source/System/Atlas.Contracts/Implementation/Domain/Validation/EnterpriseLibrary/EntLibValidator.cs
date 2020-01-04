using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using CompanyName.Atlas.Contracts.Domain.Validation;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Validation.EnterpriseLibrary
{
    /// <summary>
    /// Represents an adapter that ties the Validator interface with an implementation base of an Enterprise Library Validation Application Block's Validator.
    /// </summary>
    public class EntLibValidator : IValidator
    {
        private readonly Validator _validator;


        /// <summary>
        /// Initializes a new insance of an adapter of the Validator interface with an implementation base of an Enterprise Library Validation Application
        /// Block's Validator.
        /// </summary>
        /// <param name="validator">The Enterprise Library Validation Application Block's Validator to adapt.</param>
        /// <exception cref="ArgumentNullException"><paramref name="validator"/> is null.</exception>
        public EntLibValidator(Validator validator)
        {
            if (validator == null)
                throw new ArgumentNullException("validator");
            _validator = validator;
        }


        /// <summary>
        /// Validates the given entity.
        /// </summary>
        /// <param name="entity">The entity to validate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        /// <returns>
        /// A enumerable of strings representing the descriptions if there found errors; a null or empty enumerable in case <paramref name="entity"/> is valid.
        /// </returns>
        public IEnumerable<string> Validate(object entity)
        {
            if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            return _validator.Validate(entity).Aggregate(new List<string>(), (list, vr) =>
            {
                list.Add(vr.Message);
                return list;
            }).ToArray();
        }
    }


    /// <summary>
    /// Represents an adapter that ties the Validator{T} interface with an implementation base of an Enterprise Library Validation Application Block's Validator.
    /// </summary>
    /// <typeparam name="T">The type of the entities to validate.</typeparam>
    public class EntLibValidator<T> : EntLibValidator, IValidator<T>
    {
        /// <summary>
        /// Initializes a new insance of an adapter of the Validator{T} interface with an implementation base of an Enterprise Library Validation Application
        /// Block's Validator.
        /// </summary>
        /// <param name="validator">The Enterprise Library Validation Application Block's Validator to adapt.</param>
        /// <exception cref="ArgumentNullException"><paramref name="validator"/> is null.</exception>
        public EntLibValidator(Validator<T> validator)
            : base(validator)
        {
        }


        /// <summary>
        /// Validates the given entity.
        /// </summary>
        /// <param name="entity">The entity of type <typeparamref name="T"/> to validate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        /// <returns>
        /// A enumerable of strings representing the descriptions if there found errors; a null or empty enumerable in case <paramref name="entity"/> is valid.
        /// </returns>
        public IEnumerable<string> Validate(T entity)
        {
            return Validate((object)entity);
        }
    }
}
