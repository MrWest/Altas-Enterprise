using System;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data
{
    /// <summary>
    /// This represents the contract used to implement the Unit of work pattern for data transactions.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commits the changes made to the data to the database.
        /// </summary>
        void Commit();

        /// <summary>
        /// Drops the changes made to the data, leaving in a unmodified state.
        /// </summary>
        void Rollback();
    }
}
