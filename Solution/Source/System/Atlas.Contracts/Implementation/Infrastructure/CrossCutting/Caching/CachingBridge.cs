using System;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Caching
{
    /// <summary>
    ///     This class links the caching interception behavior with the actual caching engine.
    /// </summary>
    public class CachingBridge : ICache
    {
        private ICacheManager _cache;
        private readonly TimeSpan _expirationInterval = TimeSpan.FromSeconds(Settings.Default.ExpirationInterval);

        
        private ICacheManager Cache
        {
            get { return _cache ?? (_cache = ServiceLocator.Current.GetInstance<ICacheManager>()); }
        }


        /// <summary>
        ///     Returns the value of the given key in the cache.
        /// </summary>
        /// <param name="key">A key to find its value.</param>
        /// <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
        /// <returns>The value in the cache for that key; or null if the value has already expired.</returns>
        public object Get(string key)
        {
            if (key == null) throw new ArgumentNullException("key");

            return Cache[key];
        }

        /// <summary>
        ///     Attempts to obtain an object saved with the given key and puts it in the <paramref name="value" />
        ///     parameter if exists.
        /// </summary>
        /// <param name="key">The key to search the object using it.</param>
        /// <param name="value">The out argument to put the found value (if found).</param>
        /// <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
        /// <returns>True if there was found a value stored with the key; false otherwise.</returns>
        public bool TryGet(string key, out object value)
        {
            if (key == null) throw new ArgumentNullException("key");

            value = Cache[key];
            return Cache.Contains(key);
        }

        /// <summary>
        ///     Saves the given value in the cache identified by the given key.
        /// </summary>
        /// <param name="key">The key to identify the value.</param>
        /// <param name="value">The value to save in the cache.</param>
        /// <exception cref="ArgumentNullException"><paramref name="key" /> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="key" /> is an empty string.</exception>
        public void Store(string key, object value)
        {
            Cache.Add(key, value, CacheItemPriority.Normal, null, new AbsoluteTime(_expirationInterval));
        }

        /// <summary>
        ///     Flushes all the content of the cache.
        /// </summary>
        public void Clear()
        {
            Cache.Flush();
        }
    }
}