using System;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data
{
    /// <summary>
    /// This attribute marks properties and methods in a class to prevent them from getting a unit of work instance
    /// (<see cref="IUnitOfWork"/>) to wrap the transaction they will make.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class SkipUnitOfWorkAttribute : Attribute
    {
    }
}
