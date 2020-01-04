using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.ExceptionHandling
{
    /// <summary>
    /// Describes the interface of a Log Exception handler, use to carry the details of an exception to the logs destination.
    /// </summary>
    public interface ILogExceptionHandler : IExceptionHandler
    {
    }
}
