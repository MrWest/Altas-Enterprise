using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity.InterceptionExtension;
using CompanyName.Atlas.Contracts.Infrastructure.Data;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Data
{
    /// <summary>
    /// This behavior is intended to be attached to application services dealing with data, in order to wrap the handling of the unit of
    /// work they require.
    /// </summary>
    public class UnitOfWorkBehavior : IInterceptionBehavior
    {
        /// <summary>
        /// Gets the required interfaces this interceptor must get associated to.
        /// </summary>
        /// <returns>An empty types array.</returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        /// <summary>
        /// Makes sure that the unit of work is created before passing to the actual method call, and any aspect there has been defined for
        /// the unit of work is carried on (like: committing it, rolling it back, etc).
        /// </summary>
        /// <param name="input">The information regarding to the actual method call this behavior wraps.</param>
        /// <param name="getNext">The delegate representing the next method to call in the interception chain.</param>
        /// <returns>An instance of <see cref="IMethodReturn"/> containing information regarding to the next call's result.</returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            // Obtaint the reference to the method being intercepted
            MethodBase method = this.FindMethod(input);

            // If there was instructed to quit using the unit of work, then continue without it
            if (method.GetCustomAttributes<SkipUnitOfWorkAttribute>(true).Any())
                return getNext()(input, getNext);

            // Create a fresh unit of work
            using (var unitOfWork = ServiceLocator.Current.GetInstance<IUnitOfWork>())
            {
                // Call the next method in the chain to perform the actual job
                IMethodReturn result = getNext()(input, getNext);

                // If the transaction is faulted, then roll it back and rethron the exception to the caller
                if (result.Exception != null)
                {
                    unitOfWork.Rollback();
                    return input.CreateExceptionMethodReturn(result.Exception);
                }

                // If there was instructed to call Commit after the transaction is over successfully, then commit the transaction
                var commit = method.GetCustomAttributes<CommitAttribute>(true).SingleOrDefault();
                if (commit != null && commit.Commit)
                    unitOfWork.Commit();

                // And return the result
                return result;
            }
        }

        /// <summary>
        /// Returns a flag indicating if this behavior will actually do anything when invoked.
        /// </summary>
        public bool WillExecute
        {
            get { return true; }
        }
    }
}
