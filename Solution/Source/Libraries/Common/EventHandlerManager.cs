using System.Collections;
using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// This class allows to manage the event handlers registrations and unregistrations for an event existing in some other object. This class is thread-safe.
    /// </summary>
    /// <typeparam name="TEventArgs">
    /// The type of the event arguments that the event handlers can get passed.
    /// </typeparam>
    public class EventHandlerManager<TEventArgs> : IEnumerable<EventHandler<TEventArgs>>
        where TEventArgs : EventArgs
    {
        private static readonly object Lock = new object();
        private readonly IList<EventHandler<TEventArgs>> _handlers = new List<EventHandler<TEventArgs>>();


        /// <summary>
        /// Registers the given event handler in the handlers list.
        /// </summary>
        /// <param name="manager">The manager to which the event <paramref name="handler"/> will be registered.</param>
        /// <param name="handler">The event handler to register.</param>
        /// <exception cref="ArgumentNullException"><paramref name="handler"/> is null.</exception>
        /// <returns>
        /// The given event handler manager given at <paramref name="manager"/> with the handler given at
        /// <paramref name="handler"/> registered in it.
        /// </returns>
        public static EventHandlerManager<TEventArgs> operator +(EventHandlerManager<TEventArgs> manager, EventHandler<TEventArgs> handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");

            lock (Lock)
            {
                manager._handlers.Add(handler);
                return manager;
            }
        }

        /// <summary>
        /// Unregisters the given event handler from the handlers list.
        /// </summary>
        /// <param name="manager">The manager to which the event <paramref name="handler"/> will be unregistered.</param>
        /// <param name="handler">The event handler to unregister.</param>
        /// <exception cref="ArgumentNullException"><paramref name="handler"/> is null.</exception>
        /// <returns>
        /// The given event handler manager given at <paramref name="manager"/> with the handler given at
        /// <paramref name="handler"/> unregistered from it.
        /// </returns>
        public static EventHandlerManager<TEventArgs> operator -(EventHandlerManager<TEventArgs> manager, EventHandler<TEventArgs> handler)
        {
            if (handler == null)
                throw new ArgumentNullException("handler");

            lock (Lock)
            {
                manager._handlers.Remove(handler);
                return manager; 
            }
        }

        /// <summary>
        /// Invokes all the event handlers registered in the current event handler manager passing a sender and arguments.
        /// </summary>
        /// <param name="sender">
        /// The object sending the event calling this method to invoke all of its registered handlers.
        /// </param>
        /// <param name="args">The arguments that are meant to be passed to the event handlers.</param>
        public void CallEventHandlers(object sender, TEventArgs args)
        {
            lock (Lock)
            {
                foreach (var eventHandler in _handlers)
                    eventHandler(sender, args); 
            }
        }


        /// <summary>
        /// Returns an enumerator that iterates through the event handlers registered in the current event handler
        /// manager.
        /// </summary>
        /// <returns>
        /// A enumerator that can be used to iterate through the event handlers registered in the current event handler
        /// manager.
        /// </returns>
        public IEnumerator<EventHandler<TEventArgs>> GetEnumerator()
        {
            return _handlers.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the event handlers registered in the current event handler
        /// manager.
        /// </summary>
        /// <returns>
        /// A enumerator that can be used to iterate through the event handlers registered in the current event handler
        /// manager.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
