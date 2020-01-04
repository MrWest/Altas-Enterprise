using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.ExceptionHandling
{
    /// <summary>
    /// Contract of the exception handler that sends an email to the support team and/or the system administrators with the details of the
    /// handled exception.
    /// </summary>
    public interface IEmailExceptionHandler : IExceptionHandler
    {
    }
}
