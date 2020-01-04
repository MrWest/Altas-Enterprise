using System;
using System.Linq.Expressions;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Domain.Specification
{
    /// <summary>
    /// Describes the behavior of an specification.
    /// </summary>
    public interface ISpecification<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Returns an predicate expression telling whether an entity object may satisfy certain condition.
        /// </summary>
        Expression<Predicate<TEntity>> Predicate { get; }
    }
}