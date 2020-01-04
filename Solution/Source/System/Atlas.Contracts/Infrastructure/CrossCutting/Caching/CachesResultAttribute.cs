using System;

namespace CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching
{
    /// <summary>
    ///     Decorates a method saying that its result will be cached.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CachesResultAttribute : Attribute
    {
    }
}