using System;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace CompanyName.Atlas.Contracts.Presentation.Prism
{
    /// <summary>
    /// This class identifies a notification having an well defined intention.
    /// </summary>
    public class NotificationWithReason<TNotification> : INotification, INotificationWithReason where TNotification : class, INotification
    {
        private readonly TNotification _notification;


        /// <summary>
        /// Initializes a new instance of a notification having a reason.
        /// </summary>
        /// <param name="notification">
        /// The actual notification object wrapped by the current one in order to provide it a reason besides all the other fields.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="notification"/> is null.</exception>
        public NotificationWithReason(TNotification notification)
        {
            if (notification == null) throw new ArgumentNullException("notification");
            
            _notification = notification;
        }


        /// <summary>
        /// Gets or sets the intention of the current notification.
        /// </summary>
        public NotificationReason Reason { get; set; }

        /// <summary>
        /// Gets or sets the title to use for the notification.
        /// </summary>
        public string Title
        {
            get { return _notification.Title; }
            set { _notification.Title = value; }
        }

        /// <summary>
        /// Gets or sets the content of the notification.
        /// </summary>
        public object Content
        {
            get { return _notification.Content; }
            set { _notification.Content = value; }
        }
    }
}
