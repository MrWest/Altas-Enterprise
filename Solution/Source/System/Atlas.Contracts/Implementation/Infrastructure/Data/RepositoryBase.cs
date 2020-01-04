using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Infrastructure.Data;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data
{
    /// <summary>
    /// Base class of all the repositories with default implementations for its operations regarding of the defined
    /// data technology.
    /// </summary>
    /// <typeparam name="T">The type of the entities managed in the current repository.</typeparam>
    /// <typeparam name="TDatabaseContext">The type of the database context used by the current repository.</typeparam>
    public abstract class RepositoryBase<T, TDatabaseContext> : IRepository<T>
        where T : IEntity
        where TDatabaseContext : IDatabaseContext
    {
        protected readonly TDatabaseContext _databaseContext;


        /// <summary>
        /// Initializes a new instance of a repository to manage entities of type <typeparamref name="T"/> through a
        /// database context.
        /// </summary>
        /// <param name="databaseContext">
        /// The database context used to perform the data operations there are needed to be sent to the database.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="databaseContext"/> is null.</exception>
        protected RepositoryBase(TDatabaseContext databaseContext)
        {
            if (Equals(databaseContext, null))
                throw new ArgumentNullException("databaseContext");

            _databaseContext = (TDatabaseContext)((DataBasePathContainer)ServiceLocator.Current.GetInstance(typeof(DataBasePathContainer))).DatabaseContext;
            if (_databaseContext == null)
                _databaseContext = databaseContext;
        }


        /// <summary>
        /// Gets all the entities from the data source.
        /// </summary>
        public virtual IEnumerable<T> Entities
        {
            get { return Clone(_databaseContext.GetAll<T>()).ToArray(); }
        }

        /// <summary>
        /// Gets all the entities from the data source.
        /// </summary>
        IEnumerable<IEntity> IRepository.Entities
        {
            get { return Entities.Cast<IEntity>().ToArray(); }
        }

        /// <summary>
        /// Gets the database context this repository uses.
        /// </summary>
        protected TDatabaseContext DatabaseContext
        {
            get { return _databaseContext; }
        }


        /// <summary>
        /// Adds a new entity to the data source.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        /// <returns>The added entity.</returns>
        IEntity IRepository.Add(IEntity entity)
        {
            if (entity!=null&&entity.Id!=null)
            {
                var dbEntity = Find(entity.Id);
                if (!Equals(dbEntity, null))
                    entity.Id = DatabaseContext.GenerateKey();
            }
          
            return Add((T)entity);
        }

        /// <summary>
        /// Adds a new entity to the data source.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        /// <returns>The added entity.</returns>
        public virtual T Add(T entity)
        {
            if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            T copy = Clone(entity);
            _databaseContext.Add(copy);
            entity.Id = copy.Id;

            return copy;
        }

        /// <summary>
        /// Deletes the given entity from the data source.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        void IRepository.Delete(IEntity entity)
        {
            Delete((T)entity);
        }

        /// <summary>
        /// Deletes the given entity from the data source.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public virtual void Delete(T entity)
        {
            if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            var dbEntity = Find(entity.Id);
            if (!Equals(dbEntity, null))
                _databaseContext.Delete(dbEntity);
        }
        /// <summary>
        /// Deletes the given entity from the data source.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public virtual void Delete(IEntity entity)
        {
            if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            var dbEntity = Find(entity.Id);
            if (!Equals(dbEntity, null))
                _databaseContext.Delete(dbEntity);
        }

        /// <summary>
        /// Deletes all the entities from the data source.
        /// </summary>
        public virtual void DeleteAll()
        {
            foreach (T entity in Entities)
                Delete(entity);
        }

        /// <summary>
        /// Updates the changes made to the given entity in its corresponding entity in the currently opened transaction.
        /// </summary>
        /// <param name="entity">
        /// The entity to use its changes and apply them to its current transaction corresponding one.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        void IRepository.Update(IEntity entity)
        {
            Update((T)entity);
        }

        /// <summary>
        /// Updates the changes made to the given entity in its corresponding entity in the currently opened transaction.
        /// </summary>
        /// <param name="entity">
        /// The entity to use its changes and apply them to its current transaction corresponding one.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public virtual void Update(T entity)
        {
            if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            var dbEntity = _databaseContext.Find<T>(entity.Id);

            if (Equals(dbEntity, null))
                return;
            if (Equals(dbEntity, entity))
                return;

            entity.UpdateProperties(dbEntity, RelevantProperties);
            _databaseContext.Update(dbEntity);
        }

        /// <summary>
        /// Finds the entity with the given identifier.
        /// </summary>
        /// <param name="id">The identifier to the find entity having it.</param>
        /// <returns>An object of type <typeparamref name="T"/> being the entity having the identifier; null if not found any.</returns>
        IEntity IRepository.Find(object id)
        {
            return Find(id);
        }

        /// <summary>
        /// Finds the entity with the given identifier.
        /// </summary>
        /// <param name="id">The identifier to the find entity having it.</param>
        /// <returns>An object of type <typeparamref name="T"/> being the entity having the identifier; null if not found any.</returns>
        public virtual T Find(object id)
        {
            return Clone(_databaseContext.Find<T>(id));
        }

        /// <summary>
        /// Gets all the items witch identifiers are the provided ones.
        /// </summary>
        /// <param name="ids">The <see cref="Array"/> identifiers to find the entities matching them.</param>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is null.</exception>
        /// <returns>A <see cref="IEnumerable{T}"/> containing all the found entities matching any of the given <paramref name="ids"/>;</returns>
        IEnumerable<IEntity> IRepository.FindById(params object[] ids)
        {
            return FindById(ids).Cast<IEntity>().ToArray();
        }

        /// <summary>
        /// Gets all the items witch identifiers are the provided ones.
        /// </summary>
        /// <param name="ids">The <see cref="Array"/> identifiers to find the entities matching them.</param>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is null.</exception>
        /// <returns>A <see cref="IEnumerable{T}"/> containing all the found entities matching any of the given <paramref name="ids"/>;</returns>
        public virtual IEnumerable<T> FindById(params object[] ids)
        {
            if (ids == null)
                throw new ArgumentNullException("ids");

            return _databaseContext.Where(new EntitiesByIdSpecification<T>(ids));
        }

        /// <summary>
        /// Finds the entity matching the given specification.
        /// </summary>
        /// <param name="specification">The specfication to evaluate on each entity of type <typeparamref name="T"/> to select the desired one.</param>
        /// <exception cref="ArgumentNullException"><paramref name="specification"/> is null.</exception>
        /// <returns>
        /// An entity of type <typeparamref name="T"/> being the single entity matching <paramref name="specification"/>; or null if the specification
        /// did not evaluate to any entity.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Cannot return an single entity matching the specification, because more than one matches it.
        /// </exception>
        IEntity IRepository.Find(ISpecification<IEntity> specification)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds the entity matching the given specification.
        /// </summary>
        /// <param name="specification">The specfication to evaluate on each entity of type <typeparamref name="T"/> to select the desired one.</param>
        /// <exception cref="ArgumentNullException"><paramref name="specification"/> is null.</exception>
        /// <returns>
        /// An entity of type <typeparamref name="T"/> being the single entity matching <paramref name="specification"/>; or null if the specification
        /// did not evaluate to any entity.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Cannot return an single entity matching the specification, because more than one matches it.
        /// </exception>
        public virtual T Find(ISpecification<T> specification)
        {
            if (specification == null)
                throw new ArgumentNullException("specification");

            return Clone(_databaseContext.Find(specification));
        }

        /// <summary>
        /// Iterates over the whole set of elements of type <typeparamref name="T"/> selecting those which evaluation of a specification returns true.
        /// </summary>
        /// <param name="specification">The specification to evaluate on each element.</param>
        /// <returns>A enumerable of elements of <typeparamref name="T"/> which the evaluation of <paramref name="specification"/> returns true.</returns>
        IEnumerable<IEntity> IRepository.Where(ISpecification<IEntity> specification)
        {
            Predicate<IEntity> originalPredicate = specification.Predicate.Compile();
            ISpecification<T> spec = new Specification<T>(x => originalPredicate(x));
            
            return Where(spec).Cast<IEntity>().ToArray();
        }

        /// <summary>
        /// Iterates over the whole set of elements of type <typeparamref name="T"/> selecting those which evaluation of a specification returns true.
        /// </summary>
        /// <param name="specification">The specification to evaluate on each element.</param>
        /// <returns>A enumerable of elements of <typeparamref name="T"/> which the evaluation of <paramref name="specification"/> returns true.</returns>
        public virtual IEnumerable<T> Where(ISpecification<T> specification)
        {
            if (specification == null)
                throw new ArgumentNullException("specification");

            return Clone(_databaseContext.Where(specification).ToArray());
        }

        /// <summary>
        /// Gets all the entities sorted as the defined comparison defines.
        /// </summary>
        /// <param name="comparison">The <see cref="System.Comparison{T}"/> defining the sorting criteria.</param>
        /// <exception cref="ArgumentNullException"><paramref name="comparison"/> is null.</exception>
        /// <returns>All entities of type <typeparamref name="T"/> sorted descending.</returns>
        IEnumerable<IEntity> IRepository.GetSorted(Comparison<IEntity> comparison)
        {
            return GetSorted((x, y) => comparison(x, y)).Cast<IEntity>().ToArray();
        }

        /// <summary>
        /// Gets all the entities sorted as the defined comparison defines.
        /// </summary>
        /// <param name="comparison">The <see cref="System.Comparison{T}"/> defining the sorting criteria.</param>
        /// <exception cref="ArgumentNullException"><paramref name="comparison"/> is null.</exception>
        /// <returns>All entities of type <typeparamref name="T"/> sorted descending.</returns>
        public virtual IEnumerable<T> GetSorted(Comparison<T> comparison)
        {
            if (comparison == null)
                throw new ArgumentNullException("comparison");

            return Clone(_databaseContext.GetSorted(comparison)).ToArray();
        }

        /// <summary>
        /// Merges the changes there are in the <paramref name="currentItems" /> enumerable to the ones in
        /// the <paramref name="formerItems" />.
        /// </summary>
        /// <param name="formerItems">The status in which there were the items before changes took place.</param>
        /// <param name="currentItems">The items containing the changes to make to the <paramref name="formerItems" />.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="formerItems" /> or <paramref name="currentItems" />.
        /// </exception>
        void IRepository.Merge(IEnumerable<IEntity> formerItems, IEnumerable<IEntity> currentItems)
        {
            Merge(formerItems.Cast<T>(), currentItems.Cast<T>());
        }

        /// <summary>
        /// Merges the changes there are in the <paramref name="currentItems" /> enumerable to the ones in
        /// the <paramref name="formerItems" />.
        /// </summary>
        /// <param name="formerItems">The status in which there were the items before changes took place.</param>
        /// <param name="currentItems">The items containing the changes to make to the <paramref name="formerItems" />.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="formerItems" /> or <paramref name="currentItems" />.
        /// </exception>
        public virtual void Merge(IEnumerable<T> formerItems, IEnumerable<T> currentItems)
        {
            if (formerItems == null)
                throw new ArgumentNullException("formerItems");
            if (currentItems == null)
                throw new ArgumentNullException("currentItems");

            _databaseContext.Merge(formerItems, currentItems, x => Add(x), Update, Delete);
        }


        /// <summary>
        /// Gets all the public properties non-readonly properties that are relevant to the current repository when making its operations.
        /// </summary>
        protected virtual string[] RelevantProperties
        {
            get { return new[] { GetName(x => x.Id) }; }
        }

        /// <summary>
        /// Clones the given object.
        /// </summary>
        /// <param name="entity">The object of type <typeparamref name="T"/> to clone.</param>
        /// <returns>A clone of <paramref name="entity"/> with the same value of it in the relevant properties.</returns>
        protected virtual T Clone(T entity)
        {
            if (Equals(entity, null))
                return default(T);

            var copy = ServiceLocator.Current.GetInstance<T>();
            entity.UpdateProperties(copy, RelevantProperties);

            return copy;
        }

        /// <summary>
        /// Clones the given entities.
        /// </summary>
        /// <param name="entities">A <see cref="IEnumerable{T}"/> of entities to clone.</param>
        /// <returns>
        /// A <see cref="IEnumerable{T}"/> of clones with the same data of those at <paramref name="entities"/> argument. If
        /// <paramref name="entities"/> is null, it returns an empty <see cref="IEnumerable{T}"/>.</returns>
        protected virtual IEnumerable<T> Clone(IEnumerable<T> entities)
        {
            return (entities ?? new T[0])
                .Aggregate(new List<T>(), (list, entity) =>
                {
                    T clone = Clone(entity);
                    list.Add(clone);
                    return list;
                })
                .ToArray();
        }

        /// <summary>
        /// Gets the name of the property specified by the given expression
        /// </summary>
        /// <typeparam name="TValue">The type of the property's value.</typeparam>
        /// <param name="propertyExpression">
        /// The <see cref="Expression{T}"/> defining the expression of the property which name must be returned.
        /// </param>
        /// <returns>
        /// A string representing the name of the proeprty defined by <paramref name="propertyExpression"/>; or an empty string if given
        /// a null or not a property expression.
        /// </returns>
        protected string GetName<TValue>(Expression<Func<T, TValue>> propertyExpression)
        {
            var member = propertyExpression.Body as MemberExpression;

            return member != null ? member.Member.Name : string.Empty;
        }

        public T GetClone(T entity)
        {
            return Clone(entity);
        }

        public IEnumerable<T> Where(Func<T, bool> condition)
        {
            return Entities.Where(condition);
        }
    }
}
