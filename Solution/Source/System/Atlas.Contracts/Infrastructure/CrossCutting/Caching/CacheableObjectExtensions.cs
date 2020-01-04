using System;
using System.Reflection;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching;

namespace CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching
{
    /// <summary>
    /// Provides some extensions for <see cref="ICacheableObject"/> class.
    /// </summary>
    public static class CacheableObjectExtensions
    {
        /// <summary>
        ///     This method allows to obtain the key for the possible cached result of a method, method that must be
        ///     differentiated by a certain domain entity.
        /// </summary>
        /// <param name="cacheableObject">The cacheable object to get the key.</param>
        /// <param name="baseKey">The base key to include in the key.</param>
        /// <param name="entity">The entity to include its identifier in the key.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="cacheableObject"/>, <paramref name="baseKey"/> or <paramref name="entity"/> is
        ///     null.
        /// </exception>
        /// <returns>
        /// A string representing the key for the result to cache, which is formed using all the given data.
        /// </returns>
        public static string GetKeyFor<T>(this ICacheableObject cacheableObject, string baseKey, T entity)
            where T : IEntity
        {
            if (cacheableObject == null)
                throw new ArgumentNullException("cacheableObject");
            if (baseKey == null)
                throw new ArgumentNullException("baseKey");
            if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            baseKey = "{0}->{1}".EasyFormat(entity.Id, baseKey);

            return baseKey;
        }
    }
}
