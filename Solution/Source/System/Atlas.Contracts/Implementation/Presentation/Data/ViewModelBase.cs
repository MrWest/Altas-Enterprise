using System;
using System.Text;
using CompanyName.Atlas.Contracts.Exceptions;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.Contracts.Properties;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data
{
    /// <summary>
    /// Base class of every view model in the system.
    /// </summary>
    public abstract class ViewModelBase : BindableBase, IInteractionRequester
    {
        private IStatusBarServices _statusBarServices;
        private EventHandlerManager<InteractionRequestedEventArgs> _raisedEventHandlers = new EventHandlerManager<InteractionRequestedEventArgs>();


        protected IStatusBarServices StatusBarServices
        {
            get { return _statusBarServices ?? (_statusBarServices = ServiceLocator.Current.GetInstance<IStatusBarServices>()); }
        }


        #region IInteractionRequest Members

        /// <summary>
        /// Fired when the interaction is needed.
        /// </summary>
        public virtual event EventHandler<InteractionRequestedEventArgs> Raised
        {
            add { _raisedEventHandlers += value; }
            remove { _raisedEventHandlers -= value; }
        }

        #endregion


        /// <summary>
        /// Requests an interaction implying the given notification.
        /// </summary>
        /// <typeparam name="TNotification">The type of the notification involved in the interaction.</typeparam>
        /// <param name="notification">The actual notification that must be displayed to the user to establish the interaction.</param>
        /// <exception cref="ArgumentNullException"><paramref name="notification"/> is null.</exception>
        /// <returns>The given notification with the values possibly modified as result of the interaction.</returns>
        public TNotification Interact<TNotification>(TNotification notification) where TNotification : INotification
        {
            return Interact(notification, null);
        }

        /// <summary>
        /// Requests an interaction implying the given notification and a callback.
        /// </summary>
        /// <typeparam name="TNotification">The type of the notification involved in the interaction.</typeparam>
        /// <param name="notification">The actual notification that must be displayed to the user to establish the interaction.</param>
        /// <param name="callback">A callback that is to be executed when the interaction is finishing.</param>
        /// <exception cref="ArgumentNullException">Either <paramref name="notification"/> or <paramref name="callback"/> is null.</exception>
        /// <returns>The given notification with the values possibly modified as result of the interaction.</returns>
        public virtual TNotification Interact<TNotification>(TNotification notification, Action callback) where TNotification : INotification
        {
            if (Equals(notification, null))
                throw new ArgumentNullException("notification");

            _raisedEventHandlers.CallEventHandlers(this, new InteractionRequestedEventArgs(notification, callback));
            return notification;
        }

        /// <summary>
        /// Signals that a certain event just happened. Such signal goes to the status bar where these all are displayed.
        /// </summary>
        /// <param name="text">The explanation of the recent event.</param>
        /// <exception cref="ArgumentNullException"><paramref name="text"/> is null.</exception>
        protected virtual void SignalStatus(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            StatusBarServices.SignalText(text);
        }

        /// <summary>
        /// Requests to interact to the user in order to get confirmation about something.
        /// </summary>
        /// <param name="text">The text explaining what is to be confirmed.</param>
        /// <param name="title">A title for the confirmation element with which the user will deal to confirm or not.</param>
        /// <returns>
        /// A <see cref="IConfirmation"/> object containing the user response, it's an interaction request context object used in these
        /// cases
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="text"/> or <paramref name="title"/> is null.
        /// </exception>
        protected virtual IConfirmation Confirm(string text, string title)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            if (title == null)
                throw new ArgumentNullException("title");

            IConfirmation confirmation = new Confirmation { Content = text, Title = title };
            confirmation = new ConfirmationWithReason<IConfirmation>(confirmation) { Reason = NotificationReason.Question };

            return Interact(confirmation);
        }

        /// <summary>
        /// Request to display to the user a notification.
        /// </summary>
        /// <param name="text">The text being the content of the notification.</param>
        /// <param name="title">A title for the notification element.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="text"/> or <paramref name="title"/> is null.
        /// </exception>
        protected virtual INotification Notify(string text, string title)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            if (title == null)
                throw new ArgumentNullException("title");

            INotification notification = new Notification { Content = text, Title = title };
            notification = new NotificationWithReason<INotification>(notification) { Reason = NotificationReason.Info };

            return Interact(notification);
        }

        /// <summary>
        /// Request to display to the user an error notification.
        /// </summary>
        /// <param name="text">The text explaining what was wrong.</param>
        /// <param name="title">A title for the notification element.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="text"/> or <paramref name="title"/> is null.
        /// </exception>
        protected virtual INotification NotifyError(string text, string title)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            if (title == null)
                throw new ArgumentNullException("title");

            INotification notification = new Notification { Content = text, Title = title };
            notification = new NotificationWithReason<INotification>(notification) { Reason = NotificationReason.Error };

            return Interact(notification);
        }

        /// <summary>
        /// Executes the given method in a safe context, where there is displayed a message box, displaying the exception details (if there
        /// is any that were thrown in the method's execution). The exception expected to be catched here is
        /// <see cref="UnexpectedErrorException"/>. Exceptions of other types not being inheritors of the mentioned one are let flow to the
        /// caller.
        /// </summary>
        /// <typeparam name="T">The type of the method to execute's result.</typeparam>
        /// <param name="method">The method to be executed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null.</exception>
        /// <returns>
        /// An instance of <typeparamref name="T"/> being the method result (if the method returns something); otherwise the
        /// default value for <typeparamref name="T"/>.
        /// </returns>
        protected virtual T ExecuteSafely<T>(Func<T> method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            try
            {
                return method();
            }
            catch (Exception e)
            {
                NotifyError(e.Message, Resources.UnexpectedExceptionTitle);
            }

            return default(T);
        }

        /// <summary>
        /// Executes the given method in a safe context, where there is displayed a message box, displaying the exception details (if there
        /// is any that were thrown in the method's execution). The exception expected to be catched here is
        /// <see cref="UnexpectedErrorException"/>. Exceptions of other types not being inheritors of the mentioned one are let flow to the
        /// caller.
        /// </summary>
        /// <param name="method">The method to be executed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null.</exception>
        protected virtual void ExecuteSafely(Action method)
        {
            ExecuteSafely<object>(() => { method(); return null; });
        }

        /// <summary>
        /// Uses the given exception, title and text header to prepare a nice message for the user to see in case the exception is a
        /// <see cref="ValidationException"/>. In the is of another type than the validation related one, such exception is re-thrown.
        /// </summary>
        /// <param name="title">The title of the message that should be displayed to the user explaining the exception.</param>
        /// <param name="textHeader">The part of the message not containing the validation details, it's the introduction text.</param>
        /// <param name="exception">The exception to parse.</param>
        protected virtual void ParseException(string title, string textHeader, Exception exception)
        {
            var validationException = exception as ValidationException;
            if (validationException != null)
            {
                // If the exception was caused by validations, then format the message in a way that all the errors are notified.
                var sb = new StringBuilder();
                sb.AppendLine(textHeader);
                sb.AppendLine();
                foreach (var error in validationException.Errors)
                    sb.AppendLine("- {0}".EasyFormat(error));

                string text = sb.ToString();
                NotifyError(text, title);
            }
            else
                throw exception;
        }
    }
}
