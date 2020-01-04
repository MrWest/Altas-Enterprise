using CompanyName.Atlas.Contracts.Infrastructure.Data;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private static int _stack;
        private static readonly IDatabaseContext Context = ServiceLocator.Current.GetInstance<IDatabaseContext>();
        private static bool _rollbackProgrammed;


        /// <summary>
        /// Initializes a new instance of a unit of work.
        /// </summary>
        public EntityFrameworkUnitOfWork()
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