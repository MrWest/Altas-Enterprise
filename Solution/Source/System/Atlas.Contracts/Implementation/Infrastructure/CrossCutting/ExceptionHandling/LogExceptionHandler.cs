using System;
using System.Text;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.ExceptionHandling
{
    /// <summary>
    /// Implementation of the Log Exception Handler. Performs the details logging process of a certain exception.
    /// </summary>
    public class LogExceptionHandler : ILogExceptionHandler
    {
        private readonly ILoggerFacade _logger = ServiceLocator.Current.GetInstance<ILoggerFacade>();


        /// <summary>
        /// Takes the given exception and logs its details using the default logger
        /// (<see cref="ILoggerFacade"/>) given the exception and a unique identifier.
        /// </summary>
        /// <param name="exception">The exception to handle.</param>
        /// <param name="handlingInstanceId">The unique ID attached to the handling chain for this handling instance.</param>
        /// <returns>The same <see cref="Exception"/> given at <paramref name="exception"/> argument.</returns>
        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            if (exception == null)
                throw new ArgumentNullException("exception");

            // Prepare the log
            var message = new StringBuilder(Resources.UnexpectedExceptionLogMessage);
            message.AppendLine();
            message.AppendLine("Handling ID: {0}".EasyFormat(handlingInstanceId));
            message.AppendLine("Exception: {0}".EasyFormat(exception.GetType().Name));
            message.AppendLine("Message: {0}".EasyFormat(exception.Message));
            message.AppendLine("Target site: {0}".EasyFormat(exception.TargetSite));
            message.AppendLine("Stack trace:");
            message.AppendLine(exception.StackTrace);

            _logger.Log(message.ToString(), Category.Exception, Priority.High);

            return exception;
        }
    }
}
