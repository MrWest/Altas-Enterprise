using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Infrastructure.Data;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O
{
    /// <summary>
    /// This represents the implementation of the unit of work for the transactions influencing in database based in
    /// Db4O object oriented database system.
    /// </summary>
    public class Db4OUnitOfWork : IUnitOfWork
    {
        private static int _stack;
        private static readonly IDatabaseContext Context = ServiceLocator.Current.GetInstance<IDatabaseContext>();
        private static bool _rollbackProgrammed;


        /// <summary>
        /// Initializes a new instance of a unit of work.
        /// </summary>
        public Db4OUnitOfWork()
        {
            _stack++;
        }


        #region IUnitOfWork Members

        /// <summary>
        /// Commits the changes made to the data to the database.
        /// </summary>
        public void Commit()
        {
            if (_stack == 1 && !_rollbackProgrammed)
                Context.Save();
        }

        /// <summary>
        /// Drops the changes made to the data, leaving in a unmodified state.
        /// </summary>
        public void Rollback()
        {
            _rollbackProgrammed = true;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs cleaning operations to release the resources there were employed by the current unit of work.
        /// </summary>
        public void Dispose()
        {
            if (_stack-- > 1)
                return;

            if (_rollbackProgrammed)
                Context.DropChanges();
            
            _rollbackProgrammed = false;
        }

        #endregion
    }
}
