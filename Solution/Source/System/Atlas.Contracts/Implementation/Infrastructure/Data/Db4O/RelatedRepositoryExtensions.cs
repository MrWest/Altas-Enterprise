using System;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O
{
    /// <summary>
    /// Base class of the reositories handling entities related to other entities.
    /// </summary>
    public static class RelatedRepositoryExtensions
    {
        /// <summary>
        /// Gets the method that is used to set the reference, from an entity of the current side of the relationship to a one of the
        /// other side and viceversa.
        /// </summary>
        /// <param name="repository">The repository managing the entities.</param>
        /// <param name="current">The entity of the current side of the relationship.</param>
        /// <param name="other">The entity of the other side of the relationship.</param>
        /// <param name="databaseContext">
        /// A <see cref="IDb4ODatabaseContext"/> being the database context which facades the actual database technology where the
        /// data operations will be carried on.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="repository"/>, <paramref name="databaseContext"/>, <paramref name="current"/> or <paramref name="other"/> is null.
        /// </exception>
        public static void Relate<T, TOther, TContext>(this IRelatedRepository<T, TOther> repository, T current, TOther other, TContext databaseContext)
            where T : class, IEntity
            where TOther : class, IEntity 
            where TContext : class, IDatabaseContext
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            ExecuteCommonAction(repository.Relate, repository, current, other, databaseContext);
        }
     
       
        /// <summary>
        /// Gets the method that is used to break the reference, from an entity of the current side of the relationship to a one of the
        /// other side and viceversa.
        /// </summary>
        /// <param name="repository">The repository managing the entities.</param>
        /// <param name="current">The entity of the current side of the relationship.</param>
        /// <param name="other">The entity of the other side of the relationship.</param>
        /// /// <param name="databaseContext">
        /// A <see cref="IDb4ODatabaseContext"/> being the database context which facades the actual database technology where the
        /// data operations will be carried on.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="repository"/>, <paramref name="databaseContext"/>, <paramref name="current"/> or <paramref name="other"/> is null.
        /// </exception>
        public static void Unrelate<T, TOther, TContext>(this IRelatedRepository<T, TOther> repository, T current, TOther other, TContext databaseContext)
            where T : class, IEntity
            where TOther : class, IEntity
            where TContext : class, IDatabaseContext
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            ExecuteCommonAction(repository.Unrelate, repository, current, other, databaseContext);
        }

        private static void ExecuteCommonAction<T, TOther, TContext>(Action<T, TOther> action, IRelatedRepository<T, TOther> repository, T current, TOther other, TContext databaseContext)
            where T : class, IEntity
            where TOther : class, IEntity
            where TContext : class, IDatabaseContext
        {
            
            if (current == null)
                throw new ArgumentNullException("current");
            if (other == null)
                throw new ArgumentNullException("other");
            if (databaseContext == null)
                throw new ArgumentNullException("databaseContext");
            //here:
            var dbCurrent = databaseContext.Find<T>(current.Id);
            var dbOther = databaseContext.Find<TOther>(other.Id);
            if (dbCurrent == null || dbOther == null)
            {
                //if (dbOther == null)
                //{
                //    databaseContext.Add(other);
                //    goto here;
                //}
                throw new InvalidOperationException(Resources.CannotRelateUnrelateNulls);
            }
               
            action(dbCurrent, dbOther);
            action(current, other);

            repository.SaveReference(dbCurrent);
            repository.SaveReference(dbOther);
        }
    }
}
