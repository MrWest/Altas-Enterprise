using System;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.ExceptionHandling
{
    /// <summary>
    /// Implementation of an Exception Handler which simulates (doing actually nothing) emailing the details of an exception to the support
    /// team and/or system administrators.
    /// </summary>
    public class NullEmailExceptionHandler : IEmailExceptionHandler
    {
        /// <summary>
        /// Returns the same given exception.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> to return.</param>
        /// <param name="handlingInstanceId">The unique ID attached to the handling chain for this handling instance.</param>
        /// <returns>The same <see cref="Exception"/> provided at <paramref name="exception"/> argument.</returns>
        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            return exception;
        }
    }
}
