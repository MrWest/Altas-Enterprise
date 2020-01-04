using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O
{
    /// <summary>
    /// Contains some helpers that aid to perform common database operations, like merging to lists of entities.
    /// </summary>
    public static class Db4ODatabaseContextExtensions
    {
        /// <summary>
        /// Merges two given lists of entities.
        /// The ones found in the <paramref name="formerEntities" /> that are also found in
        /// <paramref name="currentEntities" /> are taken as they are being updated, changes from their homologous
        /// copies in the later collection and going to be copied to the homologous copies in former one; NOTE
        /// that there is required, when doing this update operation, to ensure that all foreing keys fields are
        /// set correctly, so provide a delegate that does that.
        /// The other entities that are not in <paramref name="currentEntities" />, but are in
        /// <paramref name="formerEntities" /> are classified as removed entities and so they will be mark for
        /// removal from the database.
        /// And the last case is when found entities <paramref name="currentEntities" /> but not in
        /// <paramref name="formerEntities" />. When found such case, those entities are categorized as new entities
        /// that must be added to the database, and exactly such operation is performed for them.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities to work with.</typeparam>
        /// <param name="context">The database context to make the operations (add, remove, update, etc) with.</param>
        /// <param name="formerEntities">
        /// The original collection of entities. This represents how were the entities involed in the
        /// current operations before the whole process got triggered.
        /// </param>
        /// <param name="currentEntities">
        /// The collection of entities which already suffered changes and such changes must be
        /// reflected in the database.
        /// </param>
        /// <param name="onAddAction">
        /// A delegate to be called passing it the entities being added. Provides a way to customize
        /// something in the entities before they are actually added.
        /// </param>
        /// <param name="onUpdateAction">
        /// A delegate to be called passing it the entities being updated. Provides a way to customize
        /// something in the entities before they are actually updated.
        /// </param>
        /// <param name="onDeleteAction">
        /// A delegate to be called passing it the entities being deleted. Provides a way to customize
        /// something in the entities before they are actually deleted.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="context"/>, <paramref name="formerEntities"/>, <paramref name="currentEntities"/>,
        /// <paramref name="onAddAction"/>, <paramref name="onUpdateAction"/> is null.
        /// </exception>
        public static void MergeLists<TEntity>(this IDb4ODatabaseContext context, IEnumerable<TEntity> formerEntities,
            IEnumerable<TEntity> currentEntities, Action<TEntity> onAddAction, Action<TEntity> onUpdateAction, Action<TEntity> onDeleteAction)
            where TEntity : IEntity
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (formerEntities == null)
                throw new ArgumentNullException("formerEntities");
            if (currentEntities == null)
                throw new ArgumentNullException("currentEntities");
            if (onAddAction == null)
                throw new ArgumentNullException("onAddAction");
            if (onUpdateAction == null)
                throw new ArgumentNullException("onUpdateAction");

            // Build a registry containing which of the old entities were update or removed. The updated entities are
            // detected when there are in both collection a item with the same Id, that is taken as the item being
            // updated. The removed are noticed because they are not contained anymore in the currentEntities collection
            // but they are in the formerEntitites one.
            Dictionary<TEntity, TEntity> dictionary =
                (from entity in formerEntities
                 select new
                 {
                     Key = entity,
                     Value = currentEntities.SingleOrDefault(r =>
                         !Equals(r.Id, null) &&
                         !Equals(entity.Id, null) &&
                         Equals(r.Id, entity.Id))
                 }).ToDictionary(x => x.Key, x => x.Value);

            // Then ensure there are updated the ones being updated and removed the other
            foreach (var entityData in dictionary)
            {
                TEntity formerEntity = entityData.Key;
                TEntity actualEntity = entityData.Value;

                if (actualEntity != null)
                {
                    // If the resource was modified, copy the new values to the current entry
                    onUpdateAction(actualEntity);
                }
                else
                {
                    // Otherwise mark the entity for removal
                    TEntity entity = context.Find<TEntity>(formerEntity.Id);
                    if (entity != null)
                        onDeleteAction(entity);
                }
            }

            // The entities that are in the currenEntities collection that contains Id equals to null, they are
            // recognized as new entities, below they are added to the Context
            foreach (TEntity entity in currentEntities.Where(entity => Equals(entity.Id, null)).ToArray())
                onAddAction(entity);
        }
    }
}