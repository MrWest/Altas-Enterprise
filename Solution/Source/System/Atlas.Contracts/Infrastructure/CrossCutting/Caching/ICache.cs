namespace CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching
{
    /// <summary>
    ///     Describes the behavior of a cache.
    /// </summary>
    public interface ICache
    {
        /// <summary>
        ///     Gets the object stored in the cache using the given key if exists.
        /// </summary>
        /// <param name="key">The key under which the object is stored.</param>
        /// <returns>The object identified by the given key; or null if none is found.</returns>
        object Get(string key);

        /// <summary>
        ///     Attempts to obtain an object saved with the given key and puts it in the <paramref name="value"/>
        ///     parameter if exists.
        /// </summary>
        /// <param name="key">The key to search the object using it.</param>
        /// <param name="value">The out argument to put the found value (if found).</param>
        /// <returns>True if there was found a value stored with the key; false otherwise.</returns>
        bool TryGet(string key, out object value);

        /// <summary>
        ///     Stores the given value under the given key in the cache.
        /// </summary>
        /// <param name="key">The key under which the given value will be registered.</param>
        /// <param name="value">The value to store.</param>
        void Store(string key, object value);

        /// <summary>
        ///     Removes all the stored values there are in the cache.
        /// </summary>
        void Clear();
    }
}