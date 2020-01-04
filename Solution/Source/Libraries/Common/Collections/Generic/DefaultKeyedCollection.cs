using System.Collections.ObjectModel;

namespace System.Collections.Generic
{
    /// <summary>
    ///     Represents a default keyed collection. This is a collection where all items can be identified by a given key. BEWARE that there cannot be in the collection two items with the same key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TItem">The type of the items.</typeparam>
    public class DefaultKeyedCollection<TKey, TItem> : KeyedCollection<TKey, TItem>
    {
        private readonly Func<TItem, TKey> _keySelector;


        /// <summary>
        ///     Initializes a new instance of a default keyed collection.
        /// </summary>
        /// <param name="keySelector">The function that allows to select the key from an item.</param>
        /// <exception cref="ArgumentNullException"><paramref name="keySelector"/> is null.</exception>
        public DefaultKeyedCollection(Func<TItem, TKey> keySelector)
        {
            if (keySelector == null)
                throw new ArgumentNullException("keySelector");
            
            _keySelector = keySelector;
        }


        /// <summary>
        ///     Gets the value of the key from the given item.
        /// </summary>
        /// <param name="item">The item which key will be returned.</param>
        /// <returns>The value of the item's key.</returns>
        protected override TKey GetKeyForItem(TItem item)
        {
            return _keySelector(item);
        }
    }
}