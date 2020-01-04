using System;
using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Validation;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
    /// <summary>
    /// This is the domain services base class. Derivers of this class will attempt to ensure the business rules for
    /// the entities this domain services uses.
    /// </summary>
    /// <typeparam name="T">The type of the entities used by the current domain service.</typeparam>
    public abstract class DomainServicesBase<T> : IDomainServices<T> where T : IEntity
    {
        /// <summary>
        /// Gets the validator used to validate the entities in the current domain services.
        /// </summary>
        protected IValidator<T> Validator
        {
            get
            {
                var factory = ServiceLocator.Current.GetInstance<IValidatorFactory>();
                return factory.CreateValidator<T>();
            }
        }


        /// <summary>
        /// Creates a new instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>A new instance of type <typeparamref name="T"/>.</returns>
        public virtual T Create()
        {
            return ServiceLocator.Current.GetInstance<T>();
        }

        /// <summary>
        /// Determines whether the given item can be added.
        /// </summary>
        /// <param name="item">The item to determine whether it can be added or not.</param>
        /// <returns>True if the item given at <paramref name="item"/> can be added; false otherwise.</returns>
        public virtual bool CanAdd(T item)
        {
            return true;
        }

        /// <summary>
        /// Determines whether there is allowed to add a new item.
        /// </summary>
        /// <returns>Returns true.</returns>
        public virtual bool CanAdd()
        {
            return true;
        }

        /// <summary>
        /// Determines whether the given item can be deleted.
        /// </summary>
        /// <param name="item">The item to determine whether it can be deleted or not.</param>
        /// <returns>True if <paramref name="item"/> can be deleted; false otherwise.</returns>
        public virtual bool CanDelete(T item)
        {
            return !Equals(item, null);
        }

        /// <summary>
        /// Determines whether the given item can be updated.
        /// </summary>
        /// <param name="item">The item to determine whether it can be updated or not.</param>
        /// <returns>True if the item is not null; false otherwise.</returns>
        public virtual bool CanUpdate(T item)
        {
            return !Equals(item, null);
        }

        /// <summary>
        /// Validates the given item returning the validation results.
        /// </summary>
        /// <param name="item">The item to validate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is null.</exception>
        /// <returns>
        /// An enumerable of validation entries represented by objects where each object represents the content of an error.
        /// If the enumerable is empty it means that <paramref name="item"/> is valid.
        /// </returns>
        public virtual IEnumerable<string> Validate(T item)
        {
            if (Equals(item, null))
                throw new ArgumentNullException("item");

            return Validator.Validate(item);
        }
    }
}
