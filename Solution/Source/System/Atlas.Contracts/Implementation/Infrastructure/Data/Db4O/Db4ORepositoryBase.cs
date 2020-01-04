using System;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O
{
    /// <summary>
    /// Base class of all the repositories based on Db4O database technology.
    /// </summary>
    /// <typeparam name="T">The type of the entities repositories deriving this one will deal with.</typeparam>
    public abstract class Db4ORepositoryBase<T> : RepositoryBase<T, IDb4ODatabaseContext> where T : class, IEntity
    {
       
        /// <summary>
        /// Initializes a new instance of a deriver of this base repository class given the context used to abstract
        /// the actual database.
        /// </summary>
        /// <param name="databaseContext">
        /// The database context representing the database object that performs the raw data operations.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="databaseContext"/> is null.</exception>
        protected Db4ORepositoryBase(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
