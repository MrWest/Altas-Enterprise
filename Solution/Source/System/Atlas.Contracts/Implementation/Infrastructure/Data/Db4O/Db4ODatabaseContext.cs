using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using Db4objects.Db4o;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Query;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O
{
    /// <summary>
    /// This is the implementation of a database context based in DB4O database context technology.
    /// </summary>
    public class Db4ODatabaseContext : IDb4ODatabaseContext
    {
        private readonly IObjectContainer _database;


        /// <summary>
        /// Initializes a new instance of a database context to handle a DB4O database. //old: AppDomain.CurrentDomain.BaseDirectory
        /// </summary>
        public Db4ODatabaseContext()
            : this(Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\Atlas Enterprise\\Atlas Suite\\Db4o", "db.adb"))
        {
        }

        /// <summary>
        /// Initializes a new instance of a database context to handle a DB4O database with a path.
        /// </summary>
        /// <param name="path">The path to the DB4O database file.</param>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
        public Db4ODatabaseContext(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");
            //if (ServiceLocator.IsLocationProviderSet)
            //{
            //    var dataBasePathContainer = (DataBasePathContainer)ServiceLocator.Current.GetInstance(typeof(DataBasePathContainer));

            //    if (dataBasePathContainer != null && (File.Exists(dataBasePathContainer.DataBasePath)))
            //    {
            //        _database = Db4oEmbedded.OpenFile(dataBasePathContainer.DataBasePath);
            //    }
            //    else
            //        _database = Db4oEmbedded.OpenFile(path);
            //}
            //else
            CheckCreateDirectory(path);
            _database = Db4oEmbedded.OpenFile(path);


        }
        private static void CheckCreateDirectory(string path)
        {
            FileInfo fi = new FileInfo(path);
            if (!fi.Directory.Exists)
                Directory.CreateDirectory(fi.DirectoryName);
        }



        #region IDatabaseContext Members

        /// <summary>
        /// Gets a queryable allowing to make queries over all the entities of a certain type.
        /// </summary>
        /// <typeparam name="T">The type of the entities to query.</typeparam>
        /// <returns>A queryable containing all the entities, over which queries can be performed.</returns>
        public IQueryable<T> GetAll<T>()
        {
            return _database.Query<T>().AsQueryable();
        }

        /// <summary>
        /// Generates a new primary key.
        /// </summary>
        /// <returns>A new fresh key.</returns>
        public virtual string GenerateKey()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Adds an entity to the database.
        /// </summary>
        /// <typeparam name="T">The type of the entity to add.</typeparam>
        /// <param name="entity">The entity to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        /// <returns>The new added entity.</returns>
        public void Add<T>(T entity) where T : IEntity
        {
            if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            entity.Id = GenerateKey();
            _database.Store(entity);
        }

        /// <summary>
        /// Deletes the given entity from the data source.
        /// </summary>
        /// <typeparam name="T">The type of the entity to delete.</typeparam>
        /// <param name="entity">The entity to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public void Delete<T>(T entity) where T : IEntity
        {
            if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            var dbEntity = _database.Query<T>(e => Equals(e.Id, entity.Id)).SingleOrDefault();
            if (!Equals(dbEntity, null))
                _database.Delete(dbEntity);
        }

        /// <summary>
        /// Saves the changes made to the data base.
        /// </summary>
        public void Save()
        {
            Commit();
        }

        /// <summary>
        /// Drops the changes made to the data base and cancels them all.
        /// </summary>
        public void DropChanges()
        {
            Rollback();
        }

        /// <summary>
        /// Updates the changes made to the given entity in its corresponding entity in the currently opened transaction.
        /// </summary>
        /// <param name="entity">
        /// The entity to use its changes and apply them to its current transaction corresponding one.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public void Update<T>(T entity) where T : IEntity
        {
            if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            _database.Store(entity);
        }

        /// <summary>
        /// Finds the entity which id is the given one.
        /// </summary>
        /// <param name="id">The identifier of the entity to retrieve.</param>
        /// <returns>The entity containing such identifier; null if no entity has it.</returns>
        public T Find<T>(object id) where T : IEntity
        {
            //TODO dont need this
            for (int e =_database.Query<T>(x => Equals(x.Id, id)).Count; e > 1;e--)
            {
               _database.Delete(_database.Query<T>(x => Equals(x.Id, id))[e-1]); 
            }
                

            var shit = _database.Query<T>(x => Equals(x.Id, id));
            
                    return _database.Query<T>(x => Equals(x.Id, id)).SingleOrDefault();
           
            
        }

        /// <summary>
        /// Finds the item matching the given specification.
        /// </summary>
        /// <typeparam name="T">The type of the entity to find.</typeparam>
        /// <param name="specification">The specification to evaluate on each item to select the desired one.</param>
        /// <exception cref="ArgumentNullException"><paramref name="specification"/> is null.</exception>
        /// <returns>
        /// An item of type <typeparamref name="T"/> being the single item matching <paramref name="specification"/>; or null if the specification
        /// did not evaluate to any item.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Cannot return an single item matching the specification, because more than one matches it.
        /// </exception>
        public T Find<T>(ISpecification<T> specification) where T : IEntity
        {
            if (specification == null)
                throw new ArgumentNullException("specification");
            //TODO dont need this
            for (int e = _database.Query(specification.Predicate.Compile()).Count; e > 1; e--)
            {
                _database.Delete(_database.Query(specification.Predicate.Compile())[e - 1]);
            }

            return _database.Query(specification.Predicate.Compile()).SingleOrDefault();
        }

        /// <summary>
        /// Iterates over the whole set of elements of type <typeparamref name="T"/> selecting those which evaluation of a specification returns true.
        /// </summary>
        /// <typeparam name="T">The type of the entities to find.</typeparam>
        /// <param name="specification">The specification to evaluate on each element.</param>
        /// <exception cref="ArgumentNullException"><paramref name="specification"/> is null.</exception>
        /// <returns>A enumerable of elements of <typeparamref name="T"/> which the evaluation of <paramref name="specification"/> returns true.</returns>
        public IEnumerable<T> Where<T>(ISpecification<T> specification) where T : IEntity
        {
            if (specification == null)
                throw new ArgumentNullException("specification");

            return _database.Query(specification.Predicate.Compile());
        }

        /// <summary>
        /// Gets all the entities sorted as the defined comparison defines.
        /// </summary>
        /// <typeparam name="T">The type of the entities to retrieve.</typeparam>
        /// <param name="comparison">The <see cref="System.Comparison{T}"/> defining the sorting criteria.</param>
        /// <exception cref="ArgumentNullException"><paramref name="comparison"/> is null.</exception>
        /// <returns>All entities of type <typeparamref name="T"/> sorted descending.</returns>
        public IEnumerable<T> GetSorted<T>(Comparison<T> comparison) where T : IEntity
        {
            if (comparison == null)
                throw new ArgumentNullException("comparison");

            return _database.Query(x => true, comparison);
        }

        /// <summary>
        /// Merges the changes there are in the <paramref name="currentItems" /> enumerable to the ones in
        /// the <paramref name="formerItems" />, applying a certain logic when there are additions made
        /// as defined in the <paramref name="addAction" />, updates as defined in <paramref name="updateAction" /> or
        /// deletions as defined in <paramref name="deleteAction"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the items being updated.</typeparam>
        /// <param name="formerItems">The status in which there were the items before changes took place.</param>
        /// <param name="currentItems">The items containing the changes to make to the <paramref name="formerItems" />.</param>
        /// <param name="addAction">The action to apply to the added items.</param>
        /// <param name="updateAction">The action to apply to the changed items when they were updated</param>
        /// <param name="deleteAction">The action to apply to the items when they were deleted.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="formerItems" />, <paramref name="currentItems" />, <paramref name="updateAction" />,
        /// <paramref name="addAction" /> or <paramref name="deleteAction"/> is null.
        /// </exception>
        public void Merge<TEntity>(IEnumerable<TEntity> formerItems, IEnumerable<TEntity> currentItems, Action<TEntity> addAction, Action<TEntity> updateAction, Action<TEntity> deleteAction)
            where TEntity : IEntity
        {
            if (formerItems == null)
                throw new ArgumentNullException("formerItems");
            if (currentItems == null)
                throw new ArgumentNullException("currentItems");
            if (addAction == null)
                throw new ArgumentNullException("addAction");
            if (updateAction == null)
                throw new ArgumentNullException("updateAction");
            if (deleteAction == null)
                throw new ArgumentNullException("deleteAction");

            this.MergeLists(formerItems, currentItems, addAction, updateAction, deleteAction);
        }

        #endregion


        #region IObjectContainer members

        public void Activate(object obj, int depth)
        {
            _database.Activate(obj, depth);
        }

        public bool Close()
        {
            return _database.Close();
        }

        public void Commit()
        {
            _database.Commit();
        }

        public void Deactivate(object obj, int depth)
        {
            _database.Deactivate(obj, depth);
        }

        public void Delete(object obj)
        {
            _database.Delete(obj);
        }

        public IExtObjectContainer Ext()
        {
            return _database.Ext();
        }

        public IList<Extent> Query<Extent>(IComparer<Extent> comparer)
        {
            return _database.Query(comparer);
        }

        public IList<Extent> Query<Extent>()
        {
            return _database.Query<Extent>();
        }

        public IList<ElementType> Query<ElementType>(Type extent)
        {
            return _database.Query<ElementType>(extent);
        }

        public IList<Extent> Query<Extent>(Predicate<Extent> match, Comparison<Extent> comparison)
        {
            return _database.Query(match, comparison);
        }

        public IList<Extent> Query<Extent>(Predicate<Extent> match, IComparer<Extent> comparer)
        {
            return _database.Query(match, comparer);
        }

        public IList<Extent> Query<Extent>(Predicate<Extent> match)
        {
            return _database.Query(match);
        }

        public IObjectSet Query(Predicate predicate, IComparer comparer)
        {
            return _database.Query(predicate, comparer);
        }

        public void Rollback()
        {
            _database.Rollback();
        }

        public IObjectSet Query(Predicate predicate, IQueryComparator comparator)
        {
            return _database.Query(predicate, comparator);
        }

        public IObjectSet Query(Predicate predicate)
        {
            return _database.Query(predicate);
        }

        public IObjectSet Query(Type clazz)
        {
            return _database.Query(clazz);
        }

        public IQuery Query()
        {
            return _database.Query();
        }

        public IObjectSet QueryByExample(object template)
        {
            return _database.QueryByExample(template);
        }

        public void Store(object obj)
        {
            _database.Store(obj);
        }

        #endregion


        #region IDisposable Members

        /// <summary>
        /// Performs operations to release the resources of the current database context.
        /// </summary>
        public void Dispose()
        {
            _database.Dispose();
            Close();
        }

        #endregion
    }
}
