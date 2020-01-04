using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.ExceptionHandling
{
    /// <summary>
    /// Describes the interface of an Exception Handler that replaces the actual exception for a new one.
    /// </summary>
    public interface IReplaceHandler : IExceptionHandler
    {
    }
}
