using System;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Specifications
{
    /// <summary>
    /// Specification containing the criteria allowing to select all entities which identifier is contained in a specified set.
    /// </summary>
    /// <typeparam name="T">The type of the entity to select.</typeparam>
    public class EntitiesByIdSpecification<T> : Specification<T>
        where T : IEntity
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EntitiesByIdSpecification{T}"/> given the identifiers.
        /// </summary>
        /// <param name="ids">A <see cref="Array"/> containing the identifiers of the wanted entities.</param>
        public EntitiesByIdSpecification(params object[] ids)
        {
            if (ids == null)
                throw new ArgumentNullException("ids");

            Predicate = x => ids.Contains(x.Id);
        }
    }
}