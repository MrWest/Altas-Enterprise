using System;
using System.Reflection;

namespace CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching
{
    /// <summary>
    ///     Represents a contract that must be implemented by any cacheale object in order to be usable by the cache system.
    /// </summary>
    public interface ICacheableObject
    {
        /// <summary>
        ///     This method allows to obtain the key for the possible cached result of a method.
        /// </summary>
        /// <param name="method">The method being call which result will be cached.</param>
        /// <param name="arguments">All the arguments passed to the method.</param>
        /// <returns>A string representing the key for the result to cache, which is formed using all the given data.</returns>
        string GetKeyFor(MethodBase method, params object[] arguments);
    }
}