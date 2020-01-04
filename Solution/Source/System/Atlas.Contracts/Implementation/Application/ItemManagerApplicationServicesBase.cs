using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;

namespace CompanyName.Atlas.Contracts.Implementation.Application
{
    /// <summary>
    /// Represents the base class of the entity manager application services.
    /// </summary>
    /// <typeparam name="T">The type of the entities managed by this service.</typeparam>
    /// <typeparam name="TRepository">The repository handlind the raw data operations with the entities.</typeparam>
    /// <typeparam name="TDomainService">The domain service handling the business rules for the entities.</typeparam>
    public abstract class ItemManagerApplicationServicesBase<T, TRepository, TDomainService> :
        IItemManagerApplicationServices<T>,
        ICacheableObject,
        IValidationServices<T>
        where T : class, IEntity
        where TRepository : IRepository<T>
        where TDomainService : IDomainServices<T>
    {
        private ILoggerFacade _logger;

         
        /// <summary>
        /// Performs the resources cleaning logic when this object is being garbage collected.
        /// </summary>
        [ExcludeFromCodeCoverage]
        [SkipUnitOfWork]
        ~ItemManagerApplicationServicesBase()
        {
            Dispose(false);
        }


        #region IItemManagerApplicationServices<T> Members

        /// <summary>
        /// Gets the entities from the data source.
        /// </summary>
       
        public virtual IEnumerable<T> Items
        {
          //  [CachesResult]
            get { return Repository.Entities; }
        }

        /// <summary>
        /// Gets an instance of the repository of type <typeparamref name="TRepository"/>.
        /// </summary>
        protected virtual TRepository Repository
        {
            [SkipUnitOfWork]
            get
            {


                return ServiceLocator.Current.GetInstance<TRepository>();
            }
        }

        /// <summary>
        /// Gets an instance of the domain service of type <typeparamref name="TDomainService"/>.
        /// </summary>
        protected virtual TDomainService DomainServices
        {
            [SkipUnitOfWork]
            get { return ServiceLocator.Current.GetInstance<TDomainService>(); }
        }

        /// <summary>
        /// Gets the <see cref="ILoggerFacade"/> (the logger) of the current item manager application services.
        /// </summary>
        protected ILoggerFacade Logger
        {
            get { return _logger ?? (_logger = ServiceLocator.Current.GetInstance<ILoggerFacade>()); }
        }


        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        [Validate]
        [ResetsCache]
        [Commit]
        public virtual void Add(T entity)
        {
            if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            Logger.Log("Adding: \"{0}\"".EasyFormat(entity), Category.Debug, Priority.Medium);
            Repository.Add(entity);
            Logger.Log("Added: \"{0}\"".EasyFormat(entity), Category.Info, Priority.Medium);
        }

        /// <summary>
        /// Deletes the given entity.
        /// </summary>
        /// <param name="entity">The entity to be deleted.</param>
        [Validate]
        [ResetsCache]
        [Commit]
        public virtual void Delete(T entity)
        {
            if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            Logger.Log("Deleting: \"{0}\"".EasyFormat(entity), Category.Debug, Priority.Medium);
            Repository.Delete(entity);
            Logger.Log("Deleted: \"{0}\"".EasyFormat(entity), Category.Info, Priority.Medium);
        }

        /// <summary>
        /// Deletes all the items from the system.
        /// </summary>
        [ResetsCache]
        [Commit]
        public virtual void DeleteAll()
        {
            Logger.Log("Deleting all entities.", Category.Debug, Priority.Medium);
            Repository.DeleteAll();
            Logger.Log("Deleted all entities.", Category.Info, Priority.Medium);
        }

        /// <summary>
        /// Saves the changes made to the given entity.
        /// </summary>
        /// <param name="entity">The entity to save it changes.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        [Validate]
        [ResetsCache]
        [Commit]
        public virtual void Update(T entity)
        {
            if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            Logger.Log("Updating: \"{0}\"".EasyFormat(entity), Category.Debug, Priority.Medium);
            Repository.Update(entity);
            Logger.Log("Updated: \"{0}\"".EasyFormat(entity), Category.Info, Priority.Medium);
        }

        /// <summary>
        /// Creates a new instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>A new instance of type <typeparamref name="T"/>.</returns>
        public virtual T Create()
        {
            return DomainServices.Create();
        }

        /// <summary>
        /// Determines whether the given entity can be added.
        /// </summary>
        /// <param name="entity">The entity to determine whether it can be added or not.</param>
        /// <returns>True if the entity given at <paramref name="entity"/> can be added; false otherwise.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        [CachesResult]
        public virtual bool CanAdd(T entity)
        {
            if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            return DomainServices.CanAdd(entity);
        }
        
        /// <summary>
        /// Determines whether there is allowed to add a new entity.
        /// </summary>
        /// <returns>True if there is allowed to add a new entity; false otherwise.</returns>
        [CachesResult]
        public virtual bool CanAddNew()
        {
            return DomainServices.CanAdd();
        }
        
        /// <summary>
        /// Determines whether there can be updated the given item.
        /// </summary>
        /// <param name="item">The item of type <typeparamref name="T"/> to determine whether it can be updated or not.</param>
        /// <returns>True if <paramref name="item"/> is not null and can be updated; false otherwise.</returns>
        [CachesResult]
        public bool CanUpdate(T item)
        {
            return DomainServices.CanUpdate(item);
        }

        /// <summary>
        /// Determines whether the given entity can be deleted.
        /// </summary>
        /// <param name="entity">The entity to determine whether it can be deleted or not.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        /// <returns>True if <paramref name="entity"/> can be deleted; false otherwise.</returns>
        [CachesResult]
        public bool CanDelete(T entity)
        {
            return DomainServices.CanDelete(entity);
        }

        /// <summary>
        /// Validates the given entity returning the validation results.
        /// </summary>
        /// <param name="entity">The entity to validate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        /// <returns>
        /// An IEnumerable{string} of validation entries represented by objects where each object represents the content of an error.
        /// If the enumerable is empty it means that <paramref name="entity"/> is valid.
        /// </returns>
        public IEnumerable<string> Validate(T entity)
        {
            if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            return DomainServices.Validate(entity);
        }

        /// <summary>
        /// Validates the given entity returning the validation results.
        /// </summary>
        /// <param name="entity">The entity to validate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        /// <returns>
        /// An IEnumerable{string} of validation entries represented by objects where each object represents the content of an error.
        /// If the enumerable is empty it means that <paramref name="entity"/> is valid.
        /// </returns>
        public IEnumerable<string> Validate(object entity)
        {
            return Validate(entity as T);
        }

        #endregion


        #region ICacheableObject Members

        /// <summary>
        ///     This method allows to obtain the key for the possible cached result of a method.
        /// </summary>
        /// <param name="method">The method being call which result will be cached.</param>
        /// <param name="arguments">All the arguments passed to the method.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="method" /> os <paramref name="arguments" /> is null.
        /// </exception>
        /// <returns>
        /// A string representing the key for the result to cache, which is formed using all the given data.
        /// </returns>
        [SkipUnitOfWork]
        public virtual string GetKeyFor(MethodBase method, params object[] arguments)
        {
            // TODO: If creating a new application service, dont forget to override this method in if needed
            if (method == null)
                throw new ArgumentNullException("method");
            if (arguments == null)
                throw new ArgumentNullException("arguments");

            // Simple method key resolution
            string className = GetType().Name;
            string methodName = method.Name;
            string argumentsString = arguments.Aggregate(string.Empty, (s, o) => s + o + ", ");
            argumentsString = argumentsString.Any() ? argumentsString.Substring(0, argumentsString.Length - 2) : argumentsString;
            return "{0}.{1}({2})".EasyFormat(className, methodName, argumentsString);
        }

        #endregion


        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        [ExcludeFromCodeCoverage]
        [SkipUnitOfWork]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion


        /// <summary>
        /// Performs operations to release the resources (unmanaged and optionally the managed ones) this service has
        /// used.
        /// </summary>
        /// <param name="disposing">True to dispose the managed resources along with the unmanaged ones.</param>
        [ExcludeFromCodeCoverage]
        [SkipUnitOfWork]
        protected virtual void Dispose(bool disposing)
        {
        }


        /// <summary>
        /// Creates a unit of work to handle the current transaction.
        /// </summary>
        /// <returns>A new instance of <see cref="IUnitOfWork"/>.</returns>
        [Obsolete("Remove")]
        [SkipUnitOfWork]
        protected virtual IUnitOfWork CreateUnitOfWork()
        {
            return ServiceLocator.Current.GetInstance<IUnitOfWork>();
        }

        public T Find(object id)
        {
            if (id==null)
                return null;
            var repo = ServiceLocator.Current.GetInstance<ICommonRepository<T>>();
            return repo.Find(id);
        }
    }
}
