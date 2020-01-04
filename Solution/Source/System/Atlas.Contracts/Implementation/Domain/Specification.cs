using System;
using System.Linq.Expressions;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;

namespace CompanyName.Atlas.Contracts.Implementation.Domain
{
    /// <summary>
    /// Decribes the base class of an specification.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entities to evaluate the condition on.</typeparam>
    public class Specification<TEntity> : ISpecification<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Specification{TEntity}"/> with an always true condition.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="condition" /> is null.</exception>
        public Specification()
        {
            Predicate = x => true;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Specification{TEntity}" /> given an condition.
        /// </summary>
        /// <param name="condition">
        /// A <see cref="Expression{T}" /> representing the expression which when
        /// evaluated on a given entity it states whether that entity satisfies certain condition.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="condition" /> is null.</exception>
        public Specification(Expression<Predicate<TEntity>> condition)
        {
            if (condition == null) throw new ArgumentNullException("condition");

            Predicate = condition;
        }


        /// <summary>
        /// Returns an predicate expression telling whether an entity object may satisfy certain condition.
        /// </summary>
        public Expression<Predicate<TEntity>> Predicate { get; protected set; }


        /// <summary>
        /// Constructs a new specification which is the result of evaluation the two given ones using the And operator (&).
        /// </summary>
        /// <param name="first">The Specification&lt;TEntity&gt; which is the first operand.</param>
        /// <param name="second">The ISpecification&lt;TEntity&gt; which is the second operand.</param>
        /// <returns>A new ISpecification&lt;TEntity&gt; which result is the And (&) operation of the two given specifications.</returns>
        public static Specification<TEntity> operator &(Specification<TEntity> first, ISpecification<TEntity> second)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");

            return new Specification<TEntity>(first.Predicate.And(second.Predicate));
        }

        /// <summary>
        /// Constructs a new specification which is the result of evaluation the two given ones using the Or operator (|).
        /// </summary>
        /// <param name="first">The Specification&lt;TEntity&gt; which is the first operand.</param>
        /// <param name="second">The ISpecification&lt;TEntity&gt; which is the second operand.</param>
        /// <returns>A new ISpecification&lt;TEntity&gt; which result is the Or (|) operation of the two given specifications.</returns>
        public static Specification<TEntity> operator |(Specification<TEntity> first, ISpecification<TEntity> second)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");

            return new Specification<TEntity>(first.Predicate.Or(second.Predicate));
        }

        /// <summary>
        /// Constructs a new specification which is the negation (!) of the given one.
        /// </summary>
        /// <param name="specification">The Specification&lt;TEntity&gt; which is the specification to negate.</param>
        /// <returns>A new Specification&lt;TEntity&gt; which result is the Not (!) operation of the given specification.</returns>
        public static Specification<TEntity> operator !(Specification<TEntity> specification)
        {
            if (specification == null) throw new ArgumentNullException("specification");

            return new Specification<TEntity>(specification.Predicate.Not());
        }
    }
}
