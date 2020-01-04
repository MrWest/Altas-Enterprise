using System;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data
{
    /// <summary>
    /// Base contract for the repositories managing entities involved in a relationship with another.
    /// </summary>
    /// <typeparam name="T">The type of the entities in the side of the relationship managed by the repository.</typeparam>
    /// <typeparam name="TOther">The type of the entity in the side opposed to the one the reository is at.</typeparam>
    public interface IRelatedRepository<T, TOther> : IRepository<T>
        where T : class, IEntity
        where TOther : class, IEntity
    {
        /// <summary>
        /// Gets the method that is used to set the reference, from an entity of the current side of the relationship to a one of the
        /// other side and viceversa.
        /// </summary>
        /// <param name="current">The entity of the current side of the relationship.</param>
        /// <param name="other">The entity of the other side of the relationship.</param>
        void Relate(T current, TOther other);

        /// <summary>
        /// Gets the method that is used to break the reference, from an entity of the current side of the relationship to a one of the
        /// other side and viceversa.
        /// </summary>
        /// <param name="current">The entity of the current side of the relationship.</param>
        /// <param name="other">The entity of the other side of the relationship.</param>
        void Unrelate(T current, TOther other);

        /// <summary>
        /// Saves the references in the entity on the current side of the relationship, reference that points to the other entity on the
        /// opposite side.
        /// </summary>
        /// <param name="current">The entity on the current side of the relationship.</param>
        void SaveReference(T current);

        /// <summary>
        /// Saves the references in the entity on the opposite side of the relationship, reference that points to the entity on the
        /// current side.
        /// </summary>
        /// <param name="other">The entity on the opposite side of the relationship.</param>
        void SaveReference(TOther other);
    }
}
