using System;

namespace CompanyName.Atlas.Contracts.Presentation.Data
{
    /// <summary>
    /// This is the argument of any event implying an crud view model item.
    /// </summary>
    public class ItemEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of a item related event given the involved item.
        /// </summary>
        /// <param name="item">The item involved in the event.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is null.</exception>
        public ItemEventArgs(object item)
        {
            if (Equals(item, null))
                throw new ArgumentNullException("item");
            
            Item = item;
        }


        /// <summary>
        /// Gets the item that was involved in the event using this object as argument.
        /// </summary>
        public object Item { get; private set; }
    }


    /// <summary>
    /// This is the argument of any event implying an crud view model item.
    /// </summary>
    public class ItemEventArgs<T> : ItemEventArgs
    {
        /// <summary>
        /// Initializes a new instance of a item related event given the involved item.
        /// </summary>
        /// <param name="item">The item involved in the event.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is null.</exception>
        public ItemEventArgs(T item)
            : base(item)
        {
        }


        /// <summary>
        /// Gets the item that was involved in the event using this object as argument.
        /// </summary>
        public new T Item
        {
            get { return (T)base.Item; }
        }
    }
}
