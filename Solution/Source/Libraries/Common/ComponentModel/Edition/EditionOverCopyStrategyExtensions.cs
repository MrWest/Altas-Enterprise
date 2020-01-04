using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.ComponentModel.Edition
{
    /// <summary>
    ///     Contains extensions to enhance the <see cref="System.ComponentModel.Edition.EditionStrategyBase{T}" />
    ///     type.
    /// </summary>
    public static class EditionOverCopyStrategyExtensions
    {
        /// <summary>
        ///     Merges the collection of entities owned by the object being edited, passing the values from the
        ///     collection of the edited object to the the collection of the original.
        /// </summary>
        /// <typeparam name="T">The type of the object being edited.</typeparam>
        /// <typeparam name="TEntity">The type of the owned entities.</typeparam>
        /// <param name="strategy">
        ///     The strategy which editing object is going to get one its collection of entities merged.
        /// </param>
        /// <param name="original">The object being edited.</param>
        /// <param name="getCollection">
        ///     A function that allows to the get the collection of owned entities given an object of the same type as
        ///     the one being edited.
        /// </param>
        /// <param name="comparer">A comparer to be used when determining whether two owned entities are equal.</param>
        /// <param name="isNew">A function to determine whether an owned entity is new or not.</param>
        /// <param name="setOwner">
        ///     A method that allows an owned entity to set its reference to its owner one.
        /// </param>
        /// <param name="propertiesNames">
        ///     The names of the properties of the owned entities to be set in their homologous is the owner object.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="strategy" />, <paramref name="original" />, <paramref name="getCollection" />,
        /// <paramref name="comparer" />,
        ///     <paramref name="isNew" />, <paramref name="setOwner" />, <paramref name="propertiesNames" />.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        ///     <paramref name="propertiesNames" /> contains null strings.
        /// </exception>
        public static void MergeOwnedCollection<T, TEntity>(this EditionOverCopyStrategy<T> strategy, T original,
            Func<T, ICollection<TEntity>> getCollection, IEqualityComparer<TEntity> comparer, Func<TEntity, bool> isNew,
            Action<TEntity, T> setOwner, string[] propertiesNames) where TEntity : class, new() where T : class, new()
        {
            if (strategy == null) throw new ArgumentNullException("strategy");
            if (original == null) throw new ArgumentNullException("original");
            if (getCollection == null) throw new ArgumentNullException("getCollection");
            if (comparer == null) throw new ArgumentNullException("comparer");
            if (isNew == null) throw new ArgumentNullException("isNew");
            if (setOwner == null) throw new ArgumentNullException("setOwner");
            if (propertiesNames == null) throw new ArgumentNullException("propertiesNames");
            if (propertiesNames.Any(p => p == null))
                throw new ArgumentException(Properties.Resources.CannotContainNullObjects, "propertiesNames");

            ICollection<TEntity> editedCollection = getCollection(strategy.EditingObject);
            ICollection<TEntity> originalCollection = getCollection(strategy.OriginalObject);

            // Remove the entities that were removed from the original object and updates those that still remain
            foreach (TEntity entity in originalCollection.ToArray())
            {
                TEntity edited = editedCollection.SingleOrDefault(p => comparer.Equals(p, entity));
                if (Equals(edited, null)) originalCollection.Remove(entity);
                else edited.UpdateProperties(entity, propertiesNames);
            }

            /* Add to the original the added ones and make sure that the final voucher passes gets their reference
             * to the voucher */
            foreach (TEntity entity in editedCollection.ToArray())
            {
                if (originalCollection.All(p => !comparer.Equals(p, entity)) || isNew(entity))
                    originalCollection.Add(entity);
                setOwner(entity, original);
            }
        }

        /// <summary>
        ///     Puts a copy of a collection of entities contained in an objet of a type in the same collection member
        ///     of another object of the same type, copying all defined properties of the entities in the collection of
        ///     object being the source.
        /// </summary>
        /// <typeparam name="T">The type of the objects having its collections copied from and to.</typeparam>
        /// <typeparam name="TEntity">The type of the entities contained in the collections.</typeparam>
        /// <param name="strategy">
        ///     The edition making the edition of the one of the object using the other as a copy of such object and to
        ///     be the target of the changes made in the edition.
        /// </param>
        /// <param name="original">The object being edited.</param>
        /// <param name="copy">The copy made of the edited object to be the target of the changes.</param>
        /// <param name="originalCollection">The collection in the editing object, which entities will be copied.</param>
        /// <param name="setCollection">A method that sets tothe copy the copied collection of copied entities.</param>
        /// <param name="propertiesNames">The names of the properties of the entities that will be copied.</param>
        public static void CopyOwnedCollection<T, TEntity>(this EditionOverCopyStrategy<T> strategy, T original, T copy,
            IEnumerable<TEntity> originalCollection, Action<T, IEnumerable<TEntity>> setCollection,
            string[] propertiesNames) where TEntity : class, new() where T : class, new()
        {
            if (strategy == null) throw new ArgumentNullException("strategy");
            if (original == null) throw new ArgumentNullException("original");
            if (originalCollection == null) throw new ArgumentNullException("originalCollection");
            if (copy == null) throw new ArgumentNullException("copy");
            if (setCollection == null) throw new ArgumentNullException("setCollection");
            if (propertiesNames == null) throw new ArgumentNullException("propertiesNames");
            if (propertiesNames.Any(p => p == null))
                throw new ArgumentException(Properties.Resources.CannotContainNullObjects, "propertiesNames");

            List<TEntity> copiedCollection = originalCollection.Aggregate(new List<TEntity>(), (list, originalEntity) =>
            {
                Type entityType = originalEntity.GetType();
                var entityCopy = (TEntity)Activator.CreateInstance(entityType);

                ObjectExtensions.UpdateProperties(originalEntity, entityCopy, propertiesNames);

                list.Add(entityCopy);
                return list;
            });

            setCollection(copy, copiedCollection);
        }
    }
}