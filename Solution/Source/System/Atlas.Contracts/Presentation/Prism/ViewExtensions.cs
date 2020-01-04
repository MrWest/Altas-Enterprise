using System;
using System.Windows;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Logging;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Logging;
using IView = Microsoft.Practices.Prism.Mvvm.IView;

namespace CompanyName.Atlas.Contracts.Presentation.Prism
{
    /// <summary>
    /// This class exposes some extensions for the System.Windows.IView interface.
    /// </summary>
    public static class ViewExtensions
    {
        /// <summary>
        /// Displays a message box using the content and title from the confirmation object contained in the given event arguments and
        /// places the interaction's result in the same given event arguments.
        /// </summary>
        /// <param name="view">The view being extended.</param>
        /// <param name="eventArgs">
        /// The interaction requested event arguments where the data to be used in the Message Box is.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="view"/> or <paramref name="eventArgs"/> is null.
        /// </exception>
        public static bool Confirm(this IView view, InteractionRequestedEventArgs eventArgs)
        {
            if (eventArgs == null)
                throw new ArgumentNullException("eventArgs");

            return InteractionRequestHelpers.Confirm(eventArgs);
        }

        /// <summary>
        /// Displays a message box using the content and title from the notification object contained in the given event arguments and
        /// places the interaction's result in the same given event arguments.
        /// </summary>
        /// <param name="view">The view being extended.</param>
        /// <param name="eventArgs">
        /// The interaction requested event arguments where the data to be used in the Message Box is.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="eventArgs"/> is null.
        /// </exception>
        public static bool Notify(this IView view, InteractionRequestedEventArgs eventArgs)
        {
            if (eventArgs == null)
                throw new ArgumentNullException("eventArgs");

            return InteractionRequestHelpers.Notify(eventArgs);
        }

        /// <summary>
        /// Attempts to establish the interaction specified in the context of the given arguments. First starts try to executed a confirmation, then tries for a simple
        /// notification.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> being the view involved in the interaction.</param>
        /// <param name="eventArgs">The <see cref="InteractionRequestedEventArgs"/> containing the details of the interaction.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="view"/> or <paramref name="eventArgs"/> is null.
        /// </exception>
        /// <returns>True if any interaction was possible to be established (whether it was a confirmation, notification or other of other sort); false otherwise.</returns>
        public static bool Execute(this IView view, InteractionRequestedEventArgs eventArgs)
        {
            return view.Confirm(eventArgs) || view.Notify(eventArgs);
        }

        /// <summary>
        /// Disconnects the interaction method from the old data context of the given view and connects to the new one.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to get refreshed its interaction methods connection.</param>
        /// <param name="e">The argments containing the details of the data context switch.</param>
        /// <param name="interactionMethod">The interaction method (<see cref="EventHandler{TEventArgs}"/>) that must be connected to the new data context.</param>
        /// <exception cref="ArgumentNullException"><paramref name="interactionMethod"/> is null.</exception>
        public static void SetupInteractionWithDataContext(this IView view, DependencyPropertyChangedEventArgs e, EventHandler<InteractionRequestedEventArgs> interactionMethod)
        {
            if (interactionMethod == null)
                throw new ArgumentNullException("interactionMethod");
            try
            {
                var oldViewModel = (IInteractionRequest)e.OldValue;
                var newViewModel = (IInteractionRequest)e.NewValue;

                if (oldViewModel != null)
                    oldViewModel.Raised -= interactionMethod;

                if (newViewModel != null)
                    newViewModel.Raised += interactionMethod;
            }
            catch (Exception exception)
            {
                Logger.Instance.Log(exception.Message,Category.Exception, Priority.Medium);
                
            }
           
        }
    }
}
