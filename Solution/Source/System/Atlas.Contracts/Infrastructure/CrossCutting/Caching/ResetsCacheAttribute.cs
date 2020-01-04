using System;

namespace CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching
{
    /// <summary>
    ///     Decorates a method saying the cache will be wiped out.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ResetsCacheAttribute : Attribute
    {
    }
}