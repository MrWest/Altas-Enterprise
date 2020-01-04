using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace CompanyName.Atlas.Contracts.Implementation.Domain
{
    /// <summary>
    ///     Extends the <see cref="System.Linq.Expressions.Expression{T}" />.
    /// </summary>
    public static class ExpressionBuilder
    {
        /// <summary>
        ///     Composes two expression from the given two, merging them by using the given function.
        /// </summary>
        /// <typeparam name="T">The type of the delegate describing the expressions to merge.</typeparam>
        /// <param name="first">The first <see cref="System.Linq.Expressions.Expression{T}" /> to merge.</param>
        /// <param name="second">The second <see cref="System.Linq.Expressions.Expression{T}" /> to merge.</param>
        /// <param name="merge">The <see cref="System.Func{T,T,T}" /> to use when merging the two expressions.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="first" />, <paramref name="second" /> or
        ///     <paramref name="merge" /> is null.
        /// </exception>
        /// <returns>
        ///     A new <see cref="Expression{T}" /> which is the result of merging the two given
        ///     expressions.
        /// </returns>
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");
            if (merge == null) throw new ArgumentNullException("merge");

            Dictionary<ParameterExpression, ParameterExpression> map = first.Parameters.Select((f, i) => new
            {
                f,
                s = second.Parameters[i]
            }).ToDictionary(p => p.s, p => p.f);

            Expression secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        /// <summary>
        ///     Merges two expressions using And operator.
        /// </summary>
        /// <typeparam name="T">The type of the delegate describing the expressions to merge.</typeparam>
        /// <param name="first">The first <see cref="Expression{T}" /> to merge.</param>
        /// <param name="second">The second <see cref="Expression{T}" /> to merge.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="first" /> or <paramref name="second" /> is null.
        /// </exception>
        /// <returns>
        ///     A new <see cref="Expression{T}" /> which is the result of merging the two given
        ///     expressions.
        /// </returns>
        public static Expression<T> And<T>(this Expression<T> first, Expression<T> second)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");

            return first.Compose(second, Expression.And);
        }

        /// <summary>
        ///     Merges two expressions using Or operator.
        /// </summary>
        /// <typeparam name="T">The type of the delegate describing the expressions to merge.</typeparam>
        /// <param name="first">The first <see cref="Expression{T}" /> to merge.</param>
        /// <param name="second">The second <see cref="Expression{T}" /> to merge.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="first" /> or <paramref name="second" /> is null.
        /// </exception>
        /// <returns>
        ///     A new <see cref="Expression{T}" /> which is the result of merging the two given
        ///     expressions.
        /// </returns>
        public static Expression<T> Or<T>(this Expression<T> first, Expression<T> second)
        {
            if (first == null) throw new ArgumentNullException("first");
            if (second == null) throw new ArgumentNullException("second");

            return first.Compose(second, Expression.Or);
        }

        /// <summary>
        ///     Gets an expression which when evaluated has the result of negating (!) the given one.
        /// </summary>
        /// <typeparam name="T">The type of the delegate describing the expressions to merge.</typeparam>
        /// <param name="expression">The expression <see cref="Expression{T}" /> to negate.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="expression" /> is null.
        /// </exception>
        /// <returns>
        ///     A new <see cref="Expression{T}" /> which is the result of negating the given expression.
        /// </returns>
        public static Expression<T> Not<T>(this Expression<T> expression)
        {
            if (expression == null) throw new ArgumentNullException("expression");

            return Expression.Lambda<T>(Expression.Not(expression.Body), expression.Parameters);
        }


        /// <summary>
        ///     Represents a visitor which rewrites the parameters list of expressions.
        /// </summary>
        [ExcludeFromCodeCoverage]
        class ParameterRebinder : ExpressionVisitor
        {
            readonly Dictionary<ParameterExpression, ParameterExpression> _map;


            /// <summary>
            ///     Initializes a new instance of <see cref="ExpressionBuilder.ParameterRebinder" /> given a
            ///     parameters dictionary.
            /// </summary>
            /// <param name="map">
            ///     An instance of <see cref="Dictionary{TKey,TValue}" /> containing the
            ///     parameters replacing specifications where keys represents the parameters to replace and the values the parameters
            ///     to be
            ///     their replacements.
            /// </param>
            ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }


            /// <summary>
            ///     Replaces the parameters for an expressions using replacements specifications.
            /// </summary>
            /// <param name="map">
            ///     An instance of <see cref="Dictionary{TKey,TValue}" /> containing the
            ///     parameters replacing specifications where keys represents the parameters to replace and the values the parameters
            ///     to be
            ///     their replacements.
            /// </param>
            /// <param name="exp">The <see cref="Expression" /> to replace its parameters.</param>
            /// <returns>
            ///     A new <see cref="Expression" /> containing the body of the given one and the
            ///     parameters already replaced by those containing as values of the given dictionary.
            /// </returns>
            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            /// <summary>
            ///     Visits the expression and returns a new parameter expression in case the given one is to be replaced according to
            ///     the replacement specifications already defined.
            /// </summary>
            /// <returns>
            ///     A modified <see cref="ParameterExpression" />, if it or any subexpression was modified or
            ///     is to be replaced; otherwise, returns the original expression.
            /// </returns>
            /// <param name="node">The expression to visit.</param>
            protected override Expression VisitParameter(ParameterExpression node)
            {
                ParameterExpression replacement;
                if (_map.TryGetValue(node, out replacement)) node = replacement;

                return base.VisitParameter(node);
            }
        }
    }
}